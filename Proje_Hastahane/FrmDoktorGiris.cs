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
    public partial class FrmDoktorGiris : Form
    {
        public FrmDoktorGiris()
        {
            InitializeComponent();
        }
        sqlbaglanti bql = new sqlbaglanti();
        private void BtnGirisYap_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("select DoktorAd,DoktorSoyad from Tbl_Doktorlar where DoktorTC=@p1 and DoktorSifre=@p2", bql.baglanti());
            komut.Parameters.AddWithValue("@p1", maskedTextBox1.Text);
            komut.Parameters.AddWithValue("@p2", textBox1.Text);
            SqlDataReader dr = komut.ExecuteReader(); //komuttan gelen verileri okuyup yazdırdık.
            if (dr.Read())//okuma işlemi doğru şekilde yapılıyorsa mı diye kontrol etmek için..
            {
                FrmDoktorDetayGor frmDoktorDetay = new FrmDoktorDetayGor();
                frmDoktorDetay.tc = maskedTextBox1.Text;
                frmDoktorDetay.ad_soyad = dr[0]+" "+dr[1];
                frmDoktorDetay.Show();
                this.Hide();
            }
            else
            {


                MessageBox.Show("Hatalı TC & Şifre", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            bql.baglanti().Close();
        }

        private void FrmDoktorGiris_Load(object sender, EventArgs e)
        {

        }
    }
}
