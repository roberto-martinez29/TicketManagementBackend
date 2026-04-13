using Microsoft.AspNetCore.Mvc;
using TicketManagement.Models;
using TicketManagement.Repositories;

namespace TicketManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly AdminRepository _repo;

        public AdminController(AdminRepository repo) => _repo = repo;

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _repo.GetAllAsync());

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Admin login)
        {
            var user = await _repo.LoginAsync(login.Usuario, login.Contrasena);
            if (user == null) return Unauthorized("Usuario o Contrasena incorrectos");

            return Ok(new { user.IdAdmin, user.Usuario, Mensaje = "Acceso concedido" });
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Admin admin)
        {
            if (string.IsNullOrEmpty(admin.Usuario)) return BadRequest("Datos invalidos");
            return await _repo.CreateAsync(admin) ? Ok("Admin registrado") : BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok("Funcionalidad de borrado pendiente de SP");
        }
    }
}
