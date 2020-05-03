using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;


using System.Windows.Forms;



namespace Project_sharp
{
    public partial class Form1 : Form
    {
        private int size;
        private DiamondSquare terra;
        private int seed = (int)DateTime.Now.Ticks;
        private int detail;
        private bool willWarp = false;
        private bool canShow = false;
        Bitmap terra_bmp;

        public Form1()
        {
            InitializeComponent();
        }
        private void Generate_BMP()
        {
            if (canShow)
            {
                terra_bmp = new Bitmap(terra.Size, terra.Size);

                Color tmp;
                for (int x = 0; x < terra.Size; x++)
                    for (int z = 0; z < terra.Size; z++)
                    {
                        if (50 * terra.GetValue(x, z) < 0)
                        {
                            tmp = Color.Blue;
                        }
                        else if (50 * terra.GetValue(x, z) >= 0 && 50 * terra.GetValue(x, z) < 5)
                        {
                            tmp = Color.Yellow;
                        }
                        else if (50 * terra.GetValue(x, z) >= 5 && 50 * terra.GetValue(x, z) < 30)
                        {
                            tmp = Color.Green;
                        }
                        else if (50 * terra.GetValue(x, z) >= 30 && 50 * terra.GetValue(x, z) < 50)
                        {
                            tmp = Color.Gray;
                        }
                        else
                        {
                            tmp = Color.White;
                        }
                        terra_bmp.SetPixel(x, z, tmp);
                    }
            }
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
            trackBar_size.Minimum = 2;
            size = (int)Math.Pow(2, trackBar_size.Value);
            textBox_size.Text = "" + size;

            trackBar_detail.Minimum = 2;
            detail = (int)Math.Pow(2, trackBar_detail.Value);
            textBox_detail.Text = "" + detail;

            textBox_seed.Text = "" + seed;
            checkBox_warp.Checked = willWarp;
        }

        private void trackBar_size_Scroll(object sender, EventArgs e)
        {
            size = (int)Math.Pow(2, trackBar_size.Value);
            textBox_size.Text = "" + size;
        }

        private void trackBar_detail_Scroll(object sender, EventArgs e)
        {
            detail = (int)Math.Pow(2, trackBar_detail.Value);
            textBox_detail.Text = "" + trackBar_detail.Value;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_warp.Checked)
            {
                willWarp = true;
                checkBox_warp.Checked = true;
            }
            else
            {
                willWarp = false;
                checkBox_warp.Checked = false;
            }
        }

        private void button_Generate_Click(object sender, EventArgs e)
        {
            terra = null;
            
            seed = int.Parse(textBox_seed.Text);
            terra = new DiamondSquare(size, willWarp, seed);
            terra.Generate(detail);
            canShow = true;
        }

        private void button_ShowBMP_Click(object sender, EventArgs e)
        {
            if (canShow)
            {
                Generate_BMP();
                
                Form_BMP form = new Form_BMP(terra_bmp);
                form.Show(this);
            }
        }

        private void button_3D_Click(object sender, EventArgs e)
        {
            if (canShow)
            {
                Form_3D form = new Form_3D(terra);
                form.Show();
            }
        }

        private void button_Save_Click(object sender, EventArgs e)
        {
            if (canShow)
            {
                SaveFileDialog saveDialog = new SaveFileDialog();

                saveDialog.DefaultExt = "bmp";
                saveDialog.Filter = "BMP images (*.bmp)|*.bmp";
                saveDialog.InitialDirectory = Application.StartupPath;

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    var fileName = saveDialog.FileName;
                    if (!System.IO.Path.HasExtension(fileName) || System.IO.Path.GetExtension(fileName) != "bmp")
                        fileName = fileName + ".bmp";

                    terra_bmp.Save(fileName, System.Drawing.Imaging.ImageFormat.Bmp);
                }
            }
        }
    }
}
