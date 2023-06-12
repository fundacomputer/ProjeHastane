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
    public partial class FrmHastaDetay : Form
    {
        public FrmHastaDetay()
        {
            InitializeComponent();
        }

        public string tc;
        sqlbaglanti bql = new sqlbaglanti();
        private void FrmHastaDetay_Load(object sender, EventArgs e)
        {
            //ad soyad ve  tc ekleme
            LblTC.Text = tc;
            SqlCommand komut = new SqlCommand("select HastaAd,HastaSoyad from Tbl_Hastalar where HastaTc=@p1", bql.baglanti());
            komut.Parameters.AddWithValue("@p1", LblTC.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                LblAdSoyad.Text = dr[0] + "  " + dr[1];
            }
            bql.baglanti().Close();
            //randevu geçmişi
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select *from Tbl_Randevular where HastaTC=" + tc, bql.baglanti());
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            //datagridde baglantı açıp kapatmaya gere yoktur.

            //branşları çekme
            SqlCommand komut2 = new SqlCommand("select BransAd from Tbl_Branslar", bql.baglanti());
            SqlDataReader dr2 = komut2.ExecuteReader();//verileri listemedem okumak için

            while (dr2.Read())///while koşuluyla verilerin doğru okunmasını sağlıyoruz.
            {
                CmbBrans.Items.Add(dr2[0]);
            }
            bql.baglanti().Close();
        }   

        private void LblTC_Click(object sender, EventArgs e)
        {

        }

        private void CmbBrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            CmbDoktor.Items.Clear();//cmbdoktor içindeki verileri temizlemek.Yoksa bütün veriler alt alta tekrarlanıyor.
            SqlCommand komut3 = new SqlCommand("select DoktorAd,DoktorSoyad from Tbl_Doktorlar where DoktorBrans=@p1", bql.baglanti());
            komut3.Parameters.AddWithValue("@p1", CmbBrans.Text);//where yazdığımız şartı aldık.
            SqlDataReader dr3 = komut3.ExecuteReader();//verileri listemedem okumak için

            while (dr3.Read())///while koşuluyla verilerin doğru okunmasını sağlıyoruz.
            {
                CmbDoktor.Items.Add(dr3[0]+" "+dr3[1]);
            }
            bql.baglanti().Close();
        }

        private void CmbDoktor_SelectedIndexChanged(object sender, EventArgs e)
        {
            //datagridview veri çekmede sqldataadapter kullanılır.
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select *from Tbl_Randevular where RandevuBrans='" + CmbBrans.Text + "'" + " and RandevuDoktor='" + CmbDoktor.Text + "' and  RandevuDurum=0", bql.baglanti());
            da.Fill(dt);
            dataGridView2.DataSource = dt;

        }

        private void LnkBilgiDuzenle_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmBilgiDuzenle frmBilgiDuzenle = new FrmBilgiDuzenle();
            frmBilgiDuzenle.TCno = LblTC.Text;
            

            frmBilgiDuzenle.Show();
        }

        private void BtnRandevuAl_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("update Tbl_Randevular set RandevuDurum=1,HastaTC=@p1,HastaSikayet=@p2 where Randevuid=@p3", bql.baglanti());
           var d=komut.Parameters.AddWithValue("@p1",LblTC.Text);
           var d1= komut.Parameters.AddWithValue("@p2", RchSikayet.Text);
           var d3= komut.Parameters.AddWithValue("@p3",Txtİd.Text);

           var deger= komut.ExecuteNonQuery();
            bql.baglanti().Close();
            MessageBox.Show("Randevu Güncellendi");
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //int secilen = dataGridView2.SelectedCells[0].RowIndex;
            //Txtİd.Text = dataGridView2.Rows[secilen].Cells[0].Value.ToString();
            Txtİd.Text = dataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString();
        }
    }
}
