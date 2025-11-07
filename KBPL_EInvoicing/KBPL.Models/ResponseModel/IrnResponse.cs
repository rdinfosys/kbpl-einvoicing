using System;
using System.Collections.Generic;
using System.Text;

namespace KBPL.Models.ResponseModel
{
    public class IrnResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string RequestId { get; set; }
        public string Status { get; set; }
        public IrnResult Result { get; set; } = new IrnResult();
    }

    public class IrnResult
    {
        public long AckNo { get; set; }
        public string AckDt { get; set; }
        public string Irn { get; set; }
        public string SignedQRCode { get; set; }
        public string Status { get; set; }
        public long EwbNo { get; set; }
        public string EwbDt { get; set; }
        public string EwbValidTill { get; set; }
        public string Remarks { get; set; }
        public string InvoiceNo { get; set; }
        public string InvoiceDate { get; set; }
        public string FyCode { get; set; }
        public string CompCode { get; set; }
    }
}
