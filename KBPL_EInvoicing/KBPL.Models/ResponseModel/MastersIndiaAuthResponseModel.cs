using System;
using System.Collections.Generic;
using System.Text;

namespace KBPL.Models.ResponseModel
{
    public class MastersIndiaAuthResponseModel
    {
        public MastersIndiaAuthResponseModel()
        {
            IsSuccess = true;
        }
        public string Access_token { get; set; }
        public string Expires_in { get; set; }
        public string Token_type { get; set; }
        public string Error { get; set; }
        public string Error_description { get; set; }
        public bool IsSuccess { get; set; }
    }
}
