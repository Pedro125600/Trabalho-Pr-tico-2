using System.ComponentModel.DataAnnotations;

namespace TrabalhoPratico.Models
{
    public class Fabricante
    {
        [Key]
        public int IdFabricante { get; set; }
        public string Nome { get; set; }
        public string Pais { get; set; }
    }
}
