using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;

namespace RecX_Market_Otomasyonu
{
    public partial class Perakendesatissecimi : Form
    {
        private ArrayList type_ids = new ArrayList();
        private ArrayList product_ids = new ArrayList();

        public string sel_barcode { get; set; }

        public Perakendesatissecimi()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            listBox2.ClearSelected();
            listBox2.Items.Clear();

            listBox1.Items.Clear();

            this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);

            ListTypes();
            listBox1.SelectedIndex = 0;
        }

        private void ListTypes()
        {
            type_ids.Add(0);
            listBox1.Items.Add("« Tümü »");

            string Query = "select * from kategori";
            OleDbCommand cmd = new OleDbCommand(Query, Perakendesatisgirisi.Con);

            OleDbDataReader result = cmd.ExecuteReader();

            while (result.Read())
            {
                type_ids.Add(Convert.ToInt32(result["kategori_id"]));
                listBox1.Items.Add(result["kategori_adi"].ToString());
            }
        }

        private void GetBarcode()
        {
            if (!String.IsNullOrEmpty(textBox1.Text))
            {
                this.sel_barcode = textBox1.Text;
                this.Close();
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string Query;
            int type_id = Convert.ToInt32(type_ids[listBox1.SelectedIndex]);

            product_ids = new ArrayList();

            listBox2.ClearSelected();
            listBox2.Items.Clear();

            if (type_id == 0)
            {
                Query = "select * from urunler";
            }
            else
            {
                Query = "select * from urunler where kategori_id=@kategori_id";
            }

            OleDbCommand cmd = new OleDbCommand(Query, Perakendesatisgirisi.Con);

            cmd.Parameters.AddWithValue("@kategori_id", type_id);
            OleDbDataReader result = cmd.ExecuteReader();

            while (result.Read())
            {
                product_ids.Add(Convert.ToInt32(result["urun_id"]));

                if (Convert.ToInt32(result["urun_adedi"]) == 0)
                {
                    listBox2.Items.Add("(Stokta Yok) " + result["urun_adi"].ToString());
                }
                else
                {
                    listBox2.Items.Add(result["urun_adi"].ToString());
                }


            }
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox2.SelectedItems.Count == 0)
            {
                textBox1.Text = null;
                button1.Enabled = false;
            }
            else
            {
                textBox1.Text = null;
                button1.Enabled = true;

                int product_id = Convert.ToInt32(product_ids[listBox2.SelectedIndex]);

                string Query = "select urun_barkod from urunler where urun_id=@urun_id";
                OleDbCommand cmd = new OleDbCommand(Query, Perakendesatisgirisi.Con);

                cmd.Parameters.AddWithValue("@urun_id", product_id);
                OleDbDataReader result = cmd.ExecuteReader();

                result.Read();
                textBox1.Text = result["urun_barkod"].ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GetBarcode();
        }

        private void listBox2_DoubleClick(object sender, EventArgs e)
        {
            GetBarcode();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }


    }
}
