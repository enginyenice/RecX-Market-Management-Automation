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
    public partial class satisbilgi : Form
    {

        public satisbilgi()
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
        OleDbConnection con;
        OleDbDataAdapter da;
       // OleDbCommand cmd;
        DataSet ds;
        private void vericek()
        {

            con = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source=Data/MarketData.accdb");
            da = new OleDbDataAdapter("SElect *from satislar", con);
            ds = new DataSet();
            con.Open();
            da.Fill(ds, "satislar");
            dataGridView1.DataSource = ds.Tables["satislar"];
            
        }
        private string Daily_Total()
        {
            
            DateTime date = new DateTime(monthCalendar1.SelectionRange.Start.Year, monthCalendar1.SelectionRange.Start.Month, monthCalendar1.SelectionRange.Start.Day);

            string Query = "select sum(urun_sfiyati) as daily_total from satislar where sale_date=@sale_date";
            OleDbCommand cmd = new OleDbCommand(Query, con);

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

            string Query = "select sum(urun_sfiyati) as monthly_total from satislar where sale_date>=@sale_date_start and sale_date<@sale_date_end";
            OleDbCommand cmd = new OleDbCommand(Query, con);

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


        private void satisbilgi_Load(object sender, EventArgs e)
        {
            //790; 371
            this.Size = new Size(790, 371);
            
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;

            vericek();
            //dataGridView1.RowHeadersWidth = 100;
            
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.Columns[0].HeaderText = "Satış ID";
            dataGridView1.Columns[0].Width = 100;
            dataGridView1.Columns[1].HeaderText = "Tarih";
            dataGridView1.Columns[1].Width = 100;
            dataGridView1.Columns[2].HeaderText = "Alınan Ücret (TL)";
            dataGridView1.Columns[2].Width = 140;
            dataGridView1.Columns[3].HeaderText = "Müşteri Adı";
            dataGridView1.Columns[3].Width = 120;
            label1.Text = Daily_Total() + " TL";
            label2.Text = Monthly_Total() + " TL";

        }



        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
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

                            e.Graphics.DrawString("Kasa Raporu", new Font(dataGridView1.Font, FontStyle.Bold),
                                    Brushes.Black, e.MarginBounds.Left, e.MarginBounds.Top -
                                    e.Graphics.MeasureString("Kasa Raporu", new Font(dataGridView1.Font,
                                    FontStyle.Bold), e.MarginBounds.Width).Height - 13);

                            String strDate = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString();

                            e.Graphics.DrawString(strDate, new Font(dataGridView1.Font, FontStyle.Bold),
                                    Brushes.Black, e.MarginBounds.Left + (e.MarginBounds.Width -
                                    e.Graphics.MeasureString(strDate, new Font(dataGridView1.Font,
                                    FontStyle.Bold), e.MarginBounds.Width).Width), e.MarginBounds.Top -
                                    e.Graphics.MeasureString("Kasa Raporu", new Font(new Font(dataGridView1.Font,
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

        private void button1_Click(object sender, EventArgs e)
        {
            PrintPreviewDialog onizleme = new PrintPreviewDialog();
            onizleme.Document = printDocument1;
            onizleme.ShowDialog();
        }
    }
}
