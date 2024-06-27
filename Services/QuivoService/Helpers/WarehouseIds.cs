using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOPManagement.Services.QuivoService.Helpers
{
    public static class WarehouseIds
    {
        private static readonly Dictionary<int, string> values = new Dictionary<int, string>
    {
        { 4, "DE" },
        { 7, "UK" },
        { 8, "US" }
    };

        public static ReadOnlyDictionary<int, string> Values
        {
            get { return new ReadOnlyDictionary<int, string>(values); }
        }
    }
}
