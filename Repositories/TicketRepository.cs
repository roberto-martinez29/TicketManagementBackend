using Dapper;
using System.Data;
using TicketManagement.Models;

namespace TicketManagement.Repositories
{
    public class TicketRepository : BaseRepository
    {
        public async Task<Ticket?> GetByFiltersAsync(int numTurno, string curp, int idMunicipio)
        {
            using var db = CreateConnection();
            var p = new DynamicParameters();
            p.Add("@numTurno", numTurno);
            p.Add("@curp", curp);
            p.Add("@idMunicipio", idMunicipio);
            
            return await db.QueryFirstOrDefaultAsync<Ticket>(
                "sp_LoginUsuario",
                p,
                commandType: CommandType.StoredProcedure
            );
        }
        public TicketRepository(IConfiguration config) : base(config) { }

        // READ: Obtener todos los tickets (o uno solo si se pasa el ID)
        public async Task<IEnumerable<Ticket>> GetAllAsync(int? idTicket = 0)
        {
            using var db = CreateConnection();
            // El SP sp_ObtenerTickets ya maneja el parámetro opcional @idTicket
            return await db.QueryAsync<Ticket>(
                "sp_ObtenerTickets",
                new { idTicket = idTicket },
                commandType: CommandType.StoredProcedure
            );
        }

        // CREATE: Insertar un nuevo ticket
        public async Task<int> CreateAsync(Ticket t)
        {
            using var db = CreateConnection();
            var p = new DynamicParameters();
            p.Add("@curp", t.Curp);
            p.Add("@numTurno", t.NumTurno);
            p.Add("@idMunicipio", t.IdMunicipio);
            p.Add("@tramitante", t.Tramitante);
            p.Add("@nombre", t.Nombre);
            p.Add("@apPaterno", t.ApPaterno);
            p.Add("@apMaterno", t.ApMaterno);
            p.Add("@telefono", t.Telefono);
            p.Add("@celular", t.Celular);
            p.Add("@correo", t.Correo);
            p.Add("@idNivel", t.IdNivel);
            p.Add("@idAsunto", (object)t.IdAsunto ?? DBNull.Value); // Manejo de nulo
            //p.Add("@resuelto", 0);

            var numTurnoGenerado = await db.ExecuteScalarAsync<int>(
                "sp_InsertarTicket",
                p,
                commandType: CommandType.StoredProcedure
            );
            return numTurnoGenerado;
        }

        // UPDATE: Actualizar datos de un ticket existente
        public async Task<bool> UpdateAsync(Ticket t)
        {
            using var db = CreateConnection();
            var p = new DynamicParameters();
            p.Add("@idTicket", t.IdTicket);
            p.Add("@curp", t.Curp);
            p.Add("@numTurno", t.NumTurno);
            p.Add("@idMunicipio", t.IdMunicipio);
            p.Add("@tramitante", t.Tramitante);
            p.Add("@nombre", t.Nombre);
            p.Add("@apPaterno", t.ApPaterno);
            p.Add("@apMaterno", t.ApMaterno);
            p.Add("@telefono", t.Telefono);
            p.Add("@celular", t.Celular);
            p.Add("@correo", t.Correo);
            p.Add("@idNivel", t.IdNivel);
            p.Add("@idAsunto", (object)t.IdAsunto ?? DBNull.Value);
            p.Add("@resuelto", (object)t.Resuelto ?? DBNull.Value);

            var rows = await db.ExecuteAsync(
                "sp_ActualizarTicket",
                p,
                commandType: CommandType.StoredProcedure
            );
            return rows > 0 || rows == -1;
        }

        // DELETE: Eliminar un ticket
        public async Task<bool> DeleteAsync(int id)
        {
            using var db = CreateConnection();
            var rows = await db.ExecuteAsync(
                "sp_EliminarTicket",
                new { idTicket = id },
                commandType: CommandType.StoredProcedure
            );
            return rows > 0 || rows == -1;
        }
    }
}
