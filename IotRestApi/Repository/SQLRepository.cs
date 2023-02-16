using Dapper;
using IotRestApi.Config;
using IotRestApi.Model;
using System.Data.SqlClient;

namespace IotRestApi.Repository
{
    public class SQLRepository : IRepository
    {
        private readonly SqlRepositoryConfig        _sqlRepositoryConfig;
        private readonly ILogger<SQLRepository>     _logger;


        public SQLRepository(ILogger<SQLRepository> _logger)
        {
            //TODO: get config using dependency injection
            _sqlRepositoryConfig = new SqlRepositoryConfig();
            _sqlRepositoryConfig.ConnectionString = "Server=DESKTOP-LU6BPF1\\SQLEXPRESS;Database=Research;Trusted_Connection=True;MultipleActiveResultSets=true";
        
            _logger = _logger;
        }
      
        public IEnumerable<IotAlert> GetIotAlerts(int pageNumber, int pageSize, string orderBy, bool asc)
        {
            try
            {
                using (var conn = new SqlConnection(_sqlRepositoryConfig.ConnectionString))
                {
                    string query = $"SELECT *  From AeIotAlerts with (NOLOCK) ORDER BY {orderBy} { (asc ? " asc " : " desc ")  } OFFSET {pageSize * (pageNumber - 1)}  ROWS FETCH NEXT {pageSize}  ROWS ONLY";
                    var result = conn.Query<IotAlert>(query);
                    return result;
                }
            }
            catch (Exception ex)
            {
                //TODO: log
                return null;
            }

        }
    }
}
