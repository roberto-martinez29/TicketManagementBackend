using Dapper;
using System.Data;
using TicketManagement.Models;

namespace TicketManagement.Repositories
{
    public class AsuntoRepository : BaseRepository
    {
        public AsuntoRepository(IConfiguration config) : base(config) { }

        public async Task<IEnumerable<Asunto>> GetAllAsync()
        {
            using var db = CreateConnection();
            return await db.QueryAsync<Asunto>("sp_ObtenerAsuntos", commandType: CommandType.StoredProcedure);
        }

        public async Task<bool> CreateAsync(Asunto asunto)
        {
            using var db = CreateConnection();
            var rows = await db.ExecuteAsync("sp_InsertarAsunto", new { nombre = asunto.Nombre }, commandType: CommandType.StoredProcedure);
            return rows > 0;
        }

        public async Task<bool> UpdateAsync(Asunto asunto)
        {
            using var db = CreateConnection();
            var rows = await db.ExecuteAsync("sp_ActualizarAsunto",
                new { idAsunto = asunto.IdAsunto, nombre = asunto.Nombre },
                commandType: CommandType.StoredProcedure);
            return rows > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using var db = CreateConnection();
            var rows = await db.ExecuteAsync("sp_EliminarAsunto", new { idAsunto = id }, commandType: CommandType.StoredProcedure);
            return rows > 0;
        }
    }
}
