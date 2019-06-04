using System;
using System.Collections;
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
    public partial class AnaEkran : Form
    {

        OleDbConnection bagln = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=otel-database.accdb");
        ArrayList arrayList = new ArrayList();
        bool tasinabilirlik = false;
        Point eksen = new Point(0, 0);

        int sonOdaFiyat = 0;
        int sonToplamFiyat = 0;
        int gungece = 0;

        public AnaEkran()
        {
            InitializeComponent();
        }

        private void AnaEkran_Load(object sender, EventArgs e)
        {
            ozellikBoxDoldur();
            odaSinifi();
            dataGetir();
            odaSayisiGuncelle();
            odaNoGetir();
            birimKontrol();
            rezervasyonSayisiGuncelle();


            giris_tarih.Format = DateTimePickerFormat.Custom;
            giris_tarih.CustomFormat = "dd/MM/yyyy";

            cikis_tarih.Format = DateTimePickerFormat.Custom;
            cikis_tarih.CustomFormat = "dd/MM/yyyy";
        }

        private void birimKontrol()
        {
            if (!(oda_no.Items.Count == 0))
            {
                oda_no.SelectedIndex = 0;
            }
            if (!(para_birimi.Items.Count == 0))
            {
                para_birimi.SelectedIndex = 0;
            }
            if (!(oda_fiyat_kur.Items.Count == 0))
            {
                oda_fiyat_kur.SelectedIndex = 0;
            }
        }

        private void odaSayisiGuncelle()
        {
            int oda_say = 0;

            try
            {
                bagln.Open();
                OleDbCommand komut = new OleDbCommand("SELECT oda_ID FROM odagiris", bagln);
                OleDbDataReader okuyucu = komut.ExecuteReader();
                while (okuyucu.Read())
                {
                    oda_say += 1;
                    if (oda_say < 10)
                    {
                        oda_sayisi.Text = string.Concat("0" + oda_say);
                    }
                    else if (oda_say > 10)
                    {
                        oda_sayisi.Text = string.Concat(oda_say);
                    }

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

        private void ozellikBoxDoldur()
        {
            oda_ozellik_box.Items.Clear();

            try
            {
                bagln.Open();
                OleDbCommand komut = new OleDbCommand("SELECT ozellik FROM ozellikgiris", bagln);
                OleDbDataReader okuyucu = komut.ExecuteReader();
                while (okuyucu.Read())
                {
                    oda_ozellik_box.Items.Add(okuyucu["ozellik"].ToString());
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

        private void odaSinifi()
        {
            string[] siniflar = { "Ekonomi", "V.I.P", "Kraliyet", "Business" };
            foreach (string sinifekle in siniflar)
            {
                oda_sinifi.Items.Add(sinifekle);
            }
        }

        private void dataGetir()
        {
            string komut, baglanti;
            baglanti = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=otel-database.accdb";
            komut = "SELECT oda_numarasi, oda_sinifi, oda_yatak_sayisi, oda_fiyat, oda_ozellikleri FROM odagiris";
            OleDbConnection baglan = new OleDbConnection(baglanti);
            OleDbDataAdapter getir = new OleDbDataAdapter(komut, baglan);

            try
            {
                baglan.Open();
                DataSet goster = new DataSet();
                getir.Fill(goster, "odagiris");
                dataGridView1.DataSource = goster.Tables["odagiris"];
            }
            catch (Exception hata)
            {
                Console.WriteLine(hata.ToString());
            }
            finally
            {
                getir.Dispose();
                baglan.Close();
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

        private void Cikis_buton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Oda_ara_Enter(object sender, EventArgs e)
        {
            oda_ara.Clear();
            oda_ara.ForeColor = Color.Black;
        }

        private void Oda_ara_Leave(object sender, EventArgs e)
        {
            oda_ara.ResetText();
            oda_ara.Text = "Oda Ara";
            oda_ara.ForeColor = Color.FromArgb(171, 170, 168);
        }

        private void Oda_kayit_Click(object sender, EventArgs e)
        {

            if (oda_numarasi.Text.Trim() == "" || oda_sinifi.Text.Trim() == "" || oda_yatak_sayisi.Text.Trim() == "" || oda_fiyat.Text.Trim() == "" || oda_ozellik_box.SelectedIndex == -1)
            {
                MessageBox.Show("Oda bilgileri boş bırakılamaz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            string h = null;

            for (int i = 0; i < arrayList.Count; i++)
            {
                arrayList.RemoveAt(i);
            }

            try
            {
                bagln.Open();
                OleDbCommand komut = new OleDbCommand("INSERT INTO odagiris ([oda_numarasi], [oda_sinifi], [oda_yatak_sayisi], [oda_fiyat], [oda_ozellikleri]) VALUES (@oda_numarasi, @oda_sinifi, @oda_yatak_sayisi, @oda_fiyat, @oda_ozellikleri)", bagln);
                komut.Parameters.AddWithValue("@oda_numarasi", oda_numarasi.Text.Trim());
                komut.Parameters.AddWithValue("@oda_sinifi", oda_sinifi.Text.Trim());
                komut.Parameters.AddWithValue("@oda_yatak_sayisi", oda_yatak_sayisi.Text.Trim());
                komut.Parameters.AddWithValue("@oda_fiyat", oda_fiyat.Text.Trim() + " " + oda_fiyat_kur.GetItemText(oda_fiyat_kur.SelectedItem));
                foreach (string secilen_items in oda_ozellik_box.CheckedItems)
                {
                    string item = secilen_items;
                    arrayList.Add(item);
                }

                for (int i = 0; i < arrayList.Count; i++)
                {
                    if (i == 0)
                    {
                        h += arrayList[i].ToString();
                    }
                    else if (i > 0)
                    {
                        h += ", " + arrayList[i].ToString();
                    }

                }
                komut.Parameters.AddWithValue("@oda_ozellikleri", h);
                komut.ExecuteNonQuery();
            }
            catch (Exception hata)
            {
                Console.WriteLine(hata.ToString());
            }
            finally
            {
                MessageBox.Show("Oda Kaydı başarılı", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dataGetir();
                bagln.Close();
            }

            oda_numarasi.ResetText();
            oda_sinifi.ResetText();
            oda_yatak_sayisi.ResetText();
            oda_fiyat.ResetText();
            foreach (int i in oda_ozellik_box.CheckedIndices)
            {
                oda_ozellik_box.SetItemCheckState(i, CheckState.Unchecked);
            }

        }

        private void Ozellik_ekle_Click(object sender, EventArgs e)
        {
            OzellikEkle ozellik = new OzellikEkle(this);
            ozellik.ShowDialog();
        }

        private void Ozellik_sil_Click(object sender, EventArgs e)
        {
            DialogResult sonuc;
            sonuc = MessageBox.Show("Seçilenlerin silinmesini istediğinizden eminmisiniz?", "Özeliik sil", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (sonuc == DialogResult.Yes)
            {
                foreach (string item in oda_ozellik_box.CheckedItems.OfType<string>().ToList())
                {
                    try
                    {
                        bagln.Open();
                        OleDbCommand cmd = new OleDbCommand("DELETE FROM ozellikgiris WHERE [ozellik] = @ozellik", bagln);
                        cmd.Parameters.AddWithValue("@ozellik", item);
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception hata)
                    {
                        Console.WriteLine(hata.ToString());
                    }
                    finally
                    {
                        bagln.Close();
                    }
                    oda_ozellik_box.Items.Remove(item);
                }
            }
            else
            {
                return;
            }
        }

        private void Oda_guncelle_Click(object sender, EventArgs e)
        {
            if (oda_numarasi.Text.Trim() == "" || oda_sinifi.Text.Trim() == "" || oda_yatak_sayisi.Text.Trim() == "" || oda_fiyat.Text.Trim() == "" || oda_ozellik_box.CheckedItems.Count < 0)
            {
                MessageBox.Show("Oda bilgileri boş bırakılamaz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            string h = null;
            for (int i = 0; i < arrayList.Count; i++)
            {
                arrayList.RemoveAt(i);
            }

            try
            {
                bagln.Open();
                OleDbCommand gncl = new OleDbCommand("UPDATE odagiris SET [oda_numarasi]=@oda_numarasi, [oda_sinifi]=@oda_sinifi, [oda_yatak_sayisi]=@oda_yatak_sayisi, [oda_fiyat]=@oda_fiyat WHERE [oda_numarasi]=@oda_numarasi", bagln);
                gncl.Parameters.AddWithValue("@oda_numarasi", oda_numarasi.Text.Trim());
                gncl.Parameters.AddWithValue("@oda_sinifi", oda_sinifi.Text.Trim());
                gncl.Parameters.AddWithValue("@oda_yatak_sayisi", oda_yatak_sayisi.Text.Trim());
                gncl.Parameters.AddWithValue("@oda_fiyat", oda_fiyat.Text.Trim() + " " + oda_fiyat_kur.GetItemText(oda_fiyat_kur.SelectedItem));

                /*foreach (string secilen_items in oda_ozellik_box.CheckedItems)
                {
                    string item = secilen_items;
                    arrayList.Add(item);
                }

                for (int i = 0; i < arrayList.Count; i++)
                {
                    if (i == 0)
                    {
                        h += arrayList[i].ToString();
                    }
                    else if (i > 0)
                    {
                        h += ", " + arrayList[i].ToString();
                    }
                }
                gncl.Parameters.AddWithValue("@oda_ozellikleri", h);*/
                gncl.ExecuteNonQuery();

                MessageBox.Show("Oda başarılı bir şekilde güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception hata)
            {
                Console.WriteLine(hata.ToString());
            }
            finally
            {
                dataGetir();
                bagln.Close();
            }
        }

        private void Oda_sil_Click(object sender, EventArgs e)
        {
            try
            {
                bagln.Open();
                OleDbCommand sil = new OleDbCommand("DELETE FROM odagiris WHERE [oda_numarasi]=@oda_numarasi", bagln);
                sil.Parameters.AddWithValue("@oda_numarasi", oda_numarasi.Text);
                sil.ExecuteNonQuery();
                sil.Dispose();

                MessageBox.Show("Oda başarılı bir şekilde silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.ToString());
            }
            finally
            {
                dataGetir();
                bagln.Close();
                odaSayisiGuncelle();
            }
        }

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int sec = dataGridView1.SelectedCells[0].RowIndex;
            oda_numarasi.Text = dataGridView1.Rows[sec].Cells[0].Value.ToString();
            oda_sinifi.Text = dataGridView1.Rows[sec].Cells[1].Value.ToString();
            oda_yatak_sayisi.Text = dataGridView1.Rows[sec].Cells[2].Value.ToString();
            oda_fiyat.Text = dataGridView1.Rows[sec].Cells[3].Value.ToString().Substring(0, dataGridView1.Rows[sec].Cells[3].Value.ToString().IndexOf(' ')).Trim();
            oda_fiyat_kur.Text = dataGridView1.Rows[sec].Cells[3].Value.ToString().Substring(dataGridView1.Rows[sec].Cells[3].Value.ToString().IndexOf(' ')).Trim();
        }

        private void Oda_ara_TextChanged(object sender, EventArgs e)
        {

            if (oda_ara.Text == "Oda Ara")
            {
                return;
            }

            try
            {
                bagln.Open();
                OleDbDataAdapter ara = new OleDbDataAdapter("SELECT oda_numarasi, oda_sinifi, oda_yatak_sayisi, oda_fiyat, oda_ozellikleri FROM odagiris", bagln);
                ara.SelectCommand.CommandText = "SELECT oda_numarasi, oda_sinifi, oda_yatak_sayisi, oda_fiyat, oda_ozellikleri FROM odagiris" + " WHERE(oda_numarasi like'%" + oda_ara.Text + "%')";
                DataSet goster = new DataSet();
                ara.Fill(goster, "odagiris");
                goster.Tables["odagiris"].Clear();
                dataGridView1.DataSource = goster.Tables["odagiris"];
                ara.Fill(goster, "odagiris");
                ara.Dispose();
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

        /*-----------------------------PAGE 2-----------------------------*/

        private void dataGetir2()
        {
            string komut, baglanti;
            baglanti = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=otel-database.accdb";
            komut = "SELECT musteri_adi, musteri_soyadi, musteri_tcno, musteri_telno, giris_tarihi, cikis_tarihi, gun_gece, tutar FROM musterigiris";
            OleDbConnection baglan = new OleDbConnection(baglanti);
            OleDbDataAdapter getir = new OleDbDataAdapter(komut, baglan);

            try
            {
                baglan.Open();
                DataSet goster = new DataSet();
                getir.Fill(goster, "musterigiris");
                dataGridView2.DataSource = goster.Tables["musterigiris"];
            }
            catch (Exception hata)
            {
                Console.WriteLine(hata.ToString());
            }
            finally
            {
                getir.Dispose();
                baglan.Close();
            }
        }

        private void odaNoGetir()
        {
            oda_no.Items.Clear();

            try
            {
                bagln.Open();
                OleDbCommand komut = new OleDbCommand("SELECT oda_numarasi FROM odagiris", bagln);
                OleDbDataReader okuyucu = komut.ExecuteReader();
                while (okuyucu.Read())
                {
                    oda_no.Items.Add(okuyucu["oda_numarasi"].ToString());
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

        private void paraGuncelle()
        {
            string oda_fiyati = null;

            try
            {
                bagln.Open();
                OleDbCommand komut = new OleDbCommand("SELECT oda_fiyat FROM odagiris WHERE [oda_numarasi]=@oda_numarasi", bagln);
                komut.Parameters.AddWithValue("@oda_numarasi", oda_no.GetItemText(oda_no.SelectedItem));
                OleDbDataReader okuyucu = komut.ExecuteReader();
                while (okuyucu.Read())
                {
                    oda_fiyati = okuyucu["oda_fiyat"].ToString();
                }

                Console.WriteLine();
                if (oda_fiyati.Substring(oda_fiyati.ToString().LastIndexOf(' ')).Trim() == "₺")
                {

                    /*int kurlu_fiyat = Convert.ToInt32(oda_fiyati.Substring(0, oda_fiyati.IndexOf(' ')));
                    Console.WriteLine(kurlu_fiyat);*/
                    if (para_birimi.GetItemText(para_birimi.SelectedItem) == "₺")
                    {
                        tutar.Text = Convert.ToString(sonToplamFiyat) + " ₺";
                    }
                    else if (para_birimi.GetItemText(para_birimi.SelectedItem) == "$")
                    {
                        tutar.Text = Convert.ToString(sonToplamFiyat / 6) + " $";
                    }
                    else if (para_birimi.GetItemText(para_birimi.SelectedItem) == "€")
                    {
                        tutar.Text = Convert.ToString(sonToplamFiyat / 7) + " €";
                    }
                }
                else if (oda_fiyati.Substring(oda_fiyati.ToString().LastIndexOf(' ')).Trim() == "$")
                {
                    //int kurlu_fiyat = Convert.ToInt32(oda_fiyati.Substring(0, oda_fiyati.IndexOf(' ')));
                    if (para_birimi.GetItemText(para_birimi.SelectedItem) == "₺")
                    {
                        tutar.Text = Convert.ToString(sonToplamFiyat * 6) + " ₺";
                    }
                    else if (para_birimi.GetItemText(para_birimi.SelectedItem) == "$")
                    {
                        tutar.Text = Convert.ToString(sonToplamFiyat) + " $";
                    }
                    else if (para_birimi.GetItemText(para_birimi.SelectedItem) == "€")
                    {
                        tutar.Text = Convert.ToString(Math.Ceiling(sonToplamFiyat / 1.12)) + "  €";
                    }
                }
                else if (oda_fiyati.Substring(oda_fiyati.ToString().LastIndexOf(' ')).Trim() == "€")
                {
                    //int kurlu_fiyat = Convert.ToInt32(oda_fiyati.Substring(0, oda_fiyati.IndexOf(' ')));
                    if (para_birimi.GetItemText(para_birimi.SelectedItem) == "₺")
                    {
                        tutar.Text = Convert.ToString(sonToplamFiyat * 7) + " ₺";
                    }
                    else if (para_birimi.GetItemText(para_birimi.SelectedItem) == "$")
                    {
                        tutar.Text = Convert.ToString(Math.Ceiling(sonToplamFiyat * 1.12)) + " $";
                    }
                    else if (para_birimi.GetItemText(para_birimi.SelectedItem) == "€")
                    {
                        tutar.Text = Convert.ToString(sonToplamFiyat) + " €";
                    }
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

        private void Para_birimi_SelectedIndexChanged(object sender, EventArgs e)
        {
            paraGuncelle();
        }

        private void tarihGuncelle()
        {
            int gün = cikis_tarih.Value.DayOfYear - giris_tarih.Value.DayOfYear;
            gungece = gün + 1;
            gün_gece.Text = gungece + " Gün / " + (gungece - 1) + " Gece";
            odaGuncelle();
        }

        private void odaGuncelle()
        {
            string oda_fiyati = null;
            try
            {
                bagln.Open();
                OleDbCommand komut = new OleDbCommand("SELECT oda_fiyat FROM odagiris WHERE [oda_numarasi]=@oda_numarasi", bagln);
                komut.Parameters.AddWithValue("@oda_numarasi", oda_no.GetItemText(oda_no.SelectedItem));
                OleDbDataReader okuyucu = komut.ExecuteReader();
                while (okuyucu.Read())
                {
                    oda_fiyati = okuyucu["oda_fiyat"].ToString();
                }

                sonOdaFiyat = Convert.ToInt32(oda_fiyati.Substring(0, oda_fiyati.IndexOf(' ')));
                sonToplamFiyat = gungece * sonOdaFiyat;

            }
            catch (Exception hata)
            {
                Console.WriteLine(hata.ToString());
            }
            finally
            {
                bagln.Close();
                paraGuncelle();
            }
        }

        private void Cikis_tarih_ValueChanged(object sender, EventArgs e)
        {
            tarihGuncelle();

        }

        private void Oda_no_SelectedIndexChanged(object sender, EventArgs e)
        {
            odaGuncelle();
        }

        private void TabControl1_Click(object sender, EventArgs e)
        {
            dataGetir2();
            tarihGuncelle();
            odaGuncelle();
            paraGuncelle();
        }

        private void Rezervasyon_kaydet_Click(object sender, EventArgs e)
        {
            if (musteri_adi.Text.Trim() == "" || musteri_soyadi.Text.Trim() == "" || musteri_tcno.Text.Trim() == "" || musteri_telno.Text.Trim() == "")
            {
                MessageBox.Show("Oda bilgileri boş bırakılamaz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            try
            {
                bagln.Open();
                OleDbCommand komut = new OleDbCommand("INSERT INTO musterigiris ([musteri_adi], [musteri_soyadi], [musteri_tcno], [musteri_telno], [giris_tarihi], [cikis_tarihi], [gun_gece], [tutar]) VALUES (@musteri_adi, @musteri_soyadi, @musteri_tcno, @musteri_telno, @giris_tarihi, @cikis_tarihi, @gun_gece, @tutar)", bagln);
                komut.Parameters.AddWithValue("@musteri_adi", musteri_adi.Text.Trim());
                komut.Parameters.AddWithValue("@musteri_soyadi", musteri_soyadi.Text.Trim());
                komut.Parameters.AddWithValue("@musteri_tcno", musteri_tcno.Text.Trim());
                komut.Parameters.AddWithValue("@musteri_telno", musteri_telno.Text.Trim());
                komut.Parameters.AddWithValue("@giris_tarihi", giris_tarih.Text);
                komut.Parameters.AddWithValue("@cikis_tarihi", cikis_tarih.Text);
                komut.Parameters.AddWithValue("@gun_gece", gungece.ToString());
                komut.Parameters.AddWithValue("@tutar", tutar.Text.Trim());
                komut.ExecuteNonQuery();
            }
            catch (Exception hata)
            {
                Console.WriteLine(hata.ToString());
            }
            finally
            {
                MessageBox.Show("Rezervasyon kaydı başarılı", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                bagln.Close();
                dataGetir2();
                rezervasyonSayisiGuncelle();
            }
            musteri_adi.ResetText();
            musteri_soyadi.ResetText();
            musteri_tcno.ResetText();
            musteri_telno.ResetText();
            giris_tarih.ResetText();
            cikis_tarih.ResetText();
        }

        private void Musteri_ara_TextChanged(object sender, EventArgs e)
        {
            if (musteri_ara.Text == "Müşteri Ara")
            {
                return;
            }

            try
            {
                bagln.Open();
                OleDbDataAdapter ara = new OleDbDataAdapter("SELECT musteri_adi, musteri_soyadi, musteri_tcno, musteri_telno, giris_tarihi, cikis_tarihi, gun_gece, tutar FROM musterigiris", bagln);
                ara.SelectCommand.CommandText = "SELECT musteri_adi, musteri_soyadi, musteri_tcno, musteri_telno, giris_tarihi, cikis_tarihi, gun_gece, tutar FROM musterigiris" + " WHERE(musteri_adi like'%" + musteri_ara.Text + "%')";
                DataSet goster = new DataSet();
                ara.Fill(goster, "musterigiris");
                goster.Tables["musterigiris"].Clear();
                dataGridView2.DataSource = goster.Tables["musterigiris"];
                ara.Fill(goster, "musterigiris");
                ara.Dispose();
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

        private void Musteri_ara_Enter(object sender, EventArgs e)
        {
            musteri_ara.Clear();
            musteri_ara.ForeColor = Color.Black;
        }

        private void Musteri_ara_Leave(object sender, EventArgs e)
        {
            musteri_ara.ResetText();
            musteri_ara.Text = "Müşteri Ara";
            musteri_ara.ForeColor = Color.FromArgb(171, 170, 168);
        }

        private void Rezervasyon_guncelle_Click(object sender, EventArgs e)
        {

            if (musteri_adi.Text.Trim() == "" || musteri_soyadi.Text.Trim() == "" || musteri_tcno.Text.Trim() == "" || musteri_telno.Text.Trim() == "")
            {
                MessageBox.Show("Oda bilgileri boş bırakılamaz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            try
            {
                bagln.Open();
                OleDbCommand gncl = new OleDbCommand("UPDATE musterigiris SET [musteri_adi]=@musteri_adi, [musteri_soyadi]=@musteri_soyadi, [musteri_tcno]=@musteri_tcno, [musteri_telno]=@musteri_telno, [giris_tarihi]=@giris_tarihi, [cikis_tarihi]=@cikis_tarihi, [gun_gece]=@gun_gece, [tutar]=@tutar WHERE [musteri_tcno]=@musteri_tcno", bagln);
                gncl.Parameters.AddWithValue("@musteri_adi", musteri_adi.Text.Trim());
                gncl.Parameters.AddWithValue("@musteri_soyadi", musteri_soyadi.Text.Trim());
                gncl.Parameters.AddWithValue("@musteri_tcno", musteri_tcno.Text.Trim());
                gncl.Parameters.AddWithValue("@musteri_telno", musteri_telno.Text.Trim());
                gncl.Parameters.AddWithValue("@giris_tarihi", giris_tarih.Text);
                gncl.Parameters.AddWithValue("@cikis_tarihi", cikis_tarih.Text);
                gncl.Parameters.AddWithValue("@gun_gece", gungece.ToString());
                gncl.Parameters.AddWithValue("@tutar", tutar.Text.Trim());
                gncl.ExecuteNonQuery();
                gncl.ExecuteNonQuery();

            }
            catch (Exception hata)
            {
                Console.WriteLine(hata.ToString());
            }
            finally
            {
                MessageBox.Show("Rezervasyon başarılı bir şekilde güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                bagln.Close();
                dataGetir2();
                rezervasyonSayisiGuncelle();
            }

            musteri_adi.ResetText();
            musteri_soyadi.ResetText();
            musteri_tcno.ResetText();
            musteri_telno.ResetText();
            giris_tarih.ResetText();
            cikis_tarih.ResetText();
        }

        private void Rezervasyon_sil_Click(object sender, EventArgs e)
        {
            if (musteri_adi.Text.Trim() == "" || musteri_soyadi.Text.Trim() == "" || musteri_tcno.Text.Trim() == "" || musteri_telno.Text.Trim() == "")
            {
                MessageBox.Show("Oda bilgileri boş bırakılamaz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            DialogResult sonuc;
            sonuc = MessageBox.Show("Rezervasyonu silmek istediğinize eminmisiniz?", "Rezervasyon sil", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (sonuc == DialogResult.Yes)
            {
                try
                {
                    bagln.Open();
                    OleDbCommand cmd = new OleDbCommand("DELETE FROM musterigiris WHERE [musteri_tcno] = @musteri_tcno", bagln);
                    cmd.Parameters.AddWithValue("@musteri_tcno", musteri_tcno.Text.Trim());
                    cmd.ExecuteNonQuery();
                }
                catch (Exception hata)
                {
                    Console.WriteLine(hata.ToString());
                }
                finally
                {
                    MessageBox.Show("Rezervasyon başarılı bir şekilde silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    bagln.Close();
                    dataGetir2();
                    rezervasyonSayisiGuncelle();
                }

                musteri_adi.ResetText();
                musteri_soyadi.ResetText();
                musteri_tcno.ResetText();
                musteri_telno.ResetText();
                giris_tarih.ResetText();
                cikis_tarih.ResetText();
            }
        }

        private void rezervasyonSayisiGuncelle()
        {
            int musteri_say = 0;

            try
            {
                bagln.Open();
                OleDbCommand komut = new OleDbCommand("SELECT musteri_ID FROM musterigiris", bagln);
                OleDbDataReader okuyucu = komut.ExecuteReader();
                while (okuyucu.Read())
                {
                    musteri_say += 1;
                    if (musteri_say < 10)
                    {
                        rezerve_sayisi.Text = string.Concat("0" + musteri_say);
                    }
                    else if (musteri_say > 10)
                    {
                        rezerve_sayisi.Text = string.Concat(musteri_say);
                    }

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

        private void DataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int sec = dataGridView2.SelectedCells[0].RowIndex;
            musteri_adi.Text = dataGridView2.Rows[sec].Cells[0].Value.ToString();
            musteri_soyadi.Text = dataGridView2.Rows[sec].Cells[1].Value.ToString();
            musteri_tcno.Text = dataGridView2.Rows[sec].Cells[2].Value.ToString();
            musteri_telno.Text = dataGridView2.Rows[sec].Cells[3].Value.ToString();
            giris_tarih.Text = dataGridView2.Rows[sec].Cells[4].Value.ToString();
            cikis_tarih.Text = dataGridView2.Rows[sec].Cells[5].Value.ToString();
            tutar.Text = dataGridView2.Rows[sec].Cells[7].Value.ToString();
        }
    }
}
