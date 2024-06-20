using System;
using System.Collections.Generic;

namespace SOPManagement.Services.ShopifyService
{
    internal static class Warehouse
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
            { "HR", "DE" },
            { "ZA", "CH" },
            { "FI", "DE" }
        };

        public static string MapString(string input)
        {
            if (mappings.TryGetValue(input, out string result))
            {
                return result;
            }
            return "None";
        }
    }
}