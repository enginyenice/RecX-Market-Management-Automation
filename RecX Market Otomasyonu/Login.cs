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
    public partial class Login : Form
    {
        public Login()
        {

            InitializeComponent();
        }
        OleDbConnection con;
        OleDbCommand cmd;
        OleDbDataReader dr;
        private void BtnGiris_Click(object sender, EventArgs e)
        {
            string ad = TxtID.Text;
            string sifre = TxtPass.Text;
            string ym_ad = "", ym_soyad = "";
            string ym_yetki = "";
            try
            {

                con = new OleDbConnection(@"Provider=Microsoft.ACE.Oledb.12.0;Data Source=Data/MarketData.accdb");
                con.Open();
                cmd = new OleDbCommand();
                cmd.Connection = con;
                cmd.CommandText = "SELECT * FROM yonetim where ym_kadi='" + TxtID.Text + "' AND ym_ksif='" + TxtPass.Text + "'";
            }
            catch (Exception gata)
            {

                MessageBox.Show("Bağlanamadı"+ gata);
              
            }
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                ym_ad = dr["ym_ad"].ToString();
                ym_soyad = dr["ym_soyad"].ToString();
                ym_yetki = dr["ym_yetki"].ToString();
                Home home = new Home();
                home.label1.Text = TxtID.Text;
                home.label3.Text = ym_ad + " " + ym_soyad;
                home.label5.Text = ym_yetki;

                home.Show();
                this.Hide();
            }
            else
            {
                label3.Text = "Kullanıcı adı ya da şifre hatalı !";
            }     
            con.Close();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            this.Size = new Size(310, 360);
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;
        }

        private void BtnGiris_MouseHover(object sender, EventArgs e)
        {
            BtnGiris.ForeColor = Color.White;
            BtnGiris.BackColor = Color.Green;
        }

        private void BtnGiris_MouseLeave(object sender, EventArgs e)
        {
            BtnGiris.ForeColor = Color.Black;
            BtnGiris.BackColor = Color.Transparent;

        }

    }
}
