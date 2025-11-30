using Microsoft.EntityFrameworkCore;
using TrabalhoPratico.Models;

namespace TrabalhoPratico.CRUDS
{
    public class CRUDAluguel
    {
        public void CriarAluguel(LocadoraBD context)
        {
            var a = new Aluguel();
            Console.Write("ID Cliente: "); a.IdCliente = int.Parse(Console.ReadLine());
            Console.Write("ID Veículo: "); a.IdVeiculo = int.Parse(Console.ReadLine());
            Console.Write("Data Retirada: "); a.DataRetirada = DateTime.Parse(Console.ReadLine());
            Console.Write("Data Devolução: "); a.DataDevolucao = DateTime.Parse(Console.ReadLine());
            Console.Write("KM Inicial: "); a.QuilometragemInicial = double.Parse(Console.ReadLine());
            Console.Write("Valor Diária: "); a.ValorDiaria = decimal.Parse(Console.ReadLine());

            context.Alugueis.Add(a);
            context.SaveChanges();
            Console.WriteLine("Aluguel cadastrado!");
        }

        public void ListarAlugueis(LocadoraBD context)
        {
            var lista = context.Alugueis.Include(a => a.Cliente).Include(a => a.Veiculo).ToList();
            foreach (var a in lista)
                Console.WriteLine($"ID:{a.IdAluguel} | Cliente:{a.Cliente.Nome} | Veículo:{a.Veiculo.Modelo}");
        }

        public void AtualizarAluguel(LocadoraBD context)
        {
            Console.Write("ID: ");
            int id = int.Parse(Console.ReadLine());
            var a = context.Alugueis.Find(id);
            if (a == null) { Console.WriteLine("Aluguel não encontrado."); return; }

            Console.Write("Data Retirada: "); var dr = Console.ReadLine(); if (!string.IsNullOrWhiteSpace(dr)) a.DataRetirada = DateTime.Parse(dr);
            Console.Write("Data Devolução: "); var dd = Console.ReadLine(); if (!string.IsNullOrWhiteSpace(dd)) a.DataDevolucao = DateTime.Parse(dd);
            Console.Write("KM Final: "); var km = Console.ReadLine(); if (!string.IsNullOrWhiteSpace(km)) a.QuilometragemFinal = double.Parse(km);
            Console.Write("Valor Total: "); var vt = Console.ReadLine(); if (!string.IsNullOrWhiteSpace(vt)) a.ValorTotal = decimal.Parse(vt);

            context.SaveChanges();
            Console.WriteLine(" Aluguel atualizado!");
        }

        public void ExcluirAluguel(LocadoraBD context)
        {
            Console.Write("ID: ");
            int id = int.Parse(Console.ReadLine());
            var a = context.Alugueis.Find(id);
            if (a == null) { Console.WriteLine("Aluguel não encontrado."); return; }
            context.Alugueis.Remove(a);
            context.SaveChanges();
            Console.WriteLine(" Aluguel removido!");
        }
    }
}
