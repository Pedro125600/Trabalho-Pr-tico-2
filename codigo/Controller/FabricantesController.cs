using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrabalhoPratico.Models;

namespace TrabalhoPratico.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class FabricantesController : ControllerBase
    {
        private readonly LocadoraBD _context;

        public FabricantesController(LocadoraBD context)
        {
            _context = context;
        }

        // CREATE
        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] Fabricante fabricante)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Fabricantes.Add(fabricante);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(ObterPorId), new { id = fabricante.IdFabricante }, fabricante);
        }

        // READ ALL
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Fabricante>>> Listar()
        {
            return await _context.Fabricantes.ToListAsync();
        }

        // READ BY ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Fabricante>> ObterPorId(int id)
        {
            var fabricante = await _context.Fabricantes.FindAsync(id);
            if (fabricante == null) return NotFound(new { msg = "Fabricante não encontrado" });
            return fabricante;
        }

        // UPDATE
        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(int id, [FromBody] Fabricante fabricante)
        {
            if (id != fabricante.IdFabricante) return BadRequest("IDs não coincidem");

            _context.Entry(fabricante).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Fabricantes.Any(f => f.IdFabricante == id))
                    return NotFound(new { msg = "Fabricante não encontrado" });
                throw;
            }
            return Ok(fabricante);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> AtualizarParcial(int id, [FromBody] JsonPatchDocument<Fabricante> patchDoc)
        {
            if (patchDoc == null)
                return BadRequest("Patch inválido.");

            var fabricante = await _context.Fabricantes.FindAsync(id);
            if (fabricante == null)
                return NotFound(new { msg = "Fabricante não encontrado" });

            patchDoc.ApplyTo(fabricante, ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _context.SaveChangesAsync();

            return Ok(fabricante);
        }

        // DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletar(int id)
        {
            var fabricante = await _context.Fabricantes.FindAsync(id);
            if (fabricante == null) return NotFound(new { msg = "Fabricante não encontrado" });

            _context.Fabricantes.Remove(fabricante);
            await _context.SaveChangesAsync();
            return Ok(new { msg = "Fabricante removido com sucesso" });
        }
    }
}
