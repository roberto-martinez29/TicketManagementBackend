using Microsoft.AspNetCore.Mvc;
using TicketManagement.Models;
using TicketManagement.Repositories;

namespace TicketManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly TicketRepository _repo;

        public TicketsController(TicketRepository repo) => _repo = repo;

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _repo.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var ticket = (await _repo.GetAllAsync(id)).FirstOrDefault();
            return ticket != null ? Ok(ticket) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Ticket ticket)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var numTurno = await _repo.CreateAsync(ticket);
            return numTurno > 0 
                ? Ok(new { mensaje = "Ticket creado con éxito", numTurno }) 
                : StatusCode(500, "Error al crear ticket");
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Ticket ticket)
        {
            var success = await _repo.UpdateAsync(ticket);
            return success ? Ok(new { mensaje = "Ticket actualizado" }) : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _repo.DeleteAsync(id);
            return success ? Ok(new { mensaje = "Ticket eliminado" }) : NotFound();
        }

        [HttpGet("buscar")]
        public async Task<IActionResult> BuscarTicket([FromQuery] int numTurno, [FromQuery] string curp, [FromQuery] int idMunicipio)
        {
            var ticket = await _repo.GetByFiltersAsync(numTurno, curp, idMunicipio);

            if (ticket == null)
                return NotFound(new { mensaje = "No se encontró un ticket con esos datos" });

            return Ok(ticket);
        }
    }
}
