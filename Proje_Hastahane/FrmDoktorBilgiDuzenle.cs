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
    public partial class FrmDoktorBilgiDuzenle : Form
    {
        public FrmDoktorBilgiDuzenle()
        {
            InitializeComponent();
        }

        sqlbaglanti bql = new sqlbaglanti();
        public string TCNO;
        private void FrmDoktorBilgiDuzenle_Load(object sender, EventArgs e)
        {
            maskedTextBox1.Text =TCNO;


            SqlCommand komut2 = new SqlCommand("select BransAd from Tbl_Branslar", bql.baglanti());
            SqlDataReader dr2 = komut2.ExecuteReader();//verileri listemedem okumak için

            while (dr2.Read())///while koşuluyla verilerin doğru okunmasını sağlıyoruz.
            {
                comboBox1.Items.Add(dr2[0]);
            }
            bql.baglanti().Close();


            SqlCommand komut = new SqlCommand("select *from Tbl_Doktorlar where DoktorTC=@p1", bql.baglanti());
            komut.Parameters.AddWithValue("@p1", maskedTextBox1.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                textBox1.Text = dr[1].ToString();
                textBox2.Text = dr[2].ToString();
                comboBox1.Text = dr[3].ToString();
                textBox3.Text = dr[5].ToString();



            }
            bql.baglanti().Close();



        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void BtnBilgiGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("update Tbl_Doktorlar set DoktorAd=@p1,DoktorSoyad=@p2,DoktorBrans=@p3,DoktorSifre=@p4 where DoktorTC=@p5", bql.baglanti());
            komut.Parameters.AddWithValue("@p1", textBox1.Text);
            komut.Parameters.AddWithValue("@p2", textBox2.Text);
            komut.Parameters.AddWithValue("@p3", comboBox1.Text);
            komut.Parameters.AddWithValue("@p4", textBox3.Text);
            komut.Parameters.AddWithValue("@p5", maskedTextBox1.Text);
            komut.ExecuteNonQuery();
            bql.baglanti().Close();
            MessageBox.Show("Kayıt Güncellendi");

        }
    }
}
