using System;
using System.Text.Json;

namespace ApprainERPTerminal.Modules.Voucher
{
    internal class VoucherService
    {
        private readonly VoucherRepository repo;

        public VoucherService()
        {
            repo = new VoucherRepository();
        }

        // Create & save voucher (offline-first)
        public long CreateVoucher(
            int clientId,
            string action,
            DateTime voucherDate,
            double total,
            string subject,
            string note,
            object trowsObject,   // grid / list / rows
            string entryCode,
            string companyAccount,
            int operatorId
        )
        {
            // 1️⃣ Basic validation
            if (clientId <= 0)
                throw new Exception("Client is required");

            if (total <= 0)
                throw new Exception("Voucher total must be greater than zero");

            if (string.IsNullOrWhiteSpace(action))
                throw new Exception("Action is required");

            // 2️⃣ Generate local voucher id
            string localVoucherId = GenerateLocalVoucherId();

            // 3️⃣ Convert rows to JSON
            string trowsJson = JsonSerializer.Serialize(trowsObject);

            // 4️⃣ Prepare model
            VoucherModel voucher = new VoucherModel
            {
                LocalVoucherId = localVoucherId,
                ClientId = clientId,
                Action = action,
                VoucherDate = voucherDate,
                Total = total,
                Subject = subject,
                Note = note,
                TRows = trowsJson,
                EntryCode = entryCode,
                CompanyAccount = companyAccount,
                OperatorId = operatorId,
                Status = "NEW",
                SyncStatus = "PENDING",
                CreateDate = DateTime.Now
            };

            // 5️⃣ Save locally (SQLite)
            return repo.Save(voucher);
        }

        // Generate unique local voucher id
        private string GenerateLocalVoucherId()
        {
            // Example: HD-20260104-153045
            return "HD-" + DateTime.Now.ToString("yyyyMMdd-HHmmss");
        }
    }
}
