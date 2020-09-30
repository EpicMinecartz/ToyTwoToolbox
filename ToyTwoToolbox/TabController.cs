﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ToyTwoToolbox {
    /// <summary>
    /// This is the interface for the primary tabcontrol.<para/>
    /// Any creation, deletion or modification of tabs must be done in here to maintain integrity
    /// </summary>
    public class TabController {
        T2TTabControl TabContainer;
        public List<TCTab> Tabs = new List<TCTab>();
        List<string> TabPaths = new List<string>();
        public TabController(T2TTabControl target) {
            TabContainer = target;
        }

        public void CreateTab(F_Base file, string tabNameOverride = "") {
            string tabName = (file.FilePath == null) ? file.TempName : System.IO.Path.GetFileNameWithoutExtension(file.FilePath);
            if (tabNameOverride != "") {
                tabName = CalculateTabName(file);
            }

            TabPage TP = new TabPage(tabName);

            TCTab tc = new TCTab { Name = tabName, File = file, tabpage = TP };
            Tabs.Add(tc);
            TabPaths.Add(file.FilePath);

            //we have made the link between the tab and file, now we need to make the tab exist

            //now we add the correct controls to the tab
            //to put it in tl;dr land, here we see what type of file we are dealing with, and add an editor to the TCTab we made above
            //literally, we just register it inside the TCTab and then add it to it's associated tab page, take a look 0_0
            //rewrite very soon with an interface
            if (tc.File.FileType == FileProcessor.FileTypes.NGN) {
                tc.ImplementEditor(new T2Control_NGNEditor((F_NGN)file));
            }
            if (tc.File.FileType == FileProcessor.FileTypes.Save) {
                tc.ImplementEditor(new T2Control_SaveEditor((F_Save)file));
            }
            TabContainer.TabPages.Add(TP);
        }

        public void ReloadTab(F_Base NewFile = null) {
            Tabs[TabContainer.SelectedIndex].File = NewFile;
            if (NewFile.FileType == FileProcessor.FileTypes.NGN) {
                Tabs[TabContainer.SelectedIndex].editor = new T2Control_NGNEditor((F_NGN)NewFile);
            }
            if (NewFile.FileType == FileProcessor.FileTypes.Save) {
                Tabs[TabContainer.SelectedIndex].editor = new T2Control_SaveEditor((F_Save)NewFile);
            }
        }

        public void CloseTab(int? tabID = null) {
            int tid = tabID ?? ((TabContainer.SelectedIndex + 1 == TabContainer.TabPages.Count) ? TabContainer.SelectedIndex : -1);
            if (tid != -1) {
                DialogResult msg = MessageBox.Show("Are you sure you want to close this file?", "Close?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (msg == DialogResult.Yes) {
                    Tabs[tid].tabpage.Dispose();
                    Tabs.RemoveAt(tid);
                    TabPaths.RemoveAt(tid);
                    SessionManager.GCC(); //lmao
                }
            }
        }

        public void TabRequestDestroy(Object sender, int tabID = -1) {
            CloseTab(tabID);
        }

        public static string CalculateUntitledTabName(FileProcessor.FileTypes fileType, TabController tabController) {
            int unnamedCount = 0;
            foreach (TCTab tct in tabController.Tabs) {
                if (tct.File.FileType == fileType && tct.File.FilePath == null) {
                    unnamedCount++;
                }
            }
            return "Untitled " + Enum.GetName(typeof(FileProcessor.FileTypes), fileType) + " " + ((unnamedCount > 0) ? unnamedCount.ToString() : "");
        }

        public void CloneTab(int tabIndex) {

        }

        public string CalculateTabName(F_Base file) {
            if (Tabs.Count > 0) {
                int clonecount = 0;
                foreach (TCTab tc in Tabs) {
                    if (tc.File.FilePath == file.FilePath) {
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
            public string Name;
            public F_Base File;
            public bool isClone = false;
            public UserControl editor;
            public TabPage tabpage;

            public void ImplementEditor(UserControl editorControl) {
                editor = editorControl;
                tabpage.Controls.Add(editorControl);
                if (editorControl is T2Control_SaveEditor) {

                }
            }

            //call this to save the the loaded file in the selected tab based on the editor, not the file direct
            public void Save(string path) {
                if (editor is T2Control_SaveEditor) {
                    if (((T2Control_SaveEditor)editor).SaveChanges(false, path) == true) {
                        tabpage.Text = System.IO.Path.GetFileNameWithoutExtension(File.FilePath);
                    }
                } else if (editor is T2Control_NGNEditor) {
                    if (((T2Control_NGNEditor)editor).SaveChanges(false, path) == true) {
                        tabpage.Text = System.IO.Path.GetFileNameWithoutExtension(File.FilePath);
                    }
                }
            }
        }

    }
}
