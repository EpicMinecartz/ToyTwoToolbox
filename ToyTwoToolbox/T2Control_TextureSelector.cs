using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ToyTwoToolbox {
    public partial class T2Control_TextureSelector : UserControl {
        public event TPChangedEventHandler TPChanged;
        public delegate void TPChangedEventHandler(T2TP TP, bool Invoke);
        public event SelectedIndexChangedEventHandler SelectedIndexChanged;
        public delegate void SelectedIndexChangedEventHandler(int Index);
        private int CurrentWidth = 0;
        public List<Texture> Textures;
        public List<T2TP> TexturePanels = new List<T2TP>();
        public T2TP SelectedPanel;
        public int SelectedIndex {
            get {
                if (SelectedPanel != null) {
                    return TexturePanels.IndexOf(SelectedPanel);
                } else {
                    return -1;
                }
            }
            set {
                if (value < TexturePanels.Count) {
                    TPChange((value==-1) ? null : TexturePanels[value], false);
                }
            }
        }

        public T2Control_TextureSelector() {
            InitializeComponent();
            TPChanged += new TPChangedEventHandler(TPChange);
            CurrentWidth = BasePanel.Width-20;
        }

        public void Init(List<Texture> textures) {
            if (textures != null) {
                Textures = textures;
                PopulatePanels();
            }
        }

        public void PopulatePanels() {
            BasePanel.AutoScrollPosition = new Point(0, 0);
            BasePanel.VerticalScroll.Value = 0;
            BasePanel.SuspendLayout();
            TexturePanels.Clear();
            foreach (Texture tex in Textures) {
                T2TP tp = new T2TP(this, tex);
                tp.Location = new Point(tp.Location.X, tp.Location.Y + ((tp.Height + 10) * TexturePanels.Count));
                TexturePanels.Add(tp);
            }
            BasePanel.Controls.Clear();
            BasePanel.Controls.AddRange(TexturePanels.ToArray());
            BasePanel_Resize(this, EventArgs.Empty);
            BasePanel.ResumeLayout();
        }

        public void TPChange(T2TP TP, bool InvokeSelectionChanged = true) {
            if (SelectedPanel != null && SelectedPanel != TP && SelectedPanel.selected == true) {
                SelectedPanel.ChangeSelection();
            } else if (SelectedPanel != null && SelectedPanel == TP && SelectedPanel.selected == true) {
                SelectedPanel.ChangeSelection();
                SelectedPanel = null;
                return;
            }
            if(TP!=null) {
            SelectedPanel = TP;
            if (InvokeSelectionChanged) { SelectedIndexChanged.Invoke((SelectedPanel == null) ? -1 : TexturePanels.IndexOf(SelectedPanel)); }
            TP.ChangeSelection();
            }
        }

        private void T2Control_TextureSelector_Load(object sender, EventArgs e) {

        }

        public class T2TP : Panel {
            public T2Control_TextureSelector owner;
            public Timer Animatior = new Timer();
            public UIA uia;
            public PictureBox tp;
            public T2Control_TransparentLabel tn;
            public Texture _texture;
            public Panel APanel;
            public int Anim = 1;
            public int AStep = 1;
            public bool selected = false;
            public bool unselecting = false;
            bool mouseOver = false;
            bool ignoreMouseMove = false;

            public T2TP(T2Control_TextureSelector _owner, Texture texture = null) {
                this.Height = 60;
                this.Width = 180;
                this.BackColor = Color.FromArgb(25, 25, 25);
                this.MouseUp += T2TP_MouseUp;
                this.MouseEnter += T2TP_MouseEnter;
                this.MouseLeave += T2TP_MouseLeave;
                owner = _owner;
                _texture = texture;
                Animatior.Interval = 1;
                Animatior.Tick += Animatior_Tick;
                uia = new UIA {
                    CycleOffset = 0,
                    CycleTime = 10,
                    State = 0,
                    States = new List<UIAS>() {
                        new UIAS (0,this.Width)
                    },
                    Timer_Handle = Animatior
                };
                APanel = new Panel {
                    Size = new Size(0, 60),
                    Location = new Point(0, 00),
                    BackColor = Color.FromArgb(50, 50, 50)
                };
                tp = new PictureBox {
                    Anchor = AnchorStyles.Right,
                    Location = new Point(140, 10),
                    Size = new Size(40, 40),
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    Image = _texture?.image
                };
                tn = new T2Control_TransparentLabel {
                    Text = _texture?.name,
                    Size = new Size(100, 13),
                    Location = new Point(5, 20)//(this.Height / 2) - this.Size.Height),
                };
                tp.MouseEnter += Tp_MouseEnter;
                tp.MouseLeave += Tp_MouseLeave;
                tp.MouseMove += Tp_MouseMove;
                this.Controls.Add(tp);
                tn.MouseUp += T2TP_MouseUp;
                this.Controls.Add(tn);
                APanel.MouseLeave += new EventHandler(T2TP_MouseLeave);
                APanel.MouseEnter += new EventHandler(T2TP_MouseEnter);
                APanel.MouseUp += T2TP_MouseUp;
                this.Controls.Add(APanel);
            }

            private void Tp_MouseMove(object sender, MouseEventArgs e) {

            }

            private void Tp_MouseLeave(object sender, EventArgs e) {
                tp.Location = new Point(tp.Location.X + 5, tp.Location.Y + 5);
                tp.Size = new Size(tp.Width - 10, tp.Height - 10);
            }

            private void Tp_MouseEnter(object sender, EventArgs e) {
                tp.Location = new Point(tp.Location.X - 5, tp.Location.Y - 5);
                tp.Size = new Size(tp.Width + 10, tp.Height + 10);
            }

            private void T2TP_MouseLeave(object sender, EventArgs e) {
                CheckMouseOver();
            }

            public bool CheckMouseOver() {
                Application.DoEvents();
                Point mp = Control.MousePosition;
                Point PTCmp = PointToClient(mp);
                if (!ClientRectangle.Contains(PTCmp) && !selected) {
                    uia.Reversed = true;
                    mouseOver = false;
                    Animate();
                    return true;
                }
                return false;
            }

            private void T2TP_MouseEnter(object sender, EventArgs e) {
                if (!mouseOver && !selected) {
                    mouseOver = true;
                    Animate();
                }
            }

            private void T2TP_MouseUp(object sender, MouseEventArgs e) {
                owner.TPChanged.Invoke(this, true);
            }

            public void ChangeSelection(bool? Selected = null) {
                if (Selected == null) { Selected = selected; }
                bool _sel = (bool)Selected;
                if (_sel == false) {
                    uia.CycleOffset = 0;
                    APanel.BackColor = Color.FromArgb(90, 90, 90);
                    uia.preventDecrease = true;
                    Animate();
                } else {
                    unselecting = true;
                    uia.Reversed = true;
                    uia.CycleOffset = uia.CycleTime;
                    APanel.BackColor = Color.FromArgb(0, 0, 50);
                    uia.preventDecrease = false;
                    Animate();
                }
                selected = !_sel;
            }

            private void Animatior_Tick(object sender, EventArgs e) {
                APanel.Size = new Size(XF.CEIO(uia.Increment(), uia.CurrentState().Position0, uia.CurrentState().Position1, uia.CurrentState().Correction, uia.CycleTime), APanel.Size.Height);
                if (uia.Reversed && unselecting && uia.CycleOffset == uia.CycleTime - 1) {
                    unselecting = false;
                    APanel.BackColor = Color.FromArgb(50, 50, 50);
                }
            }

            public void Animate() {
                uia.Animate();
            }

        }

        private void BasePanel_MouseEnter(object sender, EventArgs e) {

        }

        private void BasePanel_MouseLeave(object sender, EventArgs e) {
            foreach (T2TP p in TexturePanels) {
                p.CheckMouseOver();
            }
        }

        private void BasePanel_Resize(object sender, EventArgs e) {
            CurrentWidth = BasePanel.Width - 20;
            this.SuspendLayout();
            foreach (T2TP p in TexturePanels) {
                p.Width = CurrentWidth;
                p.uia.States[0].Position1 = CurrentWidth;
                if (p.selected && !p.uia.Timer_Handle.Enabled) { p.APanel.Width = CurrentWidth; }
            }
            this.ResumeLayout();
        }
    }


}
