using ShopifySharp;
using ShopifySharp.Filters;
using System.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SOPManagement.Services.ShopifyService.Helpers;
using Microsoft.Extensions.Configuration;

namespace SOPManagement.Services.ShopifyService
{
    internal class ShopifyService
    {
        private string _accessToken;
        private string _shopifyUrl;
        private readonly OrderService _orderService;
        private OrderListFilter _filter;
        private readonly InventoryLevelService _inventoryLevelService;
        private readonly LocationService _locationService;
        private readonly InventoryItemService _inventoryItemService;

        public ShopifyService(IConfiguration configuration)
        {
            _accessToken = configuration["shopifyAccessToken"];
            _shopifyUrl = configuration["shopifyUrl"];
            _orderService = new OrderService(_shopifyUrl, _accessToken);
            _inventoryLevelService = new InventoryLevelService(_shopifyUrl, _accessToken);
            _locationService = new LocationService(_shopifyUrl, _accessToken);
            _inventoryItemService = new InventoryItemService(_shopifyUrl, _accessToken);
        }

        public async Task<List<List<object>>> FetchOrdersAsync(DateTime startDatetime, DateTime endDatetime)
        {
            List<List<object>> lineOrders = new List<List<object>>();

            _filter = new OrderListFilter
            {
                Limit = 1,
                Status = "any",
                CreatedAtMax = startDatetime,
                CreatedAtMin = endDatetime,
            };

            do
            {
                try
                {
                    await Task.Delay(250);
                    var fetchedOrder = await _orderService.ListAsync(_filter);
                    var order = fetchedOrder.Items.FirstOrDefault();

                    if (order != null)
                    {
                        ProcessOrder(order, lineOrders);
                        startDatetime = MoveToNextOrder(startDatetime, order, _filter);
                    }
                    else
                    {
                        break;
                    }
                }
                catch (ShopifyException ex)
                {
                    Console.WriteLine($"Shopify error: {ex.Message}");
                }
            } while (startDatetime > endDatetime);

            return lineOrders;
        }

        private void ProcessOrder(Order order, List<List<object>> lineOrders)
        {
            var lineSKUs = order.LineItems.SelectMany(li => Enumerable.Repeat(li.SKU, li.Quantity ?? 0)).ToList();
            var lineNames = order.LineItems.SelectMany(li => Enumerable.Repeat(li.Name, li.Quantity ?? 0)).ToList();
            var lineInternalNames = order.LineItems.SelectMany(li => Enumerable.Repeat(ShopifyItemsMapping.MapItems(li.SKU), li.Quantity ?? 0)).ToList();
            int productsInOrder = order.LineItems.Sum(li => li.Quantity ?? 0);

            var lineOrder = new List<object>
            {
                order.Name,
                order.CreatedAt?.DateTime.ToString(),
                string.Join(", ", lineSKUs),
                string.Join(", ", lineNames),
                string.Join(", ", lineInternalNames),
                productsInOrder,
                order.ShippingAddress.CountryCode,
                FulfillmentCenterMapping.MapFulfillmentCenter(order.ShippingAddress.CountryCode)
            };

            lineOrders.Add(lineOrder);

            Console.WriteLine($"{order.Name} {order.CreatedAt}");
        }

        private DateTime MoveToNextOrder(DateTime startDatetime, Order order, OrderListFilter filter)
        {
            startDatetime = order.CreatedAt?.DateTime ?? DateTime.MinValue;
            filter.CreatedAtMax = startDatetime.AddSeconds(-1);

            return startDatetime;
        }

        public async Task<List<List<object>>> FetchStocksAsync()
        {
            var locations = await _locationService.ListAsync();
            var locationDict = locations.Items.ToDictionary(loc => loc.Id.Value, loc => loc.Name);

            var inventoryLevels = new List<List<object>>();

            foreach (var location in locations.Items)
            {
                var inventoryList = await _inventoryLevelService.ListAsync(new InventoryLevelListFilter
                {
                    LocationIds = new List<long> { location.Id.Value }
                });

                foreach (var inventoryLevel in inventoryList.Items)
                {
                    var inventoryItem = await _inventoryItemService.GetAsync(inventoryLevel.InventoryItemId.Value);
                    await Task.Delay(250);
                    var inventoryDetails = new List<object>
                    {
                        inventoryLevel.Available,
                        locationDict[inventoryLevel.LocationId.Value],
                        inventoryItem.SKU
                    };
                    inventoryLevels.Add(inventoryDetails);
                    Console.WriteLine($"{locationDict[inventoryLevel.LocationId.Value]} {inventoryLevel.Available} {inventoryItem.SKU}");
                }
            }

            return inventoryLevels;
        }


        public class InventoryLevelWithDetails
        {
            public long? InventoryLevel { get; set; }
            public string LocationName { get; set; }
            public string SKU { get; set; }
        }
    }
}
