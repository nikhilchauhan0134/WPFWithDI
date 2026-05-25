using Loding_Report.Database;
using Loding_Report.Models;
using Loding_Report.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loding_Report.Repositories
{
    public class ReportDataService : IReportDataService
    {
        private readonly ISqlDatabaseUtility _dbUtility;

        public ReportDataService(ISqlDatabaseUtility dbUtility)
        {
            _dbUtility = dbUtility;
        }
        public List<PropertyItem> GetProperties()
        {
            return new List<PropertyItem>
            {
                new PropertyItem { PropertyId = 40842, PropertyName = "FX FD Central QA Property1" },
                new PropertyItem { PropertyId = 900004, PropertyName = "FX FD East QA Property2" }
            };
        }

        public List<OutletItem> GetOutlets()
        {
            return new List<OutletItem>
            {
                new OutletItem { OutletCode = 0, OutletName = "All" },
                new OutletItem { OutletCode = 91050, OutletName = "Spa Plein De Vie" }
            };
        }
        public async Task<RevenueCollectionSummaryResp> GetRevenueReportAsync(RevenueReportRequest request)
        {
            var response = new RevenueCollectionSummaryResp();

            try
            {
                var procedureParams = new Dictionary<string, object>
                {
                    { "@PropertyID", request.PropertyId },
                    { "@FromDate", request.FromDate },
                    { "@TooDate", request.ToDate },
                    { "@OutletCode", request.OutletCode }
                };

                // Forward the request to your separate DB layer utility
                DataSet dataSet = await _dbUtility.GetDataSetFromProcedureAsync(procedureParams, "FXSPA_RevenueCollectionSummaryReport");

                if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                {
                    response.Status = 1;
                    response.Message = "Success";
                    response.RevenueCollectionDetailsList = JsonConvert.SerializeObject(dataSet.Tables[0]);
                }
                else
                {
                    response.Status = 0;
                    response.Message = "No Data Found";
                    response.RevenueCollectionDetailsList = "No Data";
                }
            }
            catch (Exception ex)
            {
                response.Status = 0;
                response.Message = $"Oops something went wrong: {ex.Message}";
                response.RevenueCollectionDetailsList = "No Data";
            }

            return response;
        }
    }
}
