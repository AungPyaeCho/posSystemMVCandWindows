using Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Security;

namespace VenomHubLibrary
{
    
    public class DapperService
    {
        private readonly string _connectionString;
        
        public DapperService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<T> Query<T>(string query, object ? param = null)
        {
            using IDbConnection connection = new SqlConnection(_connectionString);
            var list = connection.Query<T>(query, param).ToList();
            return list;
        }

        public T QueryFOD<T>(string query, object? param = null)
        {
            using IDbConnection connection = new SqlConnection(_connectionString);
            var item = connection.Query<T>(query, param).FirstOrDefault();
            return item!;
        }

        public int Execute(string query, object? param = null)
        {
            using IDbConnection connection = new SqlConnection(_connectionString);
            var result = connection.Execute(query, param);
            return result;
        }
    }
}
