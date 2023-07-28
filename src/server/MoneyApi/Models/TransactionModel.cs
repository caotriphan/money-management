using System.ComponentModel.DataAnnotations;

namespace MoneyApi.Models;

public record TransactionModel
{
    public int Id { get; set; }
    public string Note { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime TransactionDate { get; set; }
    public decimal Amount { get; set; }
    public DateTime? DeletedAt { get; set; }

    public record NewTransaction
    {
        [Required, MaxLength(255)]
        public string Note { get; set; } = null!;

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public DateTime TransactionDate { get; set; }
    }
}

