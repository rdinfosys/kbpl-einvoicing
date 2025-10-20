using System;
using System.Collections.Generic;
using System.Text;

namespace KBPL.Models.HelperModels
{
    public class MasterIndiaAuthSettings
    {
        public string AuthApiUrl { get; set; }
        public string EInvoiceApiUrl { get; set; }
        public string Username { get; set; }
        public string Gstin { get; set; }
        public string Password { get; set; }
        public string Client_id { get; set; }
        public string Client_secret { get; set; }
        public string Grant_type { get; set; }
    }
}
