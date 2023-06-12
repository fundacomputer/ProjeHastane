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
    public partial class FrmDoktorDetayGor : Form
    {
        public FrmDoktorDetayGor()
        {
            InitializeComponent();
        }
        sqlbaglanti bql = new sqlbaglanti();
        public string tc;
        public string ad_soyad;
        private void groupBox3_Enter(object sender, EventArgs e)
        {
           
        }

        private void FrmDoktorDetayGor_Load(object sender, EventArgs e)
        {
            LblTcNo.Text = tc;
            LblAdSoyad.Text = ad_soyad;


            //bu doktora ait  randevular
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select *from  Tbl_Randevular where RandevuDoktor='" + LblAdSoyad.Text +"'", bql.baglanti());
             da.Fill(dt);
            dataGridView1.DataSource = dt;
           
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            FrmDoktorBilgiDuzenle frmDoktorBilgiDuzenle = new FrmDoktorBilgiDuzenle();
            frmDoktorBilgiDuzenle.TCNO= LblTcNo.Text;
            frmDoktorBilgiDuzenle.Show();
            
        }

        private void BtnDuyuru_Click(object sender, EventArgs e)
        {
            FrmDuyurular frmDuyurular = new FrmDuyurular();
            frmDuyurular.Show();
        }

        private void BtnCikis_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            RchSikayet.Text = dataGridView1.Rows[secilen].Cells[7].Value.ToString();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
