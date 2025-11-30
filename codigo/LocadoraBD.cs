using Microsoft.EntityFrameworkCore;
using TrabalhoPratico.Models; 

namespace TrabalhoPratico
{
    public class LocadoraBD : DbContext
    {
        public LocadoraBD(DbContextOptions<LocadoraBD> options) : base(options) { }

        public DbSet<Fabricante> Fabricantes { get; set; }
        public DbSet<Veiculo> Veiculos { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Aluguel> Alugueis { get; set; }
        public DbSet<Pagamento> Pagamentos { get; set; }
    }
}
