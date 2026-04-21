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
using System.Data;
namespace HastaTakipSistemi
{
    public partial class frmKayit : Form
    {
        public frmKayit()
        {
            InitializeComponent();
        }
        frmSqlBaglanti bgl =new frmSqlBaglanti();
        private void btn_Update_Click(object sender, EventArgs e)
        {
            if (txt_UserName.Text!=""&& txt_Password.Text !="")
            {
              SqlCommand kayit=new SqlCommand("kayitol",bgl.baglan()); 
                kayit.CommandType=CommandType.StoredProcedure;
                kayit.Parameters.AddWithValue("@kullanıcıadi", txt_UserName.Text);
                kayit.Parameters.AddWithValue("@sifre", txt_Password.Text);
                kayit.ExecuteNonQuery();
                MessageBox.Show("Kayıt İşlemi Başarılı","Kayıt Başarılı",MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Lütfen Tüm Alanları Doldurunuz", "Hata",MessageBoxButtons.OK,MessageBoxIcon.Error);     
            }
        }

        private void frmKayit_Load(object sender, EventArgs e)
        {

        }
    }
}
