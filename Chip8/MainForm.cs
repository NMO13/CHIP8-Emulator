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
using System.Drawing.Imaging;
using System.Threading;

namespace Chip8
{
    public partial class MainForm : Form
    {
        private Emulator em = new Emulator();
        private Bitmap bitmap;

        public MainForm()
        {
            InitializeComponent();
            bitmap = new Bitmap(64, 32);
            this.pictureBoxWithInterpolationMode1.Image = bitmap;
            em.DrawGraphicsEvent += UpdateDraw;
        }

        private void UpdateDraw(byte[] pixels)
        {
            var bits = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

            unsafe
            {
                byte* pointer = (byte*)bits.Scan0;
                for (var i = 0; i < pixels.Length; i++)
                {
                    pointer[0] = 0; // Blue
                    pointer[1] = pixels[i] == 0x1 ? (byte)0x64 : (byte)0; // Green
                    pointer[2] = 0; // Red
                    pointer[3] = 255; // Alpha
                    
                    pointer += 4; // 4 bytes per pixel
                }
            }

            bitmap.UnlockBits(bits);

            try
            {
                this.Invoke((Action)this.RedrawPanel);
            }
            catch(ObjectDisposedException e)
            {

            }
        }

        void RedrawPanel()
        {
            this.pictureBoxWithInterpolationMode1.Refresh();
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            char x = (char) e.KeyData;
            em.SetKeys(x, 1);
        }

        private void MainForm_KeyUp(object sender, KeyEventArgs e)
        {
            char x = (char)e.KeyData;
            em.SetKeys(x, 0);
        }

        Task t;
        private void buttonLoad_Click(object sender, EventArgs e)
        {
            openFileDialog.FileName = "";
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                em.LoadGame(File.ReadAllBytes(openFileDialog.FileName));

                try
                {
                    em.Initialize();
                    t = Task.Factory.StartNew(
                    () => {
                        em.Run();
                    }
                    );
                }
                catch (FileNotFoundException ex)
                {
                    MessageBox.Show(ex.Message, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            em.Stop = true;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void buttonPause_Click(object sender, EventArgs e)
        {
            em.Stop = true;
            this.t.Wait();
        }
    }
}
