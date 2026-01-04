using ApprainERPTerminal.Modules.Voucher.Print;
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
    public partial class FrmVoucherList : Form
    {
        public FrmVoucherList()
        {
            InitializeComponent();
        }

        private void FrmVoucherList_Load(object sender, EventArgs e)
        {
            dgvVouchers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvVouchers.MultiSelect = false;
            dgvVouchers.ReadOnly = true;


            cboStatusFilter.Items.AddRange(
                new string[] { "All", "PENDING", "SYNCED", "FAILED" }
            );
            cboStatusFilter.SelectedIndex = 0;

            LoadVouchers();
        }


        private void LoadVouchers(string status = "All")
        {
            DatabaseHelper db = new DatabaseHelper();

            string where = "";
            if (status != "All")
                where = $"WHERE sync_status='{status}'";

            DataTable dt = db.selectQuery(
                $"SELECT id, local_voucher_id, voucherdate, clientid, total, sync_status, createdate " +
                $"FROM app_voucher {where} ORDER BY id DESC"
            );

            dgvVouchers.DataSource = dt;
        }

        private void cboStatusFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadVouchers(cboStatusFilter.SelectedItem.ToString());
        }

        private void dgvVouchers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private async void btnSyncSelected_Click(object sender, EventArgs e)
        {
            if (dgvVouchers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Select a voucher first");
                return;
            }

            btnSyncSelected.Enabled = false;

            var sync = new VoucherSyncService();
            await sync.SyncPendingVouchers(1);

            LoadVouchers(cboStatusFilter.SelectedItem.ToString());
            btnSyncSelected.Enabled = true;
        }

        private async void btnSyncAll_Click(object sender, EventArgs e)
        {
            btnSyncAll.Enabled = false;

            var sync = new VoucherSyncService();
            await sync.SyncPendingVouchers();

            LoadVouchers(cboStatusFilter.SelectedItem.ToString());
            btnSyncAll.Enabled = true;
        }

        private void dgvVouchers_RowPrePaint_1(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            var row = dgvVouchers.Rows[e.RowIndex];
            if (row.Cells["sync_status"].Value == null) return;

            string status = row.Cells["sync_status"].Value.ToString();

            if (status == "FAILED")
                row.DefaultCellStyle.BackColor = Color.MistyRose;
            else if (status == "PENDING")
                row.DefaultCellStyle.BackColor = Color.LightYellow;
            else if (status == "SYNCED")
                row.DefaultCellStyle.BackColor = Color.Honeydew;

        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (dgvVouchers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a voucher to print");
                return;
            }

            long id = Convert.ToInt64(
                dgvVouchers.SelectedRows[0].Cells["id"].Value
            );

            var repo = new VoucherRepository();
            var voucher = repo.GetById(id);

            if (voucher == null)
            {
                MessageBox.Show("Voucher not found");
                return;
            }

            var printer = new VoucherPrintService(voucher);
            printer.Print();   // ← ACTUAL PRINT
        }

        private void btnPdf_Click(object sender, EventArgs e)
        {
            if (dgvVouchers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a voucher first");
                return;
            }

            long id = Convert.ToInt64(
                dgvVouchers.SelectedRows[0].Cells["id"].Value
            );

            var repo = new VoucherRepository();
            var voucher = repo.GetById(id);

            var printer = new VoucherPrintService(voucher);
            printer.Print(toPdf: true);   // ← PDF
        }

    }
}
