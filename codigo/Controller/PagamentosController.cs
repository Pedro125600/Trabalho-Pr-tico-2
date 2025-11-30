using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrabalhoPratico.Models;

namespace TrabalhoPratico.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class PagamentosController : ControllerBase
    {
        private readonly LocadoraBD _context;

        public PagamentosController(LocadoraBD context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] Pagamento pagamento)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Pagamentos.Add(pagamento);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(ObterPorId), new { id = pagamento.IdPagamento }, pagamento);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pagamento>>> Listar()
        {
            return await _context.Pagamentos.Include(p => p.Aluguel).ThenInclude(a => a.Cliente).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Pagamento>> ObterPorId(int id)
        {
            var pagamento = await _context.Pagamentos.Include(p => p.Aluguel).ThenInclude(a => a.Cliente).FirstOrDefaultAsync(p => p.IdPagamento == id);
            if (pagamento == null) return NotFound(new { msg = "Pagamento não encontrado" });
            return pagamento;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(int id, [FromBody] Pagamento pagamento)
        {
            if (id != pagamento.IdPagamento) return BadRequest("IDs não coincidem");

            _context.Entry(pagamento).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Pagamentos.Any(p => p.IdPagamento == id))
                    return NotFound(new { msg = "Pagamento não encontrado" });
                throw;
            }
            return Ok(pagamento);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> AtualizarParcial(int id, [FromBody] JsonPatchDocument<Pagamento> patchDoc)
        {
            if (patchDoc == null)
                return BadRequest("Patch inválido.");

            var pagamento = await _context.Pagamentos.FindAsync(id);
            if (pagamento == null)
                return NotFound(new { msg = "Pagamento não encontrado" });

            patchDoc.ApplyTo(pagamento, ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _context.SaveChangesAsync();

            return Ok(pagamento);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletar(int id)
        {
            var pagamento = await _context.Pagamentos.FindAsync(id);
            if (pagamento == null) return NotFound(new { msg = "Pagamento não encontrado" });

            _context.Pagamentos.Remove(pagamento);
            await _context.SaveChangesAsync();
            return Ok(new { msg = "Pagamento removido com sucesso" });
        }
    }
}
