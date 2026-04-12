using System.Data;
using TicketManagement.Models;
using Dapper;

namespace TicketManagement.Repositories
{
    public class MunicipioRepository : BaseRepository
    {
        public MunicipioRepository(IConfiguration config) : base(config) { }

        // READ: Obtener todos los municipios
        public async Task<IEnumerable<Municipio>> GetAllAsync()
        {
            using var db = CreateConnection();
            return await db.QueryAsync<Municipio>(
                "sp_ObtenerMunicipios",
                commandType: CommandType.StoredProcedure
            );
        }

        // READ: Obtener un municipio por ID
        public async Task<Municipio?> GetByIdAsync(int id)
        {
            using var db = CreateConnection();
            return await db.QueryFirstOrDefaultAsync<Municipio>(
                "sp_ObtenerMunicipios",
                new { idMunicipio = id },
                commandType: CommandType.StoredProcedure
            );
        }

        // CREATE: Insertar nuevo municipio
        public async Task<bool> CreateAsync(Municipio municipio)
        {
            using var db = CreateConnection();
            var rows = await db.ExecuteAsync(
                "sp_InsertarMunicipio",
                new { nombre = municipio.Nombre },
                commandType: CommandType.StoredProcedure
            );
            return rows > 0;
        }

        // UPDATE: Actualizar municipio existente
        public async Task<bool> UpdateAsync(Municipio municipio)
        {
            using var db = CreateConnection();
            var rows = await db.ExecuteAsync(
                "sp_ActualizarMunicipio",
                new
                {
                    idMunicipio = municipio.IdMunicipio,
                    nombre = municipio.Nombre
                },
                commandType: CommandType.StoredProcedure
            );
            return rows > 0;
        }

        // DELETE: Eliminar municipio
        public async Task<bool> DeleteAsync(int id)
        {
            using var db = CreateConnection();
            var rows = await db.ExecuteAsync(
                "sp_EliminarMunicipio",
                new { idMunicipio = id },
                commandType: CommandType.StoredProcedure
            );
            return rows > 0;
        }
    }
}
