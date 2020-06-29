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
    public partial class Home : Form
    {
        public Menu menu;
        public Home()
        {  
            menu = new Menu();
            InitializeComponent();
    }
        public OleDbConnection bag = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source=Data/MarketData.accdb");
        public DataTable yonetim = new DataTable();
        public DataTable musteri = new DataTable();
        public DataTable sirket = new DataTable();
        public OleDbCommand kmt = new OleDbCommand();//acces komut kullanımı değişkeni tanımlanıyor.
        public bool durum;
        private void duyurular()
        {
            OleDbConnection baglanti = new OleDbConnection();
            baglanti.ConnectionString = "Provider=Microsoft.ACE.Oledb.12.0;Data Source=Data/MarketData.accdb";
            OleDbCommand komut = new OleDbCommand();
            komut.CommandText = "SELECT *FROM duyurular ORDER BY duyuru_id DESC";
            //ORDER BY kolon_adı(ları) ASC|DESC
            komut.Connection = baglanti;
            komut.CommandType = CommandType.Text;

            OleDbDataReader dr;
            baglanti.Open();
            dr = komut.ExecuteReader();
            while (dr.Read())
            {

                
                listBox1.Items.Add(dr["duyuru_text"]);
            }
            baglanti.Close();
        }
        private void Home_Load(object sender, EventArgs e)
        {

            // this.Size = new Size(900, 300);
            this.MinimumSize =new Size(1000, 600);
            duyurular();
            timer1.Start();
            Menu menu = new Menu();
            musterilistesi mlistesi = new musterilistesi();
            if (label5.Text == "False")
            {
                menu.groupBox1.Visible = false;
                menu.groupBox5.Visible = false;
              menu.Size = new Size(450, 272);
                //450; 272

                label7.Text = "Kasiyer";
            }
            else
            {
                label7.Text = "Yönetici";

                menu.groupBox1.Visible = true;
                menu.groupBox5.Visible = true;

                this.Size = new Size(655, 342);
                //655; 342

            }
            /* Arka Plan Rengi İçin Kodlar Başlangıç */
            MdiClient ctlMDI;
            foreach (Control ctl in this.Controls)
            {
                try
                {
                    ctlMDI = (MdiClient)ctl;
                    ctlMDI.BackColor = this.BackColor;
                }
                catch (InvalidCastException)
                {
                }
            }
            /* Arka Plan Rengi İçin Kodlar Son */
    
            menu.MdiParent = this;
            menu.Show();


        }
        private void groupBox1_Enter(object sender, EventArgs e)
        {
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            Application.Exit();
        }

        private void groupBox1_Enter_1(object sender, EventArgs e)
        {
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label8.Text = "Tarih : "+DateTime.Now.ToLongDateString() + "\nSaat  : " + DateTime.Now.ToLongTimeString() + "\nSistem saatiniz gösterilir!";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.FormBorderStyle == FormBorderStyle.None)
            { 
            this.FormBorderStyle = FormBorderStyle.Fixed3D;
            }
            else
            {
                this.FormBorderStyle = FormBorderStyle.None;
                this.WindowState = FormWindowState.Maximized;
            }
        }

        private void Home_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(0);
            
        }

        private void label9_Click(object sender, EventArgs e)
        {
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
          
          
        }
    }
}
