using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Sheets.v4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public async Task AppendToGoogleSheet(string spreadsheetId, string range, List<List<string>> lineOrders)
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

    }
}
