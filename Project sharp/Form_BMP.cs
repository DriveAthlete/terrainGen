using System;
using System.Drawing;
using System.Windows.Forms;

namespace Project_sharp
{
    public partial class Form_BMP : Form
    {
        Bitmap terra;
        public Form_BMP(Bitmap bmp)
        {
            terra = bmp;
            ClientSize = new Size(terra.Width, terra.Height);
            InitializeComponent();
            pictureBox1.ClientSize = new Size(terra.Width, terra.Height);
        }

        private void Form_BMP_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = terra;
        }
    }
}
