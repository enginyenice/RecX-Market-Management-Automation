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
using System.IO;
using System.Collections;
using Microsoft.VisualBasic;

namespace RecX_Market_Otomasyonu
{
    public partial class Perakendesatisgirisi : Form
    {
        public static OleDbConnection Con;

        private Perakendesatissecimi Form2 = new Perakendesatissecimi();

        private ArrayList ticket_products = new ArrayList();
        private ArrayList ticket_counts = new ArrayList();
        private ArrayList ticket_prices = new ArrayList();

        private ArrayList products = new ArrayList();
        private ArrayList types = new ArrayList();
        //Fatura//
        OleDbConnection con;
        OleDbDataAdapter da;
        OleDbCommand cmd;
        DataSet ds;
        void griddoldur()
        {
           
            con = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source=Data/MarketData.accdb");
            da = new OleDbDataAdapter("SELECT TOP 1 * FROM satislar ORDER BY id DESC;", con);
            ds = new DataSet();
            con.Open();
            da.Fill(ds, "satislar");
            dataGridView1.DataSource = ds.Tables["satislar"];
            con.Close();
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
        //son//
        private OleDbDataReader product;
        void yenile()
        {
            Con = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source=Data/MarketData.accdb");
         
            Con.Open();
            Con.Close();
           
        }
        private string BackupLoc;
        public Perakendesatisgirisi()
        {
            InitializeComponent();
        }

        private void test_Load(object sender, EventArgs e)
        {
            this.Size = new Size(473, 529);
            griddoldur();
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.Columns[0].HeaderText = "Satış ID";
           // dataGridView1.Columns[0].Width = 100;
            dataGridView1.Columns[1].HeaderText = "Tarih";
          //  dataGridView1.Columns[1].Width = 100;
            dataGridView1.Columns[2].HeaderText = "Alınan Ücret (TL)";
          //  dataGridView1.Columns[2].Width = 140;
            dataGridView1.Columns[3].HeaderText = "Müşteri Adı";
          //  dataGridView1.Columns[3].Width = 120;



            //  this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);

            try
            {
                Con = new OleDbConnection(@"Provider=Microsoft.ACE.Oledb.12.0;Data Source=Data/MarketData.accdb");
                Con.Open();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Market Otomasyonu - Veritabanı Bağlantı Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }

            Directory.CreateDirectory(Directory.GetCurrentDirectory() + @"\Data\Backups");
            Directory.CreateDirectory(Directory.GetCurrentDirectory() + @"\Data\Images");

            Backup();
            GetTypes();
            GetProducts();
        }

        private void Backup()
        {
            bool BackupEnabled = false;

            string Query = "select * from settings";
            OleDbCommand cmd = new OleDbCommand(Query, Con);

            OleDbDataReader result = cmd.ExecuteReader();

            while (result.Read())
            {
                switch (result["id"].ToString())
                {
                    case "auto_daily_backup":
                        if (result["val"].ToString() == "1")
                        {
                            BackupEnabled = true;
                        }
                        else
                        {
                            BackupEnabled = false;
                        }

                        break;

                    case "backup_loc":
                        BackupLoc = result["val"].ToString();

                        if (!Directory.Exists(BackupLoc))
                        {
                            BackupLoc = Directory.GetCurrentDirectory() + @"\Data\Backups";
                        }

                        break;
                }
            }

            if (!BackupEnabled)
            {
                //checkBox1.Checked = false;
            }
            else
            {
                string BackupFileLoc = BackupLoc + @"\" + DateTime.Now.ToString("yyyy-MM-dd") + ".accdb";

                if (!File.Exists(BackupFileLoc))
                {
                    File.Copy(@"Data\Database.accdb", BackupFileLoc);
                }
            }

        }

        private void GetTypes()
        {
            types = new ArrayList();



            string Query = "select * from urunler order by urun_id desc";
            OleDbCommand cmd = new OleDbCommand(Query, Con);

            OleDbDataReader result = cmd.ExecuteReader();

            while (result.Read())
            {
                types.Add(Convert.ToInt32(result["urun_id"]));

            }
        }

        private void GetProducts()
        {


            products = new ArrayList();

            string Query = "select * from urunler order by urun_id desc";
            OleDbCommand cmd = new OleDbCommand(Query, Con);

            OleDbDataReader result = cmd.ExecuteReader();


        }

        private void DeleteTypeImages(int type_id)
        {
            string img_loc;

            string Query = "select id from urunler where kategori_id=@kategori_id";
            OleDbCommand cmd = new OleDbCommand(Query, Con);

            cmd.Parameters.AddWithValue("@kategori_id", type_id);
            OleDbDataReader result = cmd.ExecuteReader();

            while (result.Read())
            {
                img_loc = @"Data\Images\" + result["urun_id"].ToString();
                if (File.Exists(img_loc))
                {
                    File.Delete(img_loc);
                }
            }
        }


        private string GetTypeName(int type_id)
        {
            string Query = "select kategori_adi from kategori where kategori_id=@kategori_id";
            OleDbCommand cmd = new OleDbCommand(Query, Con);

            cmd.Parameters.AddWithValue("@kategori_id", type_id);
            OleDbDataReader result = cmd.ExecuteReader();

            if (result.Read())
            {
                return result["kategori_adi"].ToString();
            }

            return "";
        }

        private string GetProductName(int product_id)
        {
            string Query = "select urun_adi from urunler where urun_id=@urun_id";
            OleDbCommand cmd = new OleDbCommand(Query, Con);

            cmd.Parameters.AddWithValue("@urun_id", product_id);
            OleDbDataReader result = cmd.ExecuteReader();

            result.Read();
            return result["urun_adi"].ToString();
        }

        private int GetProductDailySale(int product_id, int ticket_id)
        {
            string Query = "select count(*) as total from satislar where urun_id=@urun_id and ticket_id=@ticket_id";
            OleDbCommand cmd = new OleDbCommand(Query, Con);

            cmd.Parameters.AddWithValue("@urun_id", product_id);
            cmd.Parameters.AddWithValue("@ticket_id", ticket_id);
            OleDbDataReader result = cmd.ExecuteReader();

            result.Read();
            return Convert.ToInt32(result["total"]);
        }

        private string Daily_Total()
        {
            DateTime date = new DateTime(monthCalendar1.SelectionRange.Start.Year, monthCalendar1.SelectionRange.Start.Month, monthCalendar1.SelectionRange.Start.Day);

            string Query = "select sum(price) as daily_total from satislar where sale_date=@sale_date";
            OleDbCommand cmd = new OleDbCommand(Query, Con);

            cmd.Parameters.AddWithValue("@sale_date", date);
            OleDbDataReader result = cmd.ExecuteReader();

            while (result.Read())
            {
                string value = result["daily_total"].ToString();

                if (!String.IsNullOrEmpty(value))
                {
                    return value;
                }
            }

            return "0,00";
        }

        private string Monthly_Total()
        {
            DateTime start_date = new DateTime(monthCalendar1.SelectionRange.Start.Year, monthCalendar1.SelectionRange.Start.Month, 1);
            DateTime end_date = new DateTime(monthCalendar1.SelectionRange.Start.Year, monthCalendar1.SelectionRange.Start.Month, 1).AddMonths(1);

            string Query = "select sum(price) as monthly_total from satislar where sale_date>=@sale_date_start and sale_date<@sale_date_end";
            OleDbCommand cmd = new OleDbCommand(Query, Con);

            cmd.Parameters.AddWithValue("@sale_date_start", start_date);
            cmd.Parameters.AddWithValue("@sale_date_end", end_date);
            OleDbDataReader result = cmd.ExecuteReader();

            while (result.Read())
            {
                string value = result["monthly_total"].ToString();

                if (!String.IsNullOrEmpty(value))
                {
                    return value;
                }
            }

            return "0,00";
        }

        private int GetNextBarcode()
        {
            string Query = "select max(urun_barkod) as biggest from urunler";
            OleDbCommand cmd = new OleDbCommand(Query, Con);

            OleDbDataReader result = cmd.ExecuteReader();

            result.Read();

            if (!String.IsNullOrEmpty(result["biggest"].ToString()))
            {
                return Convert.ToInt32(result["biggest"]) + 1;
            }

            return 0;
        }

        private int StockLoss(int product_id)
        {
            int loss = 0;

            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                if (Convert.ToInt32(ticket_products[i]) == product_id)
                {
                    loss += Convert.ToInt32(ticket_counts[i]);
                }
            }

            return loss;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            int barcode;

            if (int.TryParse(textBox1.Text, out barcode))
            {
                string Query = "select * from urunler where urun_barkod=@urun_barkod";
                OleDbCommand cmd = new OleDbCommand(Query, Con);

                cmd.Parameters.AddWithValue("@urun_barkod", barcode);
                product = cmd.ExecuteReader();

                if (product.Read())
                {
                    //pictureBox1.ImageLocation = @"Data\Images\" + product["urun_id"].ToString();

                    label7.Text = product["urun_barkod"].ToString();
                    label6.Text = product["urun_adi"].ToString();
                    label5.Text = product["urun_sfiyati"].ToString() + " TL";

                    int type_id;

                    if (int.TryParse(product["kategori_id"].ToString(), out type_id))
                    {
                        label8.Text = GetTypeName(Convert.ToInt32(type_id));
                    }

                    numericUpDown1.Maximum = Convert.ToInt32(product["urun_adedi"]) - StockLoss(Convert.ToInt32(product["urun_id"]));
                    if (numericUpDown1.Maximum > 0)
                    {
                        numericUpDown1.Value = 1;
                    }

                    return;
                }
            }

            //pictureBox1.ImageLocation = null;

            label7.Text = null;
            label8.Text = null;
            label6.Text = null;
            label5.Text = null;

            numericUpDown1.Maximum = 0;
            numericUpDown1.Value = 0;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDown1.Value == 0)
            {
                button8.Enabled = false;
            }
            else
            {
                button8.Enabled = true;
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Form2.ShowDialog();

            if (!String.IsNullOrEmpty(Form2.sel_barcode))
            {
                textBox1.Text = Form2.sel_barcode;
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            ticket_products.Add(Convert.ToInt32(product["urun_id"]));
            ticket_prices.Add(Convert.ToDecimal(product["urun_sfiyati"]));
            ticket_counts.Add(Convert.ToInt32(numericUpDown1.Value));

            listBox1.Items.Add(numericUpDown1.Value + " x " + product["urun_adi"]);

            textBox1.Text = "";

            listBox1.SelectedIndex = listBox1.Items.Count - 1;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            ticket_products.Remove(ticket_products[listBox1.SelectedIndex]);
            ticket_prices.Remove(ticket_prices[listBox1.SelectedIndex]);
            ticket_counts.Remove(ticket_counts[listBox1.SelectedIndex]);

            listBox1.Items.Remove(listBox1.SelectedItem);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            decimal amount = 0;

            if (listBox1.SelectedIndex == -1)
            {
                button2.Enabled = false;
                button5.Enabled = false;
                button6.Enabled = false;
                button7.Enabled = false;

            }
            else
            {
                button2.Enabled = true;
                button5.Enabled = true;
                button6.Enabled = true;
                button7.Enabled = true;
            }

            for (int i = 0; i < ticket_prices.Count; i++)
            {
                amount += Convert.ToDecimal(ticket_prices[i]) * Convert.ToInt32(ticket_counts[i]);
            }

            label24.Text = amount.ToString();
        }

        private void button5_Click(object sender, EventArgs e)
        {
           try
           {
                string Query;
                OleDbCommand cmd;
                int product_id, count;

                for (int i = 0; i < listBox1.Items.Count; i++)
                {
                    product_id = Convert.ToInt32(ticket_products[i]);
                    count = Convert.ToInt32(ticket_counts[i]);

                    Query = "update urunler set urun_adedi=(urun_adedi - @count) where urun_id=@urun_id";
                    cmd = new OleDbCommand(Query, Con);

                    cmd.Parameters.AddWithValue("@count", count);
                    cmd.Parameters.AddWithValue("@urun_id", product_id);

                    cmd.ExecuteNonQuery();


                }
                string mus_adi = mus_ad.Text;
                if(mus_ad.Text == "") {
                    mus_adi = "Tanımsız Müşteri";
                }

                Query = "insert into satislar (urun_sfiyati,mus_ad)" +
                    "Values(@urun_sfiyati,'" + mus_adi + "')";
                //" values (@urun_sfiyati,"')";


                cmd = new OleDbCommand(Query, Con);

                cmd.Parameters.AddWithValue("@urun_sfiyati", label24.Text);


                cmd.ExecuteNonQuery();



                button6.PerformClick();



               MessageBox.Show("Satış Başarıyla Tamamlandı. Fiş Kes diyerek fişinizi alabilirsiniz.");
                mus_ad.Clear();
                button4.Enabled = true;

         }
            catch (Exception)
            {

                MessageBox.Show("Satış Tamamlanamadı", "Dikkat", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void button6_Click(object sender, EventArgs e)
        {
            ticket_products = new ArrayList();
            ticket_prices = new ArrayList();
            ticket_counts = new ArrayList();

            listBox1.SelectedIndex = -1;
            listBox1.Items.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

           
            perakendesatismusteri permus = new perakendesatismusteri();
            permus.ShowDialog();
            if (!String.IsNullOrEmpty(permus.musteri))
            {
                mus_ad.Text = permus.musteri;
            }

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                double sonuc;
                sonuc = Double.Parse(textBox2.Text) - Double.Parse(label24.Text);
                if (sonuc < 0)
                {
                    label11.ForeColor = Color.Red;
                }
                else
                {
                    label11.ForeColor = Color.Black;
                }
                label11.Text = sonuc.ToString() + " TL Para üstü veriniz...";
            }
            catch (Exception)
            {
                label11.Text = "Hatalı İşlem !!";
            }
        }
        

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
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

        private void groupBox1_Enter(object sender, EventArgs e)
        {

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

                            e.Graphics.DrawString("Satış Çıktısı", new Font(dataGridView1.Font, FontStyle.Bold),
                                    Brushes.Black, e.MarginBounds.Left, e.MarginBounds.Top -
                                    e.Graphics.MeasureString("Satış Çıktısı", new Font(dataGridView1.Font,
                                    FontStyle.Bold), e.MarginBounds.Width).Height - 13);

                            String strDate = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToShortTimeString();

                            e.Graphics.DrawString(strDate, new Font(dataGridView1.Font, FontStyle.Bold),
                                    Brushes.Black, e.MarginBounds.Left + (e.MarginBounds.Width -
                                    e.Graphics.MeasureString(strDate, new Font(dataGridView1.Font,
                                    FontStyle.Bold), e.MarginBounds.Width).Width), e.MarginBounds.Top -
                                    e.Graphics.MeasureString("Satış Çıktısı", new Font(new Font(dataGridView1.Font,
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

        private void button4_Click(object sender, EventArgs e)
        {
            griddoldur();
            PrintPreviewDialog onizleme = new PrintPreviewDialog();
            onizleme.Document = printDocument1;
            onizleme.ShowDialog();
        }
    }
}

