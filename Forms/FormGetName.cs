using System.Windows.Forms;

namespace ReportingServerManager.Forms
{
    public partial class FormGetName : Form
    {
        public FormGetName(string caption)
        {
            InitializeComponent();

            Text = caption; 
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