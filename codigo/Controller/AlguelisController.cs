using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrabalhoPratico.Models;

namespace TrabalhoPratico.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlugueisController : ControllerBase
    {
        private readonly LocadoraBD _context;

        public AlugueisController(LocadoraBD context)
        {
            _context = context;
        }

        // POST - Criar
        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] Aluguel aluguel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Alugueis.Add(aluguel);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(ObterPorId), new { id = aluguel.IdAluguel }, aluguel);
        }

        // GET - Listar
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Aluguel>>> Listar()
        {
            return await _context.Alugueis
                .Include(a => a.Cliente)
                .Include(a => a.Veiculo)
                .ToListAsync();
        }

        // GET - Por ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Aluguel>> ObterPorId(int id)
        {
            var aluguel = await _context.Alugueis
                .Include(a => a.Cliente)
                .Include(a => a.Veiculo)
                .FirstOrDefaultAsync(a => a.IdAluguel == id);

            if (aluguel == null)
                return NotFound(new { msg = "Aluguel não encontrado" });

            return aluguel;
        }

        // PATCH - Atualização Parcial
        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<Aluguel> patchDoc)
        {
            if (patchDoc == null)
                return BadRequest();

            var aluguel = await _context.Alugueis.FindAsync(id);
            if (aluguel == null)
                return NotFound(new { msg = "Aluguel não encontrado" });

            patchDoc.ApplyTo(aluguel); // <-- AQUI ESTÁ O AJUSTE

            // Revalidar o modelo após aplicar patch
            TryValidateModel(aluguel);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _context.SaveChangesAsync();

            return Ok(aluguel);
        }


        // DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletar(int id)
        {
            var aluguel = await _context.Alugueis.FindAsync(id);
            if (aluguel == null)
                return NotFound(new { msg = "Aluguel não encontrado" });

            _context.Alugueis.Remove(aluguel);
            await _context.SaveChangesAsync();
            return Ok(new { msg = "Aluguel removido com sucesso" });
        }
    }
}
