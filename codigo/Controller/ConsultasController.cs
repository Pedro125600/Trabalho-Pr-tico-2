using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrabalhoPratico;
using TrabalhoPratico.Models;

namespace TrabalhoPraticoPart3.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConsultasController : ControllerBase
    {
        private readonly LocadoraBD _context;

        public ConsultasController(LocadoraBD context)
        {
            _context = context;
        }

        // ============================================================
        // 1) VEÍCULOS + FABRICANTE
        // GET: api/consultas/veiculos-com-fabricante
        // ============================================================
        [HttpGet("veiculos-com-fabricante")]
        public IActionResult GetVeiculosComFabricante()
        {
            var resultado = _context.Veiculos
                .Include(v => v.Fabricante)
                .Select(v => new
                {
                    v.Modelo,
                    v.Placa,
                    Fabricante = v.Fabricante.Nome
                })
                .ToList();

            return Ok(resultado);
        }

        // ============================================================
        // 2) ALUGUÉIS + CLIENTE + VEÍCULO
        // GET: api/consultas/alugueis-com-cliente-veiculo
        // ============================================================
        [HttpGet("alugueis-com-cliente-veiculo")]
        public IActionResult GetAlugueisComClienteEVeiculo()
        {
            var resultado = _context.Alugueis
                .Include(a => a.Cliente)
                .Include(a => a.Veiculo)
                .Select(a => new
                {
                    Cliente = a.Cliente.Nome,
                    Veiculo = a.Veiculo.Modelo,
                    a.DataRetirada,
                    a.DataDevolucao
                })
                .ToList();

            return Ok(resultado);
        }

        // ============================================================
        // 3) PAGAMENTOS + CLIENTE
        // GET: api/consultas/pagamentos-com-cliente
        // ============================================================
        [HttpGet("pagamentos-com-cliente")]
        public IActionResult GetPagamentosComCliente()
        {
            var resultado = _context.Pagamentos
                .Include(p => p.Aluguel).ThenInclude(a => a.Cliente)
                .Select(p => new
                {
                    Cliente = p.Aluguel.Cliente.Nome,
                    p.ValorPago,
                    p.FormaPagamento
                })
                .ToList();

            return Ok(resultado);
        }

        // ============================================================
        // 4) CLIENTES QUE ALUGARARAM VEÍCULOS DE UM FABRICANTE
        // GET: api/consultas/clientes-por-fabricante/{idFabricante}
        // ============================================================
        [HttpGet("clientes-por-fabricante/{idFabricante:int}")]
        public IActionResult GetClientesPorFabricante(int idFabricante)
        {
            var resultado = _context.Alugueis
                .Include(a => a.Cliente)
                .Include(a => a.Veiculo).ThenInclude(v => v.Fabricante)
                .Where(a => a.Veiculo.IdFabricante == idFabricante)
                .Select(a => new
                {
                    Cliente = a.Cliente.Nome,
                    Veiculo = a.Veiculo.Modelo,
                    Fab = a.Veiculo.Fabricante.Nome
                })
                .Distinct() // evita duplicados
                .ToList();

            if (!resultado.Any())
                return NotFound("Nenhum cliente alugou veículos deste fabricante.");

            return Ok(resultado);
        }

        // ============================================================
        // 5) HISTÓRICO DE ALUGUÉIS DO CLIENTe
        // GET: api/consultas/historico-cliente/{idCliente}
        // ============================================================
        [HttpGet("historico-cliente/{idCliente:int}")]
        public IActionResult GetHistoricoCliente(int idCliente)
        {
            var resultado = _context.Alugueis
                .Include(a => a.Veiculo).ThenInclude(v => v.Fabricante)
                .Where(a => a.IdCliente == idCliente)
                .Select(a => new
                {
                    Veiculo = a.Veiculo.Modelo,
                    Fab = a.Veiculo.Fabricante.Nome,
                    a.DataRetirada,
                    a.DataDevolucao
                })
                .ToList();

            if (!resultado.Any())
                return NotFound("Nenhum histórico encontrado para este cliente.");

            return Ok(resultado);
        }
    }
}
