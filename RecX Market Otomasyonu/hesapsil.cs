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
    public partial class hesapsil : Form
    {
        OleDbConnection con;
        OleDbDataAdapter da;
        OleDbCommand cmd;
       
        public hesapsil()

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
        private void hesapsil_Load(object sender, EventArgs e)
        {
            kullaniciadi();
            this.Size = new Size(323, 137);
            //323; 137
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                con = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source=Data/MarketData.accdb");
                da = new OleDbDataAdapter("SElect *from yonetim", con);
                cmd = new OleDbCommand();
                con.Open();
                cmd.Connection = con;
                cmd.CommandText = "delete from yonetim where ym_kadi = '" + comboBox1.Text + "'";
                cmd.ExecuteNonQuery();
                MessageBox.Show("Silme İşlemi Gerçekleşti");
                this.Close();

            }
            catch (Exception)
            {
                MessageBox.Show("Bir Hata Meydana Geldi");
            }
            

        }
    }
}
