using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperDemoData.Data
{
    public class DataAccess:IDataAccess
    {
        private readonly IConfiguration configuration;

        public DataAccess(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        //this method is used for getting list of type T
        public async Task<IEnumerable<T>>GetData<T,P>
            (string query,P parameters,string connectionId= "DefaultConnection")
        {
            using IDbConnection connection =
                new SqlConnection(configuration.GetConnectionString(connectionId));
            return await connection.QueryAsync<T>(query, parameters);
        }


        //this method does not return anything
        public async Task SaveData< P>
            (string query, P parameters, string connectionId = "DefaultConnection")
        {
            using IDbConnection connection =
               new SqlConnection(configuration.GetConnectionString(connectionId));
            await connection.ExecuteAsync(query, parameters);
        }
    }
}
