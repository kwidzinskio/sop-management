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

namespace SOPManagement
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var (configuration, credentialPath) = ConfigureSettings();

            string spreadsheetId = configuration["spreadsheetId"];
            string rangeOrders = configuration["rangeOrders"];
            string rangeStocks = configuration["rangeStocks"];
            string accessToken = configuration["accessToken"];
            string shopifyUrl = configuration["shopifyUrl"];

            var shopifyService = new ShopifyService(configuration);
            var googleService = new GoogleService(credentialPath);

            #region
            DateTime startDatetime = new DateTime(2024, 08, 01);
            DateTime endDatetime = new DateTime(2024, 07, 01);
            #endregion

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
                        }*/
            #endregion

            var lineOrders = await shopifyService.FetchOrdersAsync(startDatetime, endDatetime);
            await googleService.AppendShopifyOrders(spreadsheetId, rangeOrders, lineOrders);

            var stockLevels = await shopifyService.FetchStocksAsync();
            await googleService.AppendShopifyStocks(spreadsheetId, rangeStocks, stockLevels);
        }

        static (IConfiguration, string) ConfigureSettings()
        {
            string basePath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            string configPath = Path.Combine(basePath, "sop-management", "config.json");
            string credentialPath = Path.Combine(basePath, "sop-management", "google_api_key.json");

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(configPath, optional: true, reloadOnChange: true);

            IConfiguration configuration = builder.Build();
            return (configuration, credentialPath);
        }
    }
}
