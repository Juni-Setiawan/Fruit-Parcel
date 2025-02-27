using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fruit_Parcel
{
    public partial class Login : Form
    {

        DB db;
        public Login()
        {
            InitializeComponent();
            db = new DB();
            if (!db.connect())
            {
                MessageBox.Show("Koneksi Database Gagal");
                this.Close();
            }
        }

      

        private void panel1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;


            DataSet dataSet = new DataSet();
            int i = db.executeWithData("SELECT id FROM user WHERE username='"+username+"' AND password =md5('"+password+"')").Fill(dataSet);
            if (i > 0)
            {
                MessageBox.Show("Login Berhasil!!");

                new Menu().Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Username/Password Salah");
            }

        }
    }
    
}
