using System;
using System.Collections.Generic;

namespace SOPManagement.Services.ShopifyService.Helpers
{
    internal static class ShopifyItemsMapping
    {
        private static readonly Dictionary<string, List<string>> mappings = new Dictionary<string, List<string>>
        {
            { "OXUN-0101-0201 V - K" , new List<string> { "Band V", "Sensor", "Phone stand", "Magnetbox", "Start guide" } }, // Band + Sensor - Female / Male
            { "OXUN-0101-0201 IV - K" , new List<string> { "Band IV", "Sensor", "Phone stand", "Magnetbox", "Start guide" } },
            { "OXUN-0101-0201 III - K" , new List<string> { "Band III", "Sensor", "Phone stand", "Magnetbox", "Start guide" } },
            { "OXUN-0101-0201 II - K" , new List<string> { "Band II", "Sensor", "Phone stand", "Magnetbox", "Start guide" } },
            { "OXUN-0101-0201 I - K", new List<string> { "Band I", "Sensor", "Phone stand", "Magnetbox", "Start guide" } },
            { "OXWO-0201-0101 XL - K", new List<string> { "Bra V", "Sensor", "Phone stand", "Magnetbox", "Start guide" } }, // Bra + Sensor
            { "OXWO-0201-0101 L - K", new List<string> { "Bra IV", "Sensor", "Phone stand", "Magnetbox", "Start guide" } },
            { "OXWO-0201-0101 M - K", new List<string> { "Bra III", "Sensor", "Phone stand", "Magnetbox", "Start guide" } },
            { "OXWO-0201-0101 S - K", new List<string> { "Bra II", "Sensor", "Phone stand", "Magnetbox", "Start guide" } },
            { "OXWO-0201-0101 XS - K", new List<string> { "Bra I", "Sensor", "Phone stand", "Magnetbox", "Start guide" } },
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
            { "OXUN-1001-1107 m: 2XL/3XL, f: 3XL/4XL", new List<string> { "Strap V" } }, // Extra Strap - Female
            { "OXUN-1001-1107 m: 2XL/3XL, f: XL/2XL", new List<string> { "Strap IV" } },
            { "OXUN-1001-1107 m: L/XL, f: XL/2XL", new List<string> { "Strap III" } },
            { "OXUN-1001-1107 m: S/M, f: M/L", new List<string> { "Strap II" } },
            { "OXUN-1001-1107 m: XS, f: XS/S", new List<string> { "Strap I" } },
            { "Extra Strap - Male / 2XL/3XL", new List<string> { "Strap IV" } }, // Extra Strap - Male
            { "Extra Strap - Male / L/XL", new List<string> { "Strap III" } },
            { "Extra Strap - Male / S/M", new List<string> { "Strap II" } },
            { "OXWO-ND - K - XL/V", new List<string> { "Bra XL", "Band V", "Sensor", "Phone stand", "Magnetbox", "Start guide" } }, // Night & Day Bundle - Bra
            { "OXWO-ND - K - XL/IV", new List<string> { "Bra XL", "Band IV", "Sensor", "Phone stand", "Magnetbox", "Start guide" } },
            { "OXWO-ND - K (Strap) - XL/III", new List<string> { "Bra XL", "Strap III", "Sensor", "Phone stand", "Magnetbox", "Start guide" } },
            { "OXWO-ND - K - L/V", new List<string> { "Bra L", "Band V", "Sensor", "Phone stand", "Magnetbox", "Start guide" } },
            { "OXWO-ND - K (Strap) - L/III", new List<string> { "Bra L", "Strap III", "Sensor", "Phone stand", "Magnetbox", "Start guide" } },
            { "OXWO-ND - K - L/IV", new List<string> { "Bra L", "Band IV", "Sensor", "Phone stand", "Magnetbox", "Start guide" } },
            { "OXWO-ND - K - L/III", new List<string> { "Bra L", "Band III", "Sensor", "Phone stand", "Magnetbox", "Start guide" } },
            { "OXWO-ND - K (Strap) - M/II", new List<string> { "Bra M", "Strap II", "Sensor", "Phone stand", "Magnetbox", "Start guide" } },
            { "OXWO-ND - K - M/II", new List<string> { "Bra M", "Band II", "Sensor", "Phone stand", "Magnetbox", "Start guide" } },
            { "OXWO-ND - K - M/III", new List<string> { "Bra M", "Band III", "Sensor", "Phone stand", "Magnetbox", "Start guide" } },
            { "OXWO-ND - K - S/II", new List<string> { "Bra S", "Band II", "Sensor", "Phone stand", "Magnetbox", "Start guide" } },
            { "OXWO-ND - K - S/I", new List<string> { "Bra S", "Band I", "Sensor", "Phone stand", "Magnetbox", "Start guide" } },
            { "OXWO-ND - K (Strap) - S/I", new List<string> { "Bra S", "Strap I", "Sensor", "Phone stand", "Magnetbox", "Start guide" } },
            { "OXWO-ND - K - XS/I", new List<string> { "Bra XS", "Band I", "Sensor", "Phone stand", "Magnetbox", "Start guide" } },
            { "OXWO-ND - K (Strap) - XS/I", new List<string> { "Bra XS", "Strap I", "Sensor", "Phone stand", "Magnetbox", "Start guide" } },
            { "OXME-ND - K (Strap) - XXL/IV", new List<string> { "Shirt XXL", "Strap IV", "Sensor", "Phone stand", "Magnetbox", "Start guide" } }, // Night & Day Bundle - Shirt
            { "OXME-ND - K (Strap) - XL/III", new List<string> { "Shirt XL", "Strap III", "Sensor", "Phone stand", "Magnetbox", "Start guide" } },
            { "OXME-ND - K (Strap) - L/III", new List<string> { "Shirt L", "Strap III", "Sensor", "Phone stand", "Magnetbox", "Start guide" } },
            { "OXME-ND - K - L/V", new List<string> { "Shirt L", "Band V", "Sensor", "Phone stand", "Magnetbox", "Start guide" } },
            { "OXME-ND - K - M/IV", new List<string> { "Shirt M", "Band IV", "Sensor", "Phone stand", "Magnetbox", "Start guide" } },
            { "OXME-ND - K - M/V", new List<string> { "Shirt M", "Band V", "Sensor", "Phone stand", "Magnetbox", "Start guide" } },
            { "OXME-ND - K (Strap) - M/II", new List<string> { "Shirt M", "Strap II", "Sensor", "Phone stand", "Magnetbox", "Start guide" } },
            { "OXME-ND - K (Strap) - S/II", new List<string> { "Shirt S", "Strap II", "Sensor", "Phone stand", "Magnetbox", "Start guide" } },
            { "OXME-ND - K - S/III", new List<string> { "Shirt S", "Band III", "Sensor", "Phone stand", "Magnetbox", "Start guide" } },
            { "OXME-ND - K - S/IV", new List<string> { "Shirt S", "Band III", "Sensor", "Phone stand", "Magnetbox", "Start guide" } },
            { "OXME-0301-0301 XXL - K", new List<string> { "Shirt XXL", "Sensor", "Phone stand", "Magnetbox", "Start guide" } }, // Oxa - Shirt, Shirt + Sensor - Shirt
            { "OXME-0301-0301 XL - K", new List<string> { "Shirt XL", "Sensor", "Phone stand", "Magnetbox", "Start guide" } },
            { "OXME-0301-0301 L - K", new List<string> { "Shirt L", "Sensor", "Phone stand", "Magnetbox", "Start guide" } },
            { "OXME-0301-0301 M - K", new List<string> { "Shirt M", "Sensor", "Phone stand", "Magnetbox", "Start guide" } },
            { "OXME-0301-0301 S - K", new List<string> { "Shirt S", "Sensor", "Phone stand", "Magnetbox", "Start guide" } },
            { "OXUN-1001-1107 IV - K", new List<string> { "Strap IV", "Sensor", "Phone stand", "Magnetbox", "Start guide" } }, // Strap + Sensor - Female / Male
            { "OXUN-1001-1107 III - K", new List<string> { "Strap III", "Sensor", "Phone stand", "Magnetbox", "Start guide" } },
            { "OXUN-1001-1107 II - K", new List<string> { "Strap II", "Sensor", "Phone stand", "Magnetbox", "Start guide" } },
            { "OXUN-1001-1107 I - K", new List<string> { "Strap I", "Sensor", "Phone stand", "Magnetbox", "Start guide" } },
            { "OXWO-ND3 - K - XL/V", new List<string> { "Bra XL", "Bra XL", "Band V", "Sensor", "Phone stand", "Magnetbox", "Start guide" } }, // Oxa Women Night & Day Trio
            { "OXWO-ND3 - K - XL/IV", new List<string> { "Bra XL", "Bra XL", "Band IV", "Sensor", "Phone stand", "Magnetbox", "Start guide" } },
            { "OXWO-ND3 - K - L/IV", new List<string> { "Bra L", "Bra L", "Band IV", "Sensor", "Phone stand", "Magnetbox", "Start guide" } },
            { "OXWO-ND3 - K - L/III", new List<string> { "Bra L", "Bra L", "Band III", "Sensor", "Phone stand", "Magnetbox", "Start guide" } },
            { "OXWO-ND3 - K - M/III", new List<string> { "Bra M", "Bra M", "Band III", "Sensor", "Phone stand", "Magnetbox", "Start guide" } },
            { "OXWO-ND3 - K - M/II", new List<string> { "Bra M", "Bra M", "Band II", "Sensor", "Phone stand", "Magnetbox", "Start guide" } },
            { "OXWO-ND3 - K - S/II", new List<string> { "Bra S", "Bra S", "Band II", "Sensor", "Phone stand", "Magnetbox", "Start guide" } },
            { "OXWO-ND3 - K - S/I", new List<string> { "Bra S", "Bra S", "Band I", "Sensor", "Phone stand", "Magnetbox", "Start guide" } },
            { "OXWO-ND3 - K - XS/I", new List<string> { "Bra XS", "Bra XS", "Band I", "Sensor", "Phone stand", "Magnetbox", "Start guide" } },
            { "OXME-ND3 - K - L/V", new List<string> { "Shirt L", "Shirt L", "Band V", "Sensor", "Phone stand", "Magnetbox", "Start guide" } }, // Oxa Men Night & Day Trio
            { "OXME-ND3 - K - M/V", new List<string> { "Shirt M", "Shirt M", "Band V", "Sensor", "Phone stand", "Magnetbox", "Start guide" } },
            { "OXME-ND3 - K - M/IV", new List<string> { "Shirt M", "Shirt M", "Band IV", "Sensor", "Phone stand", "Magnetbox", "Start guide" } },
            { "OXME-ND3 - K - S/IV", new List<string> { "Shirt S", "Shirt S", "Band IV", "Sensor", "Phone stand", "Magnetbox", "Start guide" } },
            { "OXME-ND3 - K - S/III", new List<string> { "Shirt S", "Shirt S", "Band III", "Sensor", "Phone stand", "Magnetbox", "Start guide" } },
            { "SKU - OXA-BP- K (Strap) - I", new List<string> { "Strap I", "Sensor", "Phone stand", "Magnetbox", "Start guide" } }, // Basic Package
            { "II - SKU - OXA-BP- K (Strap) - II", new List<string> { "Strap II", "Sensor", "Phone stand", "Magnetbox", "Start guide" } },
            { "III - SKU - OXA-BP- K (Strap) - III", new List<string> { "Strap III", "Sensor", "Phone stand", "Magnetbox", "Start guide" } },
            { "IV - SKU - OXA-BP- K (Strap) - IV", new List<string> { "Strap IV", "Sensor", "Phone stand", "Magnetbox", "Start guide" } },
            { "OXA-IP- K (BUNDLE)", new List<string> { "Strap I", "Strap II", "Strap III", "Strap IV", "Sensor", "Phone stand", "Magnetbox", "Start guide" } }, // Instructor Package - Bundle
            { "OXA-PP- K (BUNDLE)", new List<string> { "Strap I", "Strap II", "Strap III", "Strap IV", "Sensor", "Sensor", "Phone stand", "Magnetbox", "Start guide" } },
            { "OXPA-0601-0606", new List<string> { "Magnetbox"} }, // Others
            { "OXAC-0401-0404", new List<string> { "Phone stand"} },
            { "OXAC-0501-0505", new List<string> { "Zip bag"} },
            { "OXAC-0801-0906", new List<string> { "Start guide"} },
            { "OXAC-0901-1006", new List<string> { "Safety info"} },
        };

        public static string MapItems(string input)
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