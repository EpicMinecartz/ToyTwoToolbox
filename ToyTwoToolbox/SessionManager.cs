using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ToyTwoToolbox {
	public partial class SessionManager : Form {
		[System.Runtime.InteropServices.DllImportAttribute("uxtheme.dll")]
		private static extern int SetWindowTheme(IntPtr hWnd, string appname, string idlist);

		protected override void OnHandleCreated(EventArgs e) {
			SetWindowTheme(this.Handle, "", "");
			base.OnHandleCreated(e);
		}
		public SessionManager() {
			InitializeComponent();
			SMptr = this;
		}


		//Any Variables here are accessable via other forms

		//<Runtime.InteropServices.DllImport("kernel32.dll")>
		//Shared Function AllocConsole() As <Runtime.InteropServices.MarshalAs(Runtime.InteropServices.UnmanagedType.Bool)> Boolean
		//End Function

		//Shared Function CheckRemoteDebuggerPresent(hProcess As Runtime.InteropServices.SafeHandle, <Runtime.InteropServices.MarshalAs(Runtime.InteropServices.UnmanagedType.Bool)> ByRef isDebuggerPresent As Boolean) As <Runtime.InteropServices.MarshalAs(Runtime.InteropServices.UnmanagedType.Bool)> Boolean
		//<Runtime.InteropServices.DllImport("Kernel32.dll", SetLastError:=True, ExactSpelling:=True)>
		//Shared Function CheckRemoteDebuggerPresent(ByVal hProcess As Long, ByVal isDebuggerPresent As Boolean) As Boolean
		//End Function








		//SYSTEM WIDE GLOBAL VARIABLES
		public static SessionManager SMptr;
		public List<string> BT = new List<string>() { "a", "b", "rc" };
		public Version ver2 = typeof(SessionManager).Assembly.GetName().Version;
		//INSTANT C# TODO TASK: Instance fields cannot be initialized using other instance fields in C#:
		//ORIGINAL LINE: Public Ver As String = ver2.ToString.Remove(ver2.ToString.Length - 2, 2) & BT.Item(ver2.Revision)
		public static string ver = null;
		public bool RequestingShutdown = false;
		public static bool Debug = false;
		public static bool PrintMessages = false;
		public List<EC> EDict = new List<EC>();
		public bool ClientActive = false;
		public List<Form> SessionPool = new List<Form>();
		public Form MainSession = new Form();
		public string MainArg = null;

		//public List<float> Sine = WARP3D.NU3D.GenerateSine();

		public Int16 TEC = 0;

#if DEBUG
		private bool DebugA = true;
#else
	private bool DebugA = false;
#endif

		private void SessionManager_Load(object sender, EventArgs e) {
			//AllocConsole()
			this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);


			ProcessArgs();

            ver = ver2.ToString().Remove(ver2.ToString().Length - 2, 2) + BT[ver2.Revision];

            PrintMessages = Debug; //Debug
			if (ver2.Revision != 0) { this.Close(); }
            SM("Log started at " + DateTime.Now.ToString("H:mm:ss"));
            SM("Debug message printing " + PrintMessages.ToString().ToLower());
            SM("WARNING! CPU overhead will be increased by debug printing");

            CreateNewSession(MainArg, Debug);
            MainSession = SessionPool[0];
            SessionList.SelectedIndex = 0;
        }

		private void ProcessArgs() {
			string[] ApplicationArgs = Environment.GetCommandLineArgs();
			if (Environment.GetCommandLineArgs().Length > 1) {
				if (Environment.GetCommandLineArgs()[1] == "-debug") {
					Debug = true;
					if (Environment.GetCommandLineArgs().Length > 2) {
						MainArg = Environment.GetCommandLineArgs()[2];
					}
				} else {
					MainArg = Environment.GetCommandLineArgs()[1];
				}
			}
			if (Debugger.IsAttached == true || DebugA == true) {
				Debug = true;
			}
		}

		public bool DiffuseCriticalError(Exception e) {
			Debug = true;
			this.Opacity = 100;
			this.Show();
			TEC += 1;
			SM("================================================================", "", Color.FromArgb(255, 0, 0), NoInstName: true);
			SM("APPLICATION EXCEPTION - " + e.TargetSite.Module.Name, "", Color.FromArgb(255, 0, 0), NoInstName: true);
			SM("================================================================", "", Color.FromArgb(255, 0, 0), NoInstName: true);
			SM("SM.DCE::Critical Runtime Error Diffused", "[_MAIN] [_SM] [FATAL] ", Color.FromArgb(255, 0, 0));
			SM("SM.DCE::EX->" + e.GetType().Name, "[_MAIN] [_EXE] [FATAL] ", Color.FromArgb(255, 0, 0));
			SM("SM.DCE::l->" + e.Source, "[_MAIN] [_EXE] [FATAL] ", Color.FromArgb(255, 0, 0));
			SM(e.Message, "", Color.FromArgb(255, 0, 0));
			SM("Stack Trace: " + Environment.NewLine + e.StackTrace, "", Color.LightGray);
			SM("SM.DCE::EX->AREDMP -NOEXT -ADV", "[_MAIN] [_SM] [FATAL] ", Color.FromArgb(255, 0, 0));
			SM("A_EXP->" + Application.ExecutablePath, "", Color.LightGray);

			EC ecn = new EC {
				EDesc = "APPLICATION EXCEPTION - " + e.GetType().Name + "::" + e.Message + "Stack Trace: " + Environment.NewLine + e.StackTrace,
				ETime = DateTime.Now,
				EType = "Fatal"
			};
			EDict.Add(ecn);
			ErrorDisplay.Text = EDict.Count + " Errors";
			return true;
		}

		public string WL(string TX, string Pre = "", Color color = new Color(), bool NOTD = false, bool nl = true, short Override = 0) {
			if (((new System.Diagnostics.StackFrame(1)).GetMethod().Name == "SM") || Override != 0 || color == Color.Red || (Debug == true && PrintMessages == true)) {
				if (color == new Color()) {
					color = Color.FromArgb(255, 255, 255, 255);
				}
				TX = string.IsNullOrEmpty(Pre) ? TX : Pre + " " + TX;
				if (color == Color.Red) {
					//ecn.ETime = TimeOfDay.ToString("H:mm:ss")
					EC ecn = new EC {
						EDesc = TX,
						ETime = DateTime.Now,
						EType = "Fatal"
					};
					EDict.Add(ecn);
					ErrorDisplay.Text = EDict.Count + " Errors";
				}
				if (NOTD == false) {
					TX = "[" + DateTime.Now.ToString("H:mm:ss") + "] " + TX;
				}

				if (nl) {
					Log.AppendText(Environment.NewLine + TX);
					Console.WriteLine(TX);
				} else {
					Log.AppendText(TX);
					Console.Write(TX); //Trace.Write
									   //Trace.Write(TX)
				}
				//Log.Select(Log.TextLength, Log.TextLength - Log.TextLength)
				Log.Select(Log.TextLength - TX.Length, TX.Length);
				Log.SelectionColor = color;
				Log.SelectionLength = 0;
				return TX;
			} else {
				return null;
			}
		}

		public string SM(string TX, string Pre = "", Color color = new Color(), bool NOTD = true, bool nl = true, bool NoInstName = false) {
			return WL(TX, ((NoInstName == false) ? "SM >" : "") + (string.IsNullOrEmpty(Pre) ? Pre : " " + Pre), color, NOTD, nl);
		}



		//Private Sub Form1_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
		//    T2Bar1.IsFormActive = True
		//End Sub

		//Private Sub Form1_Deactivate(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Deactivate
		//    T2Bar1.IsFormActive = False
		//End Sub

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
			if (MainSession.IsDisposed == true || MainSession == null) {
				SM("SM:POOL > Main Session not found!", "[WARN]", Color.FromArgb(255, 165, 0));
				if (SessionPool.Count < 1) {
					SM("SM:POOL > Session pool is empty!", "[FATAL]", Color.FromArgb(255, 0, 0));
					SM("SM:POOL > Unable to migrate session host!", "[FATAL]", Color.FromArgb(255, 0, 0));
				} else {
					Form1 CMain = (Form1)SessionPool[SessionPool.Count - 1];
					if (!(CMain.LSID == SessionPool.Count - 1)) {
						ReRegisterSessions();
					}
					MigrateHost();
				}
			}
			RefreshUI();
		}

		public Form NewWindow() {
			return CreateNewSession();
		}

		private Form CreateNewSession(string StartupParam = "", bool ForceDebug = false, int SessionPoolID = 0) {
			Form1 NM = new Form1();
			try {
				if (SessionPoolID == 0) {
					SessionPool.Add(NM);
				} else {
					SessionPool[SessionPoolID] = NM;
				}
				NM.Debug = ForceDebug;
				NM.PrintMessages = PrintMessages;
				NM.LSID = (SessionPoolID == 0) ? SessionPool.Count - 1 : SessionPoolID;
				NM.Init(StartupParam);
				ClientActive = true;
			} catch (Exception ex) {
				DiffuseCriticalError(ex);
			}
			CheckExecution();
			//  RefreshUI()
			return NM;
		}

		private Form ReloadSession(int SessionID) {
			Form1 Session = (Form1)SessionPool[SessionID];
			bool SessionDebug = Session.Debug;

			DestroySession(SessionID, true);
			//Session.Destroy()
			return CreateNewSession("", SessionDebug, SessionID);
		}

		private bool DestroySession(int SessionID, bool Obliterate = false) {
			return RedirectShutdown((Int16)SessionID, true, Obliterate);
		}

		public bool ReRegisterSessions() {
			SM("SM:POOL > Reconfiguring session state...", "");
			for (var i = 0;i < SessionPool.Count;i++) {
				Form1 Session = (Form1)SessionPool[i];
				Session.wlid = GenerateWLID(i);
				Session.LSID = i;
				Session.Text = "Toy 2 Toobox" + ((i > 0) ? i.ToString() : ""); //should be split at : and get old title, then reappend
			}
			return true;
		}

		private bool MigrateHost() {
			SM("SM:POOL > Migrating session host...", "");
			MainSession = SessionPool[0];
			SM("SM:POOL > Session retarget complete!", "");
			return true;
		}

		public string GenerateWLID(int ID) {
			return ((ID == 0) ? "" : "[" + ID + "]");
		}

		public bool RedirectShutdown(int LocalID, bool RetainExecution = false, bool Obliterate = false) {
			try {
				Form1 Session = (Form1)SessionPool[LocalID];
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


		private void SessionManager_Shown(object sender, EventArgs e) {
			if (Debug != true) {
				this.Hide();
			}
			this.Opacity = 1;
			SplitContainer1.Panel1Collapsed = !Debug;
		}

		private void NewSession_Click(object sender, EventArgs e) {
			CreateNewSession(((UseArgs.Checked == true) ? MainArg : ""), ((UseDebug.Checked == true) ? Debug : false));
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
			GC.Collect();
			GC.WaitForPendingFinalizers();
			GC.Collect();
			//Dim MemDif As Int64 = OldMem - System.Diagnostics.Process.GetCurrentProcess().VirtualMemorySize64
			//SM(System.Diagnostics.Process.GetCurrentProcess().VirtualMemorySize64, "")
			SM("Garbage cleared up: " + Math.Round((OldMem - System.Diagnostics.Process.GetCurrentProcess().VirtualMemorySize64) / Math.Pow(1024, 2), 2) + "MB", "");
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
			//EL.ELPopulate(EDict);
		}
	}
}
