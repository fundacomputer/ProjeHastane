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
    public partial class FrmSekreterDetay : Form
    {
        public FrmSekreterDetay()
        {
            InitializeComponent();
        }

        public string tc;
        public string ad_soyad;
        sqlbaglanti bql = new sqlbaglanti();
        private void FrmSekreterDetay_Load(object sender, EventArgs e)
        {
            //ad soyad ve tc kimlik numarası çekmek için yazdım.
            label2.Text = tc;
            label4.Text = ad_soyad;
            //branşları çekeceğim
            //branşları çekmek için önce sanal tablo oluşturuyorum.Sanal tablo datatble nesnesini kullanarak oluşturacağım.
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select *from Tbl_Branslar", bql.baglanti());
            da.Fill(dt);
            dataGridView1.DataSource = dt;

            DataTable dt1 = new DataTable();
            SqlDataAdapter da1 = new SqlDataAdapter("select (DoktorAd +' '+ DoktorSoyad) as 'Doktorlar',DoktorBrans  as 'Doktor Brans'from Tbl_Doktorlar", bql.baglanti());
            da1.Fill(dt1);
            dataGridView2.DataSource = dt1;
            //comboboxda branş verilerini listelemek için
            SqlCommand komutCmbBrans = new SqlCommand("Select*From Tbl_Branslar", bql.baglanti());
            SqlDataAdapter da2 = new SqlDataAdapter(komutCmbBrans);
            DataTable dt2 = new DataTable();
            da2.Fill(dt2);
            comboBox1.ValueMember = "Bransid";
            comboBox1.DisplayMember = "BransAd";
            comboBox1.DataSource = dt2;
            //comboboxda doktor ad ve soyad getirme
            //SqlCommand komutCmbDoktor = new SqlCommand("Select DoktorAd,DoktorSoyad from Tbl_Doktorlar", bql.baglanti());
            //SqlDataReader dr2 = komutCmbDoktor.ExecuteReader();
            //while (dr2.Read())
            //{
            //    CmbDoktor.Items.Add(dr2[0] + " " + dr2[1]);
            //}
            //bql.baglanti().Close();

        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komutKaydet = new SqlCommand("insert into Tbl_Randevular (RandevuTarih,RandevuSaat,RandevuBrans,RandevuDoktor) values(@r1,@r2,@r3,@r4)", bql.baglanti());
            komutKaydet.Parameters.AddWithValue("@r1", MskTarih.Text);
            komutKaydet.Parameters.AddWithValue("@r2", maskedTextBox2.Text);
            komutKaydet.Parameters.AddWithValue("@r3", comboBox1.Text);
            komutKaydet.Parameters.AddWithValue("@r4", CmbDoktor.Text);
            komutKaydet.ExecuteNonQuery();//değişiklikleri kaydettim.
            bql.baglanti().Close();
            MessageBox.Show("Randevu Oluşturuldu");
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            CmbDoktor.Items.Clear();
            SqlCommand komut = new SqlCommand("Select DoktorAd,DoktorSoyad from Tbl_Doktorlar where DoktorBrans=@p1", bql.baglanti());
            komut.Parameters.AddWithValue("@p1", comboBox1.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                CmbDoktor.Items.Add(dr[0] + " " + dr[1]);
            }
            bql.baglanti().Close();
        }

        private void BtnDuyuruOlustur_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into Tbl_Duyurular (Duyuru) values(@d1)", bql.baglanti());
            komut.Parameters.AddWithValue("@d1", richTextBox1.Text);
            komut.ExecuteNonQuery();
            bql.baglanti().Close();
            MessageBox.Show("Duyuru Oluşturuldu");
            
        }

        private void BtnDoktorPanel_Click(object sender, EventArgs e)
        {
            FrmDoktorPanel frmDoktorPanel = new FrmDoktorPanel();
            frmDoktorPanel.Show();

        }

        private void BtnBransPanel_Click(object sender, EventArgs e)
        {
            FrmBransPanel frmBransPanel = new FrmBransPanel();
            frmBransPanel.Show();
        }

        private void BtnRandevuListe_Click(object sender, EventArgs e)
        {
            FrmRandevuListesi frmRandevular = new FrmRandevuListesi();
            frmRandevular.Show();
        }

        private void BtnDuyurular_Click(object sender, EventArgs e)
        {
            FrmDuyurular frmDuyurular = new FrmDuyurular();
            frmDuyurular.Show();
        }
    }
}
