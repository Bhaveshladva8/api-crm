using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace matcrm.data.Models.Response
{
    public class TaxRateDetailResponse
    {
        public long Id { get; set; }
        public string TaxType { get; set; }
        public long Percentage { get; set; }
        public long TaxId { get; set; }
    }
}