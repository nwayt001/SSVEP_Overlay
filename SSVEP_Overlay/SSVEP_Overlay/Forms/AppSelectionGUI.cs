using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SSVEP_Overlay.Forms
{
    public partial class AppSelectionGUI : Form
    {

        public int selectedApp = 2;

        public AppSelectionGUI()
        {
            InitializeComponent();

            
            // Create the list of current applications 
            List<string> applications = new List<string>();
            applications.Add("SSVEP Speller");
            applications.Add("SSVEP Movement Control");
            listBox1.DataSource = applications;
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            selectedApp = listBox1.SelectedIndex;
        }

        private void AppSelectionGUI_Load(object sender, EventArgs e)
        {

        }
    }
}
