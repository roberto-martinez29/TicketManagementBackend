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

        // Obtener lista de administradores
        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _repo.GetAllAsync());

        // Login de usuario
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Admin login)
        {
            var user = await _repo.LoginAsync(login.Usuario, login.Contraseña);
            if (user == null) return Unauthorized("Usuario o contraseña incorrectos");

            return Ok(new { user.IdAdmin, user.Usuario, Mensaje = "Acceso concedido" });
        }

        // Registrar nuevo admin (CREATE)
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Admin admin)
        {
            if (string.IsNullOrEmpty(admin.Usuario)) return BadRequest("Datos invalidos");
            return await _repo.CreateAsync(admin) ? Ok("Admin registrado") : BadRequest();
        }

        // Eliminar admin (Opcional, no tenías SP pero es buena práctica)
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            // Nota: Deberías crear un sp_EliminarAdmin en SQL similar a los otros
            return Ok("Funcionalidad de borrado pendiente de SP");
        }
    }
}
