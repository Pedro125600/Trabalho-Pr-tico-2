using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class Pagamento
{
    [Key]
    public int IdPagamento { get; set; }

    [Required]
    public int IdAluguel { get; set; }

    [ForeignKey("IdAluguel")]
    public Aluguel? Aluguel { get; set; }  // CORRIGIDO — Torna opcional no JSON

    public DateTime DataPagamento { get; set; }
    public decimal ValorPago { get; set; }
    public string FormaPagamento { get; set; }
}
