using TrabalhoPratico.Models;

namespace TrabalhoPratico.CRUDS
{
    public class CRUDFabricante
    {
        public void CriarFabricante(LocadoraBD context)
        {
            var f = new Fabricante();
            Console.Write("Nome: "); f.Nome = Console.ReadLine();
            Console.Write("País: "); f.Pais = Console.ReadLine();
            context.Fabricantes.Add(f);
            context.SaveChanges();
            Console.WriteLine("Fabricante cadastrado!");
        }

        public void ListarFabricantes(LocadoraBD context)
        {
            foreach (var f in context.Fabricantes.ToList())
                Console.WriteLine($"ID:{f.IdFabricante} | {f.Nome} ({f.Pais})");
        }

        public void AtualizarFabricante(LocadoraBD context)
        {
            Console.Write("ID: ");
            int id = int.Parse(Console.ReadLine());
            var f = context.Fabricantes.Find(id);
            if (f == null) { Console.WriteLine("Fabricante não encontrado."); return; }

            Console.Write("Novo nome: "); var nome = Console.ReadLine(); if (!string.IsNullOrWhiteSpace(nome)) f.Nome = nome;
            Console.Write("Novo país: "); var pais = Console.ReadLine(); if (!string.IsNullOrWhiteSpace(pais)) f.Pais = pais;

            context.SaveChanges();
            Console.WriteLine(" Fabricante atualizado!");
        }

        public void ExcluirFabricante(LocadoraBD context)
        {
            Console.Write("ID: ");
            int id = int.Parse(Console.ReadLine());
            var f = context.Fabricantes.Find(id);
            if (f == null) { Console.WriteLine("Fabricante não encontrado."); return; }
            context.Fabricantes.Remove(f);
            context.SaveChanges();
            Console.WriteLine(" Fabricante removido!");
        }


    }
}
