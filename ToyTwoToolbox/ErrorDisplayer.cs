using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ToyTwoToolbox {
    public partial class ErrorDisplayer : Form {
        public ErrorDisplayer() {
            InitializeComponent();
        }

        private void ErrorDisplay_Load(object sender, EventArgs e) {
            ErrorView.DefaultCellStyle = SessionManager.DarkThemeCellDGV;
            ErrorView.ColumnHeadersDefaultCellStyle = SessionManager.DarkThemeCellDGV;
            ErrorView.RowsDefaultCellStyle = SessionManager.DarkThemeCellDGV;
        }

        public void ELPopulate(List<SessionManager.EC> Errors) {
            this.Show();
            ErrorView.Rows.Clear();

            for (var i = 0;i < Errors.Count;i++) {
                ErrorView.Rows.Add(1);
                ((DataGridViewRow)ErrorView.Rows[i]).Cells["Time"].Value = Errors[i].ETime;
                ((DataGridViewRow)ErrorView.Rows[i]).Cells["Errors"].Value = Errors[i].EDesc;
            }
        }

    }
}
