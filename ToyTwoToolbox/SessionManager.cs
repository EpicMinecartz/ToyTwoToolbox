using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Security.Policy;
using System.Threading;
using System.Windows.Forms;
using ToyTwoToolbox.Properties;

namespace ToyTwoToolbox {
    public partial class SessionManager : Form {
        [System.Runtime.InteropServices.DllImportAttribute("uxtheme.dll")]
        private static extern int SetWindowTheme(IntPtr hWnd, string appname, string idlist);

        public enum Errors {
            Unknown = -1,
            Generic,
            NotImplemented
        }

        protected override void OnHandleCreated(EventArgs e) {
            //SetWindowTheme(this.Handle, "", "");
            SetWindowTheme(this.Handle, "", "");
            base.OnHandleCreated(e);
        }

        public SessionManager() {
            ProcessArgs();
            InitializeComponent();
            foreach (Control ctrl in XF.GetControlsOfType<Control>(this, true)) {
                SetWindowTheme(ctrl.Handle, "", "");
            }

            SMptr = this;

            DarkThemeCellDGV.BackColor = Color.FromArgb(30, 30, 30);
            DarkThemeCellDGV.ForeColor = Color.FromArgb(240, 240, 240);
            DarkThemeCellDGV.SelectionBackColor = Color.FromArgb(50, 50, 50);
            DarkThemeColumnDGV.BackColor = Color.FromArgb(15, 15, 15);
            DarkThemeColumnDGV.ForeColor = Color.FromArgb(240, 240, 240);
            DarkThemeColumnDGV.SelectionBackColor = Color.FromArgb(50, 50, 50);
            DarkThemeRowDGV.BackColor = Color.FromArgb(10, 10, 10);
            DarkThemeRowDGV.ForeColor = Color.FromArgb(240, 240, 240);
            DarkThemeRowDGV.SelectionBackColor = Color.FromArgb(50, 50, 50);
        }

        public static DataGridViewCellStyle DarkThemeCellDGV = new DataGridViewCellStyle();
        public static DataGridViewCellStyle DarkThemeColumnDGV = new DataGridViewCellStyle();
        public static DataGridViewCellStyle DarkThemeRowDGV = new DataGridViewCellStyle();

        public enum RType {
            SM = -1,
            DEBUG = 0,
            TEXT = 1,
            INFO = 2,
            WARN = 3,
            ERROR = 4
        }

        static List<Color> RTC = new List<Color> {
            Color.FromArgb(164, 164, 164), //RTC_DEBUG 0
			Color.FromArgb(240, 240, 240), //RTC_TEXT  1
			Color.FromArgb(0, 255, 255),   //RTC_INFO  2
			Color.FromArgb(240, 240, 0),   //RTC_WARN  3
			Color.FromArgb(240, 0, 0), 	   //RTC_ERROR 4
		};

        ///TODO: Change the invoke params to support normal Exception
        public static void ReportException(object sender, ThreadExceptionEventArgs e) {
            //mission critical exception, show console to user for reporting
            SessionManager.SMptr.Opacity = 100;
            SessionManager.SMptr.Show();
            EC ecn = new EC {
                EDesc = "APPLICATION EXCEPTION - " + e.GetType().Name,
                ETime = DateTime.Now,
                EType = "Fatal"
            };
            EDict.Add(ecn);
            ReportEx("APPLICATION CRITICAL EXCEPTION DIFFUSED", RType.ERROR, Color.Red, false, false, false, true);
            ReportEx("EX-> " + e.Exception.Message, RType.ERROR, RTC[3], false, false, false, true);
            ReportEx(e.Exception.StackTrace, RType.ERROR, RTC[3], false, false, false, true);
            SessionManager.SMptr.ErrorDisplay.Text = EDict.Count + " Errors";
        }

        /// <summary>Write text to the <seealso cref="SessionManager"/> console</summary>
        /// <param name="InboundText">The text to be written</param>
        /// <param name="ReportType">How to mark the text displayed using <seealso cref="RType"/></param>
        /// <param name="color">The color of the text to be displayed</param>
        /// <param name="imp">Display regardless of report type or debug status</param>
        public static void Report(string InboundText, RType ReportType = (RType)2, Color color = new Color(), bool imp = false) {
            //if (SessionManager.SMptr.InvokeRequired) {
            //	SessionManager.SMptr.Invoke((MethodInvoker)delegate {
            //		ReportEx(InboundText, ReportType, color);
            //	});
            //	return;
            //}

            if (Debug || ReportType == RType.ERROR) {
                if (ReportType == RType.DEBUG && advd != true) { return; }
                ReportEx(InboundText, ReportType, color);
            }
        }

        public static void Report(Errors ErrorType) {
            if (Debug) {
                ReportEx(ErrorType.ToString() + new System.Diagnostics.StackTrace().ToString(), RType.DEBUG);
            }
        }

        public static void ReportEx(string InboundText, RType ReportType = (RType)2, Color BaseColor = new Color(), bool Time = true, bool Tag = true, bool Append = false, bool IgnoreError = false) {
            //initialize
            RichTextBox stdout = SessionManager.SMptr.Log;
            DateTime reporttime = DateTime.Now;

            if (ReportType == RType.ERROR && IgnoreError == false) {
                EC ecn = new EC {
                    EDesc = InboundText,
                    ETime = reporttime,
                    EType = "Error"
                };
                EDict.Add(ecn);
                SessionManager.SMptr.ErrorDisplay.Text = EDict.Count + " Errors";
            }

            stdout.SuspendLayout(); //begin print

            if (Append == false) { stdout.AppendText(Environment.NewLine); }

            //PRINT TIME
            if (Time) {
                stdout.SelectionColor = RTC[1];
                stdout.AppendText("[" + reporttime.ToString("H:mm:ss") + "] ");
            }

            //PRINT REPORT TYPE
            if (Tag) {
                stdout.SelectionColor = RTC[Math.Abs((int)ReportType)];
                stdout.AppendText("[" + ReportType.ToString() + "] ");
            }

            stdout.SelectionColor = (BaseColor.IsEmpty) ? RTC[1] : BaseColor;
            stdout.AppendText(InboundText);
            stdout.ScrollToCaret();
            stdout.ResumeLayout();
            Console.WriteLine(InboundText);
        }

        //Any Variables here are accessable via other forms
        public List<string> BT = new List<string>() { "a", "b", "rc" };
        public Version ver2 = typeof(SessionManager).Assembly.GetName().Version;
        public bool RequestingShutdown = false;
        public bool ClientActive = false;
        public Form MainSession = new Form();
        public string MainArg = null;
        public List<Form> SessionPool = new List<Form>();
        public List<int> SessionIDs = new List<int>();
        public InternalCopy ICL = new InternalCopy();


        //SYSTEM WIDE GLOBAL VARIABLES
        /// <summary>A pointer to the SessionManager window class</summary>
        public static SessionManager SMptr;
        public static string ver = null;
        public static bool Debug = false;
        public static bool advd = false;
        public static bool PrintMessages = false;
        public static List<EC> EDict = new List<EC>();
        //we have a list of variables that we create once, only if required, to save on memory and performance overhead
        public static ImageList MovieImageList; //SMptr.GetMovieList
        public static string str_ImageFormatsStandard = "Bitmap Files (*.bmp)|*.bmp|Portable Network Graphic Files (*.png)|*.png|Joint Photographic Experts Group Files (*.jpg)|*.jpg|Graphics Interchange Format Files (*.gif)|*.gif|All Files (*.*)|*.*";
        public static List<float> Sine = new List<float>();
        //public List<float> Sine = WARP3D.NU3D.GenerateSine();

#if DEBUG
        private bool DebugA = true;
#else
	private bool DebugA = false;
#endif

        private void SessionManager_Load(object sender, EventArgs e) {
            //AllocConsole()
            this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);

            ProcessArgs();
            UseDebug.Checked = Debug;
            ver = ver2.ToString().Remove(ver2.ToString().Length - 2, 2) + BT[ver2.Revision];

            PrintMessages = Debug; //Debug
            ReportEx("Log started at " + DateTime.Now.ToString("H:mm:ss"), RType.SM);
            ReportEx("Debug message printing " + PrintMessages.ToString().ToLower(), RType.SM);
            ReportEx("WARNING! CPU overhead will be increased by debug printing", RType.SM);

            CreateNewSession(MainArg);
            MainSession = SessionPool[0];
            SessionList.SelectedIndex = 0;
        }

        private void ProcessArgs() {
            string[] ApplicationArgs = Environment.GetCommandLineArgs();
            if (ApplicationArgs.Length > 1) {
                if (ApplicationArgs[1] == "-debug") {
                    Debug = true;
                    if (ApplicationArgs.Length > 2) {
                        MainArg = ApplicationArgs[2];
                    }
                } else {
                    MainArg = ApplicationArgs[1];
                }
            }
            if (Debugger.IsAttached == true || DebugA == true) {
                Debug = true;
            }
        }

        private void SessionManager_Closing(object sender, FormClosingEventArgs e) {
            Process.GetCurrentProcess().Kill();
            Application.Exit();
            Application.ExitThread();
        }

        private void RefreshUI() {
            this.Invalidate();
            bool AnySessionActive = SessionPool.Count > 0;
            //T2Bar1.Invalidate()
            SessionList.Items.Clear();

            StopSession.Enabled = AnySessionActive;
            ObliterateSession.Enabled = AnySessionActive;
            SessionList.Enabled = AnySessionActive;
            RestartSession.Enabled = AnySessionActive;
            if (AnySessionActive == true) {
                for (var i = 0;i < SessionPool.Count;i++) {
                    SessionList.Items.Add("Session " + i);
                }
                SessionList.SelectedIndex = Math.Max(0, SessionList.Items.Count - 1);
            }
        }

        private void CheckExecution() {
            //checking to see if sessionstore is corrupted
            if (MainSession == null || MainSession.IsDisposed == true) {
                ReportEx("SM:POOL > Main Session not found!", RType.ERROR, IgnoreError: true);
                if (SessionPool.Count < 1) {
                    ReportEx("SM:POOL > Session pool is empty!", RType.ERROR, IgnoreError: true);
                    ReportEx("SM:POOL > Unable to migrate session host!", RType.ERROR);
                } else {
                    Main CMain = (Main)SessionPool[SessionPool.Count - 1];
                    if (!(CMain.LSID == SessionPool.Count - 1)) {
                        ReRegisterSessions();
                    }
                    MigrateHost();
                }
            }
            RefreshUI();
        }

        private Form CreateNewSession(string StartupParam = "", int SessionPoolID = 0) {
            Main NM = new Main();
            try {
                if (SessionPoolID == 0) {
                    int uuid = GenSessionToken();
                    SessionIDs.Add(uuid);
                    SessionPool.Add(NM);
                } else {
                    SessionPool[SessionPoolID] = NM;
                }
                NM.PrintMessages = PrintMessages;
                NM.LSID = (SessionPoolID == 0) ? SessionPool.Count - 1 : SessionPoolID;
                NM.Init(StartupParam);
                ClientActive = true;
            } catch (Exception ex) {
                Report("Failed to create session:\n" + ex.ToString());
            }
            CheckExecution();
            //  RefreshUI()
            return NM;
        }

        private Form ReloadSession(int SessionID) {
            Main Session = (Main)SessionPool[SessionID];

            DestroySession(SessionID, true);
            //Session.Destroy()
            return CreateNewSession("", SessionID);
        }

        private bool DestroySession(int SessionID, bool Obliterate = false) {
            return RedirectShutdown((Int16)SessionID, true, Obliterate);
        }

        private int GenSessionToken() {
            int nr = R();
            while (SessionIDs.Contains(nr) == true) {
                nr = R();
            }
            return nr;
        }

        private static readonly Random r = new Random();
        public static int R() {
            return r.Next(0, 2147483647);
        }

        public bool ReRegisterSessions() {
            ReportEx("SM:POOL > Reconfiguring session state...", RType.SM);
            for (var i = 0;i < SessionPool.Count;i++) {
                Main Session = (Main)SessionPool[i];
                Session.wlid = GenerateWLID(i);
                Session.LSID = i;
                Session.Text = "Toy 2 Toobox" + ((i > 0) ? i.ToString() : ""); //should be split at : and get old title, then reappend
            }
            return true;
        }

        private bool MigrateHost() {
            ReportEx("SM:POOL > Migrating session host...", RType.SM);
            MainSession = SessionPool[0];
            ReportEx("SM:POOL > Session retarget complete!", RType.SM);
            return true;
        }

        public string GenerateWLID(int ID) {
            return ((ID == 0) ? "" : "[" + ID + "]");
        }

        //needs fixing due to new session scripts
        public bool RedirectShutdown(int LocalID, bool RetainExecution = false, bool Obliterate = false) {
            try {
                Main Session = (Main)SessionPool[LocalID];
                bool Destroy = false;

                if (Obliterate == true) {
                    if (LocalID == 0) {
                        if (RetainExecution == false) {
                            Application.Exit();
                        } else {
                            Session.Destroy();
                            SessionPool.RemoveAt(LocalID);
                        }
                    } else {
                        Session.Destroy();
                        SessionPool.RemoveAt(LocalID);
                    }
                    CheckExecution();
                    return false;
                }

                if (Session.UnsavedWork == true) {
                    int Close = (int)MessageBox.Show("Unsaved work will be lost. Are you sure you want to exit?", "T2T", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                    if (Close == (int)DialogResult.Yes) {
                        Destroy = true;
                    }
                } else {
                    Destroy = true;
                }

                if (Destroy == true) {
                    if (LocalID == 0) {
                        if (SessionPool.Count > 1) {
                            int Close = (int)MessageBox.Show("You have extra T2T windows open, these will be closed are you still sure?", "T2T", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (Close == (int)DialogResult.No) {
                                Destroy = false;
                                return false;
                            }
                        }
                        if (RetainExecution == false) {
                            Process.GetCurrentProcess().Kill();
                        } else {
                            Session.Destroy();
                            SessionPool.RemoveAt(LocalID);
                        }
                    } else {
                        SessionPool.RemoveAt(LocalID);
                        Session.Destroy();
                    }
                }
                CheckExecution();
                RefreshUI();
                return true;
            } catch {
                Process.GetCurrentProcess().Kill();
            }
            return false;
        }

        public void RequestNewSession() {
            CreateNewSession();
        }


        private void SessionManager_Shown(object sender, EventArgs e) {
            if (Debug != true) {
                this.Hide();
            }
            this.Opacity = 1;
            SplitContainer1.Panel1Collapsed = !Debug;
        }

        private void NewSession_Click(object sender, EventArgs e) {
            CreateNewSession(((UseArgs.Checked == true) ? MainArg : ""));
        }

        public class EC {
            public string EDesc;
            public DateTime ETime = new DateTime();
            public string EType;
        }

        private void ForceExitToolStripMenuItem_Click(object sender, EventArgs e) {
            System.Runtime.InteropServices.Marshal.StructureToPtr(0, new IntPtr(unchecked((int)0x123456789)), false);
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e) {
            Application.Exit();
            Application.ExitThread();
        }

        private void RestartToolStripMenuItem_Click(object sender, EventArgs e) {
            Application.Restart();
        }

        private void ForceGCToolStripMenuItem_Click(object sender, EventArgs e) {
            Int64 OldMem = System.Diagnostics.Process.GetCurrentProcess().VirtualMemorySize64;
            //SM(OldMem, "")
            GCC();
            //Dim MemDif As Int64 = OldMem - System.Diagnostics.Process.GetCurrentProcess().VirtualMemorySize64
            //SM(System.Diagnostics.Process.GetCurrentProcess().VirtualMemorySize64, "")
            ReportEx("Garbage cleared up: " + Math.Round((OldMem - System.Diagnostics.Process.GetCurrentProcess().VirtualMemorySize64) / Math.Pow(1024, 2), 2) + "MB", RType.SM);
        }

        /// <summary>Forces a full garbace collection</summary>
        public static void GCC() {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.WaitForFullGCComplete();
            GC.Collect();
        }

        private void ArchiveToolStripMenuItem_Click(object sender, EventArgs e) {
            SaveFileDialog SFD = new SaveFileDialog();
            SFD.Filter = "Rich Text File | *.RTF";
            SFD.FileName = "T2T - " + DateTime.Now.ToString("ddmmyyyyHHmm");
            if (SFD.ShowDialog() == DialogResult.OK) {
                Log.SaveFile(SFD.FileName);
            }
        }

        private void ExportToolStripMenuItem_Click(object sender, EventArgs e) {
            SaveFileDialog SFD = new SaveFileDialog();
            SFD.Filter = "Text File | *.TXT";
            SFD.FileName = "T2T Archive - " + DateTime.Now.ToString("ddmmyyyyHHmm");
            if (SFD.ShowDialog() == DialogResult.OK) {
                Log.SaveFile(SFD.FileName, RichTextBoxStreamType.PlainText);
            }
        }

        private void ClearToolStripMenuItem_Click(object sender, EventArgs e) {
            Log.Clear();
        }

        private void StopSession_Click(object sender, EventArgs e) {
            DestroySession(SessionList.SelectedIndex);
        }

        private void RestartSession_Click(object sender, EventArgs e) {
            ReloadSession(SessionList.SelectedIndex);
        }

        private void ObliterateSession_Click(object sender, EventArgs e) {
            DestroySession(SessionList.SelectedIndex, true);
        }

        private void ErrorDisplay_Click(object sender, EventArgs e) {
            new ErrorDisplayer().ELPopulate(EDict);
        }

        public static float GenerateSine(int offset) {
            if (Sine.Count == 0) {
                List<float> SineList = new List<float>();
                for (int i = 0;i < 65535;i++) {
                    SineList.Add((float)Math.Sin(i * 0.000095873802));
                }
                Sine = SineList;
            }
            return Sine[offset];
        }

        public static Vector3 GenerateV3DSine(Vector3 v, bool co) {
            int off = (co ? 16384 : 0) + 0xFFFF;
            return new Vector3(GenerateSine((int)v.X & off), GenerateSine((int)v.Y & off), GenerateSine((int)v.Z & off));
        }

        public static ImageList GetMovieImageList() {
            if (MovieImageList == null) {
                ImageList MIL = new ImageList {
                    ImageSize = new Size(64, 64),
                    ColorDepth = ColorDepth.Depth32Bit
                };
                for (int i = 0;i < Resources.T2Image_MovieImages.Height / 64;i++) {
                    Bitmap cloneBitmap = Resources.T2Image_MovieImages.Clone(
                                            new Rectangle {
                                                Width = 64,
                                                Height = 64,
                                                X = 0,
                                                Y = i * 64
                                            }, Resources.T2Image_MovieImages.PixelFormat);
                    MIL.Images.Add(cloneBitmap);
                }
                return MIL;
            }
            return MovieImageList;
        }

        private void UseDebug_CheckedChanged(object sender, EventArgs e) {
            Debug = UseDebug.Checked;
        }

        //Protect the integrity of the rest of the NGN
        /// <summary>Validates a function contract to ensure file integrity</summary>
        /// <param name="validationSize">The function size to check against the reference provided by the contract</param>
        /// <param name="ContractSize">The reference size from the contract</param>
        /// <param name="Correct">Whether to correct any disparity</param>
        /// <returns>An <seealso cref="int"/> of the amount corrected by</returns>
        public static int ValidateContract(int validationSize, int ContractSize, bool Correct = true) {
            if (validationSize != ContractSize) {
                Report("Contract invalidated <" + validationSize + "\\" + ContractSize +
                    "> @[..." + new StackFrame(3).GetMethod().Name + " > " + new StackFrame(2).GetMethod().Name + " > " +
                    new StackFrame(1).GetMethod().Name + "]" + ((Correct == true && ContractSize - validationSize > 0) ? " - Correcting by " + (ContractSize - validationSize) + "..." : ""), RType.DEBUG);
                if (Correct == true && ContractSize - validationSize > 0) {
                    return ContractSize - validationSize;
                }
            }
            return 0; //bad
        }

        private void contextDGV_Opening(object sender, CancelEventArgs e) {

        }

        private void replaceSelectedValuesToolStripMenuItem_Click(object sender, EventArgs e) {
            ((T2Control_DGV)((ContextMenuStrip)((ToolStripMenuItem)sender).Owner).SourceControl).ignoreCellValueChanged = false;
            InputDialog input = new InputDialog();
            if (input.ShowDialog() == DialogResult.OK) {
                foreach (DataGridViewCell cell in ((T2Control_DGV)((ContextMenuStrip)((ToolStripMenuItem)sender).Owner).SourceControl).SelectedCells) {
                    cell.Value = input.input;
                }
            }
            ((T2Control_DGV)((ContextMenuStrip)((ToolStripMenuItem)sender).Owner).SourceControl).ignoreCellValueChanged = true;
        }

        private void contextDGV_Closing(object sender, ToolStripDropDownClosingEventArgs e) {
            //((T2Control_DGV)((ContextMenuStrip)sender).SourceControl).ignoreCellValueChanged = true;
        }

        private void ADVDStripMenuItem_Click(object sender, EventArgs e) {
            advd = ADVDStripMenuItem.Checked;
        }

        private void selectInvertedSelectionToolStripMenuItem_Click(object sender, EventArgs e) {
            foreach (DataGridViewCell cell in ((T2Control_DGV)((ContextMenuStrip)((ToolStripMenuItem)sender).Owner).SourceControl).SelectedCells) {
                cell.Selected = !cell.Selected;
            }
        }

        private void selectAllCellsToolStripMenuItem_Click(object sender, EventArgs e) {
            ((T2Control_DGV)((ContextMenuStrip)((ToolStripMenuItem)sender).Owner).SourceControl).SelectAll();
        }

        private void selectAllCellsInColumToolStripMenuItem_Click(object sender, EventArgs e) {
            DataGridView dgv = ((T2Control_DGV)((ContextMenuStrip)((ToolStripMenuItem)sender).Owner).SourceControl);
            int selcol = dgv.SelectedCells[dgv.SelectedCells.Count - 1].ColumnIndex;
            for (int i = 0;i < dgv.Rows.Count;i++) {
                dgv.Rows[i].Cells[selcol].Selected = true;
            }
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e) {

        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e) {

        }
    }

    /// <summary>Handles copying of data throughout the toolbox</summary>
    public class InternalCopy {
        List<object> ICLA = new List<object>();
        ICLFormat ICLF = ICLFormat.Unknown;
        Type ICLT = typeof(object);

        /// <summary>Creates a pointer reference to an object of ICLT into ICLA</summary>
        public void Copy(object Object) {
            ICLA.Add(Object);
        }

        /// <summary>For now, just return ICLA as a standard format until things are better implemented</summary>
        public List<object> Paste() {
            return ICLA;
        }

        public void Clear() {
            ICLA.Clear();
        }
        
        /// <summary>Attempts to pipe the ICLA into the Windows clipboard</summary>
        public void SendClipboard() {
            Clipboard.SetDataObject(ICLA);
        }

        public void SetCopyType(ICLFormat type) {
            ICLF = type;
            ICLT = typeof(object);
        }

        public enum ICLFormat {
            Unknown = -1,
            DGV = 0
        }
    }
}
