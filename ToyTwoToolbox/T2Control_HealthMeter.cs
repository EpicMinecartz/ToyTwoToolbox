using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ToyTwoToolbox {
    public partial class T2Control_HealthMeter : UserControl {

        public event EventHandler HealthChanged;

        public int Health = 0;

        public T2Control_HealthMeter() {
            InitializeComponent();
            CalculateHealth(0);
        }

        private void OnHealthChanged(EventArgs e) {
            // invoke UserControl event here
            if (HealthChanged != null) {
                HealthChanged(this, e);
            }
        }

        private void T2Control_HealthMeter_Load(object sender, EventArgs e) {

        }

        private void T2Control_HealthMeter_MouseMove(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Left) {
                CalculateHealth();
            }
        }

        public void CalculateHealth(int health = -1) {
            panel1.Width = 5;
            //can you believe ive rewritten this function 3 times before i've even spent more than 10 hours on this program!
            //if health == -1, we do a fresh calculation, otherwise we trust in the health value, which means we gotta split our processes
            //first, setup panel definition if we are -1, and derrive our health value from that
            if (health == -1) {
                panel1.Location = new Point(4, this.PointToClient(Cursor.Position).Y);
                panel1.Height = (this.Height - 4) - panel1.Location.Y;
                health = (int)Math.Round((decimal)(56 - (panel1.Location.Y - 4)) / 4);
            } else { //if we specified our health value, then just modify the panel definition
                panel1.Location = new Point(4, (this.Height - 4) - (health+1)*4 + 5);//plus 4 on the y so that the panel always stays in the inner region
                panel1.Height = (this.Height - 3) - panel1.Location.Y;
            }






            //if (health == -1) {
            //    panel1.Location = new Point(4, this.PointToClient(Cursor.Position).Y);
            //    panel1.Height = (this.Height - 4) - panel1.Location.Y;
            //    health = (int)Math.Round((decimal)(56 - (panel1.Location.Y - 4)) / 4);
            //} else {
            //    if ((panel1.Location.Y < 3)) {
            //        health = 14;
            //    }
            //    if (panel1.Location.Y > 59) {
            //        health = 0;
            //    }
            //}

            if (health > 14) {
                panel1.BackColor = Color.LimeGreen;
                panel1.Location = new Point(4, 4);
                panel1.Height = (this.Height - 4);
            } else if (health < 1) {
                panel1.BackColor = Color.Black;
                //panel1.Location = new Point(4, 59);
                panel1.Height = 0;
            } else {
                panel1.BackColor = Color.FromArgb(255, 17 * health, 0);
            }
            Health = Math.Max(Math.Min(health, 65535),0);
            OnHealthChanged(new EventArgs());
        }
    }
}
