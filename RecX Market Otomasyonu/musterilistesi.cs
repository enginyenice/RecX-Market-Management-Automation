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

    public partial class musterilistesi : Form
    {
        public Home home;
        OleDbConnection con;
        OleDbDataAdapter da;
        OleDbCommand cmd;
        DataSet ds;
        StringFormat strFormat;
        ArrayList arrColumnLefts = new ArrayList();
        ArrayList arrColumnWidths = new ArrayList();
        int iCellHeight = 0;
        int iTotalWidth = 0;
        int iRow = 0;
        bool bFirstPage = false;
        bool bNewPage = false;
        int iHeaderHeight = 0;

        public musterilistesi()
         
        {
       
            
            InitializeComponent();
        }


        void griddoldur()
        {
            con = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source=Data/MarketData.accdb");
            da = new OleDbDataAdapter("SElect *from musteri", con);
            ds = new DataSet();
            //SELECT isim, soyisim FROM uyeler ORDER BY soyisim DESC
            con.Open();
            da.Fill(ds, "musteri");
            dataGridView1.DataSource = ds.Tables["musteri"];
            con.Close();

        }
        void kontrol()
        {
            Home home = new Home();
           // dataGridView1.RowHeadersWidth = 150;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Width = 50;
            dataGridView1.Columns[1].HeaderText = "Firma Kodu";
            dataGridView1.Columns[1].Width = 113;
            dataGridView1.Columns[2].HeaderText = "Adı";
            dataGridView1.Columns[2].Width = 90;
            dataGridView1.Columns[3].HeaderText = "Soyadı / Unvanı";
            dataGridView1.Columns[3].Width = 130;
            dataGridView1.Columns[4].HeaderText = "Telefon";
            dataGridView1.Columns[4].Width = 90;
            dataGridView1.Columns[5].HeaderText = "Cep Telefon";
            dataGridView1.Columns[5].Width = 115;
            dataGridView1.Columns[6].HeaderText = "Fotoğraf Yolu";
            dataGridView1.Columns[6].Width = 123;
            dataGridView1.Columns[7].HeaderText = "Mail Adresi";
            dataGridView1.Columns[7].Width = 120;
            dataGridView1.Columns[8].HeaderText = "TC Kimlik No";
            dataGridView1.Columns[8].Width = 123;
            dataGridView1.Columns[9].HeaderText = "Doğum Tarihi";
            dataGridView1.Columns[9].Width = 123;
            dataGridView1.Columns[10].HeaderText = "Adresi";
            dataGridView1.Columns[10].Width = 151;



        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            mus_id.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            mus_ad.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            mus_unvan.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            mus_tel1.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            mus_cep.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            mus_adres.Text = dataGridView1.CurrentRow.Cells[10].Value.ToString();


        }

        private void musterilistesi_Load(object sender, EventArgs e)
        {
            this.Size = new Size(1287, 465);
            //1287; 465
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;
          
            griddoldur();
            kontrol();







        }

        private void button1_Click(object sender, EventArgs e)
        {
            Cari_firmagir carifirmagir = new Cari_firmagir();
            carifirmagir.ShowDialog();
          
            /*
            cmd = new OleDbCommand();
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "insert into musteri (mus_ad,mus_unvan,mus_tel1,mus_cep,mus_bakiye) values ('" + mus_ad.Text + "','" + mus_unvan.Text + "','" + mus_tel1.Text + "','" + mus_cep.Text + "','" + mus_bakiye.Text + "')";
            cmd.ExecuteNonQuery();
            con.Close();
            griddoldur();
            */
        }

        private void button3_Click(object sender, EventArgs e)
        {
            cmd = new OleDbCommand();
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "update musteri set mus_ad='" + mus_ad.Text + "',mus_unvan='" + mus_unvan.Text + "',mus_tel1='" + mus_tel1.Text + "',mus_cep='"+ mus_cep.Text+ "',mus_mahkoy='"+mus_adres.Text+ "' where mus_id=" + mus_id.Text + "";
            cmd.ExecuteNonQuery();
            con.Close();
            griddoldur();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult secenek = MessageBox.Show(mus_ad.Text +" "+ mus_unvan.Text+ " Numaralı müşteriyi silmek istiyor musunuz?" , "Bilgilendirme Penceresi",MessageBoxButtons.YesNo,MessageBoxIcon.Information);


            if (secenek == DialogResult.Yes)
            {
                cmd = new OleDbCommand();
                con.Open();
                cmd.Connection = con;
                cmd.CommandText = "delete from musteri where mus_id=" + mus_id.Text + "";
                cmd.ExecuteNonQuery();
                con.Close();
                griddoldur();
            }
            else if (secenek == DialogResult.No)
            {
                MessageBox.Show("Silme işlemi gerçekleştirilmedi" , "Silme İşlemi" ,MessageBoxButtons.OK,MessageBoxIcon.Error);
            }


        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            con = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source=Data/MarketData.accdb");
            da = new OleDbDataAdapter("SElect *from musteri where mus_ad like '" + textBox1.Text + "%'", con);
            ds = new DataSet();
            con.Open();
            da.Fill(ds, "musteri");
            dataGridView1.DataSource = ds.Tables["musteri"];
            con.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            griddoldur();
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            con = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source=Data/MarketData.accdb");
            da = new OleDbDataAdapter("SElect *from musteri where mus_tcno like '" + textBox2.Text + "%'", con);
            ds = new DataSet();
            con.Open();
            da.Fill(ds, "musteri");
            dataGridView1.DataSource = ds.Tables["musteri"];
            con.Close();
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

                            e.Graphics.DrawString("Müşteri Listesi", new Font(dataGridView1.Font, FontStyle.Bold),
                                    Brushes.Black, e.MarginBounds.Left, e.MarginBounds.Top -
                                    e.Graphics.MeasureString("Müşteri Listesi", new Font(dataGridView1.Font,
                                    FontStyle.Bold), e.MarginBounds.Width).Height - 13);

                           String strDate = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString();

                            e.Graphics.DrawString(strDate, new Font(dataGridView1.Font, FontStyle.Bold),
                                    Brushes.Black, e.MarginBounds.Left + (e.MarginBounds.Width -
                                    e.Graphics.MeasureString(strDate, new Font(dataGridView1.Font,
                                    FontStyle.Bold), e.MarginBounds.Width).Width), e.MarginBounds.Top -
                                    e.Graphics.MeasureString("Müşteri Listesi", new Font(new Font(dataGridView1.Font,
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
                printDocument1.DefaultPageSettings.Margins.Left = 0;
                printDocument1.DefaultPageSettings.Margins.Right = 0;
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
