using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace Proje_Hastahane
{
    public partial class FrmHastaGiris : Form
    {
        public FrmHastaGiris()
        {
            InitializeComponent();
        }

        private void LnkUyeOl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmHastaKayit frmHastaKayit = new FrmHastaKayit();
            frmHastaKayit.Show();
        }

        sqlbaglanti bql = new sqlbaglanti();
        private void BtnGirisYap_Click(object sender, EventArgs e)
        {
        
            SqlCommand komut = new SqlCommand("select*from Tbl_Hastalar where HastaTC=@p1 and HastaSifre=@p2", bql.baglanti());
            komut.Parameters.AddWithValue("@p1", maskedTextBox1.Text);
            komut.Parameters.AddWithValue("@p2", textBox1.Text);
            SqlDataReader dr = komut.ExecuteReader(); //komuttan gelen verileri okuyup yazdırdık.
            if (dr.Read())//okuma işlemi doğru şekilde yapılıyorsa mı diye kontrol etmek için..
            {
                FrmHastaDetay frmHastaDetay = new FrmHastaDetay();
                frmHastaDetay.tc = maskedTextBox1.Text;
                frmHastaDetay.Show();
                this.Hide();
            }
            else
            {
                
               
             MessageBox.Show("Hatalı TC & Şifre","Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            bql.baglanti().Close();



        }

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }
    }
}
