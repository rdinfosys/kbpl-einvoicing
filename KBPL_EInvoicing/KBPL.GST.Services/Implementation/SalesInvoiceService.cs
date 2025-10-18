using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KBPL.GST.Services.Interfaces;
using KBPL.Models.RequestModel;
using KBPL.Models.ResponseModel;
using KBPL.Oracle.DataAccess;

namespace KBPL.GST.Services.Implementation
{
    public class SalesInvoiceService : ISalesInvoiceService
    {
        private readonly ILogger<SalesInvoiceService> _logger;
        private readonly OracleQueryExecutor _oracleQueryExecutor;
        private readonly OracleRepository _oracleRepository;
        //public SalesInvoiceService(OracleQueryExecutor oracleQueryExecutor,
        //    ILogger<SalesInvoiceService> logger,
        //    OracleRepository oracleRepository)
        //{
        //    _oracleQueryExecutor = oracleQueryExecutor;
        //    _logger = logger;
        //    _oracleRepository = oracleRepository;
        //}

        public SalesInvoiceService()
        {
            _oracleRepository = new OracleRepository();
        }

        public async Task<EInvoiceModel> GetSalesInvoiceHeader(SalesInvoiceRequestModel requestModel)
        {
            EInvoiceModel eInvoiceModel = new EInvoiceModel();
            eInvoiceModel.access_token = requestModel.AccessToken;
            DocumentDetails documentDetails = new DocumentDetails();
            var inputs = new Dictionary<string, string>();
            inputs.Add("compcode", requestModel.CompCode);
            inputs.Add("fycode", requestModel.FyCode);
            inputs.Add("invoiceno", requestModel.InvoiceNo);
            var eInvoicingDetails = await _oracleRepository.GetMultipleList<SellerDetails, BuyerDetails, ItemDetails, FinanceModel, Ewaybill_details, ShipDetails>("sales.Pkg_Salesinvoice.getdetailsforeinvoicing", inputs);

            documentDetails.document_number = requestModel.InvoiceNo.Substring(2);
            documentDetails.document_date = Convert.ToDateTime(requestModel.InvoiceDate).ToString("dd/MM/yyyy");
            eInvoiceModel.document_details = documentDetails;
            var Seller_details = eInvoicingDetails.Item1[0];
            eInvoiceModel.seller_details = Seller_details;

            if (eInvoicingDetails.Item5.Any())
                eInvoiceModel.ewaybill_details = eInvoicingDetails.Item5[0];
            //eInvoiceModel.ewaybill_details
            eInvoiceModel.dispatch_details = new DispatchDetails()
            {
                company_name = Seller_details.legal_name,
                address1 = Seller_details.address1,
                location = Seller_details.location,
                pincode = Seller_details.pincode,
                state_code = Seller_details.state_code
            };

            var BuyerDetails = eInvoicingDetails.Item2[0];
            eInvoiceModel.buyer_details = BuyerDetails;
            eInvoiceModel.ship_details = eInvoicingDetails.Item6[0];

            eInvoiceModel.reference_details = new ReferenceDeails()
            {
                document_period_details = new DocPeriodDetails()
                {
                    invoice_period_end_date = Convert.ToDateTime(requestModel.InvoiceDate).ToString("dd/MM/yyyy"),
                    invoice_period_start_date = Convert.ToDateTime(requestModel.InvoiceDate).ToString("dd/MM/yyyy")
                },
                preceding_document_details = new Preceding_document_details()
                {
                    reference_of_original_invoice = requestModel.InvoiceNo.Substring(2),
                    preceding_invoice_date = Convert.ToDateTime(requestModel.InvoiceDate).ToString("dd/MM/yyyy"),
                }
            };

            var finModel = eInvoicingDetails.Item4;

            eInvoiceModel.item_list = eInvoicingDetails.Item3;
            int i = 1;
            eInvoiceModel.item_list.ToList().ForEach(x =>
            {
                x.item_serial_number = i;
                x.unit = x.unit == "BAGS" ? "BAG" : "KGS";
                i++;
            });
            var totDiscount = eInvoicingDetails.Item4.Where(x => x.AccoutType == "Discount").ToList();
            if (totDiscount.Any())
            {
                eInvoiceModel.value_details.total_discount = 0;//totDiscount[0].Amount;
            }

            eInvoiceModel.value_details.total_other_charge = eInvoicingDetails.Item4.Where(x => x.AccoutType == "TCS").ToList()[0].Amount;

            eInvoiceModel.value_details.total_assessable_value = Math.Round(eInvoiceModel.item_list.Sum(x => x.assessable_value), 2);
            eInvoiceModel.value_details.total_cgst_value = Math.Round(eInvoiceModel.item_list.Sum(x => x.cgst_amount), 2);
            eInvoiceModel.value_details.total_sgst_value = Math.Round(eInvoiceModel.item_list.Sum(x => x.sgst_amount), 2);
            eInvoiceModel.value_details.total_igst_value = Math.Round(eInvoiceModel.item_list.Sum(x => x.igst_amount), 2); ;

            eInvoiceModel.value_details.total_invoice_value = Math.Round(eInvoicingDetails.Item4.Where(x => x.AccoutType == "N").ToList()[0].Amount, 2);


            if (eInvoicingDetails.Item4.Where(x => x.AccoutType == "R").Any())
                eInvoiceModel.value_details.round_off_amount = eInvoicingDetails.Item4.Where(x => x.AccoutType == "R").ToList()[0].Amount;

            return eInvoiceModel;
        }

        public async Task<int> SaveSalesInvoiceEInvocingData(EInvoicingModel eInvoicingModel)
        {
            Dictionary<string, string> dynamicParameters = new Dictionary<string, string>();
            dynamicParameters.Add("errorMessage", eInvoicingModel.Results.errorMessage);
            dynamicParameters.Add("InfoDtls", eInvoicingModel.Results.InfoDtls);
            dynamicParameters.Add("status", eInvoicingModel.Results.status);
            dynamicParameters.Add("requestId", eInvoicingModel.Results.requestId);
            dynamicParameters.Add("AckNo", eInvoicingModel.Results.message.AckNo);
            dynamicParameters.Add("AckDate", eInvoicingModel.Results.message.Ackdt);
            dynamicParameters.Add("Irn", eInvoicingModel.Results.message.Irn);
            dynamicParameters.Add("SignedQRCode", eInvoicingModel.Results.message.SignedQRCode);
            dynamicParameters.Add("InvoiceNo", eInvoicingModel.Results.message.InvoiceNo);
            dynamicParameters.Add("InvoiceDate", eInvoicingModel.Results.message.InvoiceDate);
            dynamicParameters.Add("CompCode", eInvoicingModel.Results.message.CompCode);
            dynamicParameters.Add("FyCode", eInvoicingModel.Results.message.FyCode);
            dynamicParameters.Add("EwbNo", eInvoicingModel.Results.message.EwbNo);
            dynamicParameters.Add("EwbDt", eInvoicingModel.Results.message.EwbDt);
            dynamicParameters.Add("EwbValidTill", eInvoicingModel.Results.message.EwbValidTill);

            var result = await _oracleRepository.ExecuteQuery("sales.Pkg_Salesinvoice.updatedetailsforeinvoicing", dynamicParameters);
            return result;
        }
    }
}
