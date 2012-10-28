using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Hqub.GlobalStatDC100.Host.Dialogs
{
    public partial class SetIdDeviceDialog : Form
    {
        public SetIdDeviceDialog()
        {
            InitializeComponent();
        }

        public int Id
        {
            get
            {
                return !string.IsNullOrEmpty(textIdDevice.Text) ? int.Parse(textIdDevice.Text) : 0;
            }
            private set
            {

            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(textIdDevice.Text))
            {
                MessageBox.Show(Strings.ID_DEVICE_EMPTY);
                textIdDevice.Focus();
                return;
            }

            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
