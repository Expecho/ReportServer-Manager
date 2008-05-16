using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace RSS_Report_Retrievers
{
    public partial class FormGetName : Form
    {
        public FormGetName(string caption)
        {
            InitializeComponent();

            this.Text = caption; 
        }

        new public string Name
        {
            get
            {
                return txtName.Text; 
            }
            set
            {
                txtName.Text = value;
            }
        }
    }
}