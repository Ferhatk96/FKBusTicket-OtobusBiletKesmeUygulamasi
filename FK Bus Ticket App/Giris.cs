using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
namespace FK_Bus_Ticket_App
{
    public partial class Giris : Form
    {
        public Giris()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=.;Initial Catalog=FKBussTicketDB;Integrated Security=True");


        private void Giris_Load(object sender, EventArgs e)
        {

            baglanti.Open();
            SqlCommand command = new SqlCommand("select * from kullaniciHatirla", baglanti);
            command.Parameters.AddWithValue("@kullaniciHatirla", kullaniciAdiCMB.Text);
            command.Parameters.AddWithValue("@sifreHatirla", sifre.Text);
            SqlDataReader dr = command.ExecuteReader();

            while (dr.Read())
            {
                // int id = dr.GetInt32(0); // Birinci sütunu (index 0) integer olarak al
                string deger = dr.GetString(1);
                kullaniciAdiCMB.Items.Add(deger);
                string parola = sifre.Text = dr.GetString(2);


            }
            kullaniciAdiCMB.Text = null;
            sifre.Text = null;
            baglanti.Close();
        }
        public string kullaniciAd;
        public string kullaniciSifre;
        public bool kaydetbool;
        public bool guncellebool;
        public bool silbool;

        private void button1_Click(object sender, EventArgs e)
        {
            // Kullanıcı adı ve şifreyi kontrol eden SQL sorgusu
            string sorgu = "SELECT * FROM Giris WHERE kullaniciAdi=@kullaniciAdi AND sifre=@sifre";
            SqlCommand cmd = new SqlCommand(sorgu, baglanti);
            cmd.Parameters.AddWithValue("@kullaniciAdi", kullaniciAdiCMB.Text);
            cmd.Parameters.AddWithValue("@sifre", sifre.Text);

            // Veritabanı bağlantısını aç
            baglanti.Open();

            // İlk DataReader'ı oluştur
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                dr.Close();

                SqlCommand authCmd = new SqlCommand("SELECT kayitYetki, guncellemeYetki, silmeYetki FROM Giris WHERE kullaniciAdi = @kullaniciAdi", baglanti);
                authCmd.Parameters.AddWithValue("@kullaniciAdi", kullaniciAdiCMB.Text);

                // İkinci DataReader'ı oluştur
                SqlDataReader authReader = authCmd.ExecuteReader();

                if (authReader.Read())
                {
                    kaydetbool = Convert.ToBoolean(authReader["kayitYetki"]);
                    guncellebool = Convert.ToBoolean(authReader["guncellemeYetki"]);  // "guncellemeYetki" sütun adını kontrol et
                    silbool = Convert.ToBoolean(authReader["silmeYetki"]);

                }

                // İkinci DataReader'ı kapat
                authReader.Close();

                baglanti.Close();
                Kayit kayit = new Kayit();
                kayit.kaydetyetki = kaydetbool;
                kayit.guncellemeyetki = guncellebool;
                kayit.silyetki = silbool;
                if (kullaniciAdiCMB.Text != "admin" || sifre.Text != "1234")
                {

                    Form1 form1 = new Form1();

                    MessageBox.Show("Giriş Başarılı.", "Hoşgeldiniz");
                    form1.Show();
                    return;


                }
                if (kullaniciAdiCMB.Text == "admin" && sifre.Text == "1234")
                {
                    MessageBox.Show("Giriş Başarılı!", "Admin Paneline Hoşgeldiniz");
                    AdminPanel adminPanel = new AdminPanel();
                    adminPanel.kullaniciAd = kullaniciAdiCMB.Text;
                    adminPanel.Show();
                }



            }
            else
            {
                MessageBox.Show("Kullanıcı adını ve şifrenizi kontrol ediniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                baglanti.Close();
            }


        }

        private void beniHatirla_CheckedChanged(object sender, EventArgs e)
        {
            if (beniHatirla.Checked == true)
            {
                baglanti.Open();
                SqlCommand command2 = new SqlCommand("INSERT INTO kullaniciHatirla (kullaniciHatirla, sifreHatirla) VALUES (@kullaniciHatirla, @sifreHatirla)", baglanti);
                command2.Parameters.AddWithValue("@kullaniciHatirla", kullaniciAdiCMB.Text);
                command2.Parameters.AddWithValue("@sifreHatirla", sifre.Text);
                command2.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Kaydedildi. Artık Kullanıcı Adınızı Seçerek Giriş Yapabilirsiniz", "Başarılı");
            }


            beniHatirla.Checked = false;

            kullaniciAdiCMB.Text = null;
            sifre.Text = null;
            kullaniciAdiCMB.DataSource = null;
            kullaniciAdiCMB.Items.Clear();
            baglanti.Open();
            SqlCommand command = new SqlCommand("select * from kullaniciHatirla", baglanti);

            SqlDataReader drn = command.ExecuteReader();

            List<string> mevcutOgeler = kullaniciAdiCMB.Items.Cast<string>().ToList();

            while (drn.Read())
            {
                string deger = drn.GetString(1);

                // Eğer öğe zaten varsa ekleme
                if (!mevcutOgeler.Contains(deger))
                {
                    kullaniciAdiCMB.Items.Add(deger);
                }

                if (kullaniciAdiCMB.Text == deger)
                {

                    sifre.Text = drn.GetString(2);
                }
            }

            baglanti.Close();


        }

        private void kullaniciAdiCMB_SelectedIndexChanged(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand command = new SqlCommand("select * from kullaniciHatirla", baglanti);

            SqlDataReader drn = command.ExecuteReader();

            // Mevcut öğeleri al
            List<string> mevcutOgeler = kullaniciAdiCMB.Items.Cast<string>().ToList();

            while (drn.Read())
            {
                string deger = drn.GetString(1);

                // Eğer öğe zaten varsa ekleme
                if (!mevcutOgeler.Contains(deger))
                {
                    kullaniciAdiCMB.Items.Add(deger);
                }

                if (kullaniciAdiCMB.Text == deger)
                {

                    sifre.Text = drn.GetString(2);
                }
            }

            baglanti.Close();

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                DialogResult dialogResult = MessageBox.Show("Silmek İstediğinize Emin misiniz?", "Kullanıcı Bilgilerini Sil", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dialogResult == DialogResult.Yes)
                {
baglanti.Open();
                SqlCommand sqlcmd = new SqlCommand("delete kullaniciHatirla where kullaniciHatirla=@kullaniciHatirla", baglanti);
                sqlcmd.Parameters.AddWithValue("@kullaniciHatirla", kullaniciAdiCMB.Text);
                sqlcmd.ExecuteNonQuery();
                baglanti.Close(); MessageBox.Show("Beni Hatırla Bilgisi Silindi", "Silindi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                    
            }


            checkBox1.Checked = false;
            kullaniciAdiCMB.Text = null;
            sifre.Text = null;
            kullaniciAdiCMB.DataSource = null;
            kullaniciAdiCMB.Items.Clear();
            baglanti.Open();
            SqlCommand command = new SqlCommand("select * from kullaniciHatirla", baglanti);

            SqlDataReader drn = command.ExecuteReader();

            // Mevcut öğeleri al
            List<string> mevcutOgeler = kullaniciAdiCMB.Items.Cast<string>().ToList();

            while (drn.Read())
            {
                string deger = drn.GetString(1);

                // Eğer öğe zaten varsa ekleme
                if (!mevcutOgeler.Contains(deger))
                {
                    kullaniciAdiCMB.Items.Add(deger);
                }

                if (kullaniciAdiCMB.Text == deger)
                {

                    sifre.Text = drn.GetString(2);
                }
            }

            baglanti.Close();
        }
    }
}

