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
using System.Collections;

namespace RecX_Market_Otomasyonu
{
    public partial class Perakendeurungiris : Form
    {
        public Home home;
        OleDbConnection con;
        OleDbDataAdapter da;
        OleDbCommand cmd;
        DataSet ds;

        public Perakendeurungiris()
        {
            InitializeComponent();
        }
        StringFormat strFormat;
        ArrayList arrColumnLefts = new ArrayList();
        ArrayList arrColumnWidths = new ArrayList();
        int iCellHeight = 0;
        int iTotalWidth = 0;
        int iRow = 0;
        bool bFirstPage = false;
        bool bNewPage = false;
        int iHeaderHeight = 0;

        void griddoldur()
        {
            con = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source=Data/MarketData.accdb");
            da = new OleDbDataAdapter("SElect *from urunler", con);
            ds = new DataSet();
            con.Open();
            da.Fill(ds, "urunler");
            dataGridView1.DataSource = ds.Tables["urunler"];
            con.Close();
        }
        void kategoridoldur()
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
        private void kategoriid()
        {
            //Yeni
            OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source=Data/MarketData.accdb");
            con.Open(); //accdb dosyaları için
            OleDbCommand cmd = new OleDbCommand("Select kategori_id from kategori where kategori_adi= '"+comboBox1.Text+"'", con);
            OleDbDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                textBox1.Text = dr["kategori_id"].ToString();


                //Veya
                //textBox1.Text = dr.GetString(0);
                //textBox2.Text = dr.GetString(1);
                //textBox3.Text = dr.GetString(2);

            }
            con.Close();




            //Son


        }


        void kontrol()
        {
            Home home = new Home();



        }

        private void Perakendeurungiris_Load(object sender, EventArgs e)
        {
            this.Size = new Size(1140, 583);
            //1140; 583
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;
            griddoldur();
            kategoridoldur();
            kontrol();
            dataGridView1.RowHeadersWidth = 60;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[1].HeaderText = "Ürün Adı";
            dataGridView1.Columns[2].HeaderText = "Kategori";
            dataGridView1.Columns[3].HeaderText = "Adet";
            dataGridView1.Columns[4].HeaderText = "Giriş Fiyatı(TL)";
            dataGridView1.Columns[5].HeaderText = "Satış Fiyatı(TL)";
            dataGridView1.Columns[6].HeaderText = "Barkod Numarası";
            dataGridView1.Columns[7].HeaderText = "Kategori ID";


        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Benim Eklediklerim


                kategoriid();

                //Son
                cmd = new OleDbCommand();
                con.Open();
                cmd.Connection = con;
                
                cmd.CommandText = "insert into urunler (urun_adi,urun_kategori,urun_adedi,urun_fiyati,urun_sfiyati,urun_barkod,kategori_id) values ('" + urun_adi.Text + "','" + comboBox1.Text + "','" + urun_adedi.Text + "','" + urun_fiyati.Text + "','" + urun_sfiyati.Text + "','" + urun_barkod.Text + "','" + textBox1.Text + "')";
                cmd.ExecuteNonQuery();
                con.Close();
                griddoldur();
                

            }
            catch (Exception)
            {

                MessageBox.Show("Hatalı işlem yaptınız lütfen kontrol ediniz");
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                //
                kategoriid();
                //
                cmd = new OleDbCommand();
                con.Open();
                cmd.Connection = con;
                cmd.CommandText = "update urunler set urun_adi='" + urun_adi.Text + "',urun_kategori='" + comboBox1.Text + "',urun_adedi='" + Int32.Parse(urun_adedi.Text) + "',urun_fiyati='" + urun_fiyati.Text + "',urun_sfiyati='" + urun_sfiyati.Text + "',urun_barkod='" + Int32.Parse(urun_barkod.Text) + "',kategori_id='" + textBox1.Text + "' where urun_id=" + Int32.Parse(urun_id.Text) + "";
                cmd.ExecuteNonQuery();
                con.Close();
                griddoldur();
            }
            catch (Exception)
            {
                MessageBox.Show("Hatalı işlem yaptınız kontrol ediniz");
                con.Close();
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult secenek = MessageBox.Show(urun_adi.Text + " " + "Silmek istiyor musunuz?", "Bilgilendirme Penceresi", MessageBoxButtons.YesNo, MessageBoxIcon.Information);


            if (secenek == DialogResult.Yes)
            {
                cmd = new OleDbCommand();
                con.Open();
                cmd.Connection = con;
                cmd.CommandText = "delete from urunler where urun_id=" + urun_id.Text + "";
                cmd.ExecuteNonQuery();
                con.Close();
                griddoldur();
            }
            else if (secenek == DialogResult.No)
            {
                MessageBox.Show("Silme işlemi gerçekleştirilmedi", "Silme İşlemi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void urun_ara_TextChanged(object sender, EventArgs e)
        {
            con = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source=Data/MarketData.accdb");
            da = new OleDbDataAdapter("SElect *from urunler where urun_adi like '" + urun_ara.Text + "%'", con);
            ds = new DataSet();
            con.Open();
            da.Fill(ds, "urunler");
            dataGridView1.DataSource = ds.Tables["urunler"];
            con.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            griddoldur();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            urun_id.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            urun_adi.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            comboBox1.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            urun_adedi.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            urun_fiyati.Text= dataGridView1.CurrentRow.Cells[4].Value.ToString();
            urun_sfiyati.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            urun_barkod.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            textBox1.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();

        }

        private void comboBox2_TextChanged(object sender, EventArgs e)
        {
            con = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source=Data/MarketData.accdb");
            da = new OleDbDataAdapter("SElect *from urunler where urun_kategori like '" + comboBox2.Text + "%'", con);
            ds = new DataSet();
            con.Open();
            da.Fill(ds, "urunler");
            dataGridView1.DataSource = ds.Tables["urunler"];
            con.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void urun_sfiyati_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar >= 48 && (int)e.KeyChar <= 57)
            {
                e.Handled = false;//eğer rakamsa  yazdır.
            }

            else if ((int)e.KeyChar == 44)
            {
                e.Handled = false;//eğer basılan tuş virgül ise yazdır.
            }
            else if ((int)e.KeyChar == 08)
            {
                e.Handled = false;//eğer basılan tuş backspace ise yazdır.
            }

            else
            {
                e.Handled = true;//bunların dışındaysa hiçbirisini yazdırma
            }
        }

        private void urun_fiyati_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar >= 48 && (int)e.KeyChar <= 57)
            {
                e.Handled = false;//eğer rakamsa  yazdır.
            }

            else if ((int)e.KeyChar == 44)
            {
                e.Handled = false;//eğer basılan tuş virgül ise yazdır.
            }
            else if ((int)e.KeyChar == 08)
            {
                e.Handled = false;//eğer basılan tuş backspace ise yazdır.
            }

            else
            {
                e.Handled = true;//bunların dışındaysa hiçbirisini yazdırma
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void urun_adedi_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void urun_barkod_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            try
            {
                int iLeftMargin = e.MarginBounds.Left;
                int iTopMargin = e.MarginBounds.Top;
                bool bMorePagesToPrint = false;
                int iTmpWidth = 0;
                bFirstPage = true;

                if (bFirstPage)
                {
                    foreach (DataGridViewColumn GridCol in dataGridView1.Columns)
                    {
                        iTmpWidth = (int)(Math.Floor((double)((double)GridCol.Width /
                                       (double)iTotalWidth * (double)iTotalWidth *
                                       ((double)e.MarginBounds.Width / (double)iTotalWidth))));

                        iHeaderHeight = (int)(e.Graphics.MeasureString(GridCol.HeaderText,
                                    GridCol.InheritedStyle.Font, iTmpWidth).Height) + 11;


                        arrColumnLefts.Add(iLeftMargin);
                        arrColumnWidths.Add(iTmpWidth);
                        iLeftMargin += iTmpWidth;
                    }
                }

                while (iRow <= dataGridView1.Rows.Count - 1)
                {
                    DataGridViewRow GridRow = dataGridView1.Rows[iRow];

                    iCellHeight = GridRow.Height + 5;
                    int iCount = 0;

                    if (iTopMargin + iCellHeight >= e.MarginBounds.Height + e.MarginBounds.Top)
                    {
                        bNewPage = true;
                        bFirstPage = false;
                        bMorePagesToPrint = true;
                        break;
                    }
                    else
                    {
                        if (bNewPage)
                        {

                            e.Graphics.DrawString("Ürün Listesi", new Font(dataGridView1.Font, FontStyle.Bold),
                                    Brushes.Black, e.MarginBounds.Left, e.MarginBounds.Top -
                                    e.Graphics.MeasureString("Ürün Listesi", new Font(dataGridView1.Font,
                                    FontStyle.Bold), e.MarginBounds.Width).Height - 13);

                            String strDate = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString();

                            e.Graphics.DrawString(strDate, new Font(dataGridView1.Font, FontStyle.Bold),
                                    Brushes.Black, e.MarginBounds.Left + (e.MarginBounds.Width -
                                    e.Graphics.MeasureString(strDate, new Font(dataGridView1.Font,
                                    FontStyle.Bold), e.MarginBounds.Width).Width), e.MarginBounds.Top -
                                    e.Graphics.MeasureString("Ürün Listesi", new Font(new Font(dataGridView1.Font,
                                    FontStyle.Bold), FontStyle.Bold), e.MarginBounds.Width).Height - 13);


                            iTopMargin = e.MarginBounds.Top;
                            foreach (DataGridViewColumn GridCol in dataGridView1.Columns)
                            {
                                e.Graphics.FillRectangle(new SolidBrush(Color.LightGray),
                                    new Rectangle((int)arrColumnLefts[iCount], iTopMargin,
                                    (int)arrColumnWidths[iCount], iHeaderHeight));

                                e.Graphics.DrawRectangle(Pens.Black,
                                    new Rectangle((int)arrColumnLefts[iCount], iTopMargin,
                                    (int)arrColumnWidths[iCount], iHeaderHeight));

                                e.Graphics.DrawString(GridCol.HeaderText, GridCol.InheritedStyle.Font,
                                    new SolidBrush(GridCol.InheritedStyle.ForeColor),
                                    new RectangleF((int)arrColumnLefts[iCount], iTopMargin,
                                    (int)arrColumnWidths[iCount], iHeaderHeight), strFormat);
                                iCount++;
                            }
                            bNewPage = false;
                            iTopMargin += iHeaderHeight;
                        }
                        iCount = 0;

                        foreach (DataGridViewCell Cel in GridRow.Cells)
                        {
                            if (Cel.Value != null)
                            {
                                e.Graphics.DrawString(Cel.Value.ToString(), Cel.InheritedStyle.Font,
                                            new SolidBrush(Cel.InheritedStyle.ForeColor),
                                            new RectangleF((int)arrColumnLefts[iCount], (float)iTopMargin,
                                            (int)arrColumnWidths[iCount], (float)iCellHeight), strFormat);
                            }

                            e.Graphics.DrawRectangle(Pens.Black, new Rectangle((int)arrColumnLefts[iCount],
                                    iTopMargin, (int)arrColumnWidths[iCount], iCellHeight));

                            iCount++;
                        }
                    }
                    iRow++;
                    iTopMargin += iCellHeight;
                }


                if (bMorePagesToPrint)
                    e.HasMorePages = true;
                else
                    e.HasMorePages = false;
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void printDocument1_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            try
            {
                printDocument1.OriginAtMargins = true;
                printDocument1.DefaultPageSettings.Margins.Left = 10;
                printDocument1.DefaultPageSettings.Margins.Right = 40;
                printDocument1.DefaultPageSettings.Margins.Top = 40;
                printDocument1.DefaultPageSettings.Margins.Bottom = 0;
                strFormat = new StringFormat();
                strFormat.Alignment = StringAlignment.Near;
                strFormat.LineAlignment = StringAlignment.Center;
                strFormat.Trimming = StringTrimming.EllipsisCharacter;

                arrColumnLefts.Clear();
                arrColumnWidths.Clear();
                iCellHeight = 0;
                iRow = 0;
                bFirstPage = true;
                bNewPage = true;

                iTotalWidth = 0;
                foreach (DataGridViewColumn dgvGridCol in dataGridView1.Columns)
                {
                    iTotalWidth += dgvGridCol.Width;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            PrintPreviewDialog onizleme = new PrintPreviewDialog();
            onizleme.Document = printDocument1;
            onizleme.ShowDialog();
        }
    }
}
