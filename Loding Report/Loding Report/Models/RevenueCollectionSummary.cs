using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loding_Report.Models
{
    public class RevenueCollectionSummaryResp
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public string RevenueCollectionDetailsList { get; set; } // Your raw JSON string comes here
        public int TotalRecord { get; set; }
    }
    public class RevenueReportRequest
    {
        public long PropertyId { get; set; }
        public long OutletCode { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
}
