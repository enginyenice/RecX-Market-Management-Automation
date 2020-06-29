using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RecX_Market_Otomasyonu
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void Menu_Load(object sender, EventArgs e)
        {

           // this.Size = new Size(655, 272);

            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;

        }



        private void button8_Click(object sender, EventArgs e)
        {
            
            Sbilgi sbilgi = new Sbilgi();
            Home home = new Home();
            sbilgi.ShowDialog();
        }



        private void urunlist_Click(object sender, EventArgs e)
        {
            Perakendeurungiris perakendeurungiris = new Perakendeurungiris();
            perakendeurungiris.ShowDialog();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            /* Firma Girişi */
            Cari_firmagir firmagir = new Cari_firmagir();
            firmagir.ShowDialog();

        }

        private void button10_Click(object sender, EventArgs e)
        {
            Perakendeurungiris perakendeurungiris = new Perakendeurungiris();
            perakendeurungiris.ShowDialog();
             
        }

        private void button11_Click(object sender, EventArgs e)
        {
            musterilistesi musterilistesi = new musterilistesi();
            musterilistesi.ShowDialog();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            Perakendesatisgirisi satisgirisi = new Perakendesatisgirisi();
            satisgirisi.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            yonetimhesap yonetimhesap = new yonetimhesap();
            yonetimhesap.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            hesapolustur hesapolustur = new hesapolustur();
            hesapolustur.ShowDialog();
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            hesapyetkilendir hesapyetkilendir = new hesapyetkilendir();
            hesapyetkilendir.ShowDialog();
        }
        private void button4_Click_2(object sender, EventArgs e)
        {
            hesapsil hesapsil = new hesapsil();
            hesapsil.ShowDialog();
        }
        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }
        private void button5_Click(object sender, EventArgs e)
        {
            Kategoriislem kategoriislem = new Kategoriislem();
            kategoriislem.ShowDialog();
        }
        private void button6_Click(object sender, EventArgs e)
        {
            satisbilgi satisbilgi = new satisbilgi();
            satisbilgi.ShowDialog();
        }
        private void button7_Click(object sender, EventArgs e)
        {
            duyuruislem duyuruislem = new duyuruislem();
            duyuruislem.ShowDialog();
        }
    }
}
