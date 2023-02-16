using Dapper;
using IotAlertator.Model;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IotAlertator.Repository
{
    internal class SQLRepository : IRepository
    {
        private readonly SqlRepositoryConfig _sqlRepositoryConfig;
       

        public SQLRepository(SqlRepositoryConfig sqlRepositoryConfig)
        { 
            _sqlRepositoryConfig = sqlRepositoryConfig;
        }

        public int CreateIotAlert(IotAlert iotAlert)
        {
            try
            {
                string sqlQuery = $"INSERT INTO AeAlerts (SensorId,SignalTime,SignalValue,SignalType) VALUES({iotAlert.SensorId}, '{iotAlert.SignalTime.ToString("yyyy-MM-dd HH:mm:ss")}', { iotAlert.SignalValue}, {(int)iotAlert.SignalType} )";
                using (var conn = new SqlConnection(_sqlRepositoryConfig.ConnectionString))
                {
                    var rowsAffected = conn.Execute(sqlQuery,iotAlert);
                    return rowsAffected;
                }
            }
            catch (Exception ex)
            {
                //TODO: log
                return 0;
            }
        }
    }
}
