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
    public partial class perakendesatismusteri : Form
    {
        public perakendesatismusteri()
        {
            InitializeComponent();
        }
        public string musteri { get; set; }
        private void Musteri()
        {
            if (!String.IsNullOrEmpty(mus_id.Text))
            {
                this.musteri = mus_id.Text;
                this.Close();
            }
        }
        private void perakendesatismusteri_Load(object sender, EventArgs e)
        {
            OleDbConnection baglan = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source=Data/MarketData.accdb");
            baglan.Open();
            OleDbCommand command = new OleDbCommand();
            command.Connection = baglan;
            command.CommandText = "select* from musteri";
            OleDbDataReader reader = command.ExecuteReader();
            while (reader.Read()) //while’i tüm öğeleri seçmek için kullandık.
            {
                listBox1.Items.Add(reader["mus_id"]);//verileri listboxa ekliyoruz .
            }
            try
            {
                listBox1.SelectedIndex = 0;
            }
            catch (Exception)
            {

                MessageBox.Show("Kayıtlı müşteri bulunamadı veya seçim işleminde bir hata meydana geldi");
            }
           
            baglan.Close(); // en son bağlantıyı kapatıyoruz.
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            OleDbConnection baglan = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source=Data/MarketData.accdb");
            baglan.Open();
            OleDbCommand command = new OleDbCommand();
            command.Connection = baglan;

            command.CommandText = "select * from musteri where mus_id="+listBox1.Text;
            OleDbDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                mus_id.Text = reader["mus_ad"].ToString();

            }
            else
            {
                mus_id.Text = "veri cekilemedi";
            }
            baglan.Close(); // en son bağlantıyı kapatıyoruz.
        }

            private void button1_Click(object sender, EventArgs e)
        {
            Musteri();
        }

    }
    }
