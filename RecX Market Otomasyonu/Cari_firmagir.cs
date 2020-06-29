using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.IO;

namespace RecX_Market_Otomasyonu
{
    public partial class Cari_firmagir : Form
    {
        public Home home;
        public Cari_firmagir()
        {
            InitializeComponent();
        }

        string dosyaYolu;
        string dosyaAdi;
        private void textBox10_TextChanged(object sender, EventArgs e)
        {

        }


        private void button3_Click(object sender, EventArgs e)
        {


            try
            {
                dosyaAdi = Path.GetFileName(dosyaYolu); //Dosya adını alma
                string kaynak = dosyaYolu;
                string hedef = Application.StartupPath + @"\Data\img\Musteri/";
                try
                {

                    string yeniad = Guid.NewGuid() + ".jpg"; //Benzersiz isim verme
                    File.Copy(kaynak, hedef + yeniad);
                    mus_fotoyol.Text = hedef + yeniad;
                }
                catch
                {
                    //Boş Resim Seçilen Müşterilerde Belirli bir foto gelmesi için
                    string boshedef = Application.StartupPath + @"/Data/img\AppIMG/";
                    mus_fotoyol.Text = boshedef + "musteri.png";

                }

                string vtyolu = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Data/MarketData.accdb;Persist Security Info=True";
                OleDbConnection baglanti = new OleDbConnection(vtyolu);
                baglanti.Open();
                string ekle = "insert into musteri ( mus_fir,mus_ad, mus_unvan, mus_tel1, mus_cep, mus_fotoyol, mus_email, mus_tcno, mus_dogtar, mus_mahkoy)" +
                    " values                        (@mus_fir,@mus_ad,@mus_unvan,@mus_tel1,@mus_cep,@mus_fotoyol,@mus_email,@mus_tcno,@mus_dogtar,@mus_mahkoy)";
                OleDbCommand komut = new OleDbCommand(ekle, baglanti);
                komut.Parameters.AddWithValue("@mus_fir", mus_fir.Text);
                komut.Parameters.AddWithValue("@mus_ad", mus_ad.Text);
                komut.Parameters.AddWithValue("@mus_unvan", mus_unvan.Text);
                komut.Parameters.AddWithValue("@mus_tel1", mus_tel1.Text);
                //komut.Parameters.AddWithValue("@mus_tel2", mus_tel2.Text);
                komut.Parameters.AddWithValue("@mus_cep", mus_cep.Text);
               // komut.Parameters.AddWithValue("@mus_faks", mus_faks.Text);
               // komut.Parameters.AddWithValue("@mus_bakiye", mus_bakiye.Text);
                komut.Parameters.AddWithValue("@mus_fotoyol", mus_fotoyol.Text);
            //    komut.Parameters.AddWithValue("@mus_iskonto", mus_iskonto.Text);
                komut.Parameters.AddWithValue("@mus_email", mus_email.Text);
               // komut.Parameters.AddWithValue("@mus_web", mus_web.Text);
              //  komut.Parameters.AddWithValue("@mus_verda", mus_verda.Text);
               // komut.Parameters.AddWithValue("@mus_verno", mus_verno.Text);
                komut.Parameters.AddWithValue("@mus_tcno", mus_tcno.Text);

              //  komut.Parameters.AddWithValue("@mus_serno", mus_serno.Text);
              //  komut.Parameters.AddWithValue("@mus_anne", mus_anne.Text);
              //  komut.Parameters.AddWithValue("@mus_baba", mus_baba.Text);
             //   komut.Parameters.AddWithValue("@mus_dogyer", mus_dogyer.Text);
               komut.Parameters.AddWithValue("@mus_dogtar", dateTimePicker1.Value);
              //  komut.Parameters.AddWithValue("@mus_medhal", mus_medhal.Text);
                komut.Parameters.AddWithValue("@mus_mahkoy", mus_mahkoy.Text);
             //   komut.Parameters.AddWithValue("@mus_ciltno", mus_ciltno.Text);
             //   komut.Parameters.AddWithValue("@mus_ailesırano", mus_ailesırano.Text);
             //   komut.Parameters.AddWithValue("@mus_sırano", mus_sırano.Text);
             //   komut.Parameters.AddWithValue("@mus_veryer", mus_veryer.Text);



                komut.ExecuteNonQuery();

                MessageBox.Show(mus_ad.Text +" "+mus_unvan.Text + " Müşterisi Kayıt Edildi.");
                this.Close();

            }
            catch (Exception)
            {

                MessageBox.Show("İşlem Gerçekleştirilemedi !!!");
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {


            OpenFileDialog dosya = new OpenFileDialog();
            dosya.Filter = "Resim Dosyası |*.jpg;*.nef;*.png |  Tüm Dosyalar |*.*";
            dosya.ShowDialog();
            dosyaYolu = dosya.FileName;
            pictureBox1.ImageLocation = dosyaYolu;

        }



        private void mus_fir_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void mus_tel1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }


        private void mus_cep_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }


        private void mus_tcno_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }


        private void Cari_firmagir_Load(object sender, EventArgs e)
        {

        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void mus_tel1_TextChanged(object sender, EventArgs e)
        {

        }

        private void mus_verno_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void mus_iskonto_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void bilgi_Enter(object sender, EventArgs e)
        {

        }

        private void label22_Click(object sender, EventArgs e)
        {

        }

        private void mus_mahkoy_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
