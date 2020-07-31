using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using static ToyTwoToolbox.F_Save;

namespace ToyTwoToolbox {
    public partial class T2Control_SaveEditor : UserControl {
        public T2Control_SaveEditor(F_Save file) {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            this.t2Control_HealthMeter1.HealthChanged += new EventHandler(HealthBarChanged);
            LoadFile(file);
            checkTokHamm.MouseUp += tokenMouseUp;
            checkTokCollect.MouseUp += tokenMouseUp;
            checkTokRC.MouseUp += tokenMouseUp;
            checkTokMyst.MouseUp += tokenMouseUp;
            checkTokBoss.MouseUp += tokenMouseUp;
            toggleInvisibleTokens.MouseUp += tokenMouseUp;

            checkUnlockShield.MouseUp += unlockMouseUp;
            checkUnlockRocket.MouseUp += unlockMouseUp;
            checkUnlockDisc.MouseUp += unlockMouseUp;
            checkUnlockHover.MouseUp += unlockMouseUp;
            checkUnlockGrapple.MouseUp += unlockMouseUp;
        }

        public void HealthBarChanged(object sender, EventArgs e) {
            fieldBuzzHealth.Value = t2Control_HealthMeter1.Health;
        }

        public void tokenMouseUp(object sender, MouseEventArgs e) {
            CheckBox ct = (CheckBox)sender;
            ToggleTokenStates(ct.Checked, Convert.ToInt32(ct.Tag));
        }

        public void unlockMouseUp(object sender, MouseEventArgs e) {
            CheckBox cu = (CheckBox)sender;
            ToggleUnlockStates(cu.Checked, Convert.ToInt32(cu.Tag));
        }



        F_Save loadedSave = null;

        /// <summary>
        /// [<seealso cref="Main.Unsafe"/>] This function does not do any saftey checks and you must contract integrity yourself
        /// </summary>
        /// <param name="file">The file to load into the visual editor</param>
        public void LoadFile(F_Save save) {
            loadedSave = save;
            this.fieldSaveName.Text = save.name;
            this.fieldBuzzLives.Value = save.lives;
            this.fieldBuzzHealth.Value = save.health;
            this.t2Control_HealthMeter1.Health = save.health;
            this.toggleBuzzGod.Checked = save.health > 32766;
            radioCameraActive.Checked = (save.cameratype == (int)F_Save.CameraType.active);
            labelMusicVolume.Text = "Music Volume: " + save.musicVolume.ToString();
            trackMusic.Value = save.musicVolume;
            labelSoundVolume.Text = "Sound Volume: " + save.soundVolume.ToString();
            trackSound.Value = save.soundVolume;
            fieldLevel.SelectedIndex = 0;
        }

        public void InvalidateTokenToggles() {
            //here we check the state of each token toggle against the currently selected level and programatically toggle accordingly
            int levelid = fieldLevel.SelectedIndex;
            List<bool> tokens = loadedSave.tokens[levelid];
            checkTokHamm.Checked = tokens[(int)Tokens.Hamm];
            checkTokCollect.Checked = tokens[(int)Tokens.Collect];
            checkTokRC.Checked = tokens[(int)Tokens.RC];
            checkTokMyst.Checked = tokens[(int)Tokens.Mystery];
            checkTokBoss.Checked = tokens[(int)Tokens.Boss];
            toggleInvisibleTokens.Checked = tokens[(int)Tokens.Invis];
            labelInvisTokens.Text = (tokens.IndexOf(true) == -1) ? "Unlock level\nwithout tokens:" : "Hide Tokens";
        }

        public void ToggleTokenStates(bool state, int tokenID = -1) {
            for (int i = 0;i < loadedSave.tokens.Count;i++) {
                if (i == fieldLevel.SelectedIndex || checkEditAllLevels.Checked == true) {
                    if (tokenID == -1) {
                        bool hidePersist = loadedSave.tokens[i][5];
                        loadedSave.tokens[i] = (List<bool>)XF.GenerateListData<bool>(1, 6, state);
                        loadedSave.tokens[i][5] = hidePersist;
                    } else {
                        loadedSave.tokens[i][tokenID] = state;
                    }
                }
            }
            InvalidateTokenToggles();
        }

        public void ToggleUnlockStates(bool state, int unlockID = -1) {
            if (unlockID != -1) {
                loadedSave.unlocks = (List<bool>)XF.GenerateListData<bool>(1, 5, state);
            } else {
                loadedSave.unlocks[unlockID] = state;
            }
            InvalidateTokenToggles();
        }


        public void InvalidateUnlockToggles() {
            //here we programatically toggle the state of each unlock accordingly
            List<bool> unlocks = loadedSave.unlocks;
            checkUnlockShield.Checked = unlocks[(int)Unlocks.Shield];
            checkUnlockRocket.Checked = unlocks[(int)Unlocks.Rocket];
            checkUnlockDisc.Checked = unlocks[(int)Unlocks.Disc];
            checkUnlockHover.Checked = unlocks[(int)Unlocks.Hover];
            checkUnlockGrapple.Checked = unlocks[(int)Unlocks.Grapple];
        }


        private void T2Control_SaveEditor_Load(object sender, EventArgs e) {

        }

        private void t2Control_TextBox1_TextChanged(object sender, EventArgs e) {

        }

        private void t2Control_TextBox1_MouseMove(object sender, MouseEventArgs e) {

        }

        private void fieldLevel_SelectedIndexChanged(object sender, EventArgs e) {
            InvalidateTokenToggles();
        }

        private void butTokLock_Click(object sender, EventArgs e) {
            ToggleTokenStates(false);
        }

        private void butTokUnlock_Click(object sender, EventArgs e) {
            ToggleTokenStates(true);
        }

        private void trackMusic_Scroll(object sender, EventArgs e) {
            labelMusicVolume.Text = "Music Volume: " + trackMusic.Value.ToString();
        }

        private void trackSound_Scroll(object sender, EventArgs e) {
            labelSoundVolume.Text = "Sound Volume: " + trackSound.Value.ToString();
        }

        private void butSetSaveNameTL_Click(object sender, EventArgs e) {
            fieldSaveName.Text = "Not IMPL";
        }

        private void fieldBuzzHealth_ValueChanged(object sender, EventArgs e) {
            this.toggleBuzzGod.Checked = fieldBuzzHealth.Value > 32766;
            this.t2Control_HealthMeter1.CalculateHealth((int)fieldBuzzHealth.Value);
        }

        private void toggleBuzzGod_CheckedChanged(object sender, EventArgs e) {
            fieldBuzzHealth.Value = 65535;
        }

        private void linkBuzzHealthMax_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            fieldBuzzHealth.Value = 32766;
        }

        private void linkBuzzHealthIGMax_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            fieldBuzzHealth.Value = 14;
        }

        private void linkBuzzLivesMax_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            fieldBuzzLives.Value = 255;
        }

        private void linkBuzzLivesIGMax_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            fieldBuzzLives.Value = 10;
        }

        private void checkTokHamm_CheckedChanged(object sender, EventArgs e) {

        }
    }
}
