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
    public partial class FrmBilgiDuzenle : Form
    {
        public FrmBilgiDuzenle()
        {
            InitializeComponent();
        }
        public string TCno;
        sqlbaglanti bql = new sqlbaglanti();
        private void FrmBilgiDuzenle_Load(object sender, EventArgs e)
        {
            maskedTextBox1.Text = TCno;
            //verileri şartlı  listeleme
            //listelemede sqldataadapter kullanılır.

            SqlCommand komut = new SqlCommand("select* from Tbl_Hastalar where HastaTC=@p1", bql.baglanti());
            komut.Parameters.AddWithValue("@p1", maskedTextBox1.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                textBox1.Text = dr[1].ToString();
                textBox2.Text = dr[2].ToString();
                maskedTextBox2.Text = dr[4].ToString();
                textBox3.Text = dr[5].ToString();
                comboBox1.Text = dr[6].ToString();

            }
            bql.baglanti().Close();


        }

        private void BtnBilgiDuzenle_Click(object sender, EventArgs e)
        {
            SqlCommand komutguncelle = new SqlCommand("Update Tbl_Hastalar set HastaAd=@p1,HastaSoyad=@p2,HastaTelefon=@p3,HastaSifre=@p4,HastaCinsiyet=@p5 where HastaTc=@p6", bql.baglanti());
            komutguncelle.Parameters.AddWithValue("@p1", textBox1.Text);
            komutguncelle.Parameters.AddWithValue("@p2", textBox2.Text);
            komutguncelle.Parameters.AddWithValue("@p3", maskedTextBox2.Text);
            komutguncelle.Parameters.AddWithValue("@p4", textBox3.Text);
            komutguncelle.Parameters.AddWithValue("@p5", comboBox1.Text);
            komutguncelle.Parameters.AddWithValue("@p6", maskedTextBox1.Text);

            komutguncelle.ExecuteNonQuery();
            bql.baglanti().Close();
            MessageBox.Show("Bilgileriniz Güncellendi","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Warning);
        }
    }
}
