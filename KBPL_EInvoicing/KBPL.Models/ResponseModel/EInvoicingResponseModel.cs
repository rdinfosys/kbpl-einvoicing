using System;
using System.Collections.Generic;
using System.Text;

namespace KBPL.Models.ResponseModel
{

    public class EInvoicingModel
    {
        public EInvoicingModel()
        {

        }
        public EInvoicingResponseModel Results { get; set; }
    }

    public class EInvoicingResponseModel
    {
        public EInvoicingResponseModel()
        {
            message = new EInvoicingAPIResponse();
        }
        public EInvoicingAPIResponse message { get; set; }
        public string errorMessage { get; set; }
        public string InfoDtls { get; set; }
        public string status { get; set; }
        public string code { get; set; }
        public string requestId { get; set; }
    }
    public class EInvoicingAPIResponse
    {
        public EInvoicingAPIResponse()
        {

        }
        public string AckNo { get; set; }
        public string Ackdt { get; set; }
        public string Irn { get; set; }
        public string SignedInvoice { get; set; }
        public string SignedQRCode { get; set; }
        public string EwbNo { get; set; }
        public string EwbDt { get; set; }
        public string EwbValidTill { get; set; }
        public string QRCodeUrl { get; set; }
        public string EinvoicePdf { get; set; }
        public string Status { get; set; }
        public string Remarks { get; set; }
        public string alert { get; set; }
        public bool error { get; set; }
        public string InvoiceNo { get; set; }
        public string InvoiceDate { get; set; }
        public string FyCode { get; set; }
        public string CompCode { get; set; }
    }
}
