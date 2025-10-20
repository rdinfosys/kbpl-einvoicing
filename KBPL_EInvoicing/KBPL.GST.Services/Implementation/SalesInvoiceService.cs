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
            //eInvoiceModel.access_token = requestModel.AccessToken;
            DocumentDetails documentDetails = new DocumentDetails();
            var inputs = new Dictionary<string, string>();
            inputs.Add("compcode", requestModel.CompCode);
            inputs.Add("fycode", requestModel.FyCode);
            inputs.Add("invoiceno", requestModel.InvoiceNo);
            var eInvoicingDetails = await _oracleRepository.GetMultipleList<SellerDetails, BuyerDetails, ItemDetails, FinanceModel, Ewaybill_details, ShipDetails>("sales.Pkg_Salesinvoice.getdetailsforeinvoicing", inputs);

            documentDetails.No = requestModel.InvoiceNo.Substring(2);
            documentDetails.Dt = Convert.ToDateTime(requestModel.InvoiceDate).ToString("dd/MM/yyyy");
            eInvoiceModel.DocDtls = documentDetails;
            var Seller_details = eInvoicingDetails.Item1[0];
            eInvoiceModel.SellerDtls = Seller_details;

            if (eInvoicingDetails.Item5.Any())
                eInvoiceModel.EwbDtls = eInvoicingDetails.Item5[0];
            //eInvoiceModel.ewaybill_details
            eInvoiceModel.DispDtls = new DispatchDetails()
            {
                Nm = Seller_details.LglNm,
                Addr1 = Seller_details.Addr1,
                Loc = Seller_details.Loc,
                Pin = Seller_details.Pin,
                Stcd = Seller_details.Stcd
            };

            var BuyerDetails = eInvoicingDetails.Item2[0];
            eInvoiceModel.BuyerDtls = BuyerDetails;
            eInvoiceModel.ShipDtls = eInvoicingDetails.Item6[0];

            eInvoiceModel.RefDtls = new ReferenceDeails()
            {
                DocPerdDtls = new DocPeriodDetails()
                {
                    invoice_period_end_date = Convert.ToDateTime(requestModel.InvoiceDate).ToString("dd/MM/yyyy"),
                    invoice_period_start_date = Convert.ToDateTime(requestModel.InvoiceDate).ToString("dd/MM/yyyy")
                },
                PrecDocDtls = new Preceding_document_details()
                {
                    InvNo = requestModel.InvoiceNo.Substring(2),
                    InvDt = Convert.ToDateTime(requestModel.InvoiceDate).ToString("dd/MM/yyyy"),
                }
            };

            var finModel = eInvoicingDetails.Item4;

            eInvoiceModel.ItemList = eInvoicingDetails.Item3;
            int i = 1;
            eInvoiceModel.ItemList.ToList().ForEach(x =>
            {
                x.SlNo = i.ToString();
                x.Unit = x.Unit == "BAGS" ? "BAG" : "KGS";
                i++;
            });
            var totDiscount = eInvoicingDetails.Item4.Where(x => x.AccoutType == "Discount").ToList();
            if (totDiscount.Any())
            {
                eInvoiceModel.ValDtls.Discount = 0;//totDiscount[0].Amount;
            }

            eInvoiceModel.ValDtls.OthChrg = eInvoicingDetails.Item4.Where(x => x.AccoutType == "TCS").ToList()[0].Amount;

            eInvoiceModel.ValDtls.AssVal = Math.Round(eInvoiceModel.ItemList.Sum(x => x.AssAmt), 2);
            eInvoiceModel.ValDtls.CgstVal = Math.Round(eInvoiceModel.ItemList.Sum(x => x.CgstAmt), 2);
            eInvoiceModel.ValDtls.SgstVal = Math.Round(eInvoiceModel.ItemList.Sum(x => x.SgstAmt), 2);
            eInvoiceModel.ValDtls.IgstVal = Math.Round(eInvoiceModel.ItemList.Sum(x => x.IgstAmt), 2); ;

            eInvoiceModel.ValDtls.TotInvVal = Math.Round(eInvoicingDetails.Item4.Where(x => x.AccoutType == "N").ToList()[0].Amount, 2);


            if (eInvoicingDetails.Item4.Where(x => x.AccoutType == "R").Any())
                eInvoiceModel.ValDtls.RndOffAmt = eInvoicingDetails.Item4.Where(x => x.AccoutType == "R").ToList()[0].Amount;

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
