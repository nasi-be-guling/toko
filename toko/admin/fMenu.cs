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
using _tools;


namespace toko.admin
{
    public partial class FMenu : Form
    {
        CConnection _connect = new CConnection();
        CTools _tools = new CTools();
        private MySqlConnection _connection;
        private string sqlQuery;
        private readonly string _configurationManager = Properties.Settings.Default.Setting;

        public FMenu()
        {
            InitializeComponent();
        }
        
        private void btnSimpan_Click(object sender, EventArgs e)
        {
            Control theControl = null;
            sqlQuery = "";
            string errMsg = "";
            _connection = _connect.Connect(_configurationManager, ref errMsg, "123");

            int maxid = _connect.GetMaxId(_connection, "db_toko.tmenu", "id") + 1;

            if (!string.IsNullOrEmpty(errMsg))
            {
                MessageBox.Show(errMsg);
                return;
            }
                
            if (_tools.CheckEmptyTextBox(this, ref theControl))
            {
                MessageBox.Show(@"Silahkan Lengkapi Data");
                theControl.Focus();
                return;
            }
            try
            {
                _connect.Insertion(
                    "insert into db_toko.tMenu values (" + maxid + ", '" + txtNama.Text + "', '" + txtNamaForm.Text +
                    "')", _connection);
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(@"Error performing insert to database : " + ex.Message);
                _connection.Close();
                return;
            }
            _connection.Close();
        }
    }
}
