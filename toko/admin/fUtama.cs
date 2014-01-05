using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace toko.admin
{
    public partial class fUtama : Form
    {
        public fUtama()
        {
            InitializeComponent();
        }

        private void fUtama_Load(object sender, EventArgs e)
        {
            for (int i = 0; i <= 2; i++)
            {
                ToolStripItem item = new ToolStripMenuItem();
                //Name that will apear on the menu
                item.Text = "Jhon Smith";
                //Put in the Name property whatever neccessery to retrive your data on click event
                item.Name = i.ToString();
                //On-Click event
                item.Click += new EventHandler(item_Click);
                //Add the submenu to the parent menu
                toolStripMenuItem1.DropDownItems.Add(item);
            }
        }

        void item_Click(object sender, EventArgs e)
        {
            //ToolStripItem item = new ToolStripMenuItem();
            //if (item.Text == "Jhon Smith")
            ToolStripItem item = (ToolStripMenuItem)sender;
            if (item.Name == "1")
                MessageBox.Show("Test");
            //throw new NotImplementedException();
        }
    }
}
