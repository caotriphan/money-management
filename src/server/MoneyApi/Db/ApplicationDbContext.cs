using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;

namespace MoneyApi.Db;

public class Transaction
{
    public int Id { get; set; }
    [Required, MaxLength(255)]
    public string Note { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime TransactionDate { get; set; }
    [Required, Precision(18,2)]
    public decimal Amount { get; set; }
    public DateTime? DeletedAt { get; set; }

    public int UserId { get; set; }
    public User User { get; set; } = null!;
}

public class User
{
    public int Id { get; set; }

    [Required, MaxLength(255)]
    public string Username { get; set; } = null!;

    [Required, MaxLength(255)]
    public string Password { get; set; } = null!;

    [Required, MaxLength(255)]
    public string FullName { get; set; } = null!;

    public List<Transaction> Transactions { get; set; } = new();
}

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasMany(x => x.Transactions)
            .WithOne(t => t.User)
            .HasForeignKey(t => t.UserId);
    }
}

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Transaction> Transactions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new UserConfiguration());
    }
}
