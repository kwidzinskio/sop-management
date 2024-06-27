using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Sheets.v4;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOPManagement.Models;

namespace SOPManagement.Services.GoogleService
{
    internal class GoogleService
    {
        private static string[] Scopes = { SheetsService.Scope.Spreadsheets };
        private static string ApplicationName = "Google Sheets API .NET Quickstart";
        private SheetsService _service;

        public GoogleService(string credentialPath)
        {
            GoogleCredential credential;
            using (var stream = new FileStream(credentialPath, FileMode.Open, FileAccess.Read))
            {
                credential = GoogleCredential.FromStream(stream).CreateScoped(Scopes);
            }

            _service = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });
        }

        public async Task AppendShopify(string spreadsheetId, string range, List<List<object>> lineOrders)
        {
            try
            {
                var values = new List<IList<object>>();

                foreach (var item in lineOrders)
                {
                    var row = item.Select(val => (object)val).ToList();
                    values.Add(row);
                }

                var body = new ValueRange
                {
                    Values = values
                };

                var request = _service.Spreadsheets.Values.Append(body, spreadsheetId, range);
                request.ValueInputOption = SpreadsheetsResource.ValuesResource.AppendRequest.ValueInputOptionEnum.RAW;

                var response = await request.ExecuteAsync();
                Console.WriteLine("Data written to Google Sheets.");
            }
            catch (Google.GoogleApiException ex)
            {
                Console.WriteLine($"Google API Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
        public async Task AppendQuivo(string spreadsheetId, string range, Dictionary<string, List<GoogleProductQty>> productsByWarehouse)
        {
            try
            {
                await ClearSheetAsync(spreadsheetId, range);

                var values = new List<IList<object>>();

                var predefinedHeaders = new List<string>
                {
                    "FC", "Sensor", 
                    "Strap V", "Strap IV", "Strap III", "Strap II", "Strap I",
                    "Bra XL", "Bra L", "Bra M", "Bra S", "Bra XS",
                    "Shirt XXL", "Shirt XL", "Shirt L", "Shirt M", "Shirt S",
                    "Band V", "Band IV", "Band III", "Band II", "Band I",
                    "Magnetbox", "Phone stand", "Zip bag", "Start guide", "Safety info"
                };

                var headers = predefinedHeaders.Cast<object>().ToList();
                values.Add(headers);

                foreach (var warehouseEntry in productsByWarehouse)
                {
                    var row = new List<object> { warehouseEntry.Key };

                    foreach (var header in predefinedHeaders.Skip(1)) // Skip first header "FC"
                    {
                        var product = warehouseEntry.Value.FirstOrDefault(p => p.InternalName == header);
                        row.Add(product != null ? (object)product.Quantity : 0);
                    }

                    values.Add(row);
                }

                var body = new ValueRange
                {
                    Values = values
                };

                var request = _service.Spreadsheets.Values.Append(body, spreadsheetId, range);
                request.ValueInputOption = SpreadsheetsResource.ValuesResource.AppendRequest.ValueInputOptionEnum.RAW;

                var response = await request.ExecuteAsync();
                Console.WriteLine("Data written to Google Sheets.");
            }
            catch (Google.GoogleApiException ex)
            {
                Console.WriteLine($"Google API Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        public async Task ClearSheetAsync(string spreadsheetId, string range)
        {
            try
            {
                var requestBody = new ClearValuesRequest();

                var deleteRequest = _service.Spreadsheets.Values.Clear(requestBody, spreadsheetId, range);
                var deleteResponse = await deleteRequest.ExecuteAsync();
                Console.WriteLine("Data cleared from Google Sheets.");
            }
            catch (Google.GoogleApiException ex)
            {
                Console.WriteLine($"Google API Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}
