using System;
using System.Collections.Generic;
using System.Text;

namespace KBPL.Models.Constants
{
    public enum SupplyType
    {
        /// <summary>
        /// Business to Business
        /// </summary>
        B2B = 1,
        /// <summary>
        /// With SEZ payment
        /// </summary>
        SEZWP = 2,
        /// <summary>
        /// SEZ without payment
        /// </summary>
        SEZWOP = 3,
        /// <summary>
        /// Export With Payment
        /// </summary>
        EXPWP = 4,
        /// <summary>
        /// Export without payment
        /// </summary>
        EXPWOP = 5,
        /// <summary>
        /// Deemed Export
        /// </summary>
        DEXP = 6
    }
    public enum DocumentType
    {
        /// <summary>
        /// INVOICE
        /// </summary>
        INV = 1,
        /// <summary>
        /// CRN-CREDIT NOTE
        /// </summary>
        CRN = 2,
        /// <summary>
        /// DBN-DEBIT NOTE
        /// </summary>
        DBN = 3
    }
    public enum ModeofPayment
    {
        Cash = 1,
        Credit = 2,
        DirectTransfer = 3
    }

    public sealed class ModeofTransport
    {

        private ModeofTransport() { }

        public static readonly string Road = "1";
        public static readonly string Rail = "2";
        public static readonly string Air = "3";
        public static readonly string Ship = "4";
    }

    public enum Units
    {
        BAG = 1, BAL = 2, BDL = 3, BKL = 4, BOU = 5, BOX = 6, BTL = 7, BUN = 8, CAN = 9, CBM = 10, CCM = 11, CMS = 12,
        CTN = 13, DOZ = 14, DRM = 15, GGK = 16,
        GMS = 17, GRS = 18, GYD = 19, KGS = 20, KLR = 21, KME = 22, LTR = 23, MTR = 24, MLT = 25, MTS = 26, NOS = 27, OTH = 28,
        PAC = 29, PCS = 30, PRS = 31, QTL = 32,
        ROL = 33, SET = 34, SQF = 35, SQM = 36, SQY = 37, TBS = 38, TGM = 39, THD = 40, TON = 41, TUB = 42, UGS = 43, UNT = 44, YDS = 45
    }

    public enum YesNoEnum
    {
        Y = 1,
        N = 2
    }
}
