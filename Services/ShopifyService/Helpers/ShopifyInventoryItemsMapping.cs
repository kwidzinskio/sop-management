using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOPManagement.Services.ShopifyService.Helpers
{
    internal class ShopifyInventoryItemsMapping
    {
        private static readonly Dictionary<string, string> mappings = new Dictionary<string, string>
        {
            { "OXUN-0101-0201 V", "Band V" }, // Extra band - Female / Male
            { "OXUN-0101-0201 IV", "Band IV" },
            { "OXUN-0101-0201 III", "Band III" },
            { "OXUN-0101-0201 II", "Band II" },
            { "OXUN-0101-0201 I", "Band I" },
            { "OXSE-0701-0707", "Sensor" }, // Extra Sensor Unit
            { "OXWO-0201-0101 XL", "Bra XL" }, // Extra Shirt Or Bra - Bra
            { "OXWO-0201-0101 L", "Bra L" },
            { "OXWO-0201-0101 M", "Bra M" },
            { "OXWO-0201-0101 S", "Bra S" },
            { "OXWO-0201-0101 XS", "Bra XS" },
            { "OXME-0301-0301 XXL", "Shirt XXL" }, // Extra Shirt Or Bra - Shirt
            { "OXME-0301-0301 XL", "Shirt XL" },
            { "OXME-0301-0301 L", "Shirt L" },
            { "OXME-0301-0301 M", "Shirt M" },
            { "OXME-0301-0301 S", "Shirt S" },
            { "OXUN-1001-1107 m: 2XL/3XL, f: 3XL/4XL", "Strap V" }, // Extra Strap - Female / Male
            { "OXUN-1001-1107 m: 2XL/3XL, f: XL/2XL", "Strap IV" },
            { "OXUN-1001-1107 m: L/XL, f: XL/2XL", "Strap III" },
            { "OXUN-1001-1107 m: S/M, f: M/L", "Strap II" },
            { "OXUN-1001-1107 m: XS, f: XS/S", "Strap I" },
            { "OXA-IP- K (BUNDLE)", "Strap I" }, // Instructor Package - Bundle
            { "OXA-PP- K (BUNDLE)", "Strap I" },
            { "OXPA-0601-0606", "Magnetbox" }, // Others
            { "OXAC-0401-0404", "Phone stand" },
            { "OXAC-0501-0505", "Zipbag" },
            { "OXAC-0801-0906", "Start guide" },
            { "OXAC-0901-1006", "Safety info" }
        };

        public static string MapItems(string input)
        {
            if (input == null)
            {
                return "None";
            }

            if (mappings.TryGetValue(input, out string result))
            {
                return result;
            }
            return "None";
        }
    }
}
