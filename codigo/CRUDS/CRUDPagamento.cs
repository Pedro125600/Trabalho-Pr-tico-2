using Microsoft.EntityFrameworkCore;
using TrabalhoPratico.Models;

namespace TrabalhoPratico.CRUDS
{
    public class CRUDPagamento
    {
        public void CriarPagamento(LocadoraBD context)
        {
            var p = new Pagamento();
            Console.Write("ID Aluguel: "); p.IdAluguel = int.Parse(Console.ReadLine());
            Console.Write("Data Pagamento: "); p.DataPagamento = DateTime.Parse(Console.ReadLine());
            Console.Write("Valor Pago: "); p.ValorPago = decimal.Parse(Console.ReadLine());
            Console.Write("Forma Pagamento: "); p.FormaPagamento = Console.ReadLine();
            context.Pagamentos.Add(p);
            context.SaveChanges();
            Console.WriteLine("Pagamento cadastrado!");
        }

        public void ListarPagamentos(LocadoraBD context)
        {
            var lista = context.Pagamentos.Include(p => p.Aluguel).ThenInclude(a => a.Cliente).ToList();
            foreach (var p in lista)
                Console.WriteLine($"ID:{p.IdPagamento} | Cliente:{p.Aluguel.Cliente.Nome} | Valor:{p.ValorPago:C}");
        }

        public void AtualizarPagamento(LocadoraBD context)
        {
            Console.Write("ID: ");
            int id = int.Parse(Console.ReadLine());
            var p = context.Pagamentos.Find(id);
            if (p == null) { Console.WriteLine("Pagamento não encontrado."); return; }

            Console.Write("Data: "); var d = Console.ReadLine(); if (!string.IsNullOrWhiteSpace(d)) p.DataPagamento = DateTime.Parse(d);
            Console.Write("Valor: "); var v = Console.ReadLine(); if (!string.IsNullOrWhiteSpace(v)) p.ValorPago = decimal.Parse(v);
            Console.Write("Forma: "); var f = Console.ReadLine(); if (!string.IsNullOrWhiteSpace(f)) p.FormaPagamento = f;

            context.SaveChanges();
            Console.WriteLine("Pagamento atualizado!");
        }

        public void ExcluirPagamento(LocadoraBD context)
        {
            Console.Write("ID: ");
            int id = int.Parse(Console.ReadLine());
            var p = context.Pagamentos.Find(id);
            if (p == null) { Console.WriteLine("Pagamento não encontrado."); return; }
            context.Pagamentos.Remove(p);
            context.SaveChanges();
            Console.WriteLine("Pagamento removido!");
        }



    }
}
