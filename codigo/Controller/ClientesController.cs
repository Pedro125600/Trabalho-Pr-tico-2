using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrabalhoPratico.Models;

namespace TrabalhoPratico.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly LocadoraBD _context;

        public ClientesController(LocadoraBD context)
        {
            _context = context;
        }

        // CREATE
        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] Cliente cliente)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(ObterPorId), new { id = cliente.IdCliente }, cliente);
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cliente>>> Listar()
        {
            return await _context.Clientes.ToListAsync();
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Cliente>> ObterPorId(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null) return NotFound(new { msg = "Cliente não encontrado" });
            return cliente;
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(int id, [FromBody] Cliente cliente)
        {
            if (id != cliente.IdCliente) return BadRequest("IDs não coincidem");

            _context.Entry(cliente).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Clientes.Any(c => c.IdCliente == id))
                    return NotFound(new { msg = "Cliente não encontrado" });
                throw;
            }
            return Ok(cliente);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> AtualizarParcial(int id, [FromBody] JsonPatchDocument<Cliente> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest("Patch inválido.");
            }

            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return NotFound(new { msg = "Cliente não encontrado" });
            }

            patchDoc.ApplyTo(cliente, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _context.SaveChangesAsync();

            return Ok(cliente);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletar(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null) return NotFound(new { msg = "Cliente não encontrado" });

            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();
            return Ok(new { msg = "Cliente removido com sucesso" });
        }
    }
}
