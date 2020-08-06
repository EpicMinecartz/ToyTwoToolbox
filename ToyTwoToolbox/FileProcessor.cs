using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ToyTwoToolbox {
    public class FileProcessor {
        F_Base File;
        public enum FileTypes {
            NULL = -1,
            NGN,
            Save
        }

        /// <summary>
        /// Process and Return a file based on a file path. Returns <seealso cref="F_Base"/>, you MUST cast it yourself using the correct cast.
        /// </summary>
        /// <param name="path"></param>
        public static F_Base ProcessFile(string path) {
            string ext = Path.GetExtension(path);
            F_Base file;
            if (ext == ".ngn") {
                F_NGN f = new F_NGN();
                file = f.Import(path);
            } else if (ext == ".sav") {
                file = F_Save.ImportSave(path);
            } else {
                throw new TypeInitializationException("None", null);
                file = null;
            }
            return file;
        }

        public dynamic PreProcessFile(string path) {
            return new F_NGN();
        }

        public static F_Base CreateFile(FileTypes fileType, TabController tabController, string fileName = "") {
            F_Base file;
            if (fileType == FileTypes.NGN) {
                file = new F_NGN();
            } else if(fileType == FileTypes.Save) {
                file = new F_Save(true);
            } else {
                throw new TypeInitializationException("None", null);
                file = null; 
            }
            if (fileName == "" ) { 
                file.TempName = TabController.CalculateUntitledTabName(fileType, tabController); 
            }
            return file;
        }

       

        public static F_NGN ImportNGN(string path) {
            return new F_NGN();
        }

    }
}
