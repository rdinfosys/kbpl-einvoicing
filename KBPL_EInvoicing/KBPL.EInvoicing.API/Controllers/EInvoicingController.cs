using KBPL.EInvoicing.API.Services;
using KBPL.GST.Services.Implementation;
using KBPL.Models.HelperModels;
using KBPL.Models.RequestModel;
using KBPL.Models.ResponseModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace KBPL.EInvoicing.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EInvoicingController : BaseController
    {
        private readonly ILogger<EInvoicingController> _logger;
        private readonly MasterIndiaAuthSettings _masterIndiaAuthSettings;


        public EInvoicingController(ILogger<EInvoicingController> logger,
            IOptions<MasterIndiaAuthSettings> masterIndiaAuthSettings)
        {
            _logger = logger;
            _masterIndiaAuthSettings = masterIndiaAuthSettings.Value;
        }


        private async Task<MastersIndiaAuthResponseModel> GetAuthToken()
        {
            MastersIndiaAuthResponseModel mastersIndiaAuthResponseModel = new MastersIndiaAuthResponseModel();
            using (var client = new HttpClient())
            {
               
                client.DefaultRequestHeaders.Add("gspappid", _masterIndiaAuthSettings.Client_id);
                client.DefaultRequestHeaders.Add("gspappsecret", _masterIndiaAuthSettings.Client_secret);
                var response = await client.PostAsync(_masterIndiaAuthSettings.AuthApiUrl, null);
                var apiResponse = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    mastersIndiaAuthResponseModel = JsonConvert.DeserializeObject<MastersIndiaAuthResponseModel>(apiResponse);
                    Console.Write("Success");
                }
                else
                {
                    mastersIndiaAuthResponseModel = JsonConvert.DeserializeObject<MastersIndiaAuthResponseModel>(apiResponse);
                    mastersIndiaAuthResponseModel.IsSuccess = false;
                    Console.Write("Error");
                }
            }

            return mastersIndiaAuthResponseModel;
        }

        [HttpPost("GetEInvoicingData")]
        public async Task<IActionResult> GetEInvoicingDataAsync(SalesInvoiceRequestModel requestModel)
        {
            try
            {
                _logger.LogInformation("requestModel : " + JsonConvert.SerializeObject(requestModel));
                var mastersIndiaAuthResponseModel = await GetAuthToken();

                if (!mastersIndiaAuthResponseModel.IsSuccess)
                {
                    return Unauthorized("User Not Authenticated");
                }

                //requestModel.AccessToken = "730cb6928dd7cb23a031e6c3fa3413a6a8cc74ae";

                var result = await GetGenerateEInvoicingAPI(requestModel, mastersIndiaAuthResponseModel, _logger);

                return Ok(result);

            }
            catch (Exception e)
            {
                _logger.LogInformation("Exception : " + e);
                return Ok("Error : " + e.Message);

            }
        }


        private async Task<string> GetGenerateEInvoicingAPI(SalesInvoiceRequestModel requestModel, MastersIndiaAuthResponseModel mastersIndiaAuthResponseModel, ILogger<EInvoicingController> logger)
        {
            //requestModel.AccessToken = "a78e74508f285f5cd120716b81d8e91f2af96326";

            SalesInvoiceService salesInvoiceService = new SalesInvoiceService();
            var res = await salesInvoiceService.GetSalesInvoiceHeader(requestModel);

            logger.LogInformation($"{requestModel.InvoiceNo} Input JSON : {JsonConvert.SerializeObject(res)}");

            var serializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            dynamic apiResonse;

            EInvoicingModel apiResonse1;
            string EInvoiceUrl = _masterIndiaAuthSettings.EInvoiceApiUrl;
            using (var client = new HttpClient())
            {
                var stringJSON = JsonConvert.SerializeObject(res, serializerSettings);

                //client.DefaultRequestHeaders.Add("Content-Type", "application/json");
                client.DefaultRequestHeaders.Add("user_name", _masterIndiaAuthSettings.Username);
                client.DefaultRequestHeaders.Add("password", _masterIndiaAuthSettings.Password);
                client.DefaultRequestHeaders.Add("gstin", _masterIndiaAuthSettings.Gstin);
                client.DefaultRequestHeaders.Add("requestid", RandomGenerator.GenerateRandomAlphanumeric(12));
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {mastersIndiaAuthResponseModel.Access_token}");

                client.BaseAddress = new Uri(EInvoiceUrl);
                var response = await client.PostAsJsonAsync("enriched/ei/invoice", res);
                var apiResponse = await response.Content.ReadAsStringAsync();

                logger.LogInformation($"{requestModel.InvoiceNo} Output JSON : {JsonConvert.SerializeObject(apiResponse)}");


                if (response.IsSuccessStatusCode)
                {
                    apiResonse1 = (JsonConvert.DeserializeObject<EInvoicingModel>(apiResponse));

                    //if (apiResonse1.Results.status.Equals("Success"))
                    //{
                    if (apiResonse1.Results.message == null)
                    {
                        apiResonse1.Results.message = new EInvoicingAPIResponse();
                    }
                    try
                    {
                        apiResonse1.Results.message.InvoiceNo = requestModel.InvoiceNo;
                        apiResonse1.Results.message.InvoiceDate = requestModel.InvoiceDate;
                        apiResonse1.Results.message.CompCode = requestModel.CompCode;
                        apiResonse1.Results.message.FyCode = requestModel.FyCode;
                        apiResonse1.Results.message.Ackdt = Convert.ToDateTime(apiResonse1.Results.message.Ackdt).ToString("dd-MMM-yyyy");
                        apiResonse1.Results.message.EwbDt = Convert.ToDateTime(apiResonse1.Results.message.EwbDt).ToString("dd-MMM-yyyy");
                        apiResonse1.Results.message.EwbValidTill = Convert.ToDateTime(apiResonse1.Results.message.EwbValidTill).ToString("dd-MMM-yyyy");
                        var res1 = await salesInvoiceService.SaveSalesInvoiceEInvocingData(apiResonse1);
                        //}
                    }
                    catch (Exception e)
                    {
                        logger.LogError($"{requestModel.InvoiceNo} SQL Error : {e.Message}, {e.StackTrace}");
                    }

                    if (!apiResonse1.Results.status.Contains("Success"))
                    {
                        logger.LogInformation($"{requestModel.InvoiceNo} Failed : {apiResonse1.Results.errorMessage}");
                        return apiResonse1.Results.errorMessage;
                    }

                    logger.LogInformation($"{requestModel.InvoiceNo} Success : {apiResponse}");

                    Console.Write("Success");
                }
                else
                {
                    logger.LogInformation($"{requestModel.InvoiceNo} Failed : {apiResponse}");
                    apiResonse = JsonConvert.DeserializeObject<dynamic>(apiResponse);
                    Console.Write("Error");
                    return "Error";
                }
            }

            return "Success";
        }


        [HttpPost("CancelEInvoice")]
        public async Task<IActionResult> CancelEInvoiceAsync(SalesInvoiceRequestModel requestModel)
        {
            try
            {
                _logger.LogInformation("requestModel : " + JsonConvert.SerializeObject(requestModel));
                var mastersIndiaAuthResponseModel = await GetAuthToken();

                if (!mastersIndiaAuthResponseModel.IsSuccess)
                {
                    return Unauthorized("User Not Authenticated");
                }

                requestModel.AccessToken = mastersIndiaAuthResponseModel.Access_token;

                //var result = await GetGenerateEInvoicingAPI(requestModel, _logger);

                return Ok();

            }
            catch (Exception e)
            {
                _logger.LogInformation("Exception : " + e);
                return Ok("Error : " + e.Message);

            }
        }

    }
}
