using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Fruit_Parcel
{
    public partial class Menu : Form
    {

        DB db;

        bool editable;

        int id = 0;

        public Menu()
        {
            InitializeComponent();
            db = new DB();
            if (!db.connect())
            {
                MessageBox.Show("Koneksi Database Gagal");
                this.Close();
            }
            reset();

            getDataParcel();
        }

        private void getDataParcel()
        {
            DataSet dataSet = new DataSet();
            int i = db.executeWithData("SELECT * FROM parsel").Fill(dataSet);
            if (i > 0) {
                table.DataSource = dataSet.Tables[0];
                table.Refresh();
                table.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                table.Columns[0].HeaderText = "id";
                table.Columns[1].HeaderText = "Jumlah Barang";
                table.Columns[2].HeaderText = "Nama Barang";
                table.Columns[3].HeaderText = "Berat Barang";
                table.Columns[4].HeaderText = "Nilai";
                table.Columns[5].HeaderText = "Kapasitas Parsel";
                table.Columns[6].HeaderText = "Nama Parsel";
                table.Columns[7].HeaderText = "Nama Pembeli";
                table.Columns[0].Visible = false;
            }
            else
            {
                table.DataSource = null;
            }
            
        }

        private void reset()
        {
            inputJumlahBarang.Text = "";
            inputNamaBarang.Text = "";
            inputBeratBarang.Text = "";
            inputNilai.Text = "";
            inputKapasitasParcel.Text = "";
            inputNamaParcel.Text = "";
            inputNamaPembeli.Text = "";
            editable = false;
            id = 0;
            setButton();
        }

        private void setButton()
        {
            if (editable)
            {
                btnSimpan.Text = "Ubah";
            }
            else
            {
                btnSimpan.Text = "Tambah";
            }
        }

        private void btnSimpan_Click(object sender, EventArgs e)
        {
            if (!editable)
            {
                string query = "INSERT INTO parsel(jumlah_barang,nama_barang,berat_barang,nilai,kapasitas_parsel,nama_parsel,nama_pembeli) " +
                    " VALUES('" + inputJumlahBarang.Text + "','" + inputNamaBarang.Text + "','"+inputBeratBarang.Text+"'" +
                    " ,'"+inputNilai.Text+"','"+inputKapasitasParcel.Text+"','"+inputNamaParcel.Text+"','"+inputNamaPembeli.Text+"')";
                if (db.execute(query))
                {
                    MessageBox.Show("Data Parsel Berhasil Ditambah");
                    getDataParcel();
                    reset();
                }
            }
            else
            {

                string query = "UPDATE parsel SET jumlah_barang='" + inputJumlahBarang.Text + "',nama_barang='" + inputNamaBarang.Text + "'," +
                    "berat_barang='"+inputBeratBarang.Text+"',nilai='"+inputNilai.Text+"'," +
                    "kapasitas_parsel='"+inputKapasitasParcel.Text+"',nama_parsel='"+inputNamaParcel.Text+"'," +
                    "nama_pembeli='"+inputNamaPembeli.Text+"' WHERE id='"+id+"'";
                if (db.execute(query))
                {
                    MessageBox.Show("Data Parsel Berhasil Diubah");
                    getDataParcel();
                    reset();
                }

            }
        }

        
        private void btnHapus_Click(object sender, EventArgs e)
        {

            int i = table.SelectedRows[0].Index;
            if (i < table.RowCount - 1)
            {
                id = (int)table[0, i].Value;
                string query = "DELETE FROM parsel WHERE id='" + id + "'";
                if (db.execute(query))
                {
                    MessageBox.Show("Data Parsel Berhasil Dihapus");
                    getDataParcel();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            int i = table.SelectedRows[0].Index;
            if (i < table.RowCount - 1)
            {
                id = (int)table[0, i].Value;
                inputJumlahBarang.Text = table[1, i].Value.ToString();
                inputNamaBarang.Text = table[2, i].Value.ToString();
                inputBeratBarang.Text = table[3, i].Value.ToString();
                inputNilai.Text = table[4, i].Value.ToString();
                inputKapasitasParcel.Text = table[5, i].Value.ToString();
                inputNamaParcel.Text = table[6, i].Value.ToString();
                inputNamaPembeli.Text = table[7, i].Value.ToString();
                editable = true;
                setButton();
            }
        }

        private void btnBatal_Click(object sender, EventArgs e)
        {
            reset();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

        }
    }
}
