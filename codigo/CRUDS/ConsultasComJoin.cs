using Microsoft.EntityFrameworkCore;

namespace TrabalhoPratico.CRUDS
{
    public class ConsultasComJoin
    {
        public  void ConsultaVeiculosComFabricante(LocadoraBD context)
        {
            var v1 = context.Veiculos
                .Include(v => v.Fabricante)
                .Select(v => new { v.Modelo, v.Placa, Fabricante = v.Fabricante.Nome })
                .ToList();

            Console.WriteLine("\n--- VEÍCULOS COM FABRICANTE ---");
            foreach (var x in v1)
                Console.WriteLine($"{x.Modelo} ({x.Placa}) - {x.Fabricante}");
        }

     
        public  void ConsultaAlugueisComClienteEVeiculo(LocadoraBD context)
        {
            var v2 = context.Alugueis
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

            Console.WriteLine("\n--- ALUGUÉIS COM CLIENTE E VEÍCULO ---");
            foreach (var x in v2)
                Console.WriteLine($"{x.Cliente} - {x.Veiculo} [{x.DataRetirada:dd/MM} - {x.DataDevolucao:dd/MM}]");
        }


        public  void ConsultaPagamentosComCliente(LocadoraBD context)
        {
            var v3 = context.Pagamentos
                .Include(p => p.Aluguel).ThenInclude(a => a.Cliente)
                .Select(p => new
                {
                    Cliente = p.Aluguel.Cliente.Nome,
                    p.ValorPago,
                    p.FormaPagamento
                })
                .ToList();

            Console.WriteLine("\n--- PAGAMENTOS COM CLIENTE ---");
            foreach (var x in v3)
                Console.WriteLine($"{x.Cliente} pagou {x.ValorPago:C} via {x.FormaPagamento}");
        }


        public void ConsultaClientesPorFabricante(LocadoraBD context)
        {
            Console.Write("Digite o ID do fabricante: ");
            if (int.TryParse(Console.ReadLine(), out int fid))
            {
                var v4 = context.Alugueis
                    .Include(a => a.Cliente)
                    .Include(a => a.Veiculo).ThenInclude(v => v.Fabricante)
                    .Where(a => a.Veiculo.IdFabricante == fid)
                    .Select(a => new
                    {
                        Cliente = a.Cliente.Nome,
                        Veiculo = a.Veiculo.Modelo,
                        Fab = a.Veiculo.Fabricante.Nome
                    })
                    .Distinct()
                    .ToList();

                Console.WriteLine("\n--- CLIENTES POR FABRICANTE ---");
                foreach (var x in v4)
                    Console.WriteLine($"{x.Cliente} alugou {x.Veiculo} - {x.Fab}");
            }
            else
            {
                Console.WriteLine("ID inválido.");
            }
        }


        public void ConsultaHistoricoCliente(LocadoraBD context)
        {
            Console.Write("Digite o ID do cliente: ");
            if (int.TryParse(Console.ReadLine(), out int cid))
            {
                var v5 = context.Alugueis
                    .Include(a => a.Veiculo).ThenInclude(v => v.Fabricante)
                    .Where(a => a.IdCliente == cid)
                    .Select(a => new
                    {
                        Veiculo = a.Veiculo.Modelo,
                        Fab = a.Veiculo.Fabricante.Nome,
                        a.DataRetirada,
                        a.DataDevolucao
                    })
                    .ToList();

                Console.WriteLine("\n--- HISTÓRICO DO CLIENTE ---");
                foreach (var x in v5)
                    Console.WriteLine($"{x.Veiculo} ({x.Fab}) - {x.DataRetirada:dd/MM} até {x.DataDevolucao:dd/MM}");
            }
            else
            {
                Console.WriteLine("ID inválido.");
            }
        }
    }
}

