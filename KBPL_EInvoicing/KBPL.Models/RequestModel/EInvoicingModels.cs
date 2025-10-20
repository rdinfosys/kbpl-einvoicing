using KBPL.Models.Constants;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace KBPL.Models.RequestModel
{

    public class EInvoiceModel
    {
        public EInvoiceModel()
        {
            //data_source = "ERP";
            //user_gstin = "27AAACV7785N1ZG";
            DocDtls = new DocumentDetails();
            TranDtls = new TransactionDetails();
            SellerDtls = new SellerDetails();
            BuyerDtls = new BuyerDetails();
            DispDtls = new DispatchDetails();
            ShipDtls = new ShipDetails();
            ExpDtls = new ExportDeails();
            PayDtls = new PaymentDeails();
            RefDtls = new ReferenceDeails();
            //AddlDocDtls = new IEnumerable<Additional_document_details>();
            ValDtls = new Value_details();
            EwbDtls = new Ewaybill_details();
        }


        //public string access_token { get; set; }
        //public string user_gstin { get; set; }
        //public string data_source { get; set; }
        public TransactionDetails TranDtls { get; set; }
        public DocumentDetails DocDtls { get; set; }
        public SellerDetails SellerDtls { get; set; }
        public BuyerDetails BuyerDtls { get; set; }
        public DispatchDetails DispDtls { get; set; }
        public ShipDetails ShipDtls { get; set; }
        public ExportDeails ExpDtls { get; set; }
        public PaymentDeails PayDtls { get; set; }
        public ReferenceDeails RefDtls { get; set; }
        public IEnumerable<Additional_document_details> AddlDocDtls { get; set; }
        public Value_details ValDtls { get; set; }
        public Ewaybill_details EwbDtls { get; set; }
        public IEnumerable<ItemDetails> ItemList { get; set; }

    }


    public class TransactionDetails
    {
        public TransactionDetails()
        {
            SupTyp = SupplyType.B2B.ToString();
            SupTyp = YesNoEnum.N.ToString();
            IgstOnIntra = YesNoEnum.N.ToString();
            EcmGstin = "";
            RegRev = EcmGstin = TaxSch = "";
        }

        public string TaxSch { get; set; }       // Tax Scheme, e.g., GST
        public string SupTyp { get; set; }       // Supply Type, e.g., B2B
        public string RegRev { get; set; }       // Reverse Charge (Y/N)
        public string EcmGstin { get; set; }     // E-commerce GSTIN, can be null
        public string IgstOnIntra { get; set; }  // IGST on Intra-state (Y/N)

    }
    public class DocumentDetails
    {
        public DocumentDetails()
        {
            Typ = DocumentType.INV.ToString();
            No = "";
            Dt = "";
        }
        public string Typ { get; set; }
        public string No { get; set; }
        public string Dt { get; set; }

    }

    public class SellerDetails
    {
        public string Gstin { get; set; } = "";
        public string LglNm { get; set; } = "";
        public string TrdNm { get; set; } = "";
        public string Addr1 { get; set; } = "";
        public string Addr2 { get; set; } = ""; // Nullable
        public string Loc { get; set; } = "";
        public int Pin { get; set; }
        public string Stcd { get; set; } = "";
        public string Ph { get; set; } = "";  // Nullable
        public string Em { get; set; } = "";
    }

    public class BuyerDetails
    {
        public string Gstin { get; set; } = "";
        public string LglNm { get; set; } = "";
        public string TrdNm { get; set; } = "";
        public string Addr1 { get; set; } = "";
        public string Addr2 { get; set; } = "";
        public string Loc { get; set; } = "";
        public int Pin { get; set; }
        public string Pos { get; set; } = "";
        public string Stcd { get; set; } = "";
        public string Ph { get; set; } = "";
        public string Em { get; set; } = "";
    }

    public class DispatchDetails
    {
        public string Nm { get; set; } = "";
        public string Addr1 { get; set; } = "";
        public string Addr2 { get; set; } = "";
        public string Loc { get; set; } = "";
        public int Pin { get; set; }
        public string Stcd { get; set; } = "";
    }

    public class ShipDetails
    {
        public string Gstin { get; set; } = "";
        public string LglNm { get; set; } = "";
        public string TrdNm { get; set; } = "";
        public string Addr1 { get; set; } = "";
        public string Addr2 { get; set; } = "";
        public string Loc { get; set; } = "";
        public int Pin { get; set; }
        public string Stcd { get; set; } = "";
    }

    public class ExportDeails
    {
        public string ShipBNo { get; set; } = "";     // Shipping Bill Number
        public string ShipBDt { get; set; } = "";   // Shipping Bill Date (format: dd/MM/yyyy)
        public string Port { get; set; } = "";      // Port Code
        public string RefClm { get; set; } = "";     // Refund Claim (Y/N)
        public string ForCur { get; set; } = "";     // Foreign Currency
        public string CntCode { get; set; } = "";    // Country Code
        public double? ExpDuty { get; set; }      // Export Duty - nullable

    }

    public class PaymentDeails
    {
        public string Nm { get; set; } = "";      // Name of payer
        public string Accdet { get; set; } = "";     // Account details
        public string Mode { get; set; } = "";    // Payment mode (e.g., Cash)
        public string Fininsbr { get; set; } = "";   // Financial institution branch code
        public string Payterm { get; set; } = "";    // Payment terms
        public string Payinstr { get; set; } = "";    // Payment instruction
        public string Crtrn { get; set; } = "";    // Credit term (text)
        public string Dirdr { get; set; } = "";    // Direction of draw (text)
        public int Crday { get; set; }           // Credit days
        public decimal Paidamt { get; set; }     // Paid amount
        public decimal Paymtdue { get; set; }    // Payment due
    }

    public class ReferenceDeails
    {
        public string InvRm { get; set; }
        public DocPeriodDetails DocPerdDtls { get; set; }
        public Preceding_document_details PrecDocDtls { get; set; }
        public Contract_details ContrDtls { get; set; }
    }

    public class DocPeriodDetails
    {
        public string invoice_period_start_date { get; set; } = "";
        public string invoice_period_end_date { get; set; } = "";
    }

    public class Preceding_document_details
    {
        public string InvNo { get; set; } = "";
        public string InvDt { get; set; } = "";
        public string OthRefNo { get; set; } = "";
    }

    public class Contract_details
    {
        public string RecAdvRefr { get; set; } = "";// Receipt Advice Reference
        public string RecAdvDt { get; set; } = "";   // Receipt Advice Date (format: dd/MM/yyyy)
        public string Tendrefr { get; set; } = "";   // Tender Reference
        public string Contrrefr { get; set; } = "";   // Contract Reference
        public string Extrefr { get; set; } = "";    // External Reference
        public string Projrefr { get; set; } = "";    // Project Reference
        public string Porefr { get; set; } = "";     // Purchase Order Reference
        public string PoRefDt { get; set; } = "";    // PO Reference Date (format: dd/MM/yyyy)
    }

    public class Additional_document_details
    {
        public string Url { get; set; } = "";
        public string Docs { get; set; } = "";
        public string Info { get; set; } = "";
    }

    public class Value_details
    {

        public decimal AssVal { get; set; }
        public decimal CgstVal { get; set; }
        public decimal SgstVal { get; set; }
        public decimal IgstVal { get; set; }
        public decimal CesVal { get; set; }
        public decimal? StCesVal { get; set; }
        public decimal Discount { get; set; }
        public double OthChrg { get; set; }
        public double RndOffAmt { get; set; }
        public double TotInvVal { get; set; }
    }

    public class Ewaybill_details
    {
        public Ewaybill_details()
        {
            TransMode = "1";
            Vehtype = "R";
        }
        public string Transid { get; set; } = "";     // Transporter ID
        public string Transname { get; set; } = "";   // Transporter Name
        public int Distance { get; set; }          // Distance in kilometers
        public string Transdocno { get; set; } = "";   // Transport Document Number
        public string TransdocDt { get; set; } = "";   // Transport Document Date (format: dd/MM/yyyy)
        public string Vehno { get; set; } = "";      // Vehicle Number
        public string Vehtype { get; set; } = "";    // Vehicle Type (R: Regular, O: Over-dimensional Cargo)
        public string TransMode { get; set; } = "";    // Transport Mode (1: Road, 2: Rail, etc.)
    }

    public class ItemDetails
    {
        public ItemDetails()
        {
            IsServc = "N";
            BchDtls = new ItemBatchDetails();
            AttribDtls = new HashSet<ItemAttributeDetails>();
        }
        public string SlNo { get; set; } = "";              // Serial Number
        public string PrdDesc { get; set; } = "";          // Product Description
        public string IsServc { get; set; } = "";           // Is Service (Y/N)

        public string HsnCd { get; set; } = "";            // HSN Code (note the space in key)

        public string Barcde { get; set; } = "";            // Barcode
        public double Qty { get; set; }                   // Quantity
        public double FreeQty { get; set; }               // Free Quantity
        public string Unit { get; set; } = "";             // Unit of measure
        public decimal UnitPrice { get; set; }            // Unit Price
        public decimal TotAmt { get; set; }               // Total Amount
        public decimal Discount { get; set; }             // Discount
        public decimal PreTaxVal { get; set; }            // Pre-tax Value
        public decimal AssAmt { get; set; }               // Assessable Amount
        public double GstRt { get; set; }                 // GST Rate
        public decimal IgstAmt { get; set; }              // IGST Amount
        public decimal CgstAmt { get; set; }              // CGST Amount
        public decimal SgstAmt { get; set; }              // SGST Amount
        public double CesRt { get; set; }                 // CESS Rate
        public decimal CesAmt { get; set; }               // CESS Amount
        public decimal CesNonAdvlAmt { get; set; }        // CESS Non-Advol Amount
        public double StateCesRt { get; set; }            // State CESS Rate (note the trailing space)
        public decimal StateCesAmt { get; set; }          // State CESS Amount
        public decimal StateCesNonAdvlAmt { get; set; }   // State CESS Non-Advol Amount
        public decimal OthChrg { get; set; }              // Other Charges
        public decimal TotItemVal { get; set; }           // Total Item Value
        public string OrdLineRef { get; set; } = "";       // Order Line Reference
        public string OrgCntry { get; set; } = "";         // Origin Country
        public string PrdSlNo { get; set; } = "";         // Product Serial Number
        public ItemBatchDetails BchDtls { get; set; }              // Batch Details
        public HashSet<ItemAttributeDetails> AttribDtls { get; set; }  // List of Attributes

    }

    public class ItemBatchDetails
    {
        public string Nm { get; set; } = "";
        public string Expdt { get; set; } = "";
        public string wrDt { get; set; } = "";
    }

    public class ItemAttributeDetails
    {
        public string Nm { get; set; } = "";
        public string Val { get; set; } = "";
    }

    public class FinanceModel
    {
        public string AccoutType { get; set; }
        public double Amount { get; set; }
    }
}
