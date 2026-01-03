using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ApprainERPTerminal.UI.POS
{
    public partial class FrmCashCounter : Form
    {
        private readonly decimal[] denominations = { 1000, 500, 200, 100, 50, 20, 10, 5, 2, 1, 0.5m, 0.25m, 0.05m };
        private TextBox[] quantityTextBoxes;
        private Label[] amountLabels;

        public FrmCashCounter()
        {
            InitializeComponent();
            SetupDataGridView();
        }

        private void FrmCashCounter_Load(object sender, EventArgs e)
        {

        }


        private void SetupDataGridView()
        {

            // Add rows for each denomination
            foreach (var denomination in denominations)
            {
                dataGridViewCashCounter.Rows.Add(denomination, "", "");
            }

            dataGridViewCashCounter.CellValueChanged += DataGridViewCashCounter_CellValueChanged;
            dataGridViewCashCounter.EditingControlShowing += DataGridViewCashCounter_EditingControlShowing;

        }

        private void DataGridViewCashCounter_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dataGridViewCashCounter.CurrentCell.ColumnIndex == 1) // Quantity column
            {
                e.Control.KeyPress -= QuantityColumn_KeyPress;
                e.Control.KeyPress += QuantityColumn_KeyPress; // Only allow numbers and decimal point
            }
        }

        private void QuantityColumn_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void DataGridViewCashCounter_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                UpdateAmount(e.RowIndex);
                UpdateTotalAmount();
            }
        }

        private void UpdateAmount(int rowIndex)
        {
            decimal denomination = decimal.Parse(dataGridViewCashCounter.Rows[rowIndex].Cells[0].Value.ToString());
            string quantityText = dataGridViewCashCounter.Rows[rowIndex].Cells[1].Value?.ToString() ?? "0";

            if (decimal.TryParse(quantityText, out decimal quantity))
            {
                decimal amount = denomination * quantity;
                dataGridViewCashCounter.Rows[rowIndex].Cells[2].Value = amount;
            }
            else
            {
                dataGridViewCashCounter.Rows[rowIndex].Cells[2].Value = "";
            }
        }

        private void UpdateTotalAmount()
        {
            decimal total = 0;
            foreach (DataGridViewRow row in dataGridViewCashCounter.Rows)
            {
                if (decimal.TryParse(row.Cells[2].Value?.ToString(), out decimal amount))
                {
                    total += amount;
                }
            }
            labelTotalAmount.Text = Math.Round(total,2).ToString();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            dataGridViewCashCounter.Rows.Clear();
            foreach (var denomination in denominations)
            {
                dataGridViewCashCounter.Rows.Add(denomination, "", "");
            }
            labelTotalAmount.Text = "";
        }
    }
}
