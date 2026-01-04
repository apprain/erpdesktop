using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace ApprainERPTerminal.Modules.Voucher
{
    internal class VoucherSyncService
    {
        private readonly VoucherRepository repo;
        private readonly HttpClient client;

        public VoucherSyncService()
        {
            repo = new VoucherRepository();
            client = new HttpClient();
        }

        public async Task SyncPendingVouchers(int limit = 5)
        {
            var vouchers = repo.GetPendingVouchers(limit);

            foreach (var v in vouchers)
            {
                try
                {
                    bool ok = await PushVoucherToERP(v);

                    if (ok)
                        repo.MarkSynced(v.Id);
                    else
                        repo.MarkFailed(v.Id);
                }
                catch
                {
                    repo.MarkFailed(v.Id);
                }
            }
        }

        private async Task<bool> PushVoucherToERP(VoucherModel v)
        {
            Dictionary<string, string> post = new Dictionary<string, string>();

            // Auth / routing (same pattern you already use)
            post.Add("token", App.Config().Setting("token"));
            post.Add("timestamp", App.Config().Setting("token"));
            post.Add("com", "Voucher");
            post.Add("action", "createFromDesktop");


            post.Add("local_voucher_id", v.LocalVoucherId);
            post.Add("vaction", v.Action);
            post.Add("account", GetClientAccount(v.ClientId));
            post.Add("companyacc", v.CompanyAccount);
            post.Add("entrycode", v.EntryCode);
            post.Add("total", v.Total.ToString());
            post.Add("subject", v.Subject);
            post.Add("note", v.Note);
            post.Add("trows", v.TRows);

            FormUrlEncodedContent content = new FormUrlEncodedContent((IEnumerable<KeyValuePair<string, string>>)post);

                      HttpResponseMessage response = await client.PostAsync(App.Config().UrldataExchnage(), content);
            string responseString = await response.Content.ReadAsStringAsync();
            Debug.WriteLine(responseString);
            dynamic res = JsonConvert.DeserializeObject(responseString);


            return res["status"].ToString() == "1";
        }

        private string GetClientAccount(int clientId)
        {
            // TEMP: map client → account
            // Later you can fetch from local table or config
            return "CUSTOMER_ACC_" + clientId;
        }
    }
}
