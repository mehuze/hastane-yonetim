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


namespace HastaTakipSistemi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        frmSqlBaglanti bgl =new frmSqlBaglanti();
        private void btn_Update_Click(object sender, EventArgs e)
        {
        frmKayit fr =new frmKayit();
            fr.Show();

        }

        private void btn_Login_Click(object sender, EventArgs e)
        {
            if (txt_UserName.Text !=""&& txt_Password.Text!="")
            {
                SqlCommand giris =new SqlCommand("girisYap",bgl.baglan());
                giris.CommandType=CommandType.StoredProcedure;
                giris.Parameters.AddWithValue("KullanıcıAdı",txt_UserName.Text);
                giris.Parameters.AddWithValue("sifre",txt_Password.Text);
                SqlDataReader dr = giris.ExecuteReader();
                if (dr.Read())
                {
                    MessageBox.Show("Giriş İşemi Başarılı", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    frmAnaSayfa fr =new frmAnaSayfa();
                    this.Hide();
                    fr.Show();
                   
                }
            }
            else
            {
                MessageBox.Show("Giriş İşemi Başarısız", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
