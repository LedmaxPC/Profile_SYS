using System;
using System.IO;
using System.Windows.Forms;

namespace DesktopApp9
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            

            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog sec = new OpenFileDialog();
            sec.Filter = "Şəkillər|*.jpg;*.jpeg;*.png;*.bmp";

            if (sec.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.ImageLocation = sec.FileName;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() == "" ||
                textBox2.Text.Trim() == "" ||
                !maskedTextBox1.MaskCompleted ||
                !maskedTextBox2.MaskCompleted ||
                pictureBox1.ImageLocation == null)
            {
                MessageBox.Show("Bütün xanaları doldurun və şəkil seçin!");
                return;
            }

            string yol = Environment.GetFolderPath(
                Environment.SpecialFolder.DesktopDirectory) + "\\Melumatlar";

            Directory.CreateDirectory(yol);

            File.WriteAllText(yol + "\\melumat.txt",
                "İstifadəçi adı: " + textBox1.Text +
                "\r\nGmail: " + textBox2.Text +
                "\r\nTelefon: " + maskedTextBox1.Text +
                "\r\nTarix: " + maskedTextBox2.Text);

            string sekil = yol + "\\sekil" + Path.GetExtension(pictureBox1.ImageLocation);

            if (pictureBox1.ImageLocation != sekil)
            {
                File.Copy(pictureBox1.ImageLocation, sekil, true);
            }

            MessageBox.Show("Yadda saxlanıldı!");
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            button1_Click(sender, e);
        }

        private void linkLabel1_LinkClicked(
            object sender, LinkLabelLinkClickedEventArgs e)
        {
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
        }

        private void maskedTextBox1_MaskInputRejected(
            object sender, MaskInputRejectedEventArgs e)
        {
        }

        private void maskedTextBox2_MaskInputRejected(
            object sender, MaskInputRejectedEventArgs e)
        {
        }
    }
}