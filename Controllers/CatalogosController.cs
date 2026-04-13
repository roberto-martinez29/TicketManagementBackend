using Microsoft.AspNetCore.Mvc;
using TicketManagement.Models;
using TicketManagement.Repositories;

namespace TicketManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogosController : ControllerBase
    {
        private readonly MunicipioRepository _munRepo;
        private readonly NivelRepository _nivRepo;
        private readonly AsuntoRepository _asuRepo;

        public CatalogosController(MunicipioRepository munRepo, NivelRepository nivRepo, AsuntoRepository asuRepo)
        {
            _munRepo = munRepo;
            _nivRepo = nivRepo;
            _asuRepo = asuRepo;
        }

        [HttpGet("municipios")]
        public async Task<IActionResult> GetMunicipios() => Ok(await _munRepo.GetAllAsync());

        [HttpPost("municipios")]
        public async Task<IActionResult> CreateMunicipio([FromBody] Municipio m) =>
            await _munRepo.CreateAsync(m) ? Ok("Municipio creado") : BadRequest();

        [HttpDelete("municipios/{id}")]
        public async Task<IActionResult> DeleteMunicipio(int id) =>
            await _munRepo.DeleteAsync(id) ? Ok("Municipio eliminado") : NotFound();

        [HttpGet("niveles")]
        public async Task<IActionResult> GetNiveles() => Ok(await _nivRepo.GetAllAsync());

        [HttpPost("niveles")]
        public async Task<IActionResult> CreateNivel([FromBody] Nivel n) =>
            await _nivRepo.CreateAsync(n) ? Ok("Nivel creado") : BadRequest();

        [HttpPut("niveles")]
        public async Task<IActionResult> UpdateNivel([FromBody] Nivel n) =>
            await _nivRepo.UpdateAsync(n) ? Ok("Nivel actualizado") : NotFound();

        [HttpDelete("niveles/{id}")]
        public async Task<IActionResult> DeleteNivel(int id) =>
            await _nivRepo.DeleteAsync(id) ? Ok("Nivel eliminado") : NotFound();

        [HttpGet("asuntos")]
        public async Task<IActionResult> GetAsuntos() => Ok(await _asuRepo.GetAllAsync());

        [HttpPost("asuntos")]
        public async Task<IActionResult> CreateAsunto([FromBody] Asunto a) =>
            await _asuRepo.CreateAsync(a) ? Ok("Asunto creado") : BadRequest();

        [HttpPut("asuntos")]
        public async Task<IActionResult> UpdateAsunto([FromBody] Asunto a) =>
            await _asuRepo.UpdateAsync(a) ? Ok("Asunto actualizado") : NotFound();

        [HttpDelete("asuntos/{id}")]
        public async Task<IActionResult> DeleteAsunto(int id) =>
            await _asuRepo.DeleteAsync(id) ? Ok("Asunto eliminado") : NotFound();
    }
}
