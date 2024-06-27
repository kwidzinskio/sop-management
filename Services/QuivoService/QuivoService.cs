using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SOPManagement.Models;
using SOPManagement.Services.QuivoService.Helpers;
using SOPManagement.Services.ShopifyService;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SOPManagement.Services.QuivoService
{
    internal static class QuivoService
    {
        private static readonly HttpClient client = new HttpClient();
        private static readonly string baseUrl = "https://api.quivo.co";
        private static readonly string apiKey;
        private static readonly string username;
        private static readonly string password;
        private static readonly string basePath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
        private static readonly string configPath = Path.Combine(basePath, ".NET", "SOPManagement", "config.json");

        static QuivoService()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(configPath, optional: true, reloadOnChange: true);

            IConfiguration configuration = builder.Build();

            apiKey = configuration["quivoAccessToken"];
            username = configuration["quivoUsername"];
            password = configuration["quivoPassword"];
        }

        public static async Task<string> LoginAndGetTokenAsync()
        {
            string token = null;

            var loginUrl = $"{baseUrl}/login";
            var loginData = new
            {
                username,
                password
            };

            var content = new StringContent(JsonConvert.SerializeObject(loginData), Encoding.UTF8, "application/json");
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("X-Api-Key", apiKey);

            try
            {
                HttpResponseMessage response = await client.PostAsync(loginUrl, content);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                var loginResponse = JsonConvert.DeserializeObject<QuivoLoginResponse>(responseBody);
                token = loginResponse.Token;
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine($"Message :{e.Message} ");
            }

            return token;
        }

        public static async Task<Dictionary<string, List<GoogleProductQty>>> GetProductQtyAsync(string token)
        {
            var result = new Dictionary<string, List<GoogleProductQty>>();

            foreach (var warehouseId in WarehouseIds.Values)
            {
                var productList = new List<GoogleProductQty>();
                Console.WriteLine($"\n{warehouseId.Value}");
                for (int i = 0; i < 5; i++)
                {
                    var apiUrl = $"{baseUrl}/items/{warehouseId.Key}/1882?page={i}";
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Add("X-Api-Key", apiKey);
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                    try
                    {
                        HttpResponseMessage response = await client.GetAsync(apiUrl);
                        response.EnsureSuccessStatusCode();
                        string responseBody = await response.Content.ReadAsStringAsync();
                        var quivoProducts = JsonConvert.DeserializeObject<List<QuivoProductQty>>(responseBody);
                        foreach (var product in quivoProducts)
                        {
                            string mappedItem = QuivoItemMapping.MapItem(product.Sku);
                            if (mappedItem != "None")
                            {
                                productList.Add(new GoogleProductQty
                                {
                                    InternalName = mappedItem,
                                    Quantity = product.Quantity
                                });
                                Console.WriteLine($"{mappedItem}: {product.Quantity}");
                            }
                        }
                    }
                    catch (HttpRequestException e)
                    {
                        Console.WriteLine("\nException Caught!");
                        Console.WriteLine("Message :{0} ", e.Message);
                    }
                }
                result.Add(warehouseId.Value, productList);
            }

            return result;
        }

    }

}
