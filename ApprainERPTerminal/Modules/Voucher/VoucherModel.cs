using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace ApprainERPTerminal.Modules.Voucher
{
    internal class VoucherModel
    {

        public List<VoucherRowModel> Rows =>
            string.IsNullOrEmpty(TRows)
                ? new List<VoucherRowModel>()
                : JsonConvert.DeserializeObject<List<VoucherRowModel>>(TRows);

        // Primary keys
        public long Id { get; set; }                     // SQLite auto id
        public string LocalVoucherId { get; set; } = ""; // HD-2026-000001

        // Business info
        public int ClientId { get; set; }
        public string Action { get; set; } = "";          // RECEIVABLE, PAYABLE
        public DateTime VoucherDate { get; set; }
        public double Total { get; set; }

        // Accounting info
        public string EntryCode { get; set; } = "";
        public string CompanyAccount { get; set; } = "";

        // UI / display
        public string Subject { get; set; } = "";
        public string Note { get; set; } = "";

        // Transaction rows (JSON)
        public string TRows { get; set; } = "";

        // Control & audit
        public int OperatorId { get; set; }
        public string Status { get; set; } = "NEW";        // NEW, VERIFIED, POSTED
        public string SyncStatus { get; set; } = "PENDING"; // PENDING, SYNCED, FAILED
        public int SyncRetry { get; set; } = 0;

        // Timestamps
        public DateTime CreateDate { get; set; } = DateTime.Now;
    }
}
