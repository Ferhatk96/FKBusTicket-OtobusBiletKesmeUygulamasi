using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace FK_Bus_Ticket_App
{

    public partial class Form1 : Form
    {
        public void UpdateButtonStatus(bool status)
        {
            // Form1'deki butonu aktif etme / pasif yapma methodu (public).
            button1.BackColor = Color.Yellow;
            //butonRengiYesil();
            // butonListele();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=.;Initial Catalog=FKBussTicketDB;Integrated Security=True");

        //public int anlik = 0;
        // Veritabanı bağlantı dizesi
        public const string ConnectionString = "Data Source=.;Initial Catalog=FKBussTicketDB;Integrated Security=True";

        public Form1()
        {
            InitializeComponent();

        }

        public void VeriListele()
        {
            baglanti.Open();
            SqlCommand veri = new SqlCommand("select * from KalkisVaris where kalkisyeri=@kalkisyeri", baglanti);
            veri.Parameters.AddWithValue("@kalkisYeri", kalkiscmb.Text);

            SqlDataReader dr;
            dr = veri.ExecuteReader();

            while (dr.Read())
            {

                kalkslbl.Text = dr.GetValue(1).ToString();
                varislbl.Text = dr.GetValue(2).ToString();
                yolclklbl.Text = dr.GetValue(3).ToString();
                saatLBL.Text = dr.GetValue(4).ToString();
                FiyatLBL.Text = dr.GetValue(5).ToString();


            }

            kalkslbl.Text = kalkisYeri;
            varislbl.Text = varisYeri;
            yolclklbl.Text = yolTarihi;
            saatLBL.Text = saat;
            FiyatLBL.Text = Convert.ToString(toplamFiyat);
            baglanti.Close();

            baglanti.Open();
            SqlCommand command = new SqlCommand("select * from otobusPlakaKayit", baglanti);

            SqlDataReader drn = command.ExecuteReader();

            // Mevcut öğeleri al
            List<string> mevcutOgeler = otobusplakasec.Items.Cast<string>().ToList();

            while (drn.Read())
            {
                string deger = drn.GetString(1);
                // Eğer öğe zaten varsa ekleme
                if (!mevcutOgeler.Contains(deger))
                {
                    otobusplakasec.Items.Add(deger);
                }
            }
            baglanti.Close();
            // ComboBox'taki öğeleri birleştir
            otoPlaka = string.Join(", ", otobusplakasec.Items.Cast<string>());
            plakaLBL.Text = otoPlaka;
            otobusplakasec.Text = otoPlaka;

            otobusplakasec.Text = otoPlaka;
            plakaLBL.Text = otoPlaka;
            Kayit kayit1 = new Kayit();
            kayit1.plaka = otoPlaka;
            otobusplakasec.Text = otoPlaka;
            baglanti.Open();

            using (SqlCommand comman = new SqlCommand("SELECT * FROM KalkisVaris WHERE id=@id", baglanti))
            {
                comman.Parameters.AddWithValue("@id", 1);

                using (SqlDataReader reader = comman.ExecuteReader())
                {
                    if (reader.Read()) // Veri bulundu mu kontrolü
                    {
                        // Alanları değişkenlere ata
                        kalkslbl.Text = (reader["kalkisYeri"].ToString());
                        varislbl.Text = (reader["varisYeri"].ToString());
                        yolclklbl.Text = (reader["yolTarihi"].ToString());
                        saatLBL.Text = (reader["saat"].ToString());
                        FiyatLBL.Text = (reader["fiyat"].ToString());
                        // Şimdi bu değişkenleri kullanabilirsiniz


                    }

                    else
                    {
                        // Veri bulunamazsa veya okuma hatası olursa bir hata mesajı verebilirsiniz
                        MessageBox.Show("Veri bulunamadı veya okuma hatası oluştu.");
                    }
                }
            }

            baglanti.Close();
        }

        public void Veritut()
        {
            baglanti.Open();
            SqlCommand veri = new SqlCommand("select * from KalkisVaris where id=@id", baglanti);

            veri.Parameters.AddWithValue("@id", 1);

            SqlDataReader dr;
            dr = veri.ExecuteReader();

            while (dr.Read())
            {
                kalkiscmb.Text = dr.GetValue(1).ToString();
                variscmb.Text = dr.GetValue(2).ToString();
                yolculuktrhdt.Text = dr.GetValue(3).ToString();
                saatLBL.Text = dr.GetValue(4).ToString();
                FiyatLBL.Text = dr.GetValue(5).ToString();

            }

            baglanti.Close();
            otobusplakasec.Text = otoPlaka;
            plakaLBL.Text = otoPlaka;

            Kayit kayit1 = new Kayit();
            kayit1.plaka = otoPlaka;
            otobusplakasec.Text = otoPlaka;
            kalkslbl.Text = kalkisYeri;
            varislbl.Text = varisYeri;
            yolclklbl.Text = yolTarihi;
            saatLBL.Text = saat;
            FiyatLBL.Text = Convert.ToString(toplamFiyat);
            baglanti.Open();

            using (SqlCommand comman = new SqlCommand("SELECT * FROM KalkisVaris WHERE id=@id", baglanti))
            {
                comman.Parameters.AddWithValue("@id", 1);

                using (SqlDataReader reader = comman.ExecuteReader())
                {
                    if (reader.Read()) // Veri bulundu mu kontrolü
                    {
                        // Alanları değişkenlere ata
                        kalkslbl.Text = (reader["kalkisYeri"].ToString());
                        varislbl.Text = (reader["varisYeri"].ToString());
                        yolclklbl.Text = (reader["yolTarihi"].ToString());
                        saatLBL.Text = (reader["saat"].ToString());
                        FiyatLBL.Text = (reader["fiyat"].ToString());
                        // Şimdi bu değişkenleri kullan
                    }
                    else
                    {
                        MessageBox.Show("Veri bulunamadı veya okuma hatası oluştu.");
                    }
                }
            }

            baglanti.Close();
        }
        double Marmarafiyat = 219.90;
        double EgeFiyat = 309.90;
        double AkdenizFiyat = 299.90;
        double IcAnadoluFiyat = 349.90;
        double KaradenizFiyat = 269.90;
        double DoguAnadoluFİyat = 234.90;
        double GuneyAnadoluFiyat = 209.90;

        public void ButonSifirla()
        {
            this.Controls.Clear(); this.InitializeComponent();
            butonRengiYesil();
            butonListele();
            VeriListele();
            Veritut();
        }
        private void butonRengiYesil()
        {
            for (int i = 1; i <= 34; i++)
            {
                // Düğme isimlerini button1, button2, ..., button36 şeklinde oluşturuyoruz.
                string buttonName = "button" + i;

                // Kontrol adını kullanarak kontrolü buluyoruz.
                Control control = Controls.Find(buttonName, true)[0];

                // Eğer kontrol bir Button ise arka plan rengini yeşil yapıyoruz.
                if (control is Button button)
                {
                    button.BackColor = Color.LightGreen;
                }
            }
        }

        public void butonListele()
        {
            SqlConnection baglan = new SqlConnection(ConnectionString);
            baglan.Open();
            SqlCommand komut = new SqlCommand("SELECT butonAdi, renk FROM ButonRenkleriDB ORDER BY id");
            komut.Connection = baglan;
            SqlDataReader dr;
            dr = komut.ExecuteReader();
            while (dr.Read())
            {
                butonAd = dr["butonAdi"].ToString();
                butonRenk = dr["renk"].ToString();
                Color color = Color.FromName(butonRenk);

                if (button1.Text == butonAd)
                {

                    button1.BackColor = color;
                }
                if (button2.Text == butonAd)
                {

                    button2.BackColor = color;
                }
                if (button3.Text == butonAd)
                {

                    button3.BackColor = color;

                }
                if (button4.Text == butonAd)
                {

                    button4.BackColor = color;

                }
                if (button5.Text == butonAd)
                {

                    button5.BackColor = color;

                }
                if (button6.Text == butonAd)
                {

                    button6.BackColor = color;

                }
                if (button7.Text == butonAd)
                {

                    button7.BackColor = color;

                }
                if (button8.Text == butonAd)
                {

                    button8.BackColor = color;

                }
                if (button9.Text == butonAd)
                {

                    button9.BackColor = color;

                }
                if (button10.Text == butonAd)
                {

                    button10.BackColor = color;

                }
                if (button11.Text == butonAd)
                {

                    button11.BackColor = color;

                }
                if (button12.Text == butonAd)
                {

                    button12.BackColor = color;

                }
                if (button13.Text == butonAd)
                {

                    button13.BackColor = color;

                }
                if (button21.Text == butonAd)
                {

                    button21.BackColor = color;

                }
                if (button20.Text == butonAd)
                {

                    button20.BackColor = color;

                }
                if (button19.Text == butonAd)
                {

                    button19.BackColor = color;

                }
                if (button18.Text == butonAd)
                {

                    button18.BackColor = color;

                }
                if (button17.Text == butonAd)
                {

                    button17.BackColor = color;

                }
                if (button16.Text == butonAd)
                {

                    button16.BackColor = color;

                }
                if (button15.Text == butonAd)
                {

                    button15.BackColor = color;

                }
                if (button14.Text == butonAd)
                {

                    button14.BackColor = color;

                }
                if (button13.Text == butonAd)
                {

                    button13.BackColor = color;

                }
                if (button12.Text == butonAd)
                {

                    button12.BackColor = color;

                }
                if (button22.Text == butonAd)
                {

                    button22.BackColor = color;

                }
                if (button34.Text == butonAd)
                {

                    button34.BackColor = color;

                }
                if (button33.Text == butonAd)
                {

                    button33.BackColor = color;

                }
                if (button32.Text == butonAd)
                {

                    button32.BackColor = color;

                }
                if (button31.Text == butonAd)
                {

                    button31.BackColor = color;

                }
                if (button30.Text == butonAd)
                {

                    button30.BackColor = color;

                }
                if (button29.Text == butonAd)
                {

                    button29.BackColor = color;

                }
                if (button28.Text == butonAd)
                {

                    button28.BackColor = color;

                }
                if (button27.Text == butonAd)
                {

                    button27.BackColor = color;

                }
                if (button26.Text == butonAd)
                {

                    button26.BackColor = color;

                }
                if (button25.Text == butonAd)
                {

                    button25.BackColor = color;

                }
                if (button24.Text == butonAd)
                {

                    button24.BackColor = color;

                }
                if (button23.Text == butonAd)
                {

                    button23.BackColor = color;

                }
                Veritut();
                VeriListele();

            }
        }
        public string ToplamFiyat { get; set; }

        public double varisfiyat;
        public double kalkisfiyat;
        public double sonuc;
        public string otoPlaka;
        public double toplamFiyat;
        public string kalkisYeri;
        public string varisYeri;
        public string yolTarihi;
        public string saat;
        private void Form1_Load(object sender, EventArgs e)
        {


            using (SqlConnection baglanti = new SqlConnection("Data Source=.;Initial Catalog=FKBussTicketDB;Integrated Security=True"))
            {
                baglanti.Open();

                using (SqlCommand comman = new SqlCommand("SELECT kalkisYeri, varisYeri, yolTarihi, saat, fiyat FROM KalkisVaris WHERE id=@id", baglanti))
                {
                    comman.Parameters.AddWithValue("@id", 1);

                    using (SqlDataReader reader = comman.ExecuteReader())
                    {
                        if (reader.Read()) // Veri bulundu mu kontrolü
                        {
                            // Alanları değişkenlere ata
                            kalkisYeri = reader["kalkisYeri"].ToString();
                            varisYeri = reader["varisYeri"].ToString();
                            yolTarihi = reader["yolTarihi"].ToString();
                            saat = reader["saat"].ToString();
                            toplamFiyat = Convert.ToDouble(reader["fiyat"].ToString());

                        }

                        else
                        {
                            MessageBox.Show("Veri bulunamadı veya okuma hatası oluştu.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
            baglanti.Close();
            Kayit kayit = new Kayit();

            kayit.GelenkalkisYeri = kalkisYeri;
            kayit.GelenvarisYeri = varisYeri;
            kayit.GelenyolTarihi = yolTarihi;
            kayit.Gelensaat = saat;
            kayit.GelentoplamFiyat = toplamFiyat;
            yolculuktrhdt.Format = DateTimePickerFormat.Custom;
            yolculuktrhdt.CustomFormat = "dd-MMM-yyyy";
            // yolculuktrhdt.MinDate = DateTime.Today;
            Veritut();

            AdminPanel adminPanel = (AdminPanel)Application.OpenForms["AdminPanel"];  // form1 i güncelleme


            baglanti.Open();

            string sorgu1 = "select * from  FiyatListesi WHERE id = @id";
            SqlCommand cmd1 = new SqlCommand(sorgu1, baglanti);
            cmd1.Parameters.AddWithValue("@id", "1");

            SqlDataReader dr;
            dr = cmd1.ExecuteReader();

            while (dr.Read())
            {
                // int id = dr.GetInt32(0); // Birinci sütunu (index 0) integer olarak al
                Marmarafiyat = dr.GetDouble(1);
                EgeFiyat = dr.GetDouble(2);
                AkdenizFiyat = dr.GetDouble(3);
                IcAnadoluFiyat = dr.GetDouble(4);
                KaradenizFiyat = dr.GetDouble(5);
                DoguAnadoluFİyat = dr.GetDouble(6);
                GuneyAnadoluFiyat = dr.GetDouble(7);
            }
            baglanti.Close();
            butonRengiYesil();
            butonListele();

            baglanti.Open();
            SqlCommand command = new SqlCommand("select * from otobusPlakaKayit", baglanti);

            SqlDataReader drn = command.ExecuteReader();
            List<string> mevcutOgeler = otobusplakasec.Items.Cast<string>().ToList();

            while (drn.Read())
            {
                string deger = drn.GetString(1);

                // Eğer öğe zaten varsa ekleme
                if (!mevcutOgeler.Contains(deger))
                {
                    otobusplakasec.Items.Add(deger);
                }
            }

            otoPlaka = string.Join(", ", otobusplakasec.Items.Cast<string>());
            plakaLBL.Text = otoPlaka;
            otobusplakasec.Text = otoPlaka;
            baglanti.Close();


            VeriListele();
            Veritut();

        }
        private void btnKirmiziYap_Click(object sender, EventArgs e)
        {

        }


        public string butonAd;
        public string butonRenk;
        private void button1_Click(object sender, EventArgs e)
        {
            button1.BackColor = Color.Red;
            Kayit kayit = new Kayit();

            kayit.Form2ye_Gidecek_Veri = "1";
            kayit.Form2ye_Gidecek_Veri2 = "Red";
            //ButonRenkKontrol();
            SqlConnection baglan = new SqlConnection(ConnectionString);
            baglan.Open();
            SqlCommand btnRenkveri = new SqlCommand("SELECT TOP 1 id FROM ButonRenkleriDB", baglan);
            // Kayit kayit = new Kayit();
            using (SqlDataReader idReader = btnRenkveri.ExecuteReader())
            {
                while (idReader.Read())
                {
                    
                    kayit.btnidbul = idReader["id"].ToString();
                }
            }
            baglan.Close();
            SqlConnection baglan1 = new SqlConnection(ConnectionString);
            baglan1.Open();
            SqlCommand veri = new SqlCommand("SELECT TOP 1 idbtn FROM MusteriBiletKayit", baglan1);

            using (SqlDataReader idReader = veri.ExecuteReader())
            {
                if (idReader.Read())
                {
                     
                    kayit.idbul = idReader["idbtn"].ToString();
                }

            }

            baglan1.Close();
            kayit.Show();
        }

        private void btnRenklendir(Button selectedButton, Color color)
        {

        }
        public void verigonderKayit()
        {
            Kayit kayit = new Kayit();
            kayit.GelenkalkisYeri = kalkisYeri;
            kayit.GelenvarisYeri = varisYeri;
            kayit.GelenyolTarihi = yolTarihi;
            kayit.Gelensaat = saat;
            kayit.GelentoplamFiyat = toplamFiyat;

        }
        private void button1_Click_1(object sender, EventArgs e)
        {

            button1.BackColor = Color.Red;


            Kayit kayit = new Kayit();
            kayit.plaka = otoPlaka;

            kayit.GelenkalkisYeri = kalkisYeri;
            kayit.GelenvarisYeri = varisYeri;
            kayit.GelenyolTarihi = yolTarihi;
            kayit.Gelensaat = saat;
            kayit.GelentoplamFiyat = toplamFiyat;
            kayit.Form2ye_Gidecek_Veri = "1";
            kayit.Form2ye_Gidecek_Veri2 = "Red";
            SqlConnection baglan = new SqlConnection(ConnectionString);
            baglan.Open();
            SqlCommand btnRenkveri = new SqlCommand("SELECT TOP 1 id FROM ButonRenkleriDB", baglan);
            // Kayit kayit = new Kayit();
            using (SqlDataReader idReader = btnRenkveri.ExecuteReader())
            {
                while (idReader.Read())
                {
                     
                    kayit.btnidbul = idReader["id"].ToString();


                }

            }
            baglan.Close();
            //*********************

            SqlConnection baglan1 = new SqlConnection(ConnectionString);
            baglan1.Open();
            SqlCommand veri = new SqlCommand("SELECT TOP 1 idbtn FROM MusteriBiletKayit", baglan1);

            using (SqlDataReader idReader = veri.ExecuteReader())
            {
                if (idReader.Read())
                {
                    // id değerini string olarak al
                    kayit.idbul = idReader["idbtn"].ToString();


                }

            }

            baglan1.Close();
            kayit.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button2.BackColor = Color.Red;
            Kayit kayit = new Kayit();
            kayit.plaka = otoPlaka;
            kayit.GelenkalkisYeri = kalkisYeri;
            kayit.GelenvarisYeri = varisYeri;
            kayit.GelenyolTarihi = yolTarihi;
            kayit.Gelensaat = saat;
            kayit.GelentoplamFiyat = toplamFiyat;
            kayit.Form2ye_Gidecek_Veri = "2";
            kayit.Form2ye_Gidecek_Veri2 = "Red";
            SqlConnection baglan = new SqlConnection(ConnectionString);
            baglan.Open();
            SqlCommand btnRenkveri = new SqlCommand("SELECT TOP 1 id FROM ButonRenkleriDB", baglan);
            // Kayit kayit = new Kayit();
            using (SqlDataReader idReader = btnRenkveri.ExecuteReader())
            {
                while (idReader.Read())
                {
                     
                    kayit.btnidbul = idReader["id"].ToString();


                }

            }
            baglan.Close();
            SqlConnection baglan1 = new SqlConnection(ConnectionString);
            baglan1.Open();
            SqlCommand veri = new SqlCommand("SELECT TOP 1 idbtn FROM MusteriBiletKayit", baglan1);

            using (SqlDataReader idReader = veri.ExecuteReader())
            {
                if (idReader.Read())
                {
                    // id değerini string olarak al
                    kayit.idbul = idReader["idbtn"].ToString();


                }

            }

            baglan1.Close();
            kayit.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button3.BackColor = Color.Red;
            Kayit kayit = new Kayit();
            kayit.plaka = otoPlaka;
            kayit.GelenkalkisYeri = kalkisYeri;
            kayit.GelenvarisYeri = varisYeri;
            kayit.GelenyolTarihi = yolTarihi;
            kayit.Gelensaat = saat;
            kayit.GelentoplamFiyat = toplamFiyat;
            kayit.Form2ye_Gidecek_Veri = "3";
            kayit.Form2ye_Gidecek_Veri2 = "Red";


            SqlConnection baglan = new SqlConnection(ConnectionString);
            baglan.Open();
            SqlCommand btnRenkveri = new SqlCommand("SELECT TOP 1 id FROM ButonRenkleriDB", baglan);
            // Kayit kayit = new Kayit();
            using (SqlDataReader idReader = btnRenkveri.ExecuteReader())
            {
                while (idReader.Read())
                {
                     
                    kayit.btnidbul = idReader["id"].ToString();


                }

            }
            baglan.Close();
            SqlConnection baglan1 = new SqlConnection(ConnectionString);
            baglan1.Open();
            SqlCommand veri = new SqlCommand("SELECT TOP 1 idbtn FROM MusteriBiletKayit", baglan1);

            using (SqlDataReader idReader = veri.ExecuteReader())
            {
                if (idReader.Read())
                {
                     
                    kayit.idbul = idReader["idbtn"].ToString();


                }

            }

            baglan1.Close();
            kayit.Show();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            button4.BackColor = Color.Red;

            Kayit kayit = new Kayit();
            kayit.plaka = otoPlaka;
            kayit.GelenkalkisYeri = kalkisYeri;
            kayit.GelenvarisYeri = varisYeri;
            kayit.GelenyolTarihi = yolTarihi;
            kayit.Gelensaat = saat;
            kayit.GelentoplamFiyat = toplamFiyat;
            kayit.Form2ye_Gidecek_Veri = "4";
            kayit.Form2ye_Gidecek_Veri2 = "Red";
            SqlConnection baglan = new SqlConnection(ConnectionString);
            baglan.Open();
            SqlCommand btnRenkveri = new SqlCommand("SELECT TOP 1 id FROM ButonRenkleriDB", baglan);
            using (SqlDataReader idReader = btnRenkveri.ExecuteReader())
            {
                while (idReader.Read())
                {
                     
                    kayit.btnidbul = idReader["id"].ToString();


                }

            }
            baglan.Close();
          
            SqlConnection baglan1 = new SqlConnection(ConnectionString);
            baglan1.Open();
            SqlCommand veri = new SqlCommand("SELECT TOP 1 idbtn FROM MusteriBiletKayit", baglan1);

            using (SqlDataReader idReader = veri.ExecuteReader())
            {
                if (idReader.Read())
                {
                     
                    kayit.idbul = idReader["idbtn"].ToString();


                }

            }

            baglan1.Close();
            kayit.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            button5.BackColor = Color.Red;

            Kayit kayit = new Kayit();
            kayit.plaka = otoPlaka;
            kayit.GelenkalkisYeri = kalkisYeri;
            kayit.GelenvarisYeri = varisYeri;
            kayit.GelenyolTarihi = yolTarihi;
            kayit.Gelensaat = saat;
            kayit.GelentoplamFiyat = toplamFiyat;
            kayit.Form2ye_Gidecek_Veri = "5";
            kayit.Form2ye_Gidecek_Veri2 = "Red";
            SqlConnection baglan = new SqlConnection(ConnectionString);
            baglan.Open();
            SqlCommand btnRenkveri = new SqlCommand("SELECT TOP 1 id FROM ButonRenkleriDB", baglan);
            // Kayit kayit = new Kayit();
            using (SqlDataReader idReader = btnRenkveri.ExecuteReader())
            {
                while (idReader.Read())
                {
                     
                    kayit.btnidbul = idReader["id"].ToString();


                }

            }
            baglan.Close();

            SqlConnection baglan1 = new SqlConnection(ConnectionString);
            baglan1.Open();
            SqlCommand veri = new SqlCommand("SELECT TOP 1 idbtn FROM MusteriBiletKayit", baglan1);

            using (SqlDataReader idReader = veri.ExecuteReader())
            {
                if (idReader.Read())
                {
                     
                    kayit.idbul = idReader["idbtn"].ToString();


                }

            }

            baglan1.Close();
            kayit.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            button6.BackColor = Color.Red;
            Kayit kayit = new Kayit();
            kayit.plaka = otoPlaka;
            kayit.GelenkalkisYeri = kalkisYeri;
            kayit.GelenvarisYeri = varisYeri;
            kayit.GelenyolTarihi = yolTarihi;
            kayit.Gelensaat = saat;
            kayit.GelentoplamFiyat = toplamFiyat;
            kayit.Form2ye_Gidecek_Veri = "6";
            kayit.Form2ye_Gidecek_Veri2 = "Red";
            SqlConnection baglan = new SqlConnection(ConnectionString);
            baglan.Open();
            SqlCommand btnRenkveri = new SqlCommand("SELECT TOP 1 id FROM ButonRenkleriDB", baglan);
            // Kayit kayit = new Kayit();
            using (SqlDataReader idReader = btnRenkveri.ExecuteReader())
            {
                while (idReader.Read())
                {
                     
                    kayit.btnidbul = idReader["id"].ToString();


                }

            }
            baglan.Close();
            //*********************

            SqlConnection baglan1 = new SqlConnection(ConnectionString);
            baglan1.Open();
            SqlCommand veri = new SqlCommand("SELECT TOP 1 idbtn FROM MusteriBiletKayit", baglan1);

            using (SqlDataReader idReader = veri.ExecuteReader())
            {
                if (idReader.Read())
                {
                     
                    kayit.idbul = idReader["idbtn"].ToString();


                }

            }

            baglan1.Close();
            kayit.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            button7.BackColor = Color.Red;
            Kayit kayit = new Kayit();
            kayit.plaka = otoPlaka;
            kayit.GelenkalkisYeri = kalkisYeri;
            kayit.GelenvarisYeri = varisYeri;
            kayit.GelenyolTarihi = yolTarihi;
            kayit.Gelensaat = saat;
            kayit.GelentoplamFiyat = toplamFiyat;
            kayit.Form2ye_Gidecek_Veri = "7";
            kayit.Form2ye_Gidecek_Veri2 = "Red";
            SqlConnection baglan = new SqlConnection(ConnectionString);
            baglan.Open();
            SqlCommand btnRenkveri = new SqlCommand("SELECT TOP 1 id FROM ButonRenkleriDB", baglan);
            // Kayit kayit = new Kayit();
            using (SqlDataReader idReader = btnRenkveri.ExecuteReader())
            {
                while (idReader.Read())
                {
                     
                    kayit.btnidbul = idReader["id"].ToString();


                }

            }
            baglan.Close();
            //*********************

            SqlConnection baglan1 = new SqlConnection(ConnectionString);
            baglan1.Open();
            SqlCommand veri = new SqlCommand("SELECT TOP 1 idbtn FROM MusteriBiletKayit", baglan1);

            using (SqlDataReader idReader = veri.ExecuteReader())
            {
                if (idReader.Read())
                {
                     
                    kayit.idbul = idReader["idbtn"].ToString();


                }

            }

            baglan1.Close();
            kayit.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            button8.BackColor = Color.Red;

            Kayit kayit = new Kayit();
            kayit.plaka = otoPlaka;
            kayit.GelenkalkisYeri = kalkisYeri;
            kayit.GelenvarisYeri = varisYeri;
            kayit.GelenyolTarihi = yolTarihi;
            kayit.Gelensaat = saat;
            kayit.GelentoplamFiyat = toplamFiyat;
            kayit.Form2ye_Gidecek_Veri = "8";
            kayit.Form2ye_Gidecek_Veri2 = "Red";
            SqlConnection baglan = new SqlConnection(ConnectionString);
            baglan.Open();
            SqlCommand btnRenkveri = new SqlCommand("SELECT TOP 1 id FROM ButonRenkleriDB", baglan);
            // Kayit kayit = new Kayit();
            using (SqlDataReader idReader = btnRenkveri.ExecuteReader())
            {
                while (idReader.Read())
                {
                     
                    kayit.btnidbul = idReader["id"].ToString();


                }

            }
            baglan.Close();
            SqlConnection baglan1 = new SqlConnection(ConnectionString);
            baglan1.Open();
            SqlCommand veri = new SqlCommand("SELECT TOP 1 idbtn FROM MusteriBiletKayit", baglan1);

            using (SqlDataReader idReader = veri.ExecuteReader())
            {
                if (idReader.Read())
                {
                     
                    kayit.idbul = idReader["idbtn"].ToString();


                }

            }

            baglan1.Close();
            kayit.Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            button9.BackColor = Color.Red;
            Kayit kayit = new Kayit();
            kayit.plaka = otoPlaka;
            kayit.GelenkalkisYeri = kalkisYeri;
            kayit.GelenvarisYeri = varisYeri;
            kayit.GelenyolTarihi = yolTarihi;
            kayit.Gelensaat = saat;
            kayit.GelentoplamFiyat = toplamFiyat;
            kayit.Form2ye_Gidecek_Veri = "9";
            kayit.Form2ye_Gidecek_Veri2 = "Red";
            SqlConnection baglan = new SqlConnection(ConnectionString);
            baglan.Open();
            SqlCommand btnRenkveri = new SqlCommand("SELECT TOP 1 id FROM ButonRenkleriDB", baglan);
            // Kayit kayit = new Kayit();
            using (SqlDataReader idReader = btnRenkveri.ExecuteReader())
            {
                while (idReader.Read())
                {
                     
                    kayit.btnidbul = idReader["id"].ToString();


                }

            }
            baglan.Close();

            SqlConnection baglan1 = new SqlConnection(ConnectionString);
            baglan1.Open();
            SqlCommand veri = new SqlCommand("SELECT TOP 1 idbtn FROM MusteriBiletKayit", baglan1);

            using (SqlDataReader idReader = veri.ExecuteReader())
            {
                if (idReader.Read())
                {
                    kayit.idbul = idReader["idbtn"].ToString();


                }

            }

            baglan1.Close();
            kayit.Show();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            button10.BackColor = Color.Red;
            Kayit kayit = new Kayit();
            kayit.plaka = otoPlaka;
            kayit.GelenkalkisYeri = kalkisYeri;
            kayit.GelenvarisYeri = varisYeri;
            kayit.GelenyolTarihi = yolTarihi;
            kayit.Gelensaat = saat;
            kayit.GelentoplamFiyat = toplamFiyat;
            kayit.Form2ye_Gidecek_Veri = "10";
            kayit.Form2ye_Gidecek_Veri2 = "Red";
            SqlConnection baglan = new SqlConnection(ConnectionString);
            baglan.Open();
            SqlCommand btnRenkveri = new SqlCommand("SELECT TOP 1 id FROM ButonRenkleriDB", baglan);
            // Kayit kayit = new Kayit();
            using (SqlDataReader idReader = btnRenkveri.ExecuteReader())
            {
                while (idReader.Read())
                {
                    // id değerini string olarak al
                    kayit.btnidbul = idReader["id"].ToString();


                }

            }
            baglan.Close();
            //*********************

            SqlConnection baglan1 = new SqlConnection(ConnectionString);
            baglan1.Open();
            SqlCommand veri = new SqlCommand("SELECT TOP 1 idbtn FROM MusteriBiletKayit", baglan1);

            using (SqlDataReader idReader = veri.ExecuteReader())
            {
                if (idReader.Read())
                {
                    // id değerini string olarak al
                    kayit.idbul = idReader["idbtn"].ToString();


                }

            }

            baglan1.Close();
            kayit.Show();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            button11.BackColor = Color.Red;
            Kayit kayit = new Kayit();
            kayit.plaka = otoPlaka;
            kayit.GelenkalkisYeri = kalkisYeri;
            kayit.GelenvarisYeri = varisYeri;
            kayit.GelenyolTarihi = yolTarihi;
            kayit.Gelensaat = saat;
            kayit.GelentoplamFiyat = toplamFiyat;
            kayit.Form2ye_Gidecek_Veri = "11";
            kayit.Form2ye_Gidecek_Veri2 = "Red";
            SqlConnection baglan = new SqlConnection(ConnectionString);
            baglan.Open();
            SqlCommand btnRenkveri = new SqlCommand("SELECT TOP 1 id FROM ButonRenkleriDB", baglan);
            // Kayit kayit = new Kayit();
            using (SqlDataReader idReader = btnRenkveri.ExecuteReader())
            {
                while (idReader.Read())
                {
                     
                    kayit.btnidbul = idReader["id"].ToString();


                }

            }
            baglan.Close();
            //*********************

            SqlConnection baglan1 = new SqlConnection(ConnectionString);
            baglan1.Open();
            SqlCommand veri = new SqlCommand("SELECT TOP 1 idbtn FROM MusteriBiletKayit", baglan1);

            using (SqlDataReader idReader = veri.ExecuteReader())
            {
                if (idReader.Read())
                {
                     
                    kayit.idbul = idReader["idbtn"].ToString();


                }

            }

            baglan1.Close();
            kayit.Show();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            button12.BackColor = Color.Red;
            Kayit kayit = new Kayit();
            kayit.plaka = otoPlaka;
            kayit.GelenkalkisYeri = kalkisYeri;
            kayit.GelenvarisYeri = varisYeri;
            kayit.GelenyolTarihi = yolTarihi;
            kayit.Gelensaat = saat;
            kayit.GelentoplamFiyat = toplamFiyat;
            kayit.Form2ye_Gidecek_Veri = "12";
            kayit.Form2ye_Gidecek_Veri2 = "Red";
            SqlConnection baglan = new SqlConnection(ConnectionString);
            baglan.Open();
            SqlCommand btnRenkveri = new SqlCommand("SELECT TOP 1 id FROM ButonRenkleriDB", baglan);
            // Kayit kayit = new Kayit();
            using (SqlDataReader idReader = btnRenkveri.ExecuteReader())
            {
                while (idReader.Read())
                {
                     
                    kayit.btnidbul = idReader["id"].ToString();


                }

            }
            baglan.Close();
            //*********************

            SqlConnection baglan1 = new SqlConnection(ConnectionString);
            baglan1.Open();
            SqlCommand veri = new SqlCommand("SELECT TOP 1 idbtn FROM MusteriBiletKayit", baglan1);

            using (SqlDataReader idReader = veri.ExecuteReader())
            {
                if (idReader.Read())
                {
                     
                    kayit.idbul = idReader["idbtn"].ToString();


                }

            }

            baglan1.Close();
            kayit.Show();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            button13.BackColor = Color.Red;
            Kayit kayit = new Kayit();
            kayit.plaka = otoPlaka;
            kayit.GelenkalkisYeri = kalkisYeri;
            kayit.GelenvarisYeri = varisYeri;
            kayit.GelenyolTarihi = yolTarihi;
            kayit.Gelensaat = saat;
            kayit.GelentoplamFiyat = toplamFiyat;
            kayit.Form2ye_Gidecek_Veri = "13";
            kayit.Form2ye_Gidecek_Veri2 = "Red";
            SqlConnection baglan = new SqlConnection(ConnectionString);
            baglan.Open();
            SqlCommand btnRenkveri = new SqlCommand("SELECT TOP 1 id FROM ButonRenkleriDB", baglan);
            // Kayit kayit = new Kayit();
            using (SqlDataReader idReader = btnRenkveri.ExecuteReader())
            {
                while (idReader.Read())
                {
                     
                    kayit.btnidbul = idReader["id"].ToString();


                }

            }
            baglan.Close();

            SqlConnection baglan1 = new SqlConnection(ConnectionString);
            baglan1.Open();
            SqlCommand veri = new SqlCommand("SELECT TOP 1 idbtn FROM MusteriBiletKayit", baglan1);

            using (SqlDataReader idReader = veri.ExecuteReader())
            {
                if (idReader.Read())
                {
                     
                    kayit.idbul = idReader["idbtn"].ToString();


                }

            }

            baglan1.Close();
            kayit.Show();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            button14.BackColor = Color.Red;

            Kayit kayit = new Kayit();
            kayit.plaka = otoPlaka;
            kayit.GelenkalkisYeri = kalkisYeri;
            kayit.GelenvarisYeri = varisYeri;
            kayit.GelenyolTarihi = yolTarihi;
            kayit.Gelensaat = saat;
            kayit.GelentoplamFiyat = toplamFiyat;
            kayit.Form2ye_Gidecek_Veri = "14";
            kayit.Form2ye_Gidecek_Veri2 = "Red";
            SqlConnection baglan = new SqlConnection(ConnectionString);
            baglan.Open();
            SqlCommand btnRenkveri = new SqlCommand("SELECT TOP 1 id FROM ButonRenkleriDB", baglan);
            // Kayit kayit = new Kayit();
            using (SqlDataReader idReader = btnRenkveri.ExecuteReader())
            {
                while (idReader.Read())
                {
                     
                    kayit.btnidbul = idReader["id"].ToString();


                }

            }
            baglan.Close();
            SqlConnection baglan1 = new SqlConnection(ConnectionString);
            baglan1.Open();
            SqlCommand veri = new SqlCommand("SELECT TOP 1 idbtn FROM MusteriBiletKayit", baglan1);

            using (SqlDataReader idReader = veri.ExecuteReader())
            {
                if (idReader.Read())
                {
                     
                    kayit.idbul = idReader["idbtn"].ToString();


                }

            }

            baglan1.Close();
            kayit.Show();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            button15.BackColor = Color.Red;
            Kayit kayit = new Kayit();
            kayit.plaka = otoPlaka;
            kayit.GelenkalkisYeri = kalkisYeri;
            kayit.GelenvarisYeri = varisYeri;
            kayit.GelenyolTarihi = yolTarihi;
            kayit.Gelensaat = saat;
            kayit.GelentoplamFiyat = toplamFiyat;
            kayit.Form2ye_Gidecek_Veri = "15";
            kayit.Form2ye_Gidecek_Veri2 = "Red";
            SqlConnection baglan = new SqlConnection(ConnectionString);
            baglan.Open();
            SqlCommand btnRenkveri = new SqlCommand("SELECT TOP 1 id FROM ButonRenkleriDB", baglan);
            // Kayit kayit = new Kayit();
            using (SqlDataReader idReader = btnRenkveri.ExecuteReader())
            {
                while (idReader.Read())
                {
                     
                    kayit.btnidbul = idReader["id"].ToString();


                }

            }
            baglan.Close();
            //*********************

            SqlConnection baglan1 = new SqlConnection(ConnectionString);
            baglan1.Open();
            SqlCommand veri = new SqlCommand("SELECT TOP 1 idbtn FROM MusteriBiletKayit", baglan1);

            using (SqlDataReader idReader = veri.ExecuteReader())
            {
                if (idReader.Read())
                {
                     
                    kayit.idbul = idReader["idbtn"].ToString();


                }

            }

            baglan1.Close();
            kayit.Show();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            button16.BackColor = Color.Red;
            Kayit kayit = new Kayit();
            kayit.plaka = otoPlaka;
            kayit.GelenkalkisYeri = kalkisYeri;
            kayit.GelenvarisYeri = varisYeri;
            kayit.GelenyolTarihi = yolTarihi;
            kayit.Gelensaat = saat;
            kayit.GelentoplamFiyat = toplamFiyat;
            kayit.Form2ye_Gidecek_Veri = "16";
            kayit.Form2ye_Gidecek_Veri2 = "Red";
            SqlConnection baglan = new SqlConnection(ConnectionString);
            baglan.Open();
            SqlCommand btnRenkveri = new SqlCommand("SELECT TOP 1 id FROM ButonRenkleriDB", baglan);
            // Kayit kayit = new Kayit();
            using (SqlDataReader idReader = btnRenkveri.ExecuteReader())
            {
                while (idReader.Read())
                {
                     
                    kayit.btnidbul = idReader["id"].ToString();


                }

            }
            baglan.Close();
            //*********************

            SqlConnection baglan1 = new SqlConnection(ConnectionString);
            baglan1.Open();
            SqlCommand veri = new SqlCommand("SELECT TOP 1 idbtn FROM MusteriBiletKayit", baglan1);

            using (SqlDataReader idReader = veri.ExecuteReader())
            {
                if (idReader.Read())
                {
                     
                    kayit.idbul = idReader["idbtn"].ToString();


                }

            }

            baglan1.Close();
            kayit.Show();
        }

        private void button17_Click(object sender, EventArgs e)
        {
            button17.BackColor = Color.Red;
            Kayit kayit = new Kayit();
            kayit.plaka = otoPlaka;
            kayit.GelenkalkisYeri = kalkisYeri;
            kayit.GelenvarisYeri = varisYeri;
            kayit.GelenyolTarihi = yolTarihi;
            kayit.Gelensaat = saat;
            kayit.GelentoplamFiyat = toplamFiyat;
            kayit.Form2ye_Gidecek_Veri = "17";
            kayit.Form2ye_Gidecek_Veri2 = "Red";
            SqlConnection baglan = new SqlConnection(ConnectionString);
            baglan.Open();
            SqlCommand btnRenkveri = new SqlCommand("SELECT TOP 1 id FROM ButonRenkleriDB", baglan);
            // Kayit kayit = new Kayit();
            using (SqlDataReader idReader = btnRenkveri.ExecuteReader())
            {
                while (idReader.Read())
                {
                     
                    kayit.btnidbul = idReader["id"].ToString();


                }

            }
            baglan.Close();
            //*********************

            SqlConnection baglan1 = new SqlConnection(ConnectionString);
            baglan1.Open();
            SqlCommand veri = new SqlCommand("SELECT TOP 1 idbtn FROM MusteriBiletKayit", baglan1);

            using (SqlDataReader idReader = veri.ExecuteReader())
            {
                if (idReader.Read())
                {
                     
                    kayit.idbul = idReader["idbtn"].ToString();


                }

            }

            baglan1.Close();
            kayit.Show();
        }

        private void button18_Click(object sender, EventArgs e)
        {
            button18.BackColor = Color.Red;
            Kayit kayit = new Kayit();
            kayit.plaka = otoPlaka;
            kayit.GelenkalkisYeri = kalkisYeri;
            kayit.GelenvarisYeri = varisYeri;
            kayit.GelenyolTarihi = yolTarihi;
            kayit.Gelensaat = saat;
            kayit.GelentoplamFiyat = toplamFiyat;
            kayit.Form2ye_Gidecek_Veri = "18";
            kayit.Form2ye_Gidecek_Veri2 = "Red";
            SqlConnection baglan = new SqlConnection(ConnectionString);
            baglan.Open();
            SqlCommand btnRenkveri = new SqlCommand("SELECT TOP 1 id FROM ButonRenkleriDB", baglan);
            // Kayit kayit = new Kayit();
            using (SqlDataReader idReader = btnRenkveri.ExecuteReader())
            {
                while (idReader.Read())
                {
                     
                    kayit.btnidbul = idReader["id"].ToString();


                }

            }
            baglan.Close();

            SqlConnection baglan1 = new SqlConnection(ConnectionString);
            baglan1.Open();
            SqlCommand veri = new SqlCommand("SELECT TOP 1 idbtn FROM MusteriBiletKayit", baglan1);

            using (SqlDataReader idReader = veri.ExecuteReader())
            {
                if (idReader.Read())
                {
                     
                    kayit.idbul = idReader["idbtn"].ToString();


                }

            }

            baglan1.Close();
            kayit.Show();
        }

        private void button19_Click(object sender, EventArgs e)
        {

            button19.BackColor = Color.Red;
            Kayit kayit = new Kayit();
            kayit.plaka = otoPlaka;
            kayit.GelenkalkisYeri = kalkisYeri;
            kayit.GelenvarisYeri = varisYeri;
            kayit.GelenyolTarihi = yolTarihi;
            kayit.Gelensaat = saat;
            kayit.GelentoplamFiyat = toplamFiyat;
            kayit.Form2ye_Gidecek_Veri = "19";
            kayit.Form2ye_Gidecek_Veri2 = "Red";
            SqlConnection baglan = new SqlConnection(ConnectionString);
            baglan.Open();
            SqlCommand btnRenkveri = new SqlCommand("SELECT TOP 1 id FROM ButonRenkleriDB", baglan);
            // Kayit kayit = new Kayit();
            using (SqlDataReader idReader = btnRenkveri.ExecuteReader())
            {
                while (idReader.Read())
                {
                     
                    kayit.btnidbul = idReader["id"].ToString();


                }

            }
            baglan.Close();
            //*********************

            SqlConnection baglan1 = new SqlConnection(ConnectionString);
            baglan1.Open();
            SqlCommand veri = new SqlCommand("SELECT TOP 1 idbtn FROM MusteriBiletKayit", baglan1);

            using (SqlDataReader idReader = veri.ExecuteReader())
            {
                if (idReader.Read())
                {
                     
                    kayit.idbul = idReader["idbtn"].ToString();


                }

            }

            baglan1.Close();
            kayit.Show();
        }

        private void button20_Click(object sender, EventArgs e)
        {
            button20.BackColor = Color.Red;
            Kayit kayit = new Kayit();
            kayit.plaka = otoPlaka;
            kayit.GelenkalkisYeri = kalkisYeri;
            kayit.GelenvarisYeri = varisYeri;
            kayit.GelenyolTarihi = yolTarihi;
            kayit.Gelensaat = saat;
            kayit.GelentoplamFiyat = toplamFiyat;
            kayit.Form2ye_Gidecek_Veri = "20";
            kayit.Form2ye_Gidecek_Veri2 = "Red";
            SqlConnection baglan = new SqlConnection(ConnectionString);
            baglan.Open();
            SqlCommand btnRenkveri = new SqlCommand("SELECT TOP 1 id FROM ButonRenkleriDB", baglan);
            // Kayit kayit = new Kayit();
            using (SqlDataReader idReader = btnRenkveri.ExecuteReader())
            {
                while (idReader.Read())
                {
                     
                    kayit.btnidbul = idReader["id"].ToString();


                }

            }
            baglan.Close();
            //*********************

            SqlConnection baglan1 = new SqlConnection(ConnectionString);
            baglan1.Open();
            SqlCommand veri = new SqlCommand("SELECT TOP 1 idbtn FROM MusteriBiletKayit", baglan1);

            using (SqlDataReader idReader = veri.ExecuteReader())
            {
                if (idReader.Read())
                {
                     
                    kayit.idbul = idReader["idbtn"].ToString();


                }

            }

            baglan1.Close();
            kayit.Show();
        }

        private void button21_Click(object sender, EventArgs e)
        {
            button21.BackColor = Color.Red;
            Kayit kayit = new Kayit();
            kayit.plaka = otoPlaka;
            kayit.Form2ye_Gidecek_Veri = "21";
            kayit.GelenkalkisYeri = kalkisYeri;
            kayit.GelenvarisYeri = varisYeri;
            kayit.GelenyolTarihi = yolTarihi;
            kayit.Gelensaat = saat;
            kayit.GelentoplamFiyat = toplamFiyat;
            kayit.Form2ye_Gidecek_Veri2 = "Red";
            SqlConnection baglan = new SqlConnection(ConnectionString);
            baglan.Open();
            SqlCommand btnRenkveri = new SqlCommand("SELECT TOP 1 id FROM ButonRenkleriDB", baglan);
            // Kayit kayit = new Kayit();
            using (SqlDataReader idReader = btnRenkveri.ExecuteReader())
            {
                while (idReader.Read())
                {
                     
                    kayit.btnidbul = idReader["id"].ToString();


                }

            }
            baglan.Close();
            //*********************

            SqlConnection baglan1 = new SqlConnection(ConnectionString);
            baglan1.Open();
            SqlCommand veri = new SqlCommand("SELECT TOP 1 idbtn FROM MusteriBiletKayit", baglan1);

            using (SqlDataReader idReader = veri.ExecuteReader())
            {
                if (idReader.Read())
                {
                     
                    kayit.idbul = idReader["idbtn"].ToString();


                }

            }

            baglan1.Close();
            kayit.Show();
        }

        private void button22_Click(object sender, EventArgs e)
        {
            button22.BackColor = Color.Red;
            Kayit kayit = new Kayit();
            kayit.plaka = otoPlaka;
            kayit.GelenkalkisYeri = kalkisYeri;
            kayit.GelenvarisYeri = varisYeri;
            kayit.GelenyolTarihi = yolTarihi;
            kayit.Gelensaat = saat;
            kayit.GelentoplamFiyat = toplamFiyat;
            kayit.Form2ye_Gidecek_Veri = "22";
            kayit.Form2ye_Gidecek_Veri2 = "Red";
            SqlConnection baglan = new SqlConnection(ConnectionString);
            baglan.Open();
            SqlCommand btnRenkveri = new SqlCommand("SELECT TOP 1 id FROM ButonRenkleriDB", baglan);
            // Kayit kayit = new Kayit();
            using (SqlDataReader idReader = btnRenkveri.ExecuteReader())
            {
                while (idReader.Read())
                {
                     
                    kayit.btnidbul = idReader["id"].ToString();


                }

            }
            baglan.Close();
            //*********************

            SqlConnection baglan1 = new SqlConnection(ConnectionString);
            baglan1.Open();
            SqlCommand veri = new SqlCommand("SELECT TOP 1 idbtn FROM MusteriBiletKayit", baglan1);

            using (SqlDataReader idReader = veri.ExecuteReader())
            {
                if (idReader.Read())
                {
                     
                    kayit.idbul = idReader["idbtn"].ToString();


                }

            }

            baglan1.Close();
            kayit.Show();
        }

        private void button23_Click(object sender, EventArgs e)
        {
            button23.BackColor = Color.Red;
            Kayit kayit = new Kayit();
            kayit.plaka = otoPlaka;
            kayit.GelenkalkisYeri = kalkisYeri;
            kayit.GelenvarisYeri = varisYeri;
            kayit.GelenyolTarihi = yolTarihi;
            kayit.Gelensaat = saat;
            kayit.GelentoplamFiyat = toplamFiyat;
            kayit.Form2ye_Gidecek_Veri = "23";
            kayit.Form2ye_Gidecek_Veri2 = "Red";
            SqlConnection baglan = new SqlConnection(ConnectionString);
            baglan.Open();
            SqlCommand btnRenkveri = new SqlCommand("SELECT TOP 1 id FROM ButonRenkleriDB", baglan);
            // Kayit kayit = new Kayit();
            using (SqlDataReader idReader = btnRenkveri.ExecuteReader())
            {
                while (idReader.Read())
                {
                     
                    kayit.btnidbul = idReader["id"].ToString();


                }

            }
            baglan.Close();
            //*********************

            SqlConnection baglan1 = new SqlConnection(ConnectionString);
            baglan1.Open();
            SqlCommand veri = new SqlCommand("SELECT TOP 1 idbtn FROM MusteriBiletKayit", baglan1);

            using (SqlDataReader idReader = veri.ExecuteReader())
            {
                if (idReader.Read())
                {
                     
                    kayit.idbul = idReader["idbtn"].ToString();


                }

            }

            baglan1.Close();
            kayit.Show();
        }

        private void button24_Click(object sender, EventArgs e)
        {
            button24.BackColor = Color.Red;
            Kayit kayit = new Kayit();
            kayit.plaka = otoPlaka;
            kayit.GelenkalkisYeri = kalkisYeri;
            kayit.GelenvarisYeri = varisYeri;
            kayit.GelenyolTarihi = yolTarihi;
            kayit.Gelensaat = saat;
            kayit.GelentoplamFiyat = toplamFiyat;
            kayit.Form2ye_Gidecek_Veri = "24";
            kayit.Form2ye_Gidecek_Veri2 = "Red";
            SqlConnection baglan = new SqlConnection(ConnectionString);
            baglan.Open();
            SqlCommand btnRenkveri = new SqlCommand("SELECT TOP 1 id FROM ButonRenkleriDB", baglan);
            // Kayit kayit = new Kayit();
            using (SqlDataReader idReader = btnRenkveri.ExecuteReader())
            {
                while (idReader.Read())
                {
                     
                    kayit.btnidbul = idReader["id"].ToString();


                }

            }
            baglan.Close();
            //*********************

            SqlConnection baglan1 = new SqlConnection(ConnectionString);
            baglan1.Open();
            SqlCommand veri = new SqlCommand("SELECT TOP 1 idbtn FROM MusteriBiletKayit", baglan1);

            using (SqlDataReader idReader = veri.ExecuteReader())
            {
                if (idReader.Read())
                {
                     
                    kayit.idbul = idReader["idbtn"].ToString();


                }

            }

            baglan1.Close();
            kayit.Show();
        }

        private void button25_Click(object sender, EventArgs e)
        {
            button25.BackColor = Color.Red;
            Kayit kayit = new Kayit();
            kayit.plaka = otoPlaka;
            kayit.GelenkalkisYeri = kalkisYeri;
            kayit.GelenvarisYeri = varisYeri;
            kayit.GelenyolTarihi = yolTarihi;
            kayit.Gelensaat = saat;
            kayit.GelentoplamFiyat = toplamFiyat;
            kayit.Form2ye_Gidecek_Veri = "25";
            kayit.Form2ye_Gidecek_Veri2 = "Red";
            SqlConnection baglan = new SqlConnection(ConnectionString);
            baglan.Open();
            SqlCommand btnRenkveri = new SqlCommand("SELECT TOP 1 id FROM ButonRenkleriDB", baglan);
            // Kayit kayit = new Kayit();
            using (SqlDataReader idReader = btnRenkveri.ExecuteReader())
            {
                while (idReader.Read())
                {
                     
                    kayit.btnidbul = idReader["id"].ToString();


                }

            }
            baglan.Close();
            SqlConnection baglan1 = new SqlConnection(ConnectionString);
            baglan1.Open();
            SqlCommand veri = new SqlCommand("SELECT TOP 1 idbtn FROM MusteriBiletKayit", baglan1);

            using (SqlDataReader idReader = veri.ExecuteReader())
            {
                if (idReader.Read())
                {
                     
                    kayit.idbul = idReader["idbtn"].ToString();


                }

            }

            baglan1.Close();
            kayit.Show();
        }

        private void button26_Click(object sender, EventArgs e)
        {
            button26.BackColor = Color.Red;
            Kayit kayit = new Kayit();
            kayit.plaka = otoPlaka;
            kayit.GelenkalkisYeri = kalkisYeri;
            kayit.GelenvarisYeri = varisYeri;
            kayit.GelenyolTarihi = yolTarihi;
            kayit.Gelensaat = saat;
            kayit.GelentoplamFiyat = toplamFiyat;
            kayit.Form2ye_Gidecek_Veri = "26";
            kayit.Form2ye_Gidecek_Veri2 = "Red";
            SqlConnection baglan = new SqlConnection(ConnectionString);
            baglan.Open();
            SqlCommand btnRenkveri = new SqlCommand("SELECT TOP 1 id FROM ButonRenkleriDB", baglan);
            // Kayit kayit = new Kayit();
            using (SqlDataReader idReader = btnRenkveri.ExecuteReader())
            {
                while (idReader.Read())
                {
                     
                    kayit.btnidbul = idReader["id"].ToString();


                }

            }
            baglan.Close();

            SqlConnection baglan1 = new SqlConnection(ConnectionString);
            baglan1.Open();
            SqlCommand veri = new SqlCommand("SELECT TOP 1 idbtn FROM MusteriBiletKayit", baglan1);

            using (SqlDataReader idReader = veri.ExecuteReader())
            {
                if (idReader.Read())
                {
                     
                    kayit.idbul = idReader["idbtn"].ToString();


                }

            }

            baglan1.Close();
            kayit.Show();
        }

        private void button27_Click(object sender, EventArgs e)
        {
            button27.BackColor = Color.Red;
            Kayit kayit = new Kayit();
            kayit.plaka = otoPlaka;
            kayit.Form2ye_Gidecek_Veri = "27";
            kayit.GelenkalkisYeri = kalkisYeri;
            kayit.GelenvarisYeri = varisYeri;
            kayit.GelenyolTarihi = yolTarihi;
            kayit.Gelensaat = saat;
            kayit.GelentoplamFiyat = toplamFiyat;
            kayit.Form2ye_Gidecek_Veri2 = "Red";
            SqlConnection baglan = new SqlConnection(ConnectionString);
            baglan.Open();
            SqlCommand btnRenkveri = new SqlCommand("SELECT TOP 1 id FROM ButonRenkleriDB", baglan);
            // Kayit kayit = new Kayit();
            using (SqlDataReader idReader = btnRenkveri.ExecuteReader())
            {
                while (idReader.Read())
                {
                     
                    kayit.btnidbul = idReader["id"].ToString();


                }

            }
            baglan.Close();
            //*********************

            SqlConnection baglan1 = new SqlConnection(ConnectionString);
            baglan1.Open();
            SqlCommand veri = new SqlCommand("SELECT TOP 1 idbtn FROM MusteriBiletKayit", baglan1);

            using (SqlDataReader idReader = veri.ExecuteReader())
            {
                if (idReader.Read())
                {
                     
                    kayit.idbul = idReader["idbtn"].ToString();


                }

            }

            baglan1.Close();
            kayit.Show();
        }

        private void button28_Click(object sender, EventArgs e)
        {
            button28.BackColor = Color.Red;
            Kayit kayit = new Kayit();
            kayit.plaka = otoPlaka;
            kayit.Form2ye_Gidecek_Veri = "28";
            kayit.GelenkalkisYeri = kalkisYeri;
            kayit.GelenvarisYeri = varisYeri;
            kayit.GelenyolTarihi = yolTarihi;
            kayit.Gelensaat = saat;
            kayit.GelentoplamFiyat = toplamFiyat;
            kayit.Form2ye_Gidecek_Veri2 = "Red";
            SqlConnection baglan = new SqlConnection(ConnectionString);
            baglan.Open();
            SqlCommand btnRenkveri = new SqlCommand("SELECT TOP 1 id FROM ButonRenkleriDB", baglan);
            // Kayit kayit = new Kayit();
            using (SqlDataReader idReader = btnRenkveri.ExecuteReader())
            {
                while (idReader.Read())
                {
                     
                    kayit.btnidbul = idReader["id"].ToString();


                }

            }
            baglan.Close();
            //*********************

            SqlConnection baglan1 = new SqlConnection(ConnectionString);
            baglan1.Open();
            SqlCommand veri = new SqlCommand("SELECT TOP 1 idbtn FROM MusteriBiletKayit", baglan1);

            using (SqlDataReader idReader = veri.ExecuteReader())
            {
                if (idReader.Read())
                {
                     
                    kayit.idbul = idReader["idbtn"].ToString();


                }

            }

            baglan1.Close();
            kayit.Show();
        }

        private void button29_Click(object sender, EventArgs e)
        {

            button29.BackColor = Color.Red;
            Kayit kayit = new Kayit();
            kayit.plaka = otoPlaka;
            kayit.GelenkalkisYeri = kalkisYeri;
            kayit.GelenvarisYeri = varisYeri;
            kayit.GelenyolTarihi = yolTarihi;
            kayit.Gelensaat = saat;
            kayit.GelentoplamFiyat = toplamFiyat;
            kayit.Form2ye_Gidecek_Veri = "29";
            kayit.Form2ye_Gidecek_Veri2 = "Red";
            SqlConnection baglan = new SqlConnection(ConnectionString);
            baglan.Open();
            SqlCommand btnRenkveri = new SqlCommand("SELECT TOP 1 id FROM ButonRenkleriDB", baglan);
            // Kayit kayit = new Kayit();
            using (SqlDataReader idReader = btnRenkveri.ExecuteReader())
            {
                while (idReader.Read())
                {
                     
                    kayit.btnidbul = idReader["id"].ToString();


                }

            }
            baglan.Close();
            //*********************

            SqlConnection baglan1 = new SqlConnection(ConnectionString);
            baglan1.Open();
            SqlCommand veri = new SqlCommand("SELECT TOP 1 idbtn FROM MusteriBiletKayit", baglan1);

            using (SqlDataReader idReader = veri.ExecuteReader())
            {
                if (idReader.Read())
                {
                     
                    kayit.idbul = idReader["idbtn"].ToString();


                }

            }

            baglan1.Close();
            kayit.Show();
        }

        private void button30_Click(object sender, EventArgs e)
        {
            button30.BackColor = Color.Red;
            Kayit kayit = new Kayit();
            kayit.plaka = otoPlaka;
            kayit.Form2ye_Gidecek_Veri = "30";
            kayit.GelenkalkisYeri = kalkisYeri;
            kayit.GelenvarisYeri = varisYeri;
            kayit.GelenyolTarihi = yolTarihi;
            kayit.Gelensaat = saat;
            kayit.GelentoplamFiyat = toplamFiyat;
            kayit.Form2ye_Gidecek_Veri2 = "Red";
            SqlConnection baglan = new SqlConnection(ConnectionString);
            baglan.Open();
            SqlCommand btnRenkveri = new SqlCommand("SELECT TOP 1 id FROM ButonRenkleriDB", baglan);
            // Kayit kayit = new Kayit();
            using (SqlDataReader idReader = btnRenkveri.ExecuteReader())
            {
                while (idReader.Read())
                {
                     
                    kayit.btnidbul = idReader["id"].ToString();


                }

            }
            baglan.Close();
            //*********************

            SqlConnection baglan1 = new SqlConnection(ConnectionString);
            baglan1.Open();
            SqlCommand veri = new SqlCommand("SELECT TOP 1 idbtn FROM MusteriBiletKayit", baglan1);

            using (SqlDataReader idReader = veri.ExecuteReader())
            {
                if (idReader.Read())
                {
                     
                    kayit.idbul = idReader["idbtn"].ToString();


                }

            }

            baglan1.Close();
            kayit.Show();
        }

        private void button31_Click(object sender, EventArgs e)
        {
            button31.BackColor = Color.Red;
            Kayit kayit = new Kayit();
            kayit.plaka = otoPlaka;
            kayit.GelenkalkisYeri = kalkisYeri;
            kayit.GelenvarisYeri = varisYeri;
            kayit.GelenyolTarihi = yolTarihi;
            kayit.Gelensaat = saat;
            kayit.GelentoplamFiyat = toplamFiyat;
            kayit.Form2ye_Gidecek_Veri = "31";
            kayit.Form2ye_Gidecek_Veri2 = "Red";
            SqlConnection baglan = new SqlConnection(ConnectionString);
            baglan.Open();
            SqlCommand btnRenkveri = new SqlCommand("SELECT TOP 1 id FROM ButonRenkleriDB", baglan);
            // Kayit kayit = new Kayit();
            using (SqlDataReader idReader = btnRenkveri.ExecuteReader())
            {
                while (idReader.Read())
                {
                     
                    kayit.btnidbul = idReader["id"].ToString();


                }

            }
            baglan.Close();

            SqlConnection baglan1 = new SqlConnection(ConnectionString);
            baglan1.Open();
            SqlCommand veri = new SqlCommand("SELECT TOP 1 idbtn FROM MusteriBiletKayit", baglan1);

            using (SqlDataReader idReader = veri.ExecuteReader())
            {
                if (idReader.Read())
                {
                    
                    kayit.idbul = idReader["idbtn"].ToString();


                }

            }

            baglan1.Close();
            kayit.Show();
        }

        private void button32_Click(object sender, EventArgs e)
        {
            button32.BackColor = Color.Red;
            Kayit kayit = new Kayit();
            kayit.plaka = otoPlaka;
            kayit.GelenkalkisYeri = kalkisYeri;
            kayit.GelenvarisYeri = varisYeri;
            kayit.GelenyolTarihi = yolTarihi;
            kayit.Gelensaat = saat;
            kayit.GelentoplamFiyat = toplamFiyat;
            kayit.Form2ye_Gidecek_Veri = "32";
            kayit.Form2ye_Gidecek_Veri2 = "Red";
            SqlConnection baglan = new SqlConnection(ConnectionString);
            baglan.Open();
            SqlCommand btnRenkveri = new SqlCommand("SELECT TOP 1 id FROM ButonRenkleriDB", baglan);
            // Kayit kayit = new Kayit();
            using (SqlDataReader idReader = btnRenkveri.ExecuteReader())
            {
                while (idReader.Read())
                {
                     
                    kayit.btnidbul = idReader["id"].ToString();


                }

            }
            baglan.Close();
            //*********************

            SqlConnection baglan1 = new SqlConnection(ConnectionString);
            baglan1.Open();
            SqlCommand veri = new SqlCommand("SELECT TOP 1 idbtn FROM MusteriBiletKayit", baglan1);

            using (SqlDataReader idReader = veri.ExecuteReader())
            {
                if (idReader.Read())
                {
                     
                    kayit.idbul = idReader["idbtn"].ToString();


                }

            }

            baglan1.Close();
            kayit.Show();
        }

        private void button33_Click(object sender, EventArgs e)
        {
            button33.BackColor = Color.Red;
            Kayit kayit = new Kayit();
            kayit.plaka = otoPlaka;
            kayit.GelenkalkisYeri = kalkisYeri;
            kayit.GelenvarisYeri = varisYeri;
            kayit.GelenyolTarihi = yolTarihi;
            kayit.Gelensaat = saat;
            kayit.GelentoplamFiyat = toplamFiyat;
            kayit.Form2ye_Gidecek_Veri = "33";
            kayit.Form2ye_Gidecek_Veri2 = "Red";
            SqlConnection baglan = new SqlConnection(ConnectionString);
            baglan.Open();
            SqlCommand btnRenkveri = new SqlCommand("SELECT TOP 1 id FROM ButonRenkleriDB", baglan);
            // Kayit kayit = new Kayit();
            using (SqlDataReader idReader = btnRenkveri.ExecuteReader())
            {
                while (idReader.Read())
                {
                     
                    kayit.btnidbul = idReader["id"].ToString();


                }

            }
            baglan.Close();

            SqlConnection baglan1 = new SqlConnection(ConnectionString);
            baglan1.Open();
            SqlCommand veri = new SqlCommand("SELECT TOP 1 idbtn FROM MusteriBiletKayit", baglan1);

            using (SqlDataReader idReader = veri.ExecuteReader())
            {
                if (idReader.Read())
                {
                     
                    kayit.idbul = idReader["idbtn"].ToString();


                }

            }

            baglan1.Close();
            kayit.Show();
        }
        //

        public class BolgeveSehir
        {
            public string MarmaraBolgesi { get; set; }
            public string EgeBolgesi { get; set; }
            public string AkdenizBolgesi { get; set; }
            public string IcAnadoluBolgesi { get; set; }
            public string KaradenizBolgesi { get; set; }

            public string DoguAnadoluBolgesi { get; set; }
            public string GuneyDoguAnadolu { get; set; }
            public string[] sehir { get; set; }
        }
        BolgeveSehir[] Bolgeler = {
            new BolgeveSehir { MarmaraBolgesi = "Marmara", sehir = new string[] { "Edirne", "Kırklareli", "Tekirdağ", "İstanbul", "Kocaeli", "Yalova", "Sakarya", "Bilecik", "Bursa", "Balıkesir", "Çanakkale" } },
            new BolgeveSehir { EgeBolgesi = "Ege", sehir = new string[] { "İzmir", "Manisa", "Aydın", "Denizli", "Kütahya", "Afyonkarahisar", "Uşak", "Muğla" } },
            new BolgeveSehir { AkdenizBolgesi = "Akdeniz", sehir = new string[] { "Antalya", "Mersin", "Adana", "Hatay", "Isparta", "Burdur", "Osmaniye", "Kahramanmaraş" } },
            new BolgeveSehir { IcAnadoluBolgesi = "IcAnadolu", sehir = new string[] { "Aksaray", "Ankara", "Çankırı", "Eskişehir", "Karaman", "Kırıkkale", "Kırşehir", "Konya", "Nevşehir", "Niğde", "Sivas", "Yozgat", "Kayseri" } },
            new BolgeveSehir { KaradenizBolgesi = "Karadeniz", sehir = new string[] { "Rize", "Trabzon", "Artvin", "Sinop", "Tokat", "Çorum", "Amasya", "Samsun", "Zonguldak", "Bolu", "Düzce", "Karabük", "Bartın", "Kastamonu", "Bayburt", "Giresun", "Gümüşhane", "Ordu" } },
            new BolgeveSehir { DoguAnadoluBolgesi = "DoguAnadolu", sehir = new string[] { "Ağrı", "Ardahan", "Bingöl", "Bitlis", "Elazığ", "Erzincan", "Erzurum", "Hakkari", "Iğdır", "Kars", "Malatya", "Muş", "Tunceli", "Van", "Şırnak" } },
            new BolgeveSehir { GuneyDoguAnadolu = "GuneyDoguAnadolu", sehir = new string[] {"Adıyaman", "Batman", "Diyarbakır", "Gaziantep", "Kilis", "Mardin", "Siirt", "Şanlıurfa"
            } },
        };

        private void button34_Click(object sender, EventArgs e)
        {
            button34.BackColor = Color.Red;
            Kayit kayit = new Kayit();
            kayit.GelenkalkisYeri = kalkisYeri;
            kayit.GelenvarisYeri = varisYeri;
            kayit.GelenyolTarihi = yolTarihi;
            kayit.Gelensaat = saat;
            kayit.GelentoplamFiyat = toplamFiyat;
            kayit.Form2ye_Gidecek_Veri = "34";
            kayit.Form2ye_Gidecek_Veri2 = "Red";
            SqlConnection baglan = new SqlConnection(ConnectionString);
            baglan.Open();
            SqlCommand btnRenkveri = new SqlCommand("SELECT TOP 1 id FROM ButonRenkleriDB", baglan);
            // Kayit kayit = new Kayit();
            using (SqlDataReader idReader = btnRenkveri.ExecuteReader())
            {
                while (idReader.Read())
                {
                     
                    kayit.btnidbul = idReader["id"].ToString();


                }

            }
            baglan.Close();
            //*********************

            SqlConnection baglan1 = new SqlConnection(ConnectionString);
            baglan1.Open();
            SqlCommand veri = new SqlCommand("SELECT TOP 1 idbtn FROM MusteriBiletKayit", baglan1);

            using (SqlDataReader idReader = veri.ExecuteReader())
            {
                if (idReader.Read())
                {
                     
                    kayit.idbul = idReader["idbtn"].ToString();


                }

            }

            baglan1.Close();
            kayit.Show();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void kaydet_Click(object sender, EventArgs e)
        {
            foreach (var bolgeVeSehir in Bolgeler)
            {

                // MessageBox.Show($"Bölge: {bolgeVeSehir.bolge}");
                // Ege Akdeniz IcAnadolu  Karadeniz  DoguAnadolu  GuneyDoguAnadolu

                foreach (var sehir in bolgeVeSehir.sehir)
                {
                    if (sehir == kalkiscmb.Text)
                    {
                        if (bolgeVeSehir.MarmaraBolgesi == "Marmara" && sehir == kalkiscmb.Text)
                        {

                            // MessageBox.Show("fiyat " + Marmarafiyat);
                            kalkisfiyat = Marmarafiyat;
                            

                        }

                        else if (bolgeVeSehir.AkdenizBolgesi == "Akdeniz" && sehir == kalkiscmb.Text)
                        {
                            // MessageBox.Show("fiyat " + AkdenizFiyat);
                            kalkisfiyat = AkdenizFiyat;
                            

                        }
                        else if (bolgeVeSehir.IcAnadoluBolgesi == "IcAnadolu" && sehir == kalkiscmb.Text)
                        {
                            // MessageBox.Show("fiyat " + IcAnadoluFiyat);
                            kalkisfiyat = IcAnadoluFiyat;
                            
                        }
                        else if (bolgeVeSehir.KaradenizBolgesi == "Karadeniz" && sehir == kalkiscmb.Text)
                        {
                            // MessageBox.Show("fiyat " + KaradenizFiyat);
                            kalkisfiyat = KaradenizFiyat;
                            
                        }
                        else if (bolgeVeSehir.DoguAnadoluBolgesi == "DoguAnadolu" && sehir == kalkiscmb.Text)
                        {
                            // MessageBox.Show("fiyat " + DoguAnadoluFİyat);
                            kalkisfiyat = DoguAnadoluFİyat;
                            

                        }
                        else if (bolgeVeSehir.GuneyDoguAnadolu == "GuneyDoguAnadolu" && sehir == kalkiscmb.Text)
                        {
                            // MessageBox.Show("fiyat " + GuneyAnadoluFiyat);
                            kalkisfiyat = GuneyAnadoluFiyat;
                            

                        }

                        // MessageBox.Show(sehir + " " + bolgeVeSehir.bolge);
                    }


                }
            }


            foreach (var bolgeVeSehir in Bolgeler)
            {

                // MessageBox.Show($"Bölge: {bolgeVeSehir.bolge}");
                // Ege Akdeniz IcAnadolu  Karadeniz  DoguAnadolu  GuneyDoguAnadolu

                foreach (var sehir in bolgeVeSehir.sehir)
                {


                    if (sehir == variscmb.Text)
                    {
                        if (bolgeVeSehir.MarmaraBolgesi == "Marmara" && sehir == variscmb.Text)
                        {

                            //MessageBox.Show("fiyat " + Marmarafiyat);


                            varisfiyat = Marmarafiyat;
                            

                        }
                        else if (bolgeVeSehir.EgeBolgesi == "Ege" && sehir == variscmb.Text)
                        {
                            //MessageBox.Show("fiyat " + EgeFiyat);
                            varisfiyat = EgeFiyat;
                            

                        }
                        else if (bolgeVeSehir.AkdenizBolgesi == "Akdeniz" && sehir == variscmb.Text)
                        {
                            // MessageBox.Show("fiyat " + AkdenizFiyat);

                            varisfiyat = AkdenizFiyat;
                            
                        }
                        else if (bolgeVeSehir.IcAnadoluBolgesi == "IcAnadolu" && sehir == variscmb.Text)
                        {
                            // MessageBox.Show("fiyat " + IcAnadoluFiyat);

                            varisfiyat = IcAnadoluFiyat;
                            
                        }
                        else if (bolgeVeSehir.KaradenizBolgesi == "Karadeniz" && sehir == variscmb.Text)
                        {
                            //MessageBox.Show("fiyat " + KaradenizFiyat);

                            varisfiyat = KaradenizFiyat;
                            
                        }
                        else if (bolgeVeSehir.DoguAnadoluBolgesi == "DoguAnadolu" && sehir == variscmb.Text)
                        {
                            // MessageBox.Show("fiyat " + DoguAnadoluFİyat);

                            varisfiyat = DoguAnadoluFİyat;
                            
                        }
                        else if (bolgeVeSehir.GuneyDoguAnadolu == "GuneyDoguAnadolu" && sehir == variscmb.Text)
                        {
                            //  MessageBox.Show("fiyat " + GuneyAnadoluFiyat);
                            //
                            varisfiyat = GuneyAnadoluFiyat;
                            
                        }
                        // MessageBox.Show(sehir + " " + bolgeVeSehir.bolge);

                        else
                        {
                            MessageBox.Show("sorun yaşandı", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }


                        toplamFiyat = kalkisfiyat + varisfiyat;
                        //  MessageBox.Show("kalkış fiyat : " + kalkisfiyat + "varış fiyat: " + varisfiyat + " " + "sonuc : " + sonuc);


                        FiyatLBL.Text = Convert.ToString(toplamFiyat);
                    }
                    // 
                }


            }
            baglanti.Open();
            //iptBtndurum = iptalEt.Enabled = false;
            SqlCommand veriekle = new SqlCommand("update  KalkisVaris set  kalkisYeri = @kalkisYeri, varisYeri = @varisYeri, yolTarihi = @yolTarihi, saat = @saat, fiyat=@fiyat where @id = id", baglanti);
            Form1 form = new Form1();




            veriekle.Parameters.AddWithValue("@id", 1);
            veriekle.Parameters.AddWithValue("@kalkisYeri", kalkiscmb.Text);
            veriekle.Parameters.AddWithValue("@varisYeri", variscmb.Text);
            veriekle.Parameters.AddWithValue("@yolTarihi", yolculuktrhdt.Text);
            veriekle.Parameters.AddWithValue("@saat", saatsecCMB.Text);

            veriekle.Parameters.AddWithValue("@fiyat", Convert.ToDouble(FiyatLBL.Text));
            veriekle.ExecuteNonQuery();
            baglanti.Close();
            VeriListele();
            Veritut();
            MessageBox.Show("Güncelleme Tamamlandı", "Başarılı");


            yolclklbl.Text = yolculuktrhdt.Text;
            saatLBL.Text = saatsecCMB.Text;
            plakaLBL.Text = otoPlaka;

        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            AdminPanel f1 = Application.OpenForms["AdminPanel"] as AdminPanel;
            if (f1 != null)
            {
                f1.VeriListele();
            }
            else
            {

            }
        }
    }
}
