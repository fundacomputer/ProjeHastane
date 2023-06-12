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
    public partial class FrmBransPanel : Form
    {
        public FrmBransPanel()
        {
            InitializeComponent();
        }
        sqlbaglanti bql = new sqlbaglanti();
        private void BtnEkle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into Tbl_Branslar (BransAd) values (@b1)", bql.baglanti());
            komut.Parameters.AddWithValue("@b1", TxtBrans.Text);
            komut.ExecuteNonQuery();
            bql.baglanti().Close();
            MessageBox.Show("Kayıt Eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void FrmBransPanel_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter dr = new SqlDataAdapter("Select*From Tbl_Branslar", bql.baglanti());
            dr.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Delete from Tbl_Branslar where Bransid=@b1", bql.baglanti());
            komut.Parameters.AddWithValue("@b1", Txtİd.Text);
            komut.ExecuteNonQuery();
            Txtİd.Clear();
            TxtBrans.Clear();
            bql.baglanti().Close();
            MessageBox.Show("Kayıt Silindi");

        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Update Tbl_Branslar set BransAd=@b2 where Bransid=@b1", bql.baglanti());
            komut.Parameters.AddWithValue("@b1", Txtİd.Text);
            komut.Parameters.AddWithValue("@b2", TxtBrans.Text);
            komut.ExecuteNonQuery();
            bql.baglanti().Close();
            MessageBox.Show("Kayıt Güncellendi");
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Txtİd.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            TxtBrans.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
        }
    }
}
