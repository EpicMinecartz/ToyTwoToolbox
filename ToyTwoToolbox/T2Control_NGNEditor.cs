using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ToyTwoToolbox {
    public partial class T2Control_NGNEditor : UserControl {
        public T2Control_NGNEditor(F_NGN file) {
            InitializeComponent();
            tabControl1.DrawItem += new DrawItemEventHandler(DarkThemeTabControlRender.tabControl_DrawItem);
            this.Dock = DockStyle.Fill;
        }

    }
}
