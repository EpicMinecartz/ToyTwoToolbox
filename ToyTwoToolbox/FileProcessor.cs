using System;
using System.IO;
using System.Security.Cryptography;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ToyTwoToolbox {
    public class FileProcessor {
        public enum FileTypes {
            NULL = -1,
            NGN,
            Save,
            DAT,
            BIN
        }

        public enum EditorTypes {
            NULL = -1,
            NGNEditor,
            SAVEditor,
            DATEditor,
            BINEditor,
            MultiMatEditor
        }

        /// <summary>Returns an object of type <seealso cref="IEditor"/> based on the <seealso cref="EditorTypes"/> enum index</summary>
        /// <param name="editortype">The <seealso cref="EditorTypes"/> ID</param>
        /// <returns></returns>
        public static IEditor returnEditorFromType(EditorTypes editortype) {
            //As a dev, you will need to add your editor into this section, this should be the only place you will need to ""initialize"" it
            //You will however need to, you know, handle all the processing of files and whatnot on the actual UserControl itself, so, yeah...
            if (editortype == EditorTypes.NGNEditor) {
                return new T2Control_NGNEditor();
            } else if( editortype == EditorTypes.SAVEditor) {
                return new T2Control_SaveEditor();
            } else if(editortype == EditorTypes.DATEditor) {
                return null;
            } else if (editortype == EditorTypes.BINEditor) {
                return new T2Control_RAWEditor();
            } else if (editortype == EditorTypes.MultiMatEditor) {
                return new T2Control_MaterialEditor();
            } else {
                return null;
            }
        }


        /// <summary>Process and Return a file based on a file path. Returns <seealso cref="F_Base"/>, you MUST cast it yourself using the correct cast.</summary>
        /// <param name="path"></param>
        public static F_Base ProcessFile(string path) {
            string ext = Path.GetExtension(path).ToLower();
            F_Base file;
            if (ext == ".ngn") {
                file = new F_NGN().Import(path);
            } else if (ext == ".sav") {
                file = F_Save.ImportSave(path);
            } else if (ext == ".bin") {
                file = F_BIN.ImportBin(path);
            } else {
                DialogResult msg = MessageBox.Show("Would you like to force process this file as a .NGN?", "Is this an NGN?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (msg == DialogResult.Yes) {
                    file = new F_NGN().Import(path);
                } else {
                    file = null;
                }
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
            } else if (fileType == FileTypes.Save) {
                file = new F_Save(true);
            } else {
                file = null;
                throw new TypeInitializationException("None", null);
            }
            if (fileName == "") {
                file.TempName = TabController.CalculateUntitledTabName(fileType, tabController);
            }
            return file;
        }



        public static F_NGN ImportNGN(string path) {
            return new F_NGN();
        }

    }
}
