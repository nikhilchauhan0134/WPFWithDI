using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loding_Report.Database
{
    public class SqlDatabaseUtility : ISqlDatabaseUtility
    {
        private readonly string _fallbackConnectionString;

        public SqlDatabaseUtility()
        {
            // Pulls directly from your App.config file automatically
            _fallbackConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"]?.ConnectionString;
        }

        public async Task<DataSet> GetDataSetFromProcedureAsync(Dictionary<string, object> parameters, string procedureName)
        {
            string connectionString = _fallbackConnectionString;

            
            DataSet dataSet = new DataSet();

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    await sqlConnection.OpenAsync();

                    using (SqlCommand sqlCommand = new SqlCommand(procedureName, sqlConnection))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.Clear();

                        if (parameters != null)
                        {
                            foreach (var param in parameters)
                            {
                                sqlCommand.Parameters.AddWithValue(param.Key, param.Value ?? DBNull.Value);
                            }
                        }

                        using (SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCommand))
                        {
                            await Task.Run(() => dataAdapter.Fill(dataSet));
                        }
                    }
                }
            }
            catch (Exception)
            {
                return dataSet;
            }

            return dataSet;
        }
    }
}
