using Dapper;
using System.Data;
using TicketManagement.Models;

namespace TicketManagement.Repositories
{
    public class AdminRepository : BaseRepository
    {
        public AdminRepository(IConfiguration config) : base(config) { }

        public async Task<IEnumerable<Admin>> GetAllAsync()
        {
            using var db = CreateConnection();
            return await db.QueryAsync<Admin>("sp_ObtenerAdmins", commandType: CommandType.StoredProcedure);
        }

        public async Task<bool> CreateAsync(Admin admin)
        {
            using var db = CreateConnection();
            var p = new { usuario = admin.Usuario, contrasena = admin.Contraseña };
            var rows = await db.ExecuteAsync("sp_InsertarAdmin", p, commandType: CommandType.StoredProcedure);
            return rows > 0;
        }

        // Método adicional para validación de credenciales
        public async Task<Admin?> LoginAsync(string usuario, string password)
        {
            using var db = CreateConnection();
            var sql = "SELECT * FROM [Admin] WHERE usuario = @usuario AND contraseña = @password";
            return await db.QueryFirstOrDefaultAsync<Admin>(sql, new { usuario, password });
        }
    }
}
