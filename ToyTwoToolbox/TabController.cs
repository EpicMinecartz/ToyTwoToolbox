using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ToyTwoToolbox {
    /// <summary>
    /// This is the interface for the primary tabcontrol.<para/>
    /// Any creation, deletion or modification of tabs must be done in here to maintain integrity
    /// </summary>
    public class TabController {
        TabControl TabContainer;
        List<TCTab> Tabs = new List<TCTab>();
        List<string> TabPaths = new List<string>();
        public TabController(TabControl target) {
            TabContainer = target;
        }

        public void CreateTab(F_Base file, string tabNameOverride = "") {
            string tabName = (file.FileName == null) ? file.TempName : file.FileName;
            if (tabNameOverride != "") {
                tabName = CalculateTabName(file);
            }

            TabPage TP = new TabPage(tabName);

            TCTab tc = new TCTab { name = tabName, file = file, tabpage = TP };
            Tabs.Add(tc);
            TabPaths.Add(file.FilePath);

            //we have made the link between the tab and file, now we need to make the tab exist


            //now we add the correct controls to the tab
            if (tc.file.FileType == FileProcessor.FileTypes.NGN) {
                T2Control_NGNEditor NGNEditor = new T2Control_NGNEditor();
                tc.tabpage.Controls.Add(NGNEditor);
            } if (tc.file.FileType == FileProcessor.FileTypes.Save) {
                T2Control_SaveEditor SaveEditor = new T2Control_SaveEditor();
                tc.tabpage.Controls.Add(SaveEditor);
            }


            TabContainer.TabPages.Add(TP);
        }

        public string CalculateUntitledTabName(FileProcessor.FileTypes fileType) {
            int unnamedCount = 0;
            foreach (TCTab tc in Tabs) {
                if (tc.file.FileType == fileType && tc.file.FileName == null) {
                    unnamedCount++;
                }
            }
            return "untitled " + Enum.GetName(typeof(FileProcessor.FileTypes), fileType) + " " + ((unnamedCount > 0) ? unnamedCount.ToString() : "");
        }

        public void CloneTab(int tabIndex) {

        }

        public string CalculateTabName(F_Base file) {
            if (Tabs.Count > 0) {
                int clonecount = 0;
                foreach (TCTab tc in Tabs) {
                    if (tc.file.FilePath == file.FilePath) {
                        clonecount++;
                    }
                }
                return file.FilePath + " : " + clonecount;
            }
            return file.FilePath;
        }


        /// <summary>
        /// A TCTab holds the tab information for each opened file, thus the tab.
        /// </summary>
        public class TCTab {
            public string name;
            public F_Base file;
            public bool isClone = false;
            public TabPage tabpage;
        }

    }
}
