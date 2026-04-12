using System.Data;
using Microsoft.Data.SqlClient;

namespace TicketManagement.Repositories
{
    public abstract class BaseRepository
    {
        private readonly string _connectionString;
        public BaseRepository(IConfiguration configuration) =>
            _connectionString = configuration.GetConnectionString("DefaultConnection")!;

        protected IDbConnection CreateConnection() => new SqlConnection(_connectionString);
    }
}
