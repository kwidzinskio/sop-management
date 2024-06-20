using ShopifySharp;
using ShopifySharp.Filters;
using ShopifyService = SOPManagement.Services.ShopifyService.ShopifyService;
using GoogleService = SOPManagement.Services.GoogleService.GoogleService;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SOPManagement.Services.GoogleService;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using SOPManagement.Services.QuivoService;

namespace SOPManagement
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string basePath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            string credentialPath = Path.Combine(basePath, ".NET", "SOPManagement", "google_api_key.json");
            string configPath = Path.Combine(basePath, ".NET", "SOPManagement", "config.json");

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(configPath, optional: true, reloadOnChange: true);

            IConfiguration configuration = builder.Build();

            string spreadsheetId = configuration["spreadsheetId"];
            string range = configuration["range"];
            string accessToken = configuration["accessToken"];
            string shopUrl = configuration["shopUrl"];

            var shopifyService = new ShopifyService(shopUrl, accessToken);
            var shopifyFilter = new OrderListFilter();
            var googleService = new GoogleService(credentialPath);
            Console.WriteLine(  );
            DateTime startDatetime = new DateTime(2024, 06, 29);
            DateTime endDatetime = new DateTime(2024, 06, 01);

            #region
            /*            DateTime startDatetime = DateTime.Now;
                        DateTime endDatetime = DateTime.Now;

                        Console.Write("Enter the start datetime (DD/MM/YYYY HH:MM:SS): ");
                        string? inputStart = Console.ReadLine();
                        Console.Write("Enter the end datetime (DD/MM/YYYY HH:MM:SS): ");
                        string? inputEnd = Console.ReadLine();

                        try
                        {
                            endDatetime = DateTime.Parse(inputEnd).AddHours(2);
                            startDatetime = DateTime.Parse(inputStart).AddHours(2);
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("The input was not a valid date");
                        } */

            /* var lineOrders = await shopifyService.FetchOrdersAsync(startDatetime, endDatetime);
             await googleService.AppendToGoogleSheet(spreadsheetId, range, lineOrders);*/
            #endregion

            string token = await QuivoService.LoginAndGetTokenAsync();
            if (!string.IsNullOrEmpty(token))
            {
                await QuivoService.GetProductStockAsync(token);
            }
        }



        public class LoginResponse
        {
            public string Token { get; set; }
        }

        public class Magazine
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Location { get; set; }
            // Dodaj inne właściwości zgodnie z odpowiedzią API
        }
    }
}
