using Microsoft.EntityFrameworkCore;
using TrabalhoPratico.Models;

namespace TrabalhoPratico.CRUDS
{
    public class CRUDVeiculo
    {
        public void CriarVeiculo(LocadoraBD context)
        {
            var v = new Veiculo();
            Console.Write("Modelo: "); v.Modelo = Console.ReadLine();
            Console.Write("Ano: "); v.AnoFabricacao = int.Parse(Console.ReadLine());
            Console.Write("Km: "); v.Quilometragem = double.Parse(Console.ReadLine());
            Console.Write("Placa: "); v.Placa = Console.ReadLine();
            Console.Write("Cor: "); v.Cor = Console.ReadLine();
            Console.Write("ID Fabricante: "); v.IdFabricante = int.Parse(Console.ReadLine());

            context.Veiculos.Add(v);
            context.SaveChanges();
            Console.WriteLine("Veículo cadastrado!");
        }

        public void ListarVeiculos(LocadoraBD context)
        {
            var lista = context.Veiculos.Include(v => v.Fabricante).ToList();
            foreach (var v in lista)
                Console.WriteLine($"ID:{v.IdVeiculo} | {v.Modelo} ({v.Placa}) - {v.Fabricante.Nome}");
        }

        public void AtualizarVeiculo(LocadoraBD context)
        {
            Console.Write("ID: ");
            int id = int.Parse(Console.ReadLine());
            var v = context.Veiculos.Find(id);
            if (v == null) { Console.WriteLine("Veículo não encontrado."); return; }

            Console.Write("Modelo: "); var m = Console.ReadLine(); if (!string.IsNullOrWhiteSpace(m)) v.Modelo = m;
            Console.Write("Ano: "); var ano = Console.ReadLine(); if (!string.IsNullOrWhiteSpace(ano)) v.AnoFabricacao = int.Parse(ano);
            Console.Write("Km: "); var km = Console.ReadLine(); if (!string.IsNullOrWhiteSpace(km)) v.Quilometragem = double.Parse(km);
            Console.Write("Placa: "); var p = Console.ReadLine(); if (!string.IsNullOrWhiteSpace(p)) v.Placa = p;
            Console.Write("Cor: "); var c = Console.ReadLine(); if (!string.IsNullOrWhiteSpace(c)) v.Cor = c;
            Console.Write("Fabricante: "); var f = Console.ReadLine(); if (!string.IsNullOrWhiteSpace(f)) v.IdFabricante = int.Parse(f);

            context.SaveChanges();
            Console.WriteLine("Veículo atualizado!");
        }

        public void ExcluirVeiculo(LocadoraBD context)
        {
            Console.Write("ID: ");
            int id = int.Parse(Console.ReadLine());
            var v = context.Veiculos.Find(id);
            if (v == null) { Console.WriteLine("Veículo não encontrado."); return; }
            context.Veiculos.Remove(v);
            context.SaveChanges();
            Console.WriteLine("Veículo removido!");
        }
    }
}
