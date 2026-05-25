using Loding_Report.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loding_Report.Services
{
    public interface IReportDataService
    {
        List<PropertyItem> GetProperties();
        List<OutletItem> GetOutlets();
        Task<RevenueCollectionSummaryResp> GetRevenueReportAsync(RevenueReportRequest request);
    }
}
