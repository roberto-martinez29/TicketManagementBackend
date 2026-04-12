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

        // GET: api/tickets
        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _repo.GetAllAsync());

        // GET: api/tickets/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var ticket = (await _repo.GetAllAsync(id)).FirstOrDefault();
            return ticket != null ? Ok(ticket) : NotFound();
        }

        // POST: api/tickets
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Ticket ticket)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var success = await _repo.CreateAsync(ticket);
            return success ? Ok(new { mensaje = "Ticket creado con éxito" }) : StatusCode(500, "Error al crear ticket");
        }

        // PUT: api/tickets
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Ticket ticket)
        {
            var success = await _repo.UpdateAsync(ticket);
            return success ? Ok(new { mensaje = "Ticket actualizado" }) : NotFound();
        }

        // DELETE: api/tickets/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _repo.DeleteAsync(id);
            return success ? Ok(new { mensaje = "Ticket eliminado" }) : NotFound();
        }
    }
}
