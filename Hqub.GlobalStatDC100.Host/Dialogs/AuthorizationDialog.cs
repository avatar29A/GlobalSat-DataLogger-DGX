using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Hqub.GlobalStatDC100.Host.Dialogs
{
    public partial class AuthorizationDialog : Form
    {
        public AuthorizationDialog()
        {
            InitializeComponent();
        }

        public string Password
        {
            get { return textPassword.Text; }
        }

        private DialogResult Result { get; set; }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Result = DialogResult.Cancel;
            Close();
        }

        public DialogResult ShowDlg()
        {
            ShowDialog();
            return Result;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textPassword.Text))
            {
                MessageBox.Show(Strings.PASSWORD_IS_EMPTY);
                return;
            }

            Result = DialogResult.OK;
            Close();
        }
    }
}
