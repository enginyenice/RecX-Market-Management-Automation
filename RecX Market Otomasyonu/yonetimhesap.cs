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
    public partial class yonetimhesap : Form
    {
        OleDbConnection con;

        OleDbCommand cmd;


        public yonetimhesap()
        {
            InitializeComponent();
        }

            void kullaniciadi()
            {
                comboBox1.Items.Clear();
             
                con = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source=Data/MarketData.accdb");
                cmd = new OleDbCommand();
                OleDbDataReader dr;
                con.Open();
                cmd.Connection = con;
                cmd.CommandText = "SELECT * FROM yonetim";
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    comboBox1.Items.Add(dr["ym_kadi"]);
             


                }
                con.Close();

            }
        
        private void yonetimhesap_Load(object sender, EventArgs e)
        {
            kullaniciadi();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                con = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source=Data/MarketData.accdb");
                cmd = new OleDbCommand();
                con.Open();
                cmd.Connection = con;
                cmd.CommandText = "UPDATE yonetim SET ym_ksif='" + textBox2.Text + "' WHERE ym_kadi='" + comboBox1.Text + "'";
                cmd.ExecuteNonQuery();
                label3.Text = "Şifreniz başarıyla güncellendi";
                con.Close();

            }
            catch (Exception)
            {
                con.Close();
                label3.Text = "Şifreniz güncelleştirilirken bir hata meydana geldi !";

            }

        }
    }
}

