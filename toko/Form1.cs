using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using _connectMySQL;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace toko
{
    public partial class Form1 : Form
    {
        private readonly CConnection _connect = new CConnection();
        private MySqlConnection _connection;
        //private readonly string _conString =
        //    ConfigurationManager.ConnectionStrings["toko.Properties.Settings.Setting"].ConnectionString;
        
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string errMessage = "";
            _connection = _connect.Connect(Properties.Settings.Default.Setting, ref errMessage, "123");

            MySqlDataReader reader = _connect.Reading("select * from db_toko.tmutasibarang", _connection);

            reader.Read();
            textBox1.Text = reader.GetString(2);

            _connection.Close();
        }
    }
}
