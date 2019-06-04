using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace otel_otomasyonu
{
    public partial class AdminGiris : Form
    {
        OleDbConnection bagln = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=otel-database.accdb");
        bool tasinabilirlik = false;
        Point eksen = new Point(0, 0);
        AnaEkran anaEkran = new AnaEkran();

        public AdminGiris()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox1.Visible = false;
            pictureBox2.Visible = false;
        }

        private void Cikis_buton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Giris_buton_Click(object sender, EventArgs e)
        {
            string k_Adi = null;
            string k_sifre = null;

            if (kullanici_adi.Text == "" && sifre.Text == "")
            {
                pictureBox1.Visible = true;
                pictureBox2.Visible = true;
                MessageBox.Show("Bilgi kutuları boş bırakılamaz!", "Bilgiler Hatalı", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            if (kullanici_adi.Text == "")
            {
                pictureBox1.Visible = true;
                MessageBox.Show("Bilgi kutuları boş bırakılamaz!", "Bilgiler Hatalı", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            if (sifre.Text == "")
            {
                pictureBox2.Visible = true;
                MessageBox.Show("Bilgi kutuları boş bırakılamaz!", "Bilgiler Hatalı", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            try
            {
                bagln.Open();
                OleDbCommand komut = new OleDbCommand("SELECT k_adi, k_sifre FROM admingiris", bagln);
                OleDbDataReader oku = komut.ExecuteReader();
                while (oku.Read())
                {
                    k_Adi = oku["k_adi"].ToString();
                    k_sifre = oku["k_sifre"].ToString();
                }

                if(kullanici_adi.Text == k_Adi && sifre.Text == k_sifre)
                {
                    MessageBox.Show("Giriş başarılı", "Giriş", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    anaEkran.Show();
                    this.Hide();
                    return;
                }
                else
                {
                    MessageBox.Show("Giriş bilgileri hatalı", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

            }
            catch (Exception hata)
            {
                Console.WriteLine(hata.ToString());
            }
            finally
            {
                bagln.Close();
            }

        }

        private void Panel2_MouseMove(object sender, MouseEventArgs e)
        {
            if (tasinabilirlik)
            {
                Point p = PointToScreen(e.Location);
                Location = new Point(p.X - this.eksen.X, p.Y - this.eksen.Y);
            }
        }

        private void Panel2_MouseDown(object sender, MouseEventArgs e)
        {
            tasinabilirlik = true;
            eksen = new Point(e.X, e.Y);
        }

        private void Panel2_MouseUp(object sender, MouseEventArgs e)
        {
            tasinabilirlik = false;
        }

        private void Kullanici_adi_Click(object sender, EventArgs e)
        {
            pictureBox1.Visible = false;
        }

        private void Sifre_Click(object sender, EventArgs e)
        {
            pictureBox2.Visible = false;
        }
    }
}
