using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace ToyTwoToolbox {
    public class F_Save : F_Base {
        public FileProcessor.FileTypes FileType { get; } = FileProcessor.FileTypes.Save;
        /// <summary>
        /// Used when a file name isnt provided
        /// </summary>
        /// <remarks> this probably shouldnt be modified by anyone unless you know wtf your doing </remarks>
        private string tempName;
        public string TempName { get => tempName; set => tempName = value; }
        private string filePath;
        public string FilePath { get => filePath; set => filePath = value; }

        public enum CameraType {
            passive = 128,
            active = 192
        }

        public enum Tokens {
            Hamm = 0,
            Collect = 1,
            RC = 2,
            Mystery = 4,
            Boss = 3,
            Invis = 5
        }

        //The bitval for each flag
        public enum TokenReference {
            Hamm = 1,
            Collect = 2,
            RC = 4,
            Mystery = 8,
            Boss = 16
        }

        public enum Unlocks {
            Shield = 0,
            Rocket = 1,
            Disc = 2,
            Hover = 3,
            Grapple = 4
        }

        public string name = "";
        public int lives = 5;
        public int lastlevel = 0;
        public int unlocksraw = 0;
        public int cameratype = (int)CameraType.active;
        public int musicVolume = 10;
        public int soundVolume = 10;
        public int unk1;
        public int unk2;
        public int unk3;
        public int unk4;
        public int unk5;
        public int health = 14;
        public int unk6;
        public List<int> movies = new List<int>();
        public List<int> tokensraw = new List<int>();
        public List<List<bool>> tokens = new List<List<bool>>();
        public List<bool> unlocks = new List<bool>();

        public F_Save(bool create = false) {
            if (create == true) {
            movies = (List<int>)XF.GenerateListData<int>(1, 20, 0);
            tokensraw = (List<int>)XF.GenerateListData<int>(1, 16, 0);
            tokens = (List<List<bool>>)XF.GenerateListData<bool>(16, 6, false);
            unlocks = (List<bool>)XF.GenerateListData<bool>(1, 5, false);
            }
        }

        public static F_Save ImportSave(string path) {
            F_Save save = new F_Save {
                FilePath = path
            };

            FileReader fs = new FileReader(path);
            int namelength = fs.readint(4);
            save.name = fs.readstring(namelength);
            fs.seek((fs.length() - 80) - fs.foffset); //move past null data
            save.lives = fs.readint(1);
            save.lastlevel = fs.readint(1);
            save.unlocksraw = fs.readint(1);
            save.unlocks = ConvertUnlocksToBin(save.unlocksraw);
            fs.seek(1);
            save.cameratype = fs.readint(1);
            save.musicVolume = fs.readint(1);
            save.soundVolume = fs.readint(1);
            save.unk1 = fs.readint(1);
            save.unk2 = fs.readint(1);
            save.unk3 = fs.readint(1);
            save.unk4 = fs.readint(1);
            save.unk5 = fs.readint(1);
            save.health = fs.readint(2);
            save.unk6 = fs.readint(1);
            for (var I = 0;I <= 15;I++) {
                save.tokensraw.Add(fs.readint(1));
            }
            save.tokens = ConvertTokensToBin(save.tokensraw);
            fs.seek(1);
            for (int I = 0;I < 20;I++) {
                save.movies.Add(fs.readint(1));
            }
            return save;
        }

        public F_Base Import(string path) {
            return ImportSave(path);
        }

        public bool Export(string path) {
            FileWriter fwsave = new FileWriter();

            fwsave.AddInt32(name.Length);
            fwsave.Add(name);
            fwsave.Nop(312); //this pains me so much
            fwsave.AddByte(lives);
            fwsave.AddByte(lastlevel);
            fwsave.AddByte(unlocksraw);
            fwsave.Nop();
            fwsave.AddByte(cameratype);
            fwsave.AddByte(musicVolume);
            fwsave.AddByte(soundVolume);
            fwsave.AddByte(unk1);
            fwsave.AddByte(unk2);
            fwsave.AddByte(unk3);
            fwsave.AddByte(unk4);
            fwsave.AddByte(unk5);
            fwsave.AddInt16(health);
            fwsave.AddByte(unk6);
            for (int i = 0;i < 16;i++) {
                fwsave.AddByte(tokensraw[i]);
            }
            fwsave.Nop();
            for (int i = 0;i < 20;i++) {
                fwsave.AddByte(movies[i]);
            }
            fwsave.Nop(28);

            if (fwsave.Save(path) == true) {
                filePath = path;
                return true;
            } else {
                return false;
            }
            
        }

        public static List<bool> ConvertUnlocksToBin(int UnlockVal) {
            string UnlockBin = Convert.ToString(UnlockVal, 2).PadLeft(5, '0');
            List<bool> Unlocks = new List<bool>();
            for (int i = 0;i < 5;i++) {
                Unlocks.Add(UnlockBin.Substring(i, 1) == "1");
            }
            return Unlocks;
        }

        public static List<List<bool>> ConvertTokensToBin(List<int> tokensraw) {
            if (tokensraw == null) { SessionManager.Report("The raw token count was null [->F_Save->ConvertTokensToBin]"); }
            List<List<bool>> leveltokens = new List<List<bool>>();
            for (int i = 0;i < tokensraw.Count;i++) {
                if (tokensraw[i] == 0) { leveltokens.Add(new List<bool>() { false, false, false, false, false, false }); }
                List<bool> LevelBinary = new List<bool>();
                string TokenBinary = Convert.ToString(tokensraw[i], 2).PadLeft(6, '0');
                TokenBinary = TokenBinary.Remove(0, TokenBinary.Length - 6);
                for (int j = 0;j < 6;j++) {
                    LevelBinary.Add(TokenBinary.Substring(5 - j, 1) == "1");
                }
                leveltokens.Add(LevelBinary);
            }
            return leveltokens;
        }

        public static int TokUnlockToInt(List<bool> BitArray) {
            List<bool> bitlist = new List<bool>(BitArray);
            bitlist.Reverse();
            return Convert.ToInt16(string.Join("", bitlist.ConvertAll<string>((bool i) => Convert.ToInt32(i).ToString()).ToArray()), 2);    //string Bits = string.Join("", bitlist.ConvertAll<string>(delegate (byte i){return i.ToString();}));
        }

        public static List<int> ConvertBinTokensToRawTokens(List<List<bool>> tokens) {
            List<int> RawTokens = new List<int>();
            foreach (List<bool> LevTokens in tokens) {
                RawTokens.Add(TokUnlockToInt(LevTokens));
            }
            return RawTokens;
        }

        public static int GetTokenCount(List<int> LevelTokens) {
            int TT = 0;
            foreach (int LevTot in LevelTokens) {
                TT += LevTot;
            }
            return TT;
        }

    }
}
