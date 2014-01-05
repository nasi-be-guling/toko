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
        readonly CConnection _connect = new CConnection();
        readonly CTools _tools = new CTools();
        private MySqlConnection _connection;
        private string _sqlQuery;
        private readonly string _configurationManager = Properties.Settings.Default.Setting;

        public FMenu()
        {
            InitializeComponent();
            label3.TabIndex = 0;
            txtNama.TabIndex = 1;
            label4.TabIndex = 2;
            txtNamaForm.TabIndex = 3;
            btnSimpan.TabIndex = 4;
        }
        
        private void btnSimpan_Click(object sender, EventArgs e)
        {
            Control theControl = null;

            string errMsg = "";
            _connection = _connect.Connect(_configurationManager, ref errMsg, "123");

            int maxid = _connect.GetMaxId(_connection, "db_toko.tmenu", "id") + 1;

            _sqlQuery = "insert into db_toko.tMenu values (" + maxid + ", '" + txtNama.Text + "', '" + txtNamaForm.Text +
                        "')";

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
                _connect.Insertion(_sqlQuery
                    , _connection);
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(@"Error performing insert to database : " + ex.Message);
                _connection.Close();
                return;
            }
            _connection.Close();
        }

        private void FMenu_Load(object sender, EventArgs e)
        {
            


        }
    }
}
