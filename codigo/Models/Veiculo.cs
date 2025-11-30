using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TrabalhoPratico.Models;

namespace TrabalhoPratico.Models
{
    public class Veiculo
    {
        [Key]
        public int IdVeiculo { get; set; }

        public string Modelo { get; set; }
        public int AnoFabricacao { get; set; }
        public double Quilometragem { get; set; }

        [Required]
        public int IdFabricante { get; set; }

        [ForeignKey("IdFabricante")]
        public Fabricante Fabricante { get; set; }

        public string Placa { get; set; }
        public string Cor { get; set; }
    }
}
