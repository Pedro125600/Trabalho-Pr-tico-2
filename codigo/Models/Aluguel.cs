using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using TrabalhoPratico.Models;

public class Aluguel
{
    [Key]
    public int IdAluguel { get; set; }

    [Required]
    public int IdCliente { get; set; }

    [ForeignKey("IdCliente")]
    public Cliente? Cliente { get; set; }   // AGORA PODE SER NULO

    [Required]
    public int IdVeiculo { get; set; }

    [ForeignKey("IdVeiculo")]
    public Veiculo? Veiculo { get; set; }   // AGORA PODE SER NULO

    public DateTime DataRetirada { get; set; }
    public DateTime DataDevolucao { get; set; }
    public double QuilometragemInicial { get; set; }
    public double? QuilometragemFinal { get; set; }
    public decimal ValorDiaria { get; set; }
    public decimal? ValorTotal { get; set; }
}
