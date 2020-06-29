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
    public partial class duyuruislem : Form
    {
        OleDbConnection con;
        OleDbDataAdapter da;
        OleDbCommand cmd;
        DataSet ds;
        public duyuruislem()
        {
            InitializeComponent();
        }
        void duyurugetir()
        {

            comboBox1.Items.Clear();


            con = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source=Data/MarketData.accdb");
            cmd = new OleDbCommand();
            OleDbDataReader dr;
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "SELECT * FROM duyurular";
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                comboBox1.Items.Add(dr["duyuru_text"]);
               


            }
            con.Close();

        }

        void griddoldur()
        {
            con = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source=Data/MarketData.accdb");
            da = new OleDbDataAdapter("SElect * from duyurular", con);
            ds = new DataSet();
            con.Open();
            duyurugetir();
            con.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            {
                try
                {
                    griddoldur();
                    cmd = new OleDbCommand();
                    con.Open();
                    cmd.Connection = con;
                    cmd.CommandText = "insert into duyurular(duyuru_text) values ('"+textBox1.Text+"')";
                    cmd.ExecuteNonQuery();
                    con.Close();
                    label1.Text = textBox1.Text + " Duyurusu Oluşturuldu.";
                    griddoldur();
                }
                catch (Exception hata)
                {
                    label1.Text = textBox1.Text + "Oluşturulurken bir hata meydana geldi\n" + hata;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                cmd = new OleDbCommand();
                con.Open();
                cmd.Connection = con;
                cmd.CommandText = "delete from duyurular where duyuru_text='" + comboBox1.Text + "'";
                cmd.ExecuteNonQuery();
                con.Close();
                label1.Text = comboBox1.Text + " [Duyurusunu siliyorsunuz]";

                griddoldur();
            }
            catch (Exception silhat)
            {

                label1.Text ="Silme İşleminde Bir Hata Meydana Geldi\n" + silhat;
            }
        }

        private void duyuruislem_Load(object sender, EventArgs e)
        {
            griddoldur();
            duyurugetir();
        }
    }
}

