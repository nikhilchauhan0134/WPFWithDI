using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loding_Report.Database
{
    public interface ISqlDatabaseUtility
    {
        Task<DataSet> GetDataSetFromProcedureAsync(Dictionary<string, object> parameters, string procedureName);
    }
}
