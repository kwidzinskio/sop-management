﻿using ShopifySharp;
using ShopifySharp.Filters;
using System.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SOPManagement.Services.ShopifyService
{
    internal class ShopifyService
    {
        private readonly OrderService _orderService;
        private OrderListFilter _filter;

        public ShopifyService(string shopUrl, string accessToken)
        {
            _orderService = new OrderService(shopUrl, accessToken);
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
            var numberOfLineItems = order.LineItems.Count();
            bool isFirstIteration = true;

            foreach (var lineItem in order.LineItems)
            {
                List<object> lineItems = new List<object>
                {
                    order.Name,
                    order.CreatedAt?.DateTime.ToString(),
                    lineItem.SKU,
                    lineItem.Name,
                    ItemMapping.MapString(lineItem.SKU),
                    isFirstIteration ? numberOfLineItems : "-",
                    order.ShippingAddress.CountryCode,
                    Warehouse.MapString(order.ShippingAddress.CountryCode)
                };

                lineOrders.Add(lineItems);

                isFirstIteration = false; 
            }

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
