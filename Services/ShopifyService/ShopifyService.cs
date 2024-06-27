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
        private readonly OrderService _orderService;
        private OrderListFilter _filter;
        private string _accessToken;
        private string _shopifyUrl;

        public ShopifyService(IConfiguration configuration)
        {
            _accessToken = configuration["shopifyAccessToken"];
            _shopifyUrl = configuration["shopifyUrl"];
            _orderService = new OrderService(_shopifyUrl, _accessToken);
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
    }
}
