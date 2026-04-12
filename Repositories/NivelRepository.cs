using Dapper;
using System.Data;
using TicketManagement.Models;

namespace TicketManagement.Repositories
{
    public class NivelRepository : BaseRepository
    {
        public NivelRepository(IConfiguration config) : base(config) { }

        public async Task<IEnumerable<Nivel>> GetAllAsync()
        {
            using var db = CreateConnection();
            return await db.QueryAsync<Nivel>("sp_ObtenerNiveles", commandType: CommandType.StoredProcedure);
        }

        public async Task<bool> CreateAsync(Nivel nivel)
        {
            using var db = CreateConnection();
            var rows = await db.ExecuteAsync("sp_InsertarNivel", new { nombre = nivel.Nombre }, commandType: CommandType.StoredProcedure);
            return rows > 0;
        }

        public async Task<bool> UpdateAsync(Nivel nivel)
        {
            using var db = CreateConnection();
            var rows = await db.ExecuteAsync("sp_ActualizarNivel",
                new { idNivel = nivel.IdNivel, nombre = nivel.Nombre },
                commandType: CommandType.StoredProcedure);
            return rows > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using var db = CreateConnection();
            var rows = await db.ExecuteAsync("sp_EliminarNivel", new { idNivel = id }, commandType: CommandType.StoredProcedure);
            return rows > 0;
        }
    }
}
