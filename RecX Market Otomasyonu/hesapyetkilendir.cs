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
    public partial class hesapyetkilendir : Form
    {
        OleDbConnection con;

        OleDbCommand cmd;
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
        public hesapyetkilendir()
        {
            InitializeComponent();
        }

        private void hesapyetkilendir_Load(object sender, EventArgs e)
        {
            kullaniciadi();
            this.Size = new Size(361, 224);
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string veri;
            if (comboBox2.Text == "Yönetici")
            {
                veri = "Yes";

            }
            else
            {
                veri = "No";
            }
            try
            {
                cmd = new OleDbCommand();
                con.Open();
                cmd.Connection = con;
                cmd.CommandText = "update yonetim set ym_yetki = " + veri + " where ym_kadi = '" + comboBox1.Text + "'";
                cmd.ExecuteNonQuery();
                label3.Text = "Yetkilendirme işlemi başarıyla gerçekleştirildi\nKullanıcı : "+comboBox1.Text+" Yetki : "+comboBox2.Text;
                comboBox1.Enabled = false;
                comboBox2.Enabled = false;

                button2.Visible = true;
                button1.Visible = false;
                con.Close();
            }
            catch (Exception)
            {
                label3.Text = "Bir hata meydana geldi";

            }
          

        }

        private void button2_Click(object sender, EventArgs e)
        {
            comboBox1.Text = "";
            comboBox2.Text = "";
            comboBox1.Enabled = true;
            comboBox2.Enabled = true;
            button1.Visible = true;
            button2.Visible = false;
            label3.Text = "";

        }
    }
}
