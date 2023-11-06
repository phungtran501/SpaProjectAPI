using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using SpaManagement.Data.Abstract;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaManagement.Data
{
    public class DapperHelper : IDapperHelper
    {
        private readonly string connectString = string.Empty;

        public DapperHelper(IConfiguration configuration)
        {
            connectString = configuration.GetConnectionString("SpaDB");

        }

        public async Task<IEnumerable<T>> ExcuteStoreProcedureReturnList<T>(string query, DynamicParameters parameters, IDbTransaction dbTransaction = null)
        {
            using (var dbConnection = new SqlConnection(connectString))
            {
                return await dbConnection.QueryAsync<T>(query, parameters, dbTransaction, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task ExecuteNotReturn(string query, DynamicParameters parameters, IDbTransaction dbTransaction = null)
        {
            using (var dbConnection = new SqlConnection(connectString))
            {
                await dbConnection.ExecuteAsync(query, parameters, dbTransaction, commandType: CommandType.Text);
            }
        }

        public async Task<T?> ExecuteReturnScalarAsync<T>(string query, DynamicParameters parameters, IDbTransaction dbTransaction = null)
        {
            using (var dbConnection = new SqlConnection(connectString))
            {
                return (T?)Convert.ChangeType(await dbConnection.ExecuteScalarAsync<T>(query, parameters, dbTransaction, commandType: CommandType.StoredProcedure), typeof(T));
            }
        }

        public async Task<IEnumerable<T>> ExecuteSqlReturnList<T>(string query, DynamicParameters parameters, IDbTransaction dbTransaction = null)
        {
            using (var dbConnection = new SqlConnection(connectString))
            {
                return await dbConnection.QueryAsync<T>(query, parameters, dbTransaction, commandType: CommandType.Text);
            }
        }
    }
}
