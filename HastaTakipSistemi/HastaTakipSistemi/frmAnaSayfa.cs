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
using System.Drawing.Text;

namespace HastaTakipSistemi
{
    public partial class frmAnaSayfa : Form
    {
        public frmAnaSayfa()
        {
            InitializeComponent();
        }
        frmSqlBaglanti bgl=new frmSqlBaglanti();
        private void frmAnaSayfa_Load(object sender, EventArgs e)
        {
            Listele();
            durumDoldur();
            bolumDoldur();
        }
        private void Listele()
        {
            SqlCommand liste = new SqlCommand("Listele", bgl.baglan());
            SqlDataAdapter da = new SqlDataAdapter(liste);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        private void durumDoldur()
        {
            SqlCommand durum = new SqlCommand("durumDoldur",bgl.baglan());
            SqlDataAdapter da = new SqlDataAdapter(durum);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cmbDurum.DataSource = dt;
            cmbDurum.DisplayMember = "durumAd";
            cmbDurum.ValueMember = "durumID";
        }
        private void bolumDoldur()
        {
            SqlCommand bolum = new SqlCommand("bolumDoldur", bgl.baglan());
            SqlDataAdapter da = new SqlDataAdapter(bolum);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cmbBolum.DataSource = dt;
            cmbBolum.DisplayMember = "bolumAd";
            cmbBolum.ValueMember = "bolumID";
        }


        private void btnListele_Click(object sender, EventArgs e)
        {
            Listele();
            
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            txtID.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            txtAD.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            txtSoyad.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            txtTC.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            txtTelefon.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            txtYas.Text = dataGridView1.Rows[secilen].Cells[5].Value.ToString();
            txtCinsiyet.Text = dataGridView1.Rows[secilen].Cells[6].Value.ToString();
            txtSikayet.Text = dataGridView1.Rows[secilen].Cells[7].Value.ToString();
            txtTarih.Text = dataGridView1.Rows[secilen].Cells[8].Value.ToString();
            cmbDurum.SelectedValue = dataGridView1.Rows[secilen].Cells[9].Value.ToString();
            cmbBolum.SelectedValue = dataGridView1.Rows[secilen].Cells[10].Value.ToString();
            lblEx.Text = dataGridView1.Rows[secilen].Cells[11].Value.ToString();
        }

        private void rbEvet_CheckedChanged(object sender, EventArgs e)
        {
            if (rbEvet.Checked == true)
            {
                lblEx.Text = "true";
            }
            else
            {
                lblEx.Text = "False";
            }
        }

        private void rbHayır_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void lblEx_TextChanged(object sender, EventArgs e)
        {
            if (lblEx.Text == "True" && rbEvet.Checked == false)
            {
                rbEvet.Checked = true;
            }
            else if (lblEx.Text == "False" && rbHayır.Checked == false)
            {
                rbHayır.Checked = true;
            }
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            if (txtAD.Text!=""&&cmbBolum.Text!=""&&txtCinsiyet.Text!=""&&cmbDurum.Text!=""&&txtSikayet.Text != "" && txtSoyad.Text != "" && txtTC.Text != "" && txtYas.Text != "" && txtTelefon.Text != "" )
            {
                kaydet(); 
            }
            else
            {
                MessageBox.Show("lütfen ilgili tüm alanları doldurunuz!","kayıt başarısız",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void kaydet()
        {
            SqlCommand komut = new SqlCommand("kaydet", bgl.baglan());
            komut.CommandType = CommandType.StoredProcedure;
            ParametreleriEkle(komut);
            komut.ExecuteNonQuery();
            MessageBox.Show("kayıt başarılı", "bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Listele();

        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("guncelle", bgl.baglan());
            komut.CommandType = CommandType.StoredProcedure;
            ParametreleriEkle(komut);
            komut.ExecuteNonQuery();
            MessageBox.Show("kayıt başarıyla güncellendi", "bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Listele();
        }
        
        

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void lblEx_Click(object sender, EventArgs e)
        {

        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            sil();
        }
       
       private void sil()
        {
            //farklı bir yerde kullanmak için metod oluşturuyorum direk sil metodunu çağırıp silmek için
            DialogResult dr = MessageBox.Show($"{txtID.Text }numaralı kayıt silinecek.Onaylıyor musunuz?","Onay",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            if (dr==DialogResult.Yes)
            {
                SqlCommand sil = new SqlCommand("sil", bgl.baglan());
                sil.CommandType = CommandType.StoredProcedure;
                sil.Parameters.AddWithValue("id", int.Parse(txtID.Text));
                sil.ExecuteNonQuery();
                MessageBox.Show("kayıt başarıyla silindi", "kayıt silme  başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Listele();

            }
        }
        private void ParametreleriEkle(SqlCommand komut)
        {
            komut.CommandType = CommandType.StoredProcedure;

            bool isGuncelle = komut.CommandText.Equals("guncelle", StringComparison.OrdinalIgnoreCase);

            if (isGuncelle)
            {
                if (int.TryParse(txtID.Text, out int id))
                    komut.Parameters.AddWithValue("@id", id);
                else
                {
                    MessageBox.Show("Güncelleme için geçerli bir ID girilmedi!");
                    return;
                }
            }


            komut.CommandType = CommandType.StoredProcedure;
            //komut.Parameters.AddWithValue("@id",int.Parse(txtID.Text));
            komut.Parameters.AddWithValue("ad", txtAD.Text);
            komut.Parameters.AddWithValue("soyad", txtSoyad.Text);
            komut.Parameters.AddWithValue("tc", txtTC.Text);
            komut.Parameters.AddWithValue("tel", txtTelefon.Text);
            komut.Parameters.AddWithValue("yas", int.Parse(txtYas.Text.ToString()));
            komut.Parameters.AddWithValue("cins", txtCinsiyet.Text);
            komut.Parameters.AddWithValue("sikayet", txtSikayet.Text);
            komut.Parameters.AddWithValue("tarih", DateTime.Now);
            komut.Parameters.AddWithValue("durum", cmbDurum.SelectedValue);
            komut.Parameters.AddWithValue("bolum", cmbBolum.SelectedValue);
            if (lblEx.Text == "True")
            {
                komut.Parameters.AddWithValue("ex", 1);
            }
            else
            {
                komut.Parameters.AddWithValue("ex", 0);
            }
           

        }
        
        private void temizle()
        {
            txtAD.Text = "";
            cmbBolum.Text = "";
            cmbDurum.Text = "";
            txtID.Text = "";
            txtSikayet.Text = "";
            txtCinsiyet.Text = "";
            txtSoyad.Text = "";
            txtYas.Text = "";
            txtTC.Text = "";
            txtTelefon.Text = "";
            txtTarih.Text = "";
            rbHayır.Checked = true;
            lblEx.Text = "False";
        }

        private void btnFormuTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }
    }
}
