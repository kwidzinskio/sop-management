using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOPManagement.Models
{
    internal class QuivoProductQty
    {
        [JsonProperty("sku")]
        public string Sku { get; set; }

        [JsonProperty("sellerSku")]
        public string SellerSku { get; set; }

        [JsonProperty("quantity")]
        public int Quantity { get; set; }

        [JsonProperty("inventory")]
        public int Inventory { get; set; }

        [JsonProperty("inbound")]
        public int Inbound { get; set; }

        [JsonProperty("reserved")]
        public int Reserved { get; set; }
    }
}
