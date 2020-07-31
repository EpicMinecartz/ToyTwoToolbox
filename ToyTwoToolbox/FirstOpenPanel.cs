using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ToyTwoToolbox {



	public partial class FirstOpenPanel : UserControl {

		public event EventHandler OpenFile;
		private void OnOpenFile(FOPEA e) {
			if (OpenFile != null) {
				OpenFile(this, e);
			}
		}

		public event EventHandler CreateFile;
		private void OnCreateFile(FOPEA e) {
			if (CreateFile != null) {
				CreateFile(this, e);
			}
		}


		Form Parent;
        public FirstOpenPanel() {
            InitializeComponent();
			
        }

        private void FirstOpenPanel_Load(object sender, EventArgs e) {
			Parent = ((FirstOpenPanel)sender).ParentForm;
            foreach (LinkLabel LL in XF.GetControlsOfType<LinkLabel>(this)) {
				LL.MouseEnter += linkLabels_MouseEnter;
				LL.MouseLeave += linkLabels_MouseLeave;
			}
            panel1.Size = new Size(this.Width, this.Height);
			panel1.Location = new Point(panel1.Location.X-panel1.Width, panel1.Location.Y); //this.Location.X - this.Width
		}

        private void timer1_Tick(object sender, EventArgs e) {
			UIA UIAO = (UIA)panel1.Tag;
			panel1.Location = new Point(XF.CEIO(UIAO.CycleOffset, UIAO.States[UIAO.State].Position0, UIAO.States[UIAO.State].Position1, UIAO.States[UIAO.State].Correction, UIAO.CycleTime), panel1.Location.Y);
			UIAO.Increment();
		}

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
			if (panel1.Tag == null) {
				panel1.Tag = new UIA {
					CycleOffset = 0,
					CycleTime = 20,
					State = 0,
					States = new List<UIAS>() {
						new UIAS (panel1.Location.X,panel1.Location.X+panel1.Width)
					},
					Timer_Handle = timer1
				};
			} else {
				((UIA)panel1.Tag).ReverseAnimation(true);
			}
			timer1.Start();
        }

        private void linkLabels_MouseEnter(object sender, EventArgs e) {
			((LinkLabel)sender).BackColor = Color.FromArgb(45, 45, 45);
        }

        private void linkLabels_MouseLeave(object sender, EventArgs e) {
			((LinkLabel)sender).BackColor = Color.Transparent;
		}

        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
			((UIA)panel1.Tag).ReverseAnimation(true);
		}

        private void LLopenFile_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
			OnOpenFile(new FOPEA(""));
		}

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
			OnCreateFile(new FOPEA(FileType: FileProcessor.FileTypes.NGN));
		}

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
			OnCreateFile(new FOPEA(FileType: FileProcessor.FileTypes.Save));
		}
    }

	public class FOPEA : EventArgs {
		public string _FilePath;
		public FileProcessor.FileTypes _FileType;
		public FOPEA(string FilePath = null, FileProcessor.FileTypes FileType = FileProcessor.FileTypes.NULL) {
			_FilePath = FilePath;
			_FileType = FileType;
		}
	}
}
