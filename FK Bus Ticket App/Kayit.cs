using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace FK_Bus_Ticket_App
{

    public partial class Kayit : Form
    {

        public Kayit(Form1 form1)
        {
            InitializeComponent();

        }
        double Marmarafiyat = 219.90;
        double EgeFiyat = 309.90;
        double AkdenizFiyat = 299.90;
        double IcAnadoluFiyat = 349.90;
        double KaradenizFiyat = 269.90;
        double DoguAnadoluFİyat = 234.90;
        double GuneyAnadoluFiyat = 209.90;
        //

        public double GelentoplamFiyat;
        public string GelenkalkisYeri = "";
        public string GelenvarisYeri = "";
        public string GelenyolTarihi = "";
        public string Gelensaat = "";
        //

        public string Form2ye_Gidecek_Veri = "";
        public string Form2ye_Gidecek_Veri2 = "";
        public string idbul = "";
        public string btnidbul = "";
        private const string ConnectionString = "Data Source=.;Initial Catalog=FKBussTicketDB;Integrated Security=True";

        //
        public bool kaydetyetki;
        public bool guncellemeyetki;
        public bool silyetki;
        public void VeriListele()
        {

            adreskyttxtbx.ReadOnly = true;
            KoltuknoLBL.Text = KoltuknoGonder.ToString();
            //----
            Form1 form1 = new Form1();


            baglanti.Open();
            SqlCommand veri = new SqlCommand("select * from MusteriBiletKayit where idbtn=@idbtn", baglanti);
            veri.Parameters.AddWithValue("@idbtn", Form2ye_Gidecek_Veri);

            SqlDataReader dr;
            dr = veri.ExecuteReader();

            while (dr.Read())
            {
                idlbl.Text = dr.GetValue(0).ToString();
                adlbl.Text = dr.GetValue(1).ToString();
                adreskyttxtbx.Text = dr.GetString(2).ToString();
                tellbl.Text = dr.GetValue(3).ToString();
                tckmlklbl.Text = dr.GetValue(4).ToString();
                dgmlbl.Text = dr.GetValue(5).ToString();
                cinslbl.Text = dr.GetValue(6).ToString();


                ogrencilbl.Text = dr.GetValue(7).ToString();

                ogrencilbl.Text = dr.GetValue(7).ToString();


                KoltuknoLBL.Text = dr.GetValue(12).ToString();
                
                FiyatLBL.Text = dr.GetValue(14).ToString();
                iptalEt.Enabled = iptBtndurum;
                if (iptBtndurum == false)
                {

                    kaydet.Enabled = false;
                }
            }
            baglanti.Close();
            kalkslbl.Text = GelenvarisYeri;
            varislbl.Text = GelenkalkisYeri;
            yolclklbl.Text = GelenyolTarihi;
            saatLBL.Text = Gelensaat;
            Giris giris = (Giris)Application.OpenForms["Giris"];
            kaydetyetki = giris.kaydetbool;
            guncellemeyetki = giris.guncellebool;
            silyetki = giris.silbool;
            if (kaydetyetki == true)
            {
                kaydet.Enabled = true;
            }
            if (guncellemeyetki == true)
            {
                Guncelle.Enabled = true;
            }
            if (silyetki == true)
            {
                sil.Enabled = true;
            }
            if (kaydetyetki == false)
            {
                kaydet.Enabled = false;
            }
            if (guncellemeyetki == false)
            {
                Guncelle.Enabled = false;
            }
            if (silyetki == false)
            {
                sil.Enabled = false;
            }
            //--

            otobuspPlakaLBL.Text = plaka;

        }

        public void Veritut()
        {
            
            adreskyttxtbx.ReadOnly = true;
            Form1 form1 = new Form1();

            baglanti.Open();
            SqlCommand veri = new SqlCommand("select * from MusteriBiletKayit where idbtn=@idbtn", baglanti);

            veri.Parameters.AddWithValue("@idbtn", Form2ye_Gidecek_Veri);

            SqlDataReader dr;
            dr = veri.ExecuteReader();

            while (dr.Read())
            {
                adsoyadtxtbx.Text = dr.GetValue(1).ToString();
                adrestxtbx.Text = dr.GetString(2).ToString();
                teltxtbx.Text = dr.GetValue(3).ToString();
                tctxtbx.Text = dr.GetValue(4).ToString();
                dogumtar.Text = dr.GetValue(5).ToString();
                cinsiyetcmb.Text = dr.GetValue(6).ToString();

                if (ogrencievt.Text == "Evet")
                {
                    ogrencievt.Checked = true;
                }
                if (ogrencihyr.Text == "Hayır")

                {
                    ogrencihyr.Checked = true;
                }

                KoltukNoo.Text = dr.GetValue(12).ToString();
                FiyatLBL.Text = dr.GetValue(14).ToString();
                iptalEt.Enabled = iptBtndurum;

                if (iptBtndurum == false)
                {

                    kaydet.Enabled = false;
                }
            }

            baglanti.Close();
            //

        }

        public SqlConnection baglanti = new SqlConnection("Data Source=.;Initial Catalog=FKBussTicketDB;Integrated Security=True");
        public Kayit()
        {
            InitializeComponent();
        }

        public bool iptBtndurum;
        public int KoltuknoGonder;
        public string plaka { get; set; }

        private void Kayit_Load(object sender, EventArgs e)


        {
            cinsiyetcmb.DropDownStyle = ComboBoxStyle.DropDownList;
            Form1 form1 = new Form1();

            Kayit kayit = new Kayit();



            form1.Veritut();



            Veritut();
            VeriListele();
            KoltukNoo.Text = Form2ye_Gidecek_Veri.ToString();

          
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
        }
        public double kazanc;
        public double varisfiyat;
        public double kalkisfiyat;
        public double sonuc;
        private void kaydet_Click(object sender, EventArgs e)
        {
            baglanti.Open();

            SqlCommand oku = new SqlCommand("SELECT COUNT(*) FROM ButonRenkleriDB WHERE butonAdi = @butonAdi", baglanti);

            oku.Parameters.AddWithValue("@butonAdi", Form2ye_Gidecek_Veri);

            int kullaniciSayisi = (int)oku.ExecuteScalar();

            if (kullaniciSayisi > 0)
            {
                MessageBox.Show("Müşteri Kaydı Zaten Var!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                baglanti.Close();
                return;
            }
            baglanti.Close();
            baglanti.Open();
            SqlCommand veriekle = new SqlCommand("insert into MusteriBiletKayit (adSoyad,adres,telefon,tcKimlik,dogumTarihi,cinsiyet,ogrenci,kalkisYeri,varisYeri,yolTarihi,saat,koltukNo,idbtn,fiyat) values (@adSoyad,@adres,@telefon,@tcKimlik,@dogumTarihi,@cinsiyet,@ogrenci,@kalkisYeri,@varisYeri,@yolTarihi,@saat,@koltukNo,@idbtn,@fiyat)", baglanti);
            Form1 form = new Form1();


            if (adsoyadtxtbx.Text == "" || adrestxtbx.Text == "" || teltxtbx.Text == "" || tctxtbx.Text == "" || cinsiyetcmb.Text == "")
            {
                MessageBox.Show("Boş Alanları Doldurunuz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                baglanti.Close();
                return;
            }
            veriekle.Parameters.AddWithValue("@adSoyad", adsoyadtxtbx.Text);
            veriekle.Parameters.AddWithValue("@adres", adrestxtbx.Text);
            veriekle.Parameters.AddWithValue("@telefon", teltxtbx.Text);
            veriekle.Parameters.AddWithValue("@tcKimlik", tctxtbx.Text);
            veriekle.Parameters.AddWithValue("@dogumTarihi", dogumtar.Value);
            veriekle.Parameters.AddWithValue("@cinsiyet", cinsiyetcmb.Text);
            veriekle.Parameters.AddWithValue("@kalkisYeri", GelenkalkisYeri);
            veriekle.Parameters.AddWithValue("@varisYeri", GelenvarisYeri);
            veriekle.Parameters.AddWithValue("@yolTarihi", GelenyolTarihi);
            veriekle.Parameters.AddWithValue("@saat", Gelensaat);
            veriekle.Parameters.AddWithValue("@koltukNo", Form2ye_Gidecek_Veri);
            veriekle.Parameters.AddWithValue("@idbtn", Form2ye_Gidecek_Veri);
            //veriekle.Parameters.AddWithValue("@iptalBtnDurum", iptBtndurum);
            if (ogrencievt.Checked)
            {
                veriekle.Parameters.AddWithValue("@ogrenci", ogrencievt.Text);
                sonuc = GelentoplamFiyat - GelentoplamFiyat * 0.20;
                veriekle.Parameters.AddWithValue("@fiyat", sonuc);
                //ogrencievt.Checked = Enabled;
            }
            else if (ogrencihyr.Checked)
            {
                veriekle.Parameters.AddWithValue("@ogrenci", ogrencihyr.Text);
                veriekle.Parameters.AddWithValue("@fiyat", GelentoplamFiyat);
            }
            if (teltxtbx.Text.Length < 11 || tctxtbx.Text.Length < 11)
            {
                MessageBox.Show("Telefon veya TC Kimlik Numaranız 11 Rakamdan Düşük Olamaz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                baglanti.Close();
                return;
            }
            if ( ogrencievt.Checked == false || ogrencihyr.Checked == false)
            {
                if (ogrencievt.Checked == true || ogrencihyr.Checked == true)
                {
                    veriekle.ExecuteNonQuery();
                    baglanti.Close();
                }
                else
                {
                    MessageBox.Show("Boş Alanları Doldurunuz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    baglanti.Close();
                    return;
                }
            }
            MessageBox.Show("Kayıt Tamamlandı", "Başarılı");
            VeriListele();
            Veritut();
            adlbl.Text = adsoyadtxtbx.Text;
            adreskyttxtbx.Text = adrestxtbx.Text;
            tellbl.Text = teltxtbx.Text;
            tckmlklbl.Text = tctxtbx.Text;
            dgmlbl.Text = dogumtar.Text;
            cinslbl.Text = cinsiyetcmb.Text;

            if (ogrencievt.Checked)
            {
                ogrencilbl.Text = ogrencievt.Text;
            }
            else if (ogrencihyr.Checked)
            {
                ogrencilbl.Text = ogrencihyr.Text;
            }
            kalkslbl.Text = GelenkalkisYeri;
            varislbl.Text = GelenvarisYeri;
            yolclklbl.Text = GelenyolTarihi;
            saatLBL.Text = Gelensaat;
            KoltuknoLBL.Text = Form2ye_Gidecek_Veri;
            FiyatLBL.Text = Convert.ToString(GelentoplamFiyat);
            SqlConnection baglan = new SqlConnection(ConnectionString);
            baglan.Open();

            SqlCommand veriekle1 = new SqlCommand("insert into ButonRenkleriDB (butonAdi,renk) values (@butonAdi,@renk)", baglan);
            veriekle1.Parameters.AddWithValue("@butonAdi", Form2ye_Gidecek_Veri);
            veriekle1.Parameters.AddWithValue("@renk", Form2ye_Gidecek_Veri2);
            veriekle1.ExecuteNonQuery();
            baglan.Close();
            baglanti.Close();
            iptalEt.Enabled = false;

            baglanti.Open();
            using (SqlCommand comman = new SqlCommand("SELECT kazanc FROM muhasebe WHERE id=@id", baglanti))
            {
                comman.Parameters.AddWithValue("@id", 2);

                using (SqlDataReader reader = comman.ExecuteReader())
                {
                    if (reader.Read()) // Veri bulundu mu kontrolü
                    {
                        kazanc = Convert.ToDouble(reader["kazanc"].ToString());
                    }

                    else
                    {
                        MessageBox.Show("Veri bulunamadı veya okuma hatası oluştu.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            baglanti.Close();
            
            kazanc = kazanc + GelentoplamFiyat;
            baglanti.Open();

            // UPDATE sorgusunu oluştur
            SqlCommand kazancDB = new SqlCommand("UPDATE muhasebe SET kazanc=@kazanc WHERE id=@id", baglanti);
            kazancDB.Parameters.AddWithValue("@id", 2);
            kazancDB.Parameters.AddWithValue("@kazanc", kazanc);
            // Komutu çalıştır
            kazancDB.ExecuteNonQuery();
            baglanti.Close();

            baglanti.Open();
            using (SqlCommand comman = new SqlCommand("SELECT kazanc FROM muhasebe WHERE id=@id", baglanti))
            {
                comman.Parameters.AddWithValue("@id", 2);

                using (SqlDataReader reader = comman.ExecuteReader())
                {
                    if (reader.Read()) // Veri bulundu mu kontrolü
                    {
                        kazanc = Convert.ToDouble(reader["kazanc"].ToString());
                    }

                    else
                    {
                        MessageBox.Show("Veri bulunamadı veya okuma hatası oluştu.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }

            baglanti.Close();
            VeriListele();
            Veritut();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 form = new Form1();
            baglanti.Close();
            this.Hide();
            this.Close();

        }

        private void adreskyttxtbx_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void sil_Click(object sender, EventArgs e)
        {   
            DialogResult dialogResult = MessageBox.Show("Silmek İstediğinize Emin misiniz?", "Müşteri Bilgilerini Sil", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {   if(FiyatLBL.Text=="")
                {
                    MessageBox.Show("Sİlinecek Veri Bulunamadı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning); return;
                }
                Form1 form = new Form1();
                form.ButonSifirla();
                baglanti.Open();
                form.ButonSifirla();
                SqlCommand VeriSilMusteri = new SqlCommand("Delete from MusteriBiletKayit where idbtn=@idbtn", baglanti);
                SqlCommand VeriSil = new SqlCommand("Delete from ButonRenkleriDB where butonAdi=@butonAdi", baglanti);
                VeriSil.Parameters.AddWithValue("@butonAdi", Form2ye_Gidecek_Veri);
                VeriSilMusteri.Parameters.AddWithValue("@idbtn", Form2ye_Gidecek_Veri);
                VeriSil.ExecuteNonQuery();
                VeriSilMusteri.ExecuteNonQuery();
                form.ButonSifirla();
                baglanti.Close();
                baglanti.Open();
                using (SqlCommand comman = new SqlCommand("SELECT kazanc FROM muhasebe WHERE id=@id", baglanti))
                {
                    comman.Parameters.AddWithValue("@id", 2);

                    using (SqlDataReader reader = comman.ExecuteReader())
                    {
                        if (reader.Read()) // Veri bulundu mu kontrolü
                        {
                            // Alanları değişkenlere ata
                            kazanc = Convert.ToDouble(reader["kazanc"].ToString());
                            // Şimdi bu değişkenleri kullan

                        }

                        else
                        {
                            MessageBox.Show("Veri bulunamadı veya okuma hatası oluştu.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }

                baglanti.Close();
                kazanc = kazanc - GelentoplamFiyat;
                
                baglanti.Open();
                SqlCommand kazancDB = new SqlCommand("UPDATE muhasebe SET kazanc=@kazanc WHERE id=@id", baglanti);

                // Parametreleri ekle
                kazancDB.Parameters.AddWithValue("@id", 2);
                kazancDB.Parameters.AddWithValue("@kazanc", kazanc);

                // Komutu çalıştır
                kazancDB.ExecuteNonQuery();
                baglanti.Close();

                baglanti.Open();
                using (SqlCommand comman = new SqlCommand("SELECT kazanc FROM muhasebe WHERE id=@id", baglanti))
                {
                    comman.Parameters.AddWithValue("@id", 2);

                    using (SqlDataReader reader = comman.ExecuteReader())
                    {
                        if (reader.Read()) // Veri bulundu mu
                        {
                            kazanc = Convert.ToDouble(reader["kazanc"].ToString());

                        }

                        else
                        {

                            MessageBox.Show("Veri bulunamadı veya okuma hatası oluştu.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }

                baglanti.Close();
                //
                //MessageBox.Show("kazanç :" + kazanc);
                this.Close();
                // OnForm2DataDeleted();
                form.ButonSifirla();
                form.butonListele();
            }
            else
            {

            }

        }
        public double indirim;
        private void Guncelle_Click(object sender, EventArgs e)
        {

            // 
            DialogResult dialogResult = MessageBox.Show("Güncellemek İstediğinize Emin misiniz?", "Müşteri Bilgilerini Güncelle", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {

                //
                try { baglanti.Open();
                //iptBtndurum = iptalEt.Enabled = false;
                SqlCommand veriguncelle = new SqlCommand("update  MusteriBiletKayit set  adSoyad=@adSoyad,adres=@adres,telefon=@telefon,tcKimlik=@tcKimlik,dogumTarihi=@dogumTarihi,cinsiyet=@cinsiyet,ogrenci=@ogrenci,kalkisYeri=@kalkisYeri,varisYeri=@varisYeri,yolTarihi=@yolTarihi,saat=@saat,koltukNo=@koltukNo,idbtn=@idbtn,fiyat=@fiyat where @id=id", baglanti);
                Form1 form = new Form1();
                    if (adsoyadtxtbx.Text == "" || adrestxtbx.Text == "" || teltxtbx.Text == "" || tctxtbx.Text == "" || cinsiyetcmb.Text == "")
                    {
                        MessageBox.Show("Boş Alanları Doldurunuz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        baglanti.Close();
                        return;
                    }
                    veriguncelle.Parameters.AddWithValue("@id", idlbl.Text);
                veriguncelle.Parameters.AddWithValue("@adSoyad", adsoyadtxtbx.Text);
                veriguncelle.Parameters.AddWithValue("@adres", adrestxtbx.Text);
                veriguncelle.Parameters.AddWithValue("@telefon", teltxtbx.Text);
                veriguncelle.Parameters.AddWithValue("@tcKimlik", tctxtbx.Text);
                veriguncelle.Parameters.AddWithValue("@dogumTarihi", dogumtar.Value);
                veriguncelle.Parameters.AddWithValue("@cinsiyet", cinsiyetcmb.Text);
                if (ogrencievt.Checked)
                {
                    veriguncelle.Parameters.AddWithValue("@ogrenci", ogrencievt.Text);
                }
                else if (ogrencihyr.Checked)
                {
                    veriguncelle.Parameters.AddWithValue("@ogrenci", ogrencihyr.Text);
                }

                veriguncelle.Parameters.AddWithValue("@kalkisYeri", GelenkalkisYeri);
                veriguncelle.Parameters.AddWithValue("@varisYeri", GelenvarisYeri);
                veriguncelle.Parameters.AddWithValue("@yolTarihi", GelenyolTarihi);
                veriguncelle.Parameters.AddWithValue("@saat", Gelensaat);
                veriguncelle.Parameters.AddWithValue("@koltukNo", Form2ye_Gidecek_Veri);
                veriguncelle.Parameters.AddWithValue("@idbtn", Form2ye_Gidecek_Veri);
                //veriguncelle.Parameters.AddWithValue("@iptalBtnDurum", iptBtndurum);
                if (ogrencievt.Checked)
                {        
                        sonuc = GelentoplamFiyat - GelentoplamFiyat * 0.20;
                        veriguncelle.Parameters.AddWithValue("@fiyat", sonuc);                   
                }
                    else if (ogrencihyr.Checked)
                {

                    veriguncelle.Parameters.AddWithValue("@fiyat", GelentoplamFiyat);
                }

                veriguncelle.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Güncelleme Tamamlandı", "Başarılı");

                adlbl.Text = adsoyadtxtbx.Text;
                adreskyttxtbx.Text = adrestxtbx.Text;
                tellbl.Text = teltxtbx.Text;
                tckmlklbl.Text = tctxtbx.Text;
                dgmlbl.Text = dogumtar.Text;
                cinslbl.Text = cinsiyetcmb.Text;

                if (ogrencievt.Checked)
                {
                    ogrencilbl.Text = ogrencievt.Text;
                }
                else if (ogrencihyr.Checked)
                {
                    ogrencilbl.Text = ogrencihyr.Text;
                }
                kalkslbl.Text = GelenkalkisYeri;
                varislbl.Text = GelenvarisYeri;

                yolclklbl.Text = GelenyolTarihi;
                saatLBL.Text = Gelensaat;
                FiyatLBL.Text = FiyatLBL.Text;
                KoltuknoLBL.Text = Form2ye_Gidecek_Veri;

                VeriListele();
                Veritut();
                }
                catch
                {
                    baglanti.Close();
                    MessageBox.Show("Hiçbir Veri Girilmedi.","Uyarı",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                    return;
                }
                
            }
           


            Veritut();
            VeriListele();
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void Kayit_FormClosed(object sender, FormClosedEventArgs e)
        {
            
            Form1 f1 = Application.OpenForms["Form1"] as Form1;
            if (f1 != null)
            {
                f1.ButonSifirla();
                
            }
            else
            {
                MessageBox.Show("Yeniden Giriş Yapınız...", "Uyarı!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void yolculuktrhdt_ValueChanged(object sender, EventArgs e)
        {

        }


        private void yazdir_Click(object sender, EventArgs e)
        {



            printPreviewDialog1.ShowDialog();

        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            // string logoPath = Path.Combine(Application.StartupPath, "logo", "png-transparent-computer-icons-50x50-voicemail-message-box-miscellaneous-text-rectangle-thumbnail.png");
            //  Image logo = Image.FromFile(logoPath);
            //*
            // int yeniGenislik = 5; // Yeni genişlik değeri
            // Orijinal resmi belirli bir genişlikte orantılı olarak ölçeklendirme
            //  int yeniYukseklik = (int)(((float)logo.Height / logo.Width) * yeniGenislik);
            // Yeni bir bitmap oluştur
            // Bitmap yenidenBoyutlandirilmisLogo = new Bitmap(yeniGenislik, yeniYukseklik);
            // Yeni bitmap üzerine orijinal resmi çiz
            // using (Graphics g = Graphics.FromImage(yenidenBoyutlandirilmisLogo))
            //  {
            //  g.DrawImage(logo, 0, 0, yeniGenislik, yeniYukseklik);
            //  }
            // yenidenBoyutlandirilmisLogo'yu kullanabilirsiniz
            // e.Graphics.DrawImage(logo, new Point(100, 100));
            Font font = new Font("Arial", 14);
            SolidBrush firca = new SolidBrush(Color.Black);
            e.Graphics.DrawString($"Tarih: {DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")}", font, firca, 50, 25);
            font = new Font("Arial", 20, FontStyle.Bold);
            e.Graphics.DrawString("Müşteri Bilet Bilgileri", font, firca, 280, 75);
            e.Graphics.DrawString("****************************************************************************", font, firca, 0, 115);
            font = new Font("Arial", 15, FontStyle.Bold);
            e.Graphics.DrawString("Ad Soyad:", font, firca, 60, 150);
            e.Graphics.DrawString("Adres:", font, firca, 60, 200);
            e.Graphics.DrawString("Telefon:", font, firca, 60, 300);
            e.Graphics.DrawString("TC Kimlik No:", font, firca, 60, 350);
            e.Graphics.DrawString("Doğum Tarihi:", font, firca, 60, 400);
            e.Graphics.DrawString("Cinsiyet:", font, firca, 60, 450);
            e.Graphics.DrawString("Öğrenci mi?:", font, firca, 60, 500);
            e.Graphics.DrawString("Kalkış Yeri:", font, firca, 60, 550);
            e.Graphics.DrawString("Varış Yeri:", font, firca, 60, 600);
            e.Graphics.DrawString("Yolculuk Tarihi:", font, firca, 60, 650);
            e.Graphics.DrawString("Koltuk Numarası:", font, firca, 60, 700);
            e.Graphics.DrawString("Otobüs Plaka:", font, firca, 60, 750);
            e.Graphics.DrawString("Bilet Tutarı:", font, firca, 60, 800);

            font = new Font("Arial", 15, FontStyle.Bold);
            e.Graphics.DrawString(adlbl.Text, font, firca, 250, 150);
            int genislik = 420; // Metni sığdırmak istediğim genişlik
            string adres = adreskyttxtbx.Text;
            int karakterSayisi = adres.Length;
            int satirBasinaKarakter = genislik / 10; // Varsayılan karakter genişliği 10
            int satirSayisi = (karakterSayisi / satirBasinaKarakter) + 1;

            for (int i = 0; i < satirSayisi; i++)
            {
                int startIndex = i * satirBasinaKarakter;
                int uzunluk = Math.Min(satirBasinaKarakter, karakterSayisi - startIndex);

                string satir = adres.Substring(startIndex, uzunluk);
                e.Graphics.DrawString(satir, font, firca, 250, 200 + i * 20); // Yeni satırı her 20 birimde çiziyoruz, bu değeri ayarlayabilirsiniz.
            }

            e.Graphics.DrawString(tellbl.Text, font, firca, 250, 300);
            e.Graphics.DrawString(tckmlklbl.Text, font, firca, 250, 350);
            e.Graphics.DrawString(dgmlbl.Text, font, firca, 250, 400);
            e.Graphics.DrawString(cinslbl.Text, font, firca, 250, 450);
            e.Graphics.DrawString(ogrencilbl.Text, font, firca, 250, 500);
            e.Graphics.DrawString(kalkslbl.Text, font, firca, 250, 550);
            e.Graphics.DrawString(varislbl.Text, font, firca, 250, 600);
            e.Graphics.DrawString(yolclklbl.Text + "   " + "Kalkış Saati: " + saatLBL.Text, font, firca, 250, 650);
            e.Graphics.DrawString(KoltuknoLBL.Text, font, firca, 250, 700);
            e.Graphics.DrawString(otobuspPlakaLBL.Text, font, firca, 250, 750);
            e.Graphics.DrawString(FiyatLBL.Text+" TL", font, firca, 250, 800);

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void teltxtbx_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);

        }
    }
}
