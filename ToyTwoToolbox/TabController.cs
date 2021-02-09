using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ToyTwoToolbox {
    /// <summary>
    /// This is the interface for the primary tabcontrol.<para/>
    /// Any creation, deletion or modification of tabs/states must be done in here to maintain integrity
    /// </summary>
    public class TabController {
        //remember, this is essentially the file/editor controller too!
        public T2TTabControl TabContainer;
        public List<TCTab> Tabs = new List<TCTab>();
        //List<string> TabPaths = new List<string>(); //uh i dont think this has a purpose?
        public TabController(T2TTabControl target) {
            TabContainer = target;
        }


        /// <summary>Return a <see cref="List{"/> of <see cref="F_NGN"/> from the TCTAB pool</summary>
        /// <returns>A <see cref="List{"/> containing all the currently opened levels </returns>
        public List<F_NGN> GetAllLevels() {
            List<F_NGN> levs = new List<F_NGN>();
            foreach (TCTab tab in Tabs) {
                if (tab.File.FileType == FileProcessor.FileTypes.NGN && tab.isClone == false) {
                    levs.Add((F_NGN)tab.File);
                }
            }
            return levs;
        }

        /// <summary>Return a <see cref="F_NGN"/> from the <see cref="TCTab"/> pool</summary>
        /// <param name="index">The TCT ID to return for level data, or -1 to use the pointer to the <see cref="TabContainer"/></param>
        /// <returns>The <see cref="F_NGN"/> referenced by the <paramref name="index"/> </returns>
        public F_NGN GetLevel(int index = -1) {
            if(index==-1) { index = TabContainer.SelectedIndex; }
            if (Tabs[index].File.FileType == FileProcessor.FileTypes.NGN) {
                return (F_NGN)Tabs[index].File;
            } else {
                SessionManager.Report("Specified TCT was not of F_NGN format.", SessionManager.RType.WARN);
                return null;
            }
        }

        public TCTab CreateTab(F_Base file = null, string tabNameOverride = "", FileProcessor.EditorTypes overriedEditor = FileProcessor.EditorTypes.NULL) {
            string tabName = tabNameOverride;
            if (file != null) {
                tabName = (file.FilePath == null) ? file.TempName : System.IO.Path.GetFileNameWithoutExtension(file.FilePath);
                if (tabNameOverride != "") {
                    tabName = CalculateTabName(file);
                }
            }

            TabPage TP = new TabPage(tabName);
            TCTab tc = new TCTab { Name = tabName, owner=this, File = file, tabpage = TP };
            Tabs.Add(tc);

            //we have made the link between the tab and file, now we need to make the tab exist

            //we need add the correct controls to the tab
            //to put it in tl;dr land, here we see what type of file we are dealing with, and add an editor to the TCTab we made above
            //literally, we just register it inside the TCTab and then add it to it's associated tab page, take a look 0_0
            //tc's ImplementEditor function will automatically assign an editor for the tc based on the TCTab.File.FileType, however you can override it with the func(<param>)
            tc.ImplementEditor(overriedEditor);
            TabContainer.TabPages.Add(TP);
            return tc;
        }

        public void ReloadTab(F_Base NewFile = null) {
            Tabs[TabContainer.SelectedIndex].File = NewFile;
            Tabs[TabContainer.SelectedIndex].editor = null;
            Tabs[TabContainer.SelectedIndex].ImplementEditor();
        }

        public void CloseTab(int? tabID = null) {
            int tid = tabID ?? ((TabContainer.SelectedIndex + 1 == TabContainer.TabPages.Count) ? TabContainer.SelectedIndex : -1);
            if (tid != -1) {
                DialogResult msg = MessageBox.Show("Are you sure you want to close this file?", "Close?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (msg == DialogResult.Yes) {
                    Tabs[tid].tabpage.Dispose();
                    Tabs.RemoveAt(tid);//something better than these two lines, like merge the remove and dispose calls
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

        /// <summary>
        /// Create a duplicate view of an editor, note, this creates a direct reference, no cloning of data occurs
        /// </summary>
        /// <param name="tabIndex">The Tab ID to make a copy of</param>
        public void CloneTab(int tabIndex) {
            TCTab oldTC = Tabs[tabIndex];
            TabPage TP = new TabPage(oldTC.tabpage.Name + ":C"); //please do something better than this lol
            Tabs.Add(new TCTab { Name = oldTC.Name, owner = this, File = oldTC.File, tabpage = TP, editor = oldTC.editor, isClone = true });
            TabContainer.TabPages.Add(TP);
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


        /// <summary>A TCTab(TabControllerTab) holds the tab information for each opened tab, thus everything within.</summary>
        public class TCTab {
            public int id;
            public int GUID;
            public string Name;
            public F_Base File;
            public bool isClone = false;
            public IEditor editor;
            public TabPage tabpage;
            public TabController owner;


            public TCTab(TabController _owner = null) {
                if (_owner != null) { owner = _owner; }
                GUID = XF.Random(0, 999999999);
            }

            /// <summary>
            /// Register an editor control with this tab.
            /// <para/>Note: This will add the editor control into the tabpage <seealso cref="Control.ControlCollection"/>
            /// </summary>
            /// <param name="editorControl">The <seealso cref="IEditor"/> control to add</param>
            public IEditor ImplementEditor(FileProcessor.EditorTypes editorControl = FileProcessor.EditorTypes.NULL) {
                //NOTE: FileProcessor.FileTypes == FileProcessor.EditorTypes
                //we assume that because no file was specified, we are using an editor that doesnt require a file, you better have manually specified the editorControl param!
                //NOTE V2: If you actually use the editorControl param, this func uses that regardless of the file type, so be warned!

                editor = FileProcessor.returnEditorFromType(editorControl == FileProcessor.EditorTypes.NULL ? (FileProcessor.EditorTypes)(int)File.FileType : editorControl);
                editor.owner = this;

                //As long as you follow the EditorTemplate(visible in IEditor) your editor should contain an Init() func, for late initialization scenarios
                editor.Init(File);
                tabpage.Controls.Add(editor.main);
                return editor;
            }

            //call this to save the the loaded file in the selected tab based on the editor, not the file direct
            public void Save(string path) {
                if (editor.SaveChanges(false, path) == true) {
                    tabpage.Text = System.IO.Path.GetFileNameWithoutExtension(File.FilePath);
                }
            }

            public void Clone() {
                //((T2TTabControl)tabpage.Parent).TabControl.CloneTab(((TabController)tabpage.Parent).GetTabID(this));
            }
        }

    }
}
