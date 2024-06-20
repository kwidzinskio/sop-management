using System;
using System.Collections.Generic;

namespace SOPManagement.Services.ShopifyService
{
    internal static class ItemMapping
    {
        private static readonly Dictionary<string, List<string>> mappings = new Dictionary<string, List<string>>
        {
            { "OXUN-0101-0201 V - K" , new List<string> { "Band V", "Sensor" } }, // Band + Sensor - Female / Male
            { "OXUN-0101-0201 IV - K" , new List<string> { "Band IV", "Sensor" } },
            { "OXUN-0101-0201 III - K" , new List<string> { "Band III", "Sensor" } },
            { "OXUN-0101-0201 II - K" , new List<string> { "Band II", "Sensor" } },
            { "OXUN-0101-0201 I - K", new List<string> { "Band I", "Sensor" } },
            { "OXWO-0201-0101 XL - K", new List<string> { "Bra V", "Sensor" } }, // Bra + Sensor
            { "OXWO-0201-0101 L - K", new List<string> { "Bra IV", "Sensor" } },
            { "OXWO-0201-0101 M - K", new List<string> { "Bra III", "Sensor" } },
            { "OXWO-0201-0101 S - K", new List<string> { "Bra II", "Sensor" } },
            { "OXWO-0201-0101 XS - K", new List<string> { "Bra I", "Sensor" } },
            { "OXUN-0101-0201 V", new List<string> { "Band V" } }, // Extra band - Female / Male
            { "OXUN-0101-0201 IV", new List<string> { "Band IV" } },
            { "OXUN-0101-0201 III", new List<string> { "Band III" } },
            { "OXUN-0101-0201 II", new List<string> { "Band II" } },
            { "OXUN-0101-0201 I", new List<string> { "Band I" } },
            { "OXSE-0701-0707", new List<string> { "Sensor" } }, // Extra Sensor Unit
            { "OXWO-0201-0101 XL", new List<string> { "Bra XL" } }, // Extra Shirt Or Bra - Bra
            { "OXWO-0201-0101 L", new List<string> { "Bra L" } }, 
            { "OXWO-0201-0101 M", new List<string> { "Bra M" } }, 
            { "OXWO-0201-0101 S", new List<string> { "Bra S" } }, 
            { "OXWO-0201-0101 XS", new List<string> { "Bra XS" } },
            { "OXME-0301-0301 XXL", new List<string> { "Shirt XXL" } }, // Extra Shirt Or Bra - Shirt
            { "OXME-0301-0301 XL", new List<string> { "Shirt XL" } }, 
            { "OXME-0301-0301 L", new List<string> { "Shirt L" } }, 
            { "OXME-0301-0301 M", new List<string> { "Shirt M" } }, 
            { "OXME-0301-0301 S", new List<string> { "Shirt S" } }, 
            { "OXUN-1001-1107 m: L/XL, f: XL/2XL", new List<string> { "Strap III" } }, // Extra Strap - Female
            { "OXUN-1001-1107 m: S/M, f: M/L", new List<string> { "Strap II" } }, 
            { "OXUN-1001-1107 m: XS, f: XS/S", new List<string> { "Strap I" } }, 
            { "Extra Strap - Male / 2XL/3XL", new List<string> { "Strap IV" } }, // Extra Strap - Male
            { "Extra Strap - Male / L/XL", new List<string> { "Strap III" } }, 
            { "Extra Strap - Male / S/M", new List<string> { "Strap II" } }, 
            { "OXWO-ND - K - XL/V", new List<string> { "Bra XL", "Band V", "Sensor" } }, // Night & Day Bundle - Bra
            { "OXWO-ND - K (Strap) - XL/III", new List<string> { "Bra XL", "Strap III", "Sensor" } },
            { "OXWO-ND - K - L/V", new List<string> { "Bra L", "Band V", "Sensor" } }, 
            { "OXWO-ND - K (Strap) - L/III", new List<string> { "Bra L", "Strap III", "Sensor" } },
            { "OXWO-ND - K - L/IV", new List<string> { "Bra L", "Band IV", "Sensor" } },
            { "OXWO-ND - K - L/III", new List<string> { "Bra L", "Band III", "Sensor" } },
            { "OXWO-ND - K (Strap) - M/II", new List<string> { "Bra M", "Strap II", "Sensor" } },
            { "OXWO-ND - K - M/II", new List<string> { "Bra M", "Band II", "Sensor" } },
            { "OXWO-ND - K - M/III", new List<string> { "Bra M", "Band III", "Sensor" } },
            { "OXWO-ND - K - S/II", new List<string> { "Bra S", "Band II", "Sensor" } },
            { "OXWO-ND - K - S/I", new List<string> { "Bra S", "Band I", "Sensor" } },
            { "OXWO-ND - K (Strap) - S/I", new List<string> { "Bra S", "Strap I", "Sensor" } },
            { "OXWO-ND - K - XS/I", new List<string> { "Bra XS", "Band I", "Sensor" } },
            { "OXWO-ND - K (Strap) - XS/I", new List<string> { "Bra XS", "Strap I", "Sensor" } },
            { "OXME-ND - K (Strap) - XXL/IV", new List<string> { "Shirt XXL", "Strap IV", "Sensor" } }, // Night & Day Bundle - Shirt
            { "OXME-ND - K (Strap) - XL/III", new List<string> { "Shirt XL", "Strap III", "Sensor" } }, 
            { "OXME-ND - K (Strap) - L/III", new List<string> { "Shirt L", "Strap III", "Sensor" } }, 
            { "OXME-ND - K - L/V", new List<string> { "Shirt L", "Band V", "Sensor" } }, 
            { "OXME-ND - K - M/IV", new List<string> { "Shirt M", "Band IV", "Sensor" } }, 
            { "OXME-ND - K (Strap) - M/II", new List<string> { "Shirt M", "Strap II", "Sensor" } }, 
            { "OXME-ND - K (Strap) - S/II", new List<string> { "Shirt S", "Strap II", "Sensor" } }, 
            { "OXME-ND - K - S/III", new List<string> { "Shirt S", "Band III", "Sensor" } }, 
            { "OXME-0301-0301 XXL - K", new List<string> { "Shirt XXL" } }, // Oxa - Shirt, Shirt + Sensor - Shirt
            { "OXME-0301-0301 XL - K", new List<string> { "Shirt XL" } }, 
            { "OXME-0301-0301 L - K", new List<string> { "Shirt L" } }, 
            { "OXME-0301-0301 M - K", new List<string> { "Shirt M" } }, 
            { "OXME-0301-0301 S - K", new List<string> { "Shirt S" } }, 
            { "OXUN-1001-1107 IV - K", new List<string> { "Strap IV", "Sensor" } }, // Strap + Sensor - Female / Male
            { "OXUN-1001-1107 III - K", new List<string> { "Strap III", "Sensor" } },
            { "OXUN-1001-1107 II - K", new List<string> { "Strap II", "Sensor" } },
            { "OXUN-1001-1107 I - K", new List<string> { "Strap I", "Sensor" } },
        };

        public static string MapString(string input)
        {
            if (mappings.TryGetValue(input, out List<string> result))
            {
                string lineItemNameCommaSeparated = string.Join(", ", result);
                return lineItemNameCommaSeparated;
            }
            return "None";
        }
    }
}