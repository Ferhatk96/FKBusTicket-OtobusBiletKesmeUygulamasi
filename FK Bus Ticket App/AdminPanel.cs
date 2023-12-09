using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace FK_Bus_Ticket_App
{
    public partial class AdminPanel : Form
    {
        public AdminPanel()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=.;Initial Catalog=FKBussTicketDB;Integrated Security=True");

        public string kullaniciVarmi;

        public void VeriListele()
        {
            baglanti.Open();
            da = new SqlDataAdapter("Select * from MusteriBiletKayit", baglanti);
            cmdb = new SqlCommandBuilder(da);
            ds = new DataSet();
            da.Fill(ds, "MusteriBiletKayit");
            dataGridView2.DataSource = ds.Tables[0];
            baglanti.Close();


            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            kayitGetir();
            dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            
          
            // MusterikayitGetir();
            //
            baglanti.Open();

            using (SqlCommand comman = new SqlCommand("SELECT kazanc FROM muhasebe WHERE id=@id", baglanti))
            {
                comman.Parameters.AddWithValue("@id", 2);

                using (SqlDataReader reader = comman.ExecuteReader())
                {
                    if (reader.Read()) // Veri bulundu mu kontrolü
                    {
                        // Alanları değişkenlere ata
                        kazanc.Text = $"{reader["kazanc"]} TL";

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

        public double Marmarafiyat;
        public double EgeFiyat;
        public double AkdenizFiyat;
        public double IcAnadoluFiyat;
        public double KaradenizFiyat;
        public double DoguAnadoluFİyat;
        public double GuneyAnadoluFiyat;
        public string kullaniciAd;
        public string kullaniciSifre;
        public bool kaydetbool;
        public bool guncellebool;
        public bool silbool;
        SqlDataAdapter da;
        DataSet ds;
        SqlCommandBuilder cmdb;
        
        private void AdminPanel_Load(object sender, EventArgs e)
        {

            baglanti.Close();

            baglanti.Open();
            da = new SqlDataAdapter("Select * from MusteriBiletKayit", baglanti);
            cmdb = new SqlCommandBuilder(da);
            ds = new DataSet();
            da.Fill(ds, "MusteriBiletKayit");
            dataGridView2.DataSource = ds.Tables[0];
            baglanti.Close();


            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            kayitGetir();
            //dataGridView1.Columns["id"].Visible = false;
            dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView2.Columns["idbtn"].Visible = false;  // gizlenecek kolon
            dataGridView2.Columns["id"].Visible = false;

           // dataGridView2.Columns["id"].HeaderText = "ID";
            dataGridView2.Columns["adSoyad"].HeaderText = "Ad Soyad";
            dataGridView2.Columns["adres"].HeaderText = "Adres";
            dataGridView2.Columns["telefon"].HeaderText = "Telefon";
            dataGridView2.Columns["tcKimlik"].HeaderText = "TC Kimlik";
            dataGridView2.Columns["dogumTarihi"].HeaderText = "Doğum Tarihi";
            dataGridView2.Columns["cinsiyet"].HeaderText = "Cinsiyet";
            dataGridView2.Columns["ogrenci"].HeaderText = "Öğrenci mi?";
            dataGridView2.Columns["kalkisYeri"].HeaderText = "Kalkış Yeri";
            dataGridView2.Columns["varisYeri"].HeaderText = "Varış Yeri";
            dataGridView2.Columns["yolTarihi"].HeaderText = "Yol Tarihi";
            dataGridView2.Columns["saat"].HeaderText = "Saat";
            dataGridView2.Columns["fiyat"].HeaderText = "Fİyat";
            // MusterikayitGetir();
            //
            baglanti.Open();

            using (SqlCommand comman = new SqlCommand("SELECT kazanc FROM muhasebe WHERE id=@id", baglanti))
            {
                comman.Parameters.AddWithValue("@id", 2);

                using (SqlDataReader reader = comman.ExecuteReader())
                {
                    if (reader.Read()) // Veri bulundu mu kontrolü
                    {
                        // Alanları değişkenlere ata
                        kazanc.Text = $"{reader["kazanc"]} TL";

                        // Şimdi bu değişkenleri kullan


                    }

                    else
                    {

                        MessageBox.Show("Veri bulunamadı veya okuma hatası oluştu.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }

            baglanti.Close();
            //
            baglanti.Open();

            string sorgu1 = "select * from  FiyatListesi WHERE id = @id";
            SqlCommand cmd1 = new SqlCommand(sorgu1, baglanti);
            cmd1.Parameters.AddWithValue("@id", "1");

            SqlDataReader dr;
            dr = cmd1.ExecuteReader();

            while (dr.Read())
            {
                marmaratxt.Text = Convert.ToString(dr.GetValue(1).ToString());
                egetxt.Text = Convert.ToString(dr.GetValue(2).ToString());
                akdeniztxt.Text = Convert.ToString(dr.GetValue(3).ToString());
                icanadolutxt.Text = Convert.ToString(dr.GetValue(4).ToString());
                karadeniztxt.Text = Convert.ToString(dr.GetValue(5).ToString());
                doguanadolutxt.Text = Convert.ToString(dr.GetValue(6).ToString());
                guneydogutxt.Text = Convert.ToString(dr.GetValue(7).ToString());



            }
            baglanti.Close();
            Marmarafiyat = Convert.ToDouble(marmaratxt.Text);
            EgeFiyat = Convert.ToDouble(egetxt.Text);
            AkdenizFiyat = Convert.ToDouble(akdeniztxt.Text);
            IcAnadoluFiyat = Convert.ToDouble(icanadolutxt.Text);
            KaradenizFiyat = Convert.ToDouble(karadeniztxt.Text);
            DoguAnadoluFİyat = Convert.ToDouble(doguanadolutxt.Text);
            GuneyAnadoluFiyat = Convert.ToDouble(guneydogutxt.Text);


        }
        private void kayitGetir()
        {
            baglanti.Open();
            string kayit = "SELECT * from Giris";
            //musteriler tablosundaki tüm kayıtları çekecek olan sql sorgusu.
            SqlCommand komut = new SqlCommand(kayit, baglanti);
            //Sorgumuzu ve baglantimizi parametre olarak alan bir SqlCommand nesnesi oluşturuyoruz.
            SqlDataAdapter da = new SqlDataAdapter(komut);
            //SqlDataAdapter sınıfı verilerin databaseden aktarılması işlemini gerçekleştirir.
            DataTable dt = new DataTable();
            da.Fill(dt);
            //Bir DataTable oluşturarak DataAdapter ile getirilen verileri tablo içerisine dolduruyoruz.
            dataGridView1.DataSource = dt;
            //Formumuzdaki DataGridViewin veri kaynağını oluşturduğumuz tablo olarak gösteriyoruz.
            baglanti.Close();
        }
        private void MusterikayitGetir()
        {
            baglanti.Open();
            string kayit = "SELECT * from MusteriBiletKayit";
            //musteriler tablosundaki tüm kayıtları çekecek olan sql sorgusu.
            SqlCommand komut = new SqlCommand(kayit, baglanti);
            //Sorgumuzu ve baglantimizi parametre olarak alan bir SqlCommand nesnesi oluşturuyoruz.
            SqlDataAdapter da = new SqlDataAdapter(komut);
            //SqlDataAdapter sınıfı verilerin databaseden aktarılması işlemini gerçekleştirir.
            DataTable dt = new DataTable();
            da.Fill(dt);
            //Bir DataTable oluşturarak DataAdapter ile getirilen verileri tablo içerisine dolduruyoruz.
            dataGridView2.DataSource = dt;
            //Formumuzdaki DataGridViewin veri kaynağını oluşturduğumuz tablo olarak gösteriyoruz.
            baglanti.Close();
        }


        private void FiyatGuncelle_Click(object sender, EventArgs e)
        {

            DialogResult dialogResult = MessageBox.Show("Güncellemek İstediğinize Emin misiniz?", "Bölge Bilet Fiyatları Güncelle", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {

                try
                {
                    baglanti.Open();

                    string sorgu = "UPDATE FiyatListesi SET MarmaraFiyat = @MarmaraFiyat, EgeFiyat = @EgeFiyat, AkdenizFiyat = @AkdenizFiyat, IcAnadoluFiyat = @IcAnadoluFiyat, KaradenizFiyat = @KaradenizFiyat, DoguAnadoluFİyat = @DoguAnadoluFİyat, GuneyAnadoluFiyat = @GuneyAnadoluFiyat WHERE id = @id";
                    SqlCommand cmd = new SqlCommand(sorgu, baglanti);


                    // Burada ID değerini güncellenen kaydın birincil anahtarıyla değiştirmeniz gerekiyor.
                    cmd.Parameters.AddWithValue("@id", "1"); 

                    cmd.Parameters.AddWithValue("@MarmaraFiyat", Convert.ToDouble(marmaratxt.Text));
                    cmd.Parameters.AddWithValue("@EgeFiyat", Convert.ToDouble(egetxt.Text));
                    cmd.Parameters.AddWithValue("@AkdenizFiyat", Convert.ToDouble(akdeniztxt.Text));
                    cmd.Parameters.AddWithValue("@IcAnadoluFiyat", Convert.ToDouble(icanadolutxt.Text));
                    cmd.Parameters.AddWithValue("@KaradenizFiyat", Convert.ToDouble(karadeniztxt.Text));
                    cmd.Parameters.AddWithValue("@DoguAnadoluFİyat", Convert.ToDouble(doguanadolutxt.Text));
                    cmd.Parameters.AddWithValue("@GuneyAnadoluFiyat", Convert.ToDouble(guneydogutxt.Text));

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Bölge Fiyat Güncellemesi Gerçekleşti", "Fiyat Güncelle");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Bölge Fiyatı Güncellenirken Hata Oluştu: " + ex.Message);
                }
                finally
                {
                    baglanti.Close();
                }

            }
            else
            {

            }

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
            dataGridView1.Columns["id"].HeaderText = "ID";
            dataGridView1.Columns["kullaniciAdi"].HeaderText = "Kullanıcı Adı";
            dataGridView1.Columns["sifre"].HeaderText = "Şifre";
            dataGridView1.Columns["kayitYetki"].HeaderText = "Kayıt Yetki";
            dataGridView1.Columns["guncellemeYetki"].HeaderText = "Güncelleme Yetki";
            dataGridView1.Columns["silmeYetki"].HeaderText = "Silme Yetki";
            try
            {
                id.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                kullaniciAdi.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                sifre.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                kayit.Checked = Convert.ToBoolean(dataGridView1.CurrentRow.Cells[3].Value.ToString());
                guncelleme.Checked = Convert.ToBoolean(dataGridView1.CurrentRow.Cells[4].Value.ToString());
                silme.Checked = Convert.ToBoolean(dataGridView1.CurrentRow.Cells[5].Value.ToString());
            }
            catch
            {

            }
        }



        private void txtsil_Click(object sender, EventArgs e)
        {

            kullaniciAdi.Text = null;
            sifre.Text = null;
            kayit.Checked = false;
            guncelleme.Checked = false;
            silme.Checked = false;
        }


        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void button3_Click(object sender, EventArgs e)
        {

            DialogResult dialogResult = MessageBox.Show("Güncellemek İstediğinize Emin misiniz?", "Müşteri Bilet Güncelle", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {
                da.Fill(ds, "MusteriBiletKayit");
                da.Update(ds, "MusteriBiletKayit");
                AdminPanel_Load(sender, e);
                MessageBox.Show("Müşteri Kaydı Güncellendi!", "Güncelle");
            }
            else
            {

            }
        }


        private void button4_Click(object sender, EventArgs e)
        {


            DialogResult dialogResult = MessageBox.Show("Silmek İstediğinize Emin misiniz?", "Müşteri Sil", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {
                //
                baglanti.Open();
                try
                {
                    // Seçili bir satır var mı kontrol et
                    if (dataGridView2.CurrentRow != null)
                    {
                        int id = Convert.ToInt32(dataGridView2.CurrentRow.Cells[0].Value.ToString());

                        // MüşteriBiletKayit tablosundan veriyi sil
                        SqlCommand sql = new SqlCommand("delete from MusteriBiletKayit where id=@id", baglanti);
                        sql.Parameters.AddWithValue("@id", id);
                        sql.ExecuteNonQuery();

                        // ButonRenkleriDB tablosunda silinecek veri varsa, sil
                        string butonAdiAl = dataGridView2.CurrentRow.Cells[13].Value?.ToString();
                        if (!string.IsNullOrEmpty(butonAdiAl))
                        {
                            SqlCommand VeriSil = new SqlCommand("Delete from ButonRenkleriDB where butonAdi=@butonAdi", baglanti);
                            VeriSil.Parameters.AddWithValue("@butonAdi", butonAdiAl);
                            VeriSil.ExecuteNonQuery();
                        }

                        MessageBox.Show("Müşteri Silindi", "Silindi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        AdminPanel_Load(sender, e);

                        return;

                    }
                    else
                    {
                        MessageBox.Show("Lütfen bir satır seçiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    AdminPanel_Load(sender, e);
                }
                finally
                {
                    AdminPanel_Load(sender, e);
                    baglanti.Close();
                }
                AdminPanel_Load(sender, e);
                //



            }

            else
            {
            }

        }

        private void printPreviewDialog1_Load(object sender, EventArgs e)
        {

        }
        StringFormat strFormat;
        ArrayList arrColumnLefts = new ArrayList();
        ArrayList arrColumnWidths = new ArrayList();
        int iCellHeight = 0;
        int iTotalWidth = 0;
        int iRow = 0;
        bool bFirstPage = false;
        bool bNewPage = false;
        int iHeaderHeight = 0;
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {

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
            // e.Graphics.DrawString("Otobüs Plaka:", font, firca, 60, 750);
            e.Graphics.DrawString("Bilet Tutarı:", font, firca, 60, 750);
            e.Graphics.DrawString("***************************************************************************************************", font, firca, 0, 830);
            e.Graphics.DrawString("İyi Yolculuklar Dileriz..", font, firca, 310, 850);
            // if(dataGridView1.SelectAll==true)
            font = new Font("Arial", 15, FontStyle.Bold);
            e.Graphics.DrawString(dataGridView2.CurrentRow.Cells[1].Value.ToString(), font, firca, 250, 150);
            //e.Graphics.DrawString(adreskyttxtbx.Text, font, firca, 250, 200);
            // string kisalmisAdres = adreskyttxtbx.Text.Length <= 100 ? adreskyttxtbx.Text : adreskyttxtbx.Text.Substring(0,0);
            // e.Graphics.DrawString(kisalmisAdres, font, firca, 250, 200);
            int genislik = 420; // Metni sığdırmak istediğiniz genişlik
            string adres = dataGridView2.CurrentRow.Cells[2].Value.ToString();
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

            e.Graphics.DrawString(dataGridView2.CurrentRow.Cells[3].Value.ToString(), font, firca, 250, 300);
            e.Graphics.DrawString(dataGridView2.CurrentRow.Cells[4].Value.ToString(), font, firca, 250, 350);
            e.Graphics.DrawString(dataGridView2.CurrentRow.Cells[5].Value.ToString(), font, firca, 250, 400);
            e.Graphics.DrawString(dataGridView2.CurrentRow.Cells[6].Value.ToString(), font, firca, 250, 450);
            e.Graphics.DrawString(dataGridView2.CurrentRow.Cells[7].Value.ToString(), font, firca, 250, 500);
            e.Graphics.DrawString(dataGridView2.CurrentRow.Cells[8].Value.ToString(), font, firca, 250, 550);
            e.Graphics.DrawString(dataGridView2.CurrentRow.Cells[9].Value.ToString(), font, firca, 250, 600);
            e.Graphics.DrawString(dataGridView2.CurrentRow.Cells[10].Value.ToString() + "   " + "Kalkış Saati: " + dataGridView2.CurrentRow.Cells[11].Value.ToString(), font, firca, 250, 650);

            e.Graphics.DrawString(dataGridView2.CurrentRow.Cells[12].Value.ToString(), font, firca, 250, 700);
            // e.Graphics.DrawString(kayitGET.otoPlaka., font, firca, 250, 700); ;
            // e.Graphics.DrawString(, font, firca, 250, 750);
            e.Graphics.DrawString(dataGridView2.CurrentRow.Cells[14].Value.ToString()+" TL", font, firca, 250, 750);

        }



        private void yazdir_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count == 0)
            {MessageBox.Show("Yazdırılacak Veriyi Seçin", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
 

            }

            else { printPreviewDialog1.ShowDialog();
                return;

            }
           
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Form1 form = new Form1();
            form.Show();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            using (SqlConnection baglanti = new SqlConnection("Data Source=.;Initial Catalog=FKBussTicketDB;Integrated Security=True"))
            {
                baglanti.Open();
                try
                {
                    SqlCommand oku = new SqlCommand("SELECT COUNT(*) FROM Giris WHERE LOWER(kullaniciAdi) = LOWER(@kullaniciAdi)\r\n", baglanti);
                    oku.Parameters.AddWithValue("@kullaniciAdi", kullaniciAdi.Text);

                    int kullaniciSayisi = (int)oku.ExecuteScalar();

                    if (kullaniciSayisi > 0)
                    {
                        MessageBox.Show("Bu Kullanıcı Zaten Var!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    else
                    {

                        MessageBox.Show("Kullanıcı eklendi");
                    }

                    string sorgu = "insert into Giris (kullaniciAdi, sifre, kayitYetki, guncellemeYetki, silmeYetki) values(@kullaniciAdi, @sifre, @kayitYetki, @guncellemeYetki, @silmeYetki)";
                    SqlCommand cmd = new SqlCommand(sorgu, baglanti);
                    cmd.Parameters.AddWithValue("@kullaniciAdi", kullaniciAdi.Text);
                    cmd.Parameters.AddWithValue("@sifre", sifre.Text);
                    cmd.Parameters.AddWithValue("@kayitYetki", kayit.Checked);
                    cmd.Parameters.AddWithValue("@guncellemeYetki", guncelleme.Checked);
                    cmd.Parameters.AddWithValue("@silmeYetki", silme.Checked);

                    // Burada sorguyu gerçekleştir
                    int etkilenenSatirSayisi = cmd.ExecuteNonQuery();
                    kullaniciAd = kullaniciAdi.Text;

                    // Kullanıcı adının ve şifrenin boş olup olmadığını kontrol et
                    if (!string.IsNullOrWhiteSpace(kullaniciAdi.Text) && !string.IsNullOrWhiteSpace(sifre.Text))
                    {

                        baglanti.Close();
                    }
                }
                finally
                {
                    // Bağlantıyı her durumda kapat
                    if (baglanti.State == ConnectionState.Open)
                    {
                        baglanti.Close();
                    }
                }
                AdminPanel_Load(sender, e);
                baglanti.Close();
            }
        }

        private void KullaniciGuncelle_Click_1(object sender, EventArgs e)
        {
            if (Convert.ToInt16(id.Text) == 1009)
            {
                MessageBox.Show("Admin Güncellenemez!","Uyarı",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
            else
            {

                DialogResult dialogResult = MessageBox.Show("Güncellemek İstediğinize Emin misiniz?", "Kulanıcı Bilgilerini Güncelle", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dialogResult == DialogResult.Yes)
                {
                    baglanti.Open();
                    SqlCommand guncellecmd = new SqlCommand("update Giris Set KullaniciAdi=@KullaniciAdi, sifre=@sifre, kayitYetki=@kayitYetki, guncellemeYetki=@guncellemeYetki,silmeYetki=@silmeYetki where id=@id", baglanti);
                    guncellecmd.Parameters.AddWithValue("@id", id.Text);
                    guncellecmd.Parameters.AddWithValue("@kullaniciAdi", kullaniciAdi.Text);
                    guncellecmd.Parameters.AddWithValue("@sifre", sifre.Text);
                    guncellecmd.Parameters.AddWithValue("@kayitYetki", kayit.Checked);
                    guncellecmd.Parameters.AddWithValue("@guncellemeYetki", guncelleme.Checked);
                    guncellecmd.Parameters.AddWithValue("@silmeYetki", silme.Checked);
                    guncellecmd.ExecuteNonQuery();
                    baglanti.Close();
                    AdminPanel_Load(sender, e); // güncellemeden sonra datagrid yenilenir.
                    MessageBox.Show("Kullanıcı Bilgileri Güncellendi!", "Kullanıcı Güncelle");
                }
                else
                {

                }

            }
        }

        private void KullaniciSil_Click_1(object sender, EventArgs e)
        {
            if (Convert.ToInt16(id.Text) == 1009)
            {
                MessageBox.Show("Admin Silinemez!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {

                DialogResult dialogResult = MessageBox.Show("Silmek İstediğinize Emin misiniz?", "Kullanıcı Sil", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dialogResult == DialogResult.Yes)
                {
                    baglanti.Open();
                    SqlCommand sql = new SqlCommand("delete from Giris where id=@id", baglanti);
                    sql.Parameters.AddWithValue("@id", id.Text);
                    sql.ExecuteNonQuery();
                    baglanti.Close();
                    AdminPanel_Load(sender, e);
                    MessageBox.Show("Kullanıcı Silindi.", "Kullanıcı Sil", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {

                }

            }

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }
    }
}
