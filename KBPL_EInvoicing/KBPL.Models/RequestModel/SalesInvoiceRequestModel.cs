using System;
using System.Collections.Generic;
using System.Text;

namespace KBPL.Models.RequestModel
{
    public class SalesInvoiceRequestModel
    {
        public SalesInvoiceRequestModel()
        {
            InvoiceType = InvoiceType.SalesInvoice;
        }
        public string InvoiceNo { get; set; }
        public string InvoiceDate { get; set; }
        public string CompCode { get; set; }
        public string FyCode { get; set; }
        public InvoiceType InvoiceType { get; set; }

        public string AccessToken { get; set; }
    }

    public enum InvoiceType
    {
        SalesInvoice = 1,
        CashInvoice = 2
    }
}
