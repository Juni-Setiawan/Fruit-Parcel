using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Fruit_Parcel
{
    class DB
    {
        private MySqlConnection conn;
        private MySqlCommand cmd;
        public bool connect()
        {
            string myConnectionString = "server=localhost;uid=root;pwd=;database=fruit_parcel";

            try
            {
                conn = new MySqlConnection();
                conn.ConnectionString = myConnectionString;
                conn.Open();
                cmd = new MySqlCommand();
                return true;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

        }

        public bool execute(string query)
        {
            try
            {
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = query;
                cmd.Connection = conn;
                cmd.ExecuteNonQuery();
                return true;
            }catch(MySqlException e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
        }
        public MySqlDataAdapter executeWithData(string query)
        {
            try
            {
                return new MySqlDataAdapter(query, conn);
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }

        }

    }
}
