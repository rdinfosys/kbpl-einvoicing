using System;
using System.Collections.Generic;
using System.Text;

namespace KBPL.Models.RequestModel
{
    public class GstAuthentication
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Cliend_id { get; set; }
        public string Cliend_secret { get; set; }
        public string Grant_type { get; set; }
    }
}
