using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ApprainERPTerminal.Modules.Voucher.UI
{
    public partial class FrmVoucherEntry : Form
    {
        public FrmVoucherEntry()
        {
            InitializeComponent();
        }

        private void FrmVoucherEntry_Load(object sender, EventArgs e)
        {
            LoadCustomers();
            LoadPurposeCodes();
            LoadCompanyAccounts();


            // Purpose column
            var colPurpose = new DataGridViewComboBoxColumn();
            colPurpose.Name = "PurposeCode";
            colPurpose.HeaderText = "Purpose Code";
            colPurpose.Items.Add("ADVANCE");
            colPurpose.Items.Add("SALES");
            colPurpose.Items.Add("SERVICE");
            dgvRows.Columns.Add(colPurpose);

            // Description
            dgvRows.Columns.Add("Description", "Description");

            // Amount
            dgvRows.Columns.Add("Amount", "Amount");

            dgvRows.CellValueChanged += dgvRows_CellValueChanged;
            dgvRows.RowsRemoved += dgvRows_RowsRemoved;
        }




        private bool SaveVoucherLocal()
        {
            var rows = GetRowsFromGrid();

            if (rows.Count == 0)
            {
                MessageBox.Show("No transaction rows entered");
                return false;
            }

            if (cboCustomer.SelectedValue == null)
            {
                MessageBox.Show("Please select a customer");
                return false;
            }

            if (cboCompanyAcc.SelectedValue == null)
            {
                MessageBox.Show("Please select a bill account");
                return false;
            }

            var service = new VoucherService();

            service.CreateVoucher(
                clientId: Convert.ToInt32(cboCustomer.SelectedValue),
                action: lblAction.Text.Trim(),   // RECEIVABLE
                voucherDate: dtVoucherDate.Value,
                total: rows.Sum(r => r.Amount),
                subject: txtSubject.Text,
                note: txtNote.Text,
                trowsObject: rows,
                entryCode: rows[0].PurposeCode,
                companyAccount: cboCompanyAcc.SelectedValue.ToString(),
                operatorId: 1
            );

            return true;
        }



        private void btnSave_Click(object sender, EventArgs e)
        {
            if (SaveVoucherLocal())
            {
                MessageBox.Show("Voucher saved locally (Pending Sync)");
                ClearForm();
            }
        }


        private DataTable LoadPurposeCodes()
        {
            var dt = new DataTable();
            dt.Columns.Add("code", typeof(string));
            dt.Columns.Add("name", typeof(string));

            dt.Rows.Add("ADVANCE", "Advance");
            dt.Rows.Add("SALES", "Sales");
            dt.Rows.Add("SERVICE", "Service Charge");

            return dt;
        }


        private void LoadCustomers()
        {
            var dt = new DataTable();
            dt.Columns.Add("id", typeof(int));
            dt.Columns.Add("name", typeof(string));

            dt.Rows.Add(1, "Ayan MRK");
            dt.Rows.Add(2, "Hungry Duck");
            dt.Rows.Add(3, "Walk-in Customer");

            cboCustomer.DataSource = dt;
            cboCustomer.DisplayMember = "name";
            cboCustomer.ValueMember = "id";
            cboCustomer.SelectedIndex = -1;
        }



        private void ClearForm()
        {
            dgvRows.Rows.Clear();
            txtSubject.Clear();
            txtNote.Clear();
            lblTotal.Text = "TOTAL: 0.00";
        }




        private void LoadCompanyAccounts()
        {
            var dt = new DataTable();
            dt.Columns.Add("code", typeof(string));
            dt.Columns.Add("name", typeof(string));

            dt.Rows.Add("INCOME A/C", "Income Account");
            dt.Rows.Add("CASH A/C", "Cash Account");
            dt.Rows.Add("BANK A/C", "Bank Account");

            cboCompanyAcc.DataSource = dt;
            cboCompanyAcc.DisplayMember = "name";
            cboCompanyAcc.ValueMember = "code";
            cboCompanyAcc.SelectedIndex = -1;
        }



        private void dgvRows_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            CalculateTotal();
        }

        private void dgvRows_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            CalculateTotal();
        }

        private void CalculateTotal()
        {
            decimal total = 0;

            foreach (DataGridViewRow row in dgvRows.Rows)
            {
                if (row.Cells["Amount"].Value != null)
                {
                    decimal.TryParse(row.Cells["Amount"].Value.ToString(), out decimal amt);
                    total += amt;
                }
            }

            lblTotal.Text = "TOTAL: " + total.ToString("N2");
        }

        private List<VoucherRowModel> GetRowsFromGrid()
        {
            var rows = new List<VoucherRowModel>();

            foreach (DataGridViewRow row in dgvRows.Rows)
            {
                if (row.IsNewRow) continue;

                if (row.Cells["Amount"].Value == null) continue;

                decimal.TryParse(row.Cells["Amount"].Value.ToString(), out decimal amount);
                if (amount <= 0) continue;

                rows.Add(new VoucherRowModel
                {
                    PurposeCode = row.Cells["PurposeCode"].Value?.ToString() ?? "",
                    Description = row.Cells["Description"].Value?.ToString() ?? "",
                    Amount = (double)amount
                });
            }

            return rows;
        }

        private void cboCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboCustomer.SelectedValue == null) return;

            int clientId = 1;// Convert.ToInt32(cboCustomer.SelectedValue);
            LoadCustomerBalance(clientId);

            //ValidateForm();
        }
        private void LoadCustomerBalance(int clientId)
        {
            DatabaseHelper db = new DatabaseHelper();

            DataTable dt = db.selectQuery(
                $"SELECT balance FROM app_accounts WHERE clientid={clientId} LIMIT 1"
            );

            if (dt.Rows.Count > 0)
            {
                decimal balance = Convert.ToDecimal(dt.Rows[0]["balance"]);
                lblBalance.Text = balance.ToString("N2");
                lblBalance.ForeColor = balance < 0 ? Color.Red : Color.Green;
            }
            else
            {
                lblBalance.Text = "0.00";
            }

           // ValidateForm();

        }

        private void ValidateForm()
        {
            btnSave.Enabled =
                cboCustomer.SelectedValue != null &&
                cboCompanyAcc.SelectedValue != null;
        }

        private void dgvRows_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private async void btnSaveSync_Click(object sender, EventArgs e)
        {
            if (!SaveVoucherLocal())
                return;

            try
            {
                btnSaveSync.Enabled = false;
                btnSaveSync.Text = "Syncing...";

                var syncService = new VoucherSyncService();
                await syncService.SyncPendingVouchers();

                MessageBox.Show("Voucher saved and synced successfully");
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Voucher saved locally, but sync failed.\n\n" + ex.Message,
                    "Sync Warning",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
            }
            finally
            {
                btnSaveSync.Text = "Save and Sync";
                btnSaveSync.Enabled = true;
            }
        }

    }

}
