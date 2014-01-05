using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using _connectMySQL;

namespace toko.admin
{
    public partial class FUser : Form
    {
        readonly CConnection _connect = new CConnection();
        private MySqlConnection _connection;
        private readonly string _configurationManager = Properties.Settings.Default.Setting;

        public FUser()
        {
            InitializeComponent();
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            string errMsg = "";
            _connection = _connect.Connect(_configurationManager, ref errMsg, "123");
            int MaxId = _connect.GetMaxId(connection: _connection, tableName: "db_toko.tmutasibarang", tableId: "id");

            //if ()

            //try
            //{

            //}
            //catch (Exception)
            //{
                
            //    throw;
            //}

            _connection.Close();
        }


    }
}
