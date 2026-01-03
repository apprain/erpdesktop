using ApprainERPTerminal.Modules;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ApprainERPTerminal
{
    public partial class FrmPinVerification : Form
    {
        String PinProfile = "";
        public FrmPinVerification(String Profile)
        {
            PinProfile = Profile;
            InitializeComponent();
        }

        private void FrmPinVerification_Load(object sender, EventArgs e)
        {
            Text = "[" + PinProfile + "]";
        }

        private void txtPin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                string[] common = Utility.PinList("Master");
                if (common.Contains(txtPin.Text))
                {
                    App.PIN_VERIFIED = "SUCCESSFUL";
                }
                else
                {
                    common = Utility.PinList(PinProfile);
                    if (common.Contains(txtPin.Text))
                    {
                        App.PIN_VERIFIED = "SUCCESSFUL";
                    }
                }               
                this.Dispose();
            }
        }
    }
}
