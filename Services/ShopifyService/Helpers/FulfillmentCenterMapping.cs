using System;
using System.Collections.Generic;

namespace SOPManagement.Services.ShopifyService.Helpers
{
    internal static class FulfillmentCenterMapping
    {
        private static readonly Dictionary<string, string> mappings = new Dictionary<string, string>
        {
            { "FR", "DE" },
            { "US", "US" },
            { "CH", "CH" },
            { "PT", "DE" },
            { "DE", "DE" },
            { "LT", "DE" },
            { "GB", "UK" },
            { "IE", "DE" },
            { "NL", "DE" },
            { "BE", "DE" },
            { "PL", "DE" },
            { "CA", "US" },
            { "DK", "DE" },
            { "CZ", "DE" },
            { "NO", "DE" },
            { "AT", "DE" },
            { "IT", "DE" },
            { "ES", "DE" },
            { "SE", "DE" },
            { "LV", "DE" },
            { "SK", "DE" },
            { "MT", "DE" },
            { "LU", "DE" },
            { "HR", "DE" },
            { "ZA", "CH" },
            { "FI", "DE" },
            { "RO", "DE" },
            { "HU", "DE" },
        };

        public static string MapFulfillmentCenter(string input)
        {
            if (mappings.TryGetValue(input, out string result))
            {
                return result;
            }
            return "None";
        }
    }
}