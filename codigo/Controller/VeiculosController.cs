using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrabalhoPratico.Models;

namespace TrabalhoPratico.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class VeiculosController : ControllerBase
    {
        private readonly LocadoraBD _context;

        public VeiculosController(LocadoraBD context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] Veiculo veiculo)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Veiculos.Add(veiculo);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(ObterPorId), new { id = veiculo.IdVeiculo }, veiculo);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Veiculo>>> Listar()
        {
            return await _context.Veiculos.Include(v => v.Fabricante).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Veiculo>> ObterPorId(int id)
        {
            var veiculo = await _context.Veiculos.Include(v => v.Fabricante).FirstOrDefaultAsync(v => v.IdVeiculo == id);
            if (veiculo == null) return NotFound(new { msg = "Veículo não encontrado" });
            return veiculo;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(int id, [FromBody] Veiculo veiculo)
        {
            if (id != veiculo.IdVeiculo) return BadRequest("IDs não coincidem");

            _context.Entry(veiculo).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Veiculos.Any(v => v.IdVeiculo == id))
                    return NotFound(new { msg = "Veículo não encontrado" });
                throw;
            }
            return Ok(veiculo);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> AtualizarParcial(int id, [FromBody] JsonPatchDocument<Veiculo> patchDoc)
        {
            if (patchDoc == null)
                return BadRequest("Patch inválido.");

            var veiculo = await _context.Veiculos.FindAsync(id);
            if (veiculo == null)
                return NotFound(new { msg = "Veículo não encontrado" });

            patchDoc.ApplyTo(veiculo, ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _context.SaveChangesAsync();

            return Ok(veiculo);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletar(int id)
        {
            var veiculo = await _context.Veiculos.FindAsync(id);
            if (veiculo == null) return NotFound(new { msg = "Veículo não encontrado" });

            _context.Veiculos.Remove(veiculo);
            await _context.SaveChangesAsync();
            return Ok(new { msg = "Veículo removido com sucesso" });
        }
    }
}
