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

            // ProgressBar-ın minimum və maksimum dəyərlərini təyin edirik
            progressBar1.Minimum = 0;
            progressBar1.Maximum = 100;
            progressBar1.Value = 0;
        }

        // Xanaların dolma vəziyyətinə görə progress bar-ı addım-addım (25%) yeniləyən funksiya
        private void ProqresYenile()
        {
            int faiz = 0;

            // 1. İstifadəçi adı dolubsa +25%
            if (!string.IsNullOrWhiteSpace(textBox1.Text)) faiz += 25;

            // 2. Gmail dolubsa +25%
            if (!string.IsNullOrWhiteSpace(textBox2.Text)) faiz += 25;

            // 3. Telefon maskası tam doldurulubsa +25%
            if (maskedTextBox1.MaskCompleted) faiz += 25;

            // 4. Tarix maskası tam doldurulubsa və ya şəkil seçilibsə max 100% olmasın deyə yoxlayırıq.
            // Düyməyə basanda tam 100% olması üçün buradakı hesablama max 75% limitsiz qala bilər.
            if (maskedTextBox2.MaskCompleted) faiz += 25;

            // Əgər hər şey doldurulubsa amma hələ yadda saxlanılmayıbsa, proqres bar maksimum 75% göstərsin
            if (faiz > 75 && (string.IsNullOrEmpty(pictureBox1.ImageLocation) || progressBar1.Value < 100))
            {
                faiz = 75;
            }
            // Əgər şəkil də seçilibsə amma hələ düyməyə basılmayıbsa 75% qalsın, düyməyə basanda 100% olacaq
            else if (!string.IsNullOrEmpty(pictureBox1.ImageLocation) && faiz < 75)
            {
                faiz += 25;
            }

            // ProgressBar dəyərini yeniləyirik
            progressBar1.Value = Math.Min(faiz, 75);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog sec = new OpenFileDialog())
            {
                sec.Filter = "Şəkillər|*.jpg;*.jpeg;*.png;*.bmp";

                if (sec.ShowDialog() == DialogResult.OK)
                {
                    pictureBox1.ImageLocation = sec.FileName;
                    ProqresYenile(); // Şəkil seçilən kimi proqresi yoxla
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Giriş məlumatlarının doğrulanması
            if (string.IsNullOrWhiteSpace(textBox1.Text) ||
                string.IsNullOrWhiteSpace(textBox2.Text) ||
                !maskedTextBox1.MaskCompleted ||
                !maskedTextBox2.MaskCompleted ||
                string.IsNullOrEmpty(pictureBox1.ImageLocation))
            {
                MessageBox.Show("Bütün xanaları doldurun və şəkil seçin!");
                return;
            }

            try
            {
                string yol = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), "Melumatlar");
                Directory.CreateDirectory(yol);

                File.WriteAllText(Path.Combine(yol, "melumat.txt"),
                    "İstifadəçi adı: " + textBox1.Text +
                    "\r\nGmail: " + textBox2.Text +
                    "\r\nTelefon: " + maskedTextBox1.Text +
                    "\r\nTarix: " + maskedTextBox2.Text);

                string sekil = Path.Combine(yol, "sekil" + Path.GetExtension(pictureBox1.ImageLocation));

                if (Path.GetFullPath(pictureBox1.ImageLocation) != Path.GetFullPath(sekil))
                {
                    File.Copy(pictureBox1.ImageLocation, sekil, true);
                }

                // HƏR ŞEY UĞURLA YÜKLƏNDİ - Proqres bar tam dolur (100%)
                progressBar1.Value = 100;

                MessageBox.Show("Yadda saxlanıldı!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Xəta baş verdi: " + ex.Message);
            }
        }

        // İstifadədə olan yazı xanaları dəyişdikcə proqres bar real vaxtda yenilənir
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            ProqresYenile();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            ProqresYenile();
        }

        private void maskedTextBox1_TextChanged(object sender, EventArgs e)
        {
            ProqresYenile();
        }

        private void maskedTextBox2_TextChanged(object sender, EventArgs e)
        {
            ProqresYenile();
        }

        // Gərəksiz köhnə təkrarlanan düymə hadisəsi təmizləndi
        private void button1_Click_1(object sender, EventArgs e)
        {
            button1_Click(sender, e);
        }

        // Digər boş qalan metodlar (Silmək dizayner xətası verməsin deyə saxlanıldı)
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) { }
        private void progressBar1_Click(object sender, EventArgs e) { }
        private void pictureBox1_Click(object sender, EventArgs e) { }
        private void Form1_Load(object sender, EventArgs e) { }
        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e) { }
        private void maskedTextBox2_MaskInputRejected(object sender, MaskInputRejectedEventArgs e) { }
    }
}
