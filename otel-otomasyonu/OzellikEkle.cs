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
    public partial class OzellikEkle : Form
    {

        OleDbConnection bagln = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=otel-database.accdb");
        AnaEkran anaEkran;
        bool tasinabilirlik = false;
        Point eksen = new Point(0, 0);

        public OzellikEkle(AnaEkran anaEkran)
        {
            InitializeComponent();
            this.anaEkran = anaEkran;
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

        private void Cikis_buton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Ozellik_kayıt_Click(object sender, EventArgs e)
        {
            string ozelliktemp = ozellik.Text.Trim();

            try
            {
                bagln.Open();
                OleDbCommand komut = new OleDbCommand("INSERT INTO ozellikgiris ([ozellik]) VALUES (@ozellik)", bagln);
                if (anaEkran.oda_ozellik_box.Items.Contains(ozelliktemp))
                {
                    MessageBox.Show("Bu ozellik zaten mevcut!", "Ozellik", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    komut.Parameters.AddWithValue("@ozellik", ozellik.Text.Trim());
                    komut.ExecuteNonQuery();
                    anaEkran.oda_ozellik_box.Items.Add(ozellik.Text.Trim());
                }

            }
            catch (Exception hata)
            {
                Console.WriteLine(hata.ToString());
            }
            finally
            {
                bagln.Close();
                this.Close();
            }
        }
    }
}
