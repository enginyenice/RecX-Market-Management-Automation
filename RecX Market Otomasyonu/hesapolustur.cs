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
    public partial class hesapolustur : Form
    {
        OleDbConnection con;

        OleDbCommand cmd;
        public hesapolustur()
        {
            InitializeComponent();
        }

        private void hesapolustur_Load(object sender, EventArgs e)
        {
            this.Size = new Size(540, 300);
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string veri;
            if (comboBox1.Text == "Yönetici")
            {
                veri = "Yes";

            }
            else
            {
                veri = "No";
            }
            try
            {
                if (textBox1.Text == "" && textBox2.Text == "" && textBox3.Text == "" && textBox4.Text == "" && textBox5.Text =="" && textBox6.Text == "" && comboBox1.Text == "")

                {
                    MessageBox.Show("Alanlar boş bırakılamaz !!");
                }
                else
                { 
                con = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source=Data/MarketData.accdb");
                cmd = new OleDbCommand();
                con.Open();
                cmd.Connection = con;
                cmd.CommandText = "INSERT INTO yonetim(ym_kadi,ym_ksif,ym_ad,ym_soyad,ym_tel,ym_adres,ym_yetki) VALUES ('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text + "','" + textBox6.Text + "'," + veri + ")";
             
                cmd.ExecuteNonQuery();
                    label8.BackColor = Color.DarkGreen;
                label8.Text = textBox3.Text + "  " + textBox4.Text + " Hesabınız Oluşturuldu.\n" + "Hesabınızın Yetki Durumu : " + comboBox1.Text;
                textBox1.Enabled = false;
                textBox2.Enabled = false;
                textBox3.Enabled = false;
                textBox4.Enabled = false;
                textBox5.Enabled = false;
                textBox6.Enabled = false;
                comboBox1.Enabled = false;
                button2.Visible = true;
                button1.Enabled = false;
                con.Close();
            }
            }
            catch (Exception)
            {
                label8.BackColor = Color.DarkRed;
                
                label8.Text = "İşlem yapılırken bir hata meydana geldi.\nAlanları Kontrol ediniz !";

            }
          
        }

        private void button2_Click(object sender, EventArgs e)
        {
            label8.Text = "";
            button1.Enabled = true;
            button2.Visible = false;
            textBox1.Enabled = true;
            textBox2.Enabled = true;
            textBox3.Enabled = true;
            textBox4.Enabled = true;
            textBox5.Enabled = true;
            textBox6.Enabled = true;
            comboBox1.Enabled = true;
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            comboBox1.Text = "";


        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
