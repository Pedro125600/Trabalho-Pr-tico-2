using TrabalhoPratico.Models;

namespace TrabalhoPratico.CRUDS
{
    public class CRUDCliente
    {

        public void CriarCliente(LocadoraBD context)
        {
            var cliente = new Cliente();
            Console.Write("Nome: "); cliente.Nome = Console.ReadLine();
            Console.Write("CPF: "); cliente.CPF = Console.ReadLine();
            Console.Write("Email: "); cliente.Email = Console.ReadLine();
            Console.Write("Telefone: "); cliente.Telefone = Console.ReadLine();

            context.Clientes.Add(cliente);
            context.SaveChanges();
            Console.WriteLine("Cliente cadastrado!");
        }

        public void ListarClientes(LocadoraBD context)
        {
            var clientes = context.Clientes.ToList();
            if (!clientes.Any()) { Console.WriteLine("Nenhum cliente encontrado."); return; }
            foreach (var c in clientes)
                Console.WriteLine($"ID:{c.IdCliente} | Nome:{c.Nome} | CPF:{c.CPF}");
        }

        public void BuscarCliente(LocadoraBD context)
        {
            Console.Write("Digite o ID: ");
            int id = int.Parse(Console.ReadLine());
            var cliente = context.Clientes.Find(id);
            if (cliente == null) { Console.WriteLine("Cliente não encontrado."); return; }
            Console.WriteLine($"ID:{cliente.IdCliente}\nNome:{cliente.Nome}\nEmail:{cliente.Email}\nTelefone:{cliente.Telefone}");
        }

        public void AtualizarCliente(LocadoraBD context)
        {
            Console.Write("ID do cliente: ");
            int id = int.Parse(Console.ReadLine());
            var cliente = context.Clientes.Find(id);
            if (cliente == null) { Console.WriteLine("Cliente não encontrado."); return; }

            Console.Write("Novo nome (Enter mantém): ");
            var nome = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(nome)) cliente.Nome = nome;

            Console.Write("Novo CPF (Enter mantém): ");
            var cpf = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(cpf)) cliente.CPF = cpf;

            Console.Write("Novo Email (Enter mantém): ");
            var email = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(email)) cliente.Email = email;

            Console.Write("Novo Telefone (Enter mantém): ");
            var telefone = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(telefone)) cliente.Telefone = telefone;

            context.SaveChanges();
            Console.WriteLine("Cliente atualizado!");
        }


        public void ExcluirCliente(LocadoraBD context)
        {
            Console.Write("ID do cliente: ");
            int id = int.Parse(Console.ReadLine());
            var cliente = context.Clientes.Find(id);
            if (cliente == null) { Console.WriteLine("Cliente não encontrado."); return; }
            context.Clientes.Remove(cliente);
            context.SaveChanges();
            Console.WriteLine("Cliente removido!");
        }


    }
}
