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

namespace RecX_Market_Otomasyonu
{
    public partial class Sbilgi : Form
    {
        OleDbConnection con;
        OleDbDataAdapter da;
        OleDbCommand cmd;
        DataSet ds;
        public Sbilgi()
        {
            InitializeComponent();
        }

        void griddoldur()
        {
            con = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source=Data/MarketData.accdb");
            da = new OleDbDataAdapter("SElect *from sirket", con);
            ds = new DataSet();
            con.Open();
            da.Fill(ds, "sirket");
            con.Close();
        }


        private void label6_Click(object sender, EventArgs e)
        {

        }



        private void Sbilgi_Load(object sender, EventArgs e)
        {
 
            griddoldur();
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            /* Verileri Güncelleme */

            try
            {

                cmd = new OleDbCommand();
                con.Open();
                cmd.Connection = con;
                cmd.CommandText = "update sirket set s_sk='" + s_sk.Text + "',s_ad='" + s_ad.Text
                    + "',s_tel='" + s_tel.Text + "',s_faks='" + s_faks.Text + "',s_mail='"
                    + s_mail.Text + "',s_web='" + s_web.Text + "',s_adres='" + s_adres.Text
                    + "',s_sehir='" + s_sehir.Text + "' where s_id=" + "1" + "";
                cmd.ExecuteNonQuery();
                con.Close();
                griddoldur();
                MessageBox.Show("Kayıt İşlemi Gerçekleşti");



            }
            catch (Exception)
            {
                MessageBox.Show("Bir Hata Oluştu");

            }




        }

        private void button4_Click(object sender, EventArgs e)
        {

            /* Verileri TextBox Getirme */
       
                con.Open();
                OleDbCommand cmd = new OleDbCommand("Select s_sk, s_ad, s_tel,s_faks,s_mail,s_web,s_adres,s_sehir from sirket where s_id=1", con);
                OleDbDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    s_sk.Text = dr["s_sk"].ToString();
                    s_ad.Text = dr["s_ad"].ToString();
                    s_tel.Text = dr["s_tel"].ToString();
                    s_faks.Text = dr["s_faks"].ToString();
                    s_mail.Text = dr["s_mail"].ToString();
                    s_web.Text = dr["s_web"].ToString();
                    s_adres.Text = dr["s_adres"].ToString();
                    s_sehir.Text = dr["s_sehir"].ToString();

                }
                con.Close();
                griddoldur();

            




        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnTemizle_MouseHover(object sender, EventArgs e)
        {
            BtnTemizle.ForeColor = Color.Red;

        }

        private void BtnTemizle_MouseLeave(object sender, EventArgs e)
        {
            BtnTemizle.ForeColor = Color.Black;
        }

        private void BtnKaydet_MouseHover(object sender, EventArgs e)
        {
            BtnKaydet.ForeColor = Color.Red;
        }

        private void BtnKaydet_MouseLeave(object sender, EventArgs e)
        {
            BtnKaydet.ForeColor = Color.Black;
        }

        private void button4_MouseHover(object sender, EventArgs e)
        {
            button4.ForeColor = Color.Red;
        }

        private void button4_MouseLeave(object sender, EventArgs e)
        {
            button4.ForeColor = Color.Black;
        }

        private void button5_MouseHover(object sender, EventArgs e)
        {
            button5.ForeColor = Color.Red;
        }

        private void button5_MouseLeave(object sender, EventArgs e)
        {
            button5.ForeColor = Color.Black;
        }

        private void BtnTemizle_Click(object sender, EventArgs e)
        {

            /* TextBox Temizleme */
            foreach (Control item in this.Controls)

            {

                if (item is TextBox)
                {
                    TextBox tbox = (TextBox)item;

                    tbox.Clear();

                }

            }

        }

        private void s_sk_TextChanged(object sender, EventArgs e)
        {

        }

    }

}

