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
    public partial class Kategoriislem : Form
    {
        OleDbConnection con;
        OleDbDataAdapter da;
        OleDbCommand cmd;
        DataSet ds;
        public Kategoriislem()
        {
            InitializeComponent();
        }

        void griddoldur()
        {
            con = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source=Data/MarketData.accdb");
            da = new OleDbDataAdapter("SElect *from kategori", con);
            ds = new DataSet();
            con.Open();

            con.Close();
            kategorigetir();
            
        }
        void kategorigetir()
        {

            comboBox1.Items.Clear();
            comboBox2.Items.Clear();

            con = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source=Data/MarketData.accdb");
            cmd = new OleDbCommand();
            OleDbDataReader dr;
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "SELECT * FROM kategori";
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                comboBox1.Items.Add(dr["kategori_adi"]);
                comboBox2.Items.Add(dr["kategori_adi"]);


            }
            con.Close();

        }

    
            private void Kategoriislem_Load(object sender, EventArgs e)
            {
            griddoldur();
            //459; 254
             this.Size = new Size(459, 254);

            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;
        }

        private void button1_Click(object sender, EventArgs e)
            {
            try
            {
                cmd = new OleDbCommand();
                con.Open();
                cmd.Connection = con;
                cmd.CommandText = "insert into kategori (kategori_adi) values ('" + textBox1.Text + "')";
                cmd.ExecuteNonQuery();
                con.Close();
                label2.Text = textBox1.Text + " Adın bir kategori oluşturuldu";
                griddoldur();
            }
            catch (Exception hata)
            {
                label2.Text = textBox1.Text + "Oluşturulurken bir hata meydana geldi\n" +hata;
            }
            
            

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                cmd = new OleDbCommand();
                con.Open();
                cmd.Connection = con;
                cmd.CommandText = "update kategori set kategori_adi='" + textBox2.Text + "' where kategori_adi='" + comboBox1.Text + "'";
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show(comboBox1.Text+" İsimli kategori "+ textBox2.Text+" Şeklinde değiştirilmiştir");
                griddoldur();
            }
            catch (Exception gunhat)
            {

                MessageBox.Show("Değiştirme işleminde hata oluştu" +gunhat);
            }
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                cmd = new OleDbCommand();
                con.Open();
                cmd.Connection = con;
                cmd.CommandText = "delete from kategori where kategori_adi='" + comboBox2.Text + "'";
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show(comboBox2.Text + " İsimli kategori silindi");
                griddoldur();
            }
            catch (Exception silhat)
            {

                MessageBox.Show("Silme İşleminde Bir Hata Meydana Geldi\n"+silhat);
            }

            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = true;
            groupBox1.Location = new Point(12, 67);
            groupBox2.Visible = false;
            groupBox3.Visible = false;
            label2.Text = "";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            groupBox2.Visible = true;
            groupBox2.Location = new Point(12, 67);
            groupBox1.Visible = false;
            groupBox3.Visible = false;
            label2.Text = "";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            groupBox3.Visible = true;
            groupBox3.Location = new Point(12, 67);
            groupBox1.Visible = false;
            groupBox2.Visible = false;
            label2.Text = "";
        }
    }
    }

