using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Chip8
{
    public partial class MainForm : Form
    {
        private Emulator em = new Emulator();
        public MainForm()
        {
            InitializeComponent();
            button1_Click(null, null);
            em.DrawGraphicsEvent += UpdateDraw;

        }

        private void UpdateDraw(byte[] pixels)
        {
            Bitmap flag = new Bitmap(64, 32);
            Graphics flagGraphics = Graphics.FromImage(flag);

            for(int i = 0; i < pixels.Length; i++)
            {
                Brush color = pixels[i] == 0 ? Brushes.Black : Brushes.White;
                flagGraphics.FillRectangle(color, (int) Math.Floor(i / 64d), i % 32, 1, 1);
            }
            pictureBox1.Image = flag;
        }

        public void CreateBitmapAtRuntime()
        {
            pictureBox1.Size = new Size(64, 32);
            this.Controls.Add(pictureBox1);

            Bitmap flag = new Bitmap(64, 32);
            Graphics flagGraphics = Graphics.FromImage(flag);
            int red = 0;
            int white = 11;
            while (white <= 100)
            {
                flagGraphics.FillRectangle(Brushes.Red, 0, red, 200, 10);
                flagGraphics.FillRectangle(Brushes.White, 0, white, 200, 10);
                red += 20;
                white += 20;
            }
            pictureBox1.Image = flag;

        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                em.LoadGame(File.ReadAllBytes("../../roms/pong.rom"));
                em.Initialize();
                System.Threading.Thread gameThread =
                    new System.Threading.Thread(new System.Threading.ThreadStart(em.Run));
                gameThread.Start();
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show(ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            em.Stop = true;
        }
    }
}
