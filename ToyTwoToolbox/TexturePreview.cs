using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ToyTwoToolbox {
    public partial class TexturePreview : UserControl {
        Image Texture;
        public TexturePreview(Image texture) {
            InitializeComponent();
            Texture = texture;
        }

        private void TexturePreview_Load(object sender, EventArgs e) {

        }
    }
}
