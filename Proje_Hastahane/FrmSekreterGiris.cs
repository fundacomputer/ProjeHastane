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
    public partial class FrmSekreterGiris : Form
    {
        public FrmSekreterGiris()
        {
            InitializeComponent();
        }

        sqlbaglanti bql = new sqlbaglanti();
        private void BtnGirisYap_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("select SekreterAdSoyad from Tbl_Sekreter where SekreterTC=@p1 and SekreterSifre=@p2", bql.baglanti());
            komut.Parameters.AddWithValue("@p1", maskedTextBox1.Text);
            komut.Parameters.AddWithValue("@p2", textBox1.Text);
            SqlDataReader dr = komut.ExecuteReader(); //komuttan gelen verileri okuyup yazdırdık.
            if (dr.Read())//okuma işlemi doğru şekilde yapılıyorsa mı diye kontrol etmek için..
            {
                FrmSekreterDetay frmsekreterDetay = new FrmSekreterDetay();
                frmsekreterDetay.tc = maskedTextBox1.Text;
                frmsekreterDetay.ad_soyad = dr[0].ToString();
                frmsekreterDetay.Show();
                this.Hide();
            }
            else
            {


                MessageBox.Show("Hatalı TC & Şifre", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            bql.baglanti().Close();


        }
    }
}
