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
            toggleBuzzGod.Checked = false;
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



        F_Save loadedSave = null; //THIS IS NOT A NEW SAVE IT IS A REFERENCE

        /// <summary>
        /// [<seealso cref="Main.Unsafe"/>] This function does not do any saftey checks and you must contract integrity yourself
        /// </summary>
        /// <param name="file">The file to load into the visual editor</param>
        public void LoadFile(F_Save save) {
            loadedSave = save;
            //trackMusic.BackColor = XF.GetTransparentColor(this);
            this.fieldSaveName.Text = save.name;
            this.fieldBuzzLives.Value = save.lives;
            this.fieldBuzzHealth.Value = save.health;
            this.t2Control_HealthMeter1.Health = save.health;
            this.t2Control_HealthMeter1.CalculateHealth(save.health);
            this.toggleBuzzGod.Checked = save.health > 32766;
            radioCameraActive.Checked = (save.cameratype == (int)F_Save.CameraType.active);
            labelMusicVolume.Text = "Music Volume: " + save.musicVolume.ToString();
            trackMusic.Value = save.musicVolume;
            labelSoundVolume.Text = "Sound Volume: " + save.soundVolume.ToString();
            trackSound.Value = save.soundVolume;
            fieldLastLevel.Value = save.lastlevel;
            fieldLevel.SelectedIndex = 0;
            fieldMovies.Items[0].Checked = true;
        }

        public bool SaveChanges(bool JustMemory = false, string path = "") {
            if (path == "" || path == null) {
                SaveFileDialog SFD = new SaveFileDialog {
                    DefaultExt = ".sav",
                    Filter = "T2 Save (.sav)|*.sav|All files (*.*)|*.*",
                    FileName = (loadedSave.FilePath == null) ? loadedSave.TempName : System.IO.Path.GetFileNameWithoutExtension(loadedSave.FilePath)
                };
                if (SFD.ShowDialog() == DialogResult.OK) {
                    path = SFD.FileName;
                    loadedSave.name = this.fieldSaveName.Text;
                    loadedSave.lives = (int)this.fieldBuzzLives.Value;
                    loadedSave.health = (int)this.fieldBuzzHealth.Value;
                    loadedSave.health = (int)this.fieldBuzzHealth.Value;
                    loadedSave.cameratype = (radioCameraActive.Checked) ? 192 : 128;
                    loadedSave.musicVolume = trackMusic.Value;
                    loadedSave.soundVolume = trackSound.Value;
                    loadedSave.lastlevel = (int)fieldLastLevel.Value;
                    loadedSave.tokensraw = F_Save.ConvertBinTokensToRawTokens(loadedSave.tokens);
                    loadedSave.unlocksraw = F_Save.TokUnlockToInt(loadedSave.unlocks);
                    if (JustMemory == false) { 
                        return loadedSave.Export((path == null) ? loadedSave.FilePath : path); 
                    }
                }
            }
            return false;
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
            labelInvisTokens.Text = (tokens.IndexOf(true) < 5 && tokens.IndexOf(true) > -1) ? "Hide Tokens:" : "Unlock level without tokens:";
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
            if (unlockID == -1) {
                loadedSave.unlocks = (List<bool>)XF.GenerateListData<bool>(1, 5, state);
            } else {
                loadedSave.unlocks[unlockID] = state;
            }
            InvalidateUnlockToggles();
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
            this.GetType().InvokeMember("DoubleBuffered", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.SetProperty, null, this, new object[] { true });
            groupBox1.GetType().InvokeMember("DoubleBuffered", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.SetProperty, null, groupBox1, new object[] { true });
            groupBox2.GetType().InvokeMember("DoubleBuffered", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.SetProperty, null, groupBox2, new object[] { true });
            groupBox3.GetType().InvokeMember("DoubleBuffered", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.SetProperty, null, groupBox3, new object[] { true });
            groupBox4.GetType().InvokeMember("DoubleBuffered", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.SetProperty, null, groupBox4, new object[] { true });
            groupBox5.GetType().InvokeMember("DoubleBuffered", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.SetProperty, null, groupBox5, new object[] { true });
            groupBox6.GetType().InvokeMember("DoubleBuffered", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.SetProperty, null, groupBox6, new object[] { true });
            groupBox7.GetType().InvokeMember("DoubleBuffered", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.SetProperty, null, groupBox7, new object[] { true });
            groupBox8.GetType().InvokeMember("DoubleBuffered", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.SetProperty, null, groupBox8, new object[] { true });
            fieldMovies.LargeImageList = SessionManager.GetMovieImageList();
            fieldMovies.SmallImageList = SessionManager.GetMovieImageList();
            List<string> MovieNames = new List<string>() { "Trailer (Always Enabled)", "Level 1 Intro", "Level 2 Intro", "Level 3 Bonus", "Level 4 Intro", "Level 5 Intro", "Level 6 Bonus", "Level 7 Intro", "Level 8 Intro", "Level 9 Bonus", "Level 10 Intro", "Level 11 Intro", "Level 12 Intro", "Level 12 Bonus", "Level 13 Intro", "Level 14 Intro", "Level 15 Bonus 1", "Level 15 Bonus 2", "End", "Unused 1", "Unused 2" };
            for (int i = 1;i < fieldMovies.Items.Count;i++) {
                ListViewItem mov = fieldMovies.Items[i];
                mov.ImageIndex = i;
                mov.Text = MovieNames[i];
                mov.Checked = loadedSave.movies[i-1] == 1;
            }
            fieldMovies.Items[1].Checked = true;
            Resize += Form1_Resize;
            this.SetStyle(ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.SupportsTransparentBackColor, true);

        }

        private void Form1_Resize(object sender, System.EventArgs e) {
            this.Update();
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
            fieldSaveName.Text = "TOK " + F_Save.GetTokenCount(F_Save.ConvertBinTokensToRawTokens(loadedSave.tokens)) + " LEV " + fieldLastLevel.Value.ToString();
        }

        private void fieldBuzzHealth_ValueChanged(object sender, EventArgs e) {
            this.toggleBuzzGod.Checked = false;
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

        private void groupBox8_Enter(object sender, EventArgs e) {

        }

        private void labelMusicVolume_Click(object sender, EventArgs e) {

        }

        private void labelSoundVolume_Click(object sender, EventArgs e) {

        }

        private void radioCameraPassive_CheckedChanged(object sender, EventArgs e) {

        }

        private void groupBox1_Enter(object sender, EventArgs e) {

        }

        private void label2_Click(object sender, EventArgs e) {

        }

        private void groupBox2_Enter(object sender, EventArgs e) {

        }

        private void toggleInvisibleTokens_CheckedChanged(object sender, EventArgs e) {

        }

        private void labelInvisTokens_Click(object sender, EventArgs e) {

        }

        private void checkEditAllLevels_CheckedChanged(object sender, EventArgs e) {

        }

        private void checkTokBoss_CheckedChanged(object sender, EventArgs e) {

        }

        private void checkTokMyst_CheckedChanged(object sender, EventArgs e) {

        }

        private void checkTokRC_CheckedChanged(object sender, EventArgs e) {

        }

        private void checkTokCollect_CheckedChanged(object sender, EventArgs e) {

        }

        private void label5_Click(object sender, EventArgs e) {

        }

        private void groupBox4_Enter(object sender, EventArgs e) {

        }

        private void fieldMovies_SelectedIndexChanged(object sender, EventArgs e) {
            butMoviesLockSel.Enabled = (fieldMovies.SelectedIndices.Count > 0);
            butMoviesLockSel.Enabled = (fieldMovies.SelectedIndices.Count > 0);

        }

        private void groupBox7_Enter(object sender, EventArgs e) {

        }

        private void label6_Click(object sender, EventArgs e) {

        }

        private void t2Control_HealthMeter1_Load(object sender, EventArgs e) {

        }

        private void pictureBox2_Click(object sender, EventArgs e) {

        }

        private void groupBox6_Enter(object sender, EventArgs e) {

        }

        private void fieldBuzzLives_ValueChanged(object sender, EventArgs e) {

        }

        private void pictureBox1_Click(object sender, EventArgs e) {

        }

        private void fieldSaveName_TextChanged(object sender, EventArgs e) {

        }

        private void radioCameraActive_CheckedChanged(object sender, EventArgs e) {

        }

        private void groupBox3_Enter(object sender, EventArgs e) {

        }

        private void butUnlockUnlock_Click(object sender, EventArgs e) {
            ToggleUnlockStates(true);
        }

        private void butUnlockLock_Click(object sender, EventArgs e) {
            ToggleUnlockStates(false);
        }

        private void checkUnlockGrapple_CheckedChanged(object sender, EventArgs e) {

        }

        private void checkUnlockHover_CheckedChanged(object sender, EventArgs e) {

        }

        private void checkUnlockDisc_CheckedChanged(object sender, EventArgs e) {

        }

        private void checkUnlockRocket_CheckedChanged(object sender, EventArgs e) {

        }

        private void checkUnlockShield_CheckedChanged(object sender, EventArgs e) {

        }

        private void fieldMovies_ItemCheck(object sender, ItemCheckEventArgs e) {
            if(e.Index == 0) { 
                e.NewValue = CheckState.Checked; 
            } else {
                loadedSave.movies[e.Index-1] = Convert.ToInt32(e.NewValue);
            }
            
            //butMoviesLockAll.Enabled = (fieldMovies.Items.Count == fieldMovies.CheckedIndices.Count);
            //butMoviesUnlockAll.Enabled = (fieldMovies.CheckedIndices.Count == 0);
        }

        public void MovieButtonSelHandler(object sender, EventArgs e) {
            foreach (ListViewItem item in fieldMovies.SelectedItems) {
                item.Checked = (Button)sender == butMoviesUnlockSel;
            }
        }

        public void MovieAllButtonHandler(object sender, EventArgs e) {
            foreach (ListViewItem item in fieldMovies.Items) {
                item.Checked = (Button)sender == butMoviesUnlockAll;
            }
        }
    }
}
