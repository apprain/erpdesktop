using System;
using System.Collections.Generic;
using System.Data;
using ApprainERPTerminal.Modules.Voucher;

namespace ApprainERPTerminal.Modules.Voucher
{
    internal class VoucherRepository
    {
        private readonly DatabaseHelper db;
        private readonly string TABLE_NAME = DatabaseHelper.TABLE_PREFIX + "voucher";

        public VoucherRepository()
        {
            db = new DatabaseHelper();
        }

        // Save or update voucher
        public long Save(VoucherModel v)
        {
            Dictionary<string, string> data = new Dictionary<string, string>
            {
                { "local_voucher_id", v.LocalVoucherId },
                { "clientid", v.ClientId.ToString() },
                { "action", v.Action },
                { "voucherdate", v.VoucherDate.ToString("yyyy-MM-dd") },
                { "total", v.Total.ToString() },
                { "subject", v.Subject },
                { "note", v.Note },
                { "trows", v.TRows },
                { "entrycode", v.EntryCode },
                { "companyac", v.CompanyAccount },
                { "operator", v.OperatorId.ToString() },
                { "status", v.Status },
                { "sync_status", v.SyncStatus },
                { "sync_retry", v.SyncRetry.ToString() },
                { "createdate", v.CreateDate.ToString("yyyy-MM-dd HH:mm:ss") }
            };

            if (v.Id > 0)
            {
                data.Add("id", v.Id.ToString());
            }

            return db.Save(TABLE_NAME, data);
        }

        // Get vouchers waiting to be synced
        public List<VoucherModel> GetPendingVouchers(int limit = 10)
        {
            List<VoucherModel> list = new List<VoucherModel>();

            DataTable dt = db.FindAll(
                TABLE_NAME,
                "sync_status='PENDING' OR sync_status='FAILED' LIMIT " + limit
            );

            foreach (DataRow r in dt.Rows)
            {
                list.Add(MapRow(r));
            }

            return list;
        }

        // Mark voucher as synced
        public void MarkSynced(long id)
        {
            db.execSQL(
                $"UPDATE {TABLE_NAME} SET sync_status='SYNCED', sync_retry=0 WHERE id={id}"
            );
        }

        // Mark voucher as failed
        public void MarkFailed(long id)
        {
            db.execSQL(
                $"UPDATE {TABLE_NAME} SET sync_status='FAILED', sync_retry=sync_retry+1 WHERE id={id}"
            );
        }

        // Map DataRow → VoucherModel
        private VoucherModel MapRow(DataRow r)
        {
            return new VoucherModel
            {
                Id = Convert.ToInt64(r["id"]),
                LocalVoucherId = r["local_voucher_id"].ToString() ?? "",
                ClientId = Convert.ToInt32(r["clientid"]),
                Action = r["action"].ToString() ?? "",
                VoucherDate = DateTime.Parse(r["voucherdate"].ToString() ?? DateTime.Now.ToString()),
                Total = Convert.ToDouble(r["total"]),
                Subject = r["subject"].ToString() ?? "",
                Note = r["note"].ToString() ?? "",
                TRows = r["trows"].ToString() ?? "",
                EntryCode = r["entrycode"].ToString() ?? "",
                CompanyAccount = r["companyac"].ToString() ?? "",
                OperatorId = Convert.ToInt32(r["operator"]),
                Status = r["status"].ToString() ?? "NEW",
                SyncStatus = r["sync_status"].ToString() ?? "PENDING",
                SyncRetry = Convert.ToInt32(r["sync_retry"]),
                CreateDate = DateTime.Parse(r["createdate"].ToString() ?? DateTime.Now.ToString())
            };
        }

        public VoucherModel GetById(long id)
        {
            DatabaseHelper db = new DatabaseHelper();

            DataTable dt = db.selectQuery(
                $"SELECT * FROM app_voucher WHERE id={id} LIMIT 1"
            );

            if (dt.Rows.Count == 0) return null;

            DataRow r = dt.Rows[0];

            return new VoucherModel
            {
                Id = Convert.ToInt64(r["id"]),
                LocalVoucherId = r["local_voucher_id"].ToString(),
                ClientId = Convert.ToInt32(r["clientid"]),
                Action = r["action"].ToString(),
                VoucherDate = Convert.ToDateTime(r["voucherdate"]),
                Total = Convert.ToDouble(r["total"]),
                Subject = r["subject"]?.ToString() ?? "",
                Note = r["note"]?.ToString() ?? "",
                EntryCode = r.Table.Columns.Contains("entrycode")
                    ? r["entrycode"].ToString()
                    : "",
                CompanyAccount = r.Table.Columns.Contains("companyacc")
                    ? r["companyacc"].ToString()
                    : "",
                TRows = r["trows"].ToString(),
                OperatorId = Convert.ToInt32(r["operator"]),
                Status = r["status"].ToString(),
                SyncStatus = r["sync_status"].ToString(),
                CreateDate = Convert.ToDateTime(r["createdate"])
            };

            /*
            return new VoucherModel
            {
                Id = Convert.ToInt64(r["id"]),
                LocalVoucherId = r["local_voucher_id"].ToString(),
                ClientId = Convert.ToInt32(r["clientid"]),
                Action = r["action"].ToString(),
                VoucherDate = Convert.ToDateTime(r["voucherdate"]),
                Total = Convert.ToDouble(r["total"]),
                EntryCode = r["entrycode"].ToString(),
                CompanyAccount = r["companyacc"].ToString(),
                Subject = r["subject"].ToString(),
                Note = r["note"].ToString(),
                TRows = r["trows"].ToString(),
                SyncStatus = r["sync_status"].ToString(),
                CreateDate = Convert.ToDateTime(r["createdate"])
            };*/
        }

    }
}
