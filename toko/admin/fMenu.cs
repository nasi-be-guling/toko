﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using _connectMySQL;
using _tools;


namespace toko.admin
{
    public partial class FMenu : Form
    {
        #region KOMPONEN WAJIB
        readonly CConnection _connect = new CConnection();
        readonly CTools _tools = new CTools();
        private MySqlConnection _connection;
        private string _sqlQuery;
        private readonly string _configurationManager = Properties.Settings.Default.Setting; 
        #endregion

        public FMenu()
        {
            InitializeComponent();
            label3.TabIndex = 0;
            txtNama.TabIndex = 1;
            label4.TabIndex = 2;
            txtNamaForm.TabIndex = 3;
            btnSimpan.TabIndex = 4;
        }

        private void ShowUser()
        {
            List<ListViewItem> groupItems = new List<ListViewItem>();
            string errMsg = "";
            
            _connection = _connect.Connect(_configurationManager, ref errMsg, "123");

            MySqlDataReader reader = _connect.Reading("SELECT a.namaMenu, a.namaForm , a.id from " +
                "db_toko.tmenu a", _connection);
            
            if (!string.IsNullOrEmpty(errMsg))
            {
                MessageBox.Show(errMsg);
                return;
            }

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    ListViewItem item = new ListViewItem(Convert.ToString(reader[0]));
                    item.SubItems.Add(Convert.ToString(reader[1]));
                    item.SubItems.Add(Convert.ToString(reader[2]));
                    groupItems.Add(item);
                }
                reader.Close();
            }
            lvDaftarUser.BeginUpdate();
            lvDaftarUser.Items.Clear();
            lvDaftarUser.Items.AddRange(groupItems.ToArray());
            lvDaftarUser.EndUpdate();
            groupItems.Clear();

            _tools.AutoResizeListView(lvDaftarUser, true);
            _connection.Close();
        }
        
        private void btnSimpan_Click(object sender, EventArgs e)
        {
            Control theControl = null; // the control which will be returned

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
            ShowUser();
            _tools.ClearControlText(this);
        }

        private void FMenu_Load(object sender, EventArgs e)
        {
            ShowUser();
        }

        // Mendisable column header
        private void lvDaftarUser_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            e.Cancel = true;
            e.NewWidth = lvDaftarUser.Columns[e.ColumnIndex].Width;
        }

        private void lvDaftarUser_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                contextMenuStrip1.Show(lvDaftarUser, e.Location);
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string errMsg = "";
            _connection = _connect.Connect(_configurationManager, ref errMsg, "123");

            _sqlQuery = "delete from db_toko.tmenu where id = " + lvDaftarUser.SelectedItems[0].SubItems[2].Text +
                        "";

            if (!string.IsNullOrEmpty(errMsg))
            {
                MessageBox.Show(errMsg);
                return;
            }

            try
            {
                _connect.Insertion(_sqlQuery
                    , _connection);
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(@"Error performing delete in database : " + ex.Message);
                _connection.Close();
                return;
            }
            _connection.Close();
            ShowUser();
        }

    }
}
