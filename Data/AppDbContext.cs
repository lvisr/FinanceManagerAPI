using Microsoft.EntityFrameworkCore;
using FinanceManagerAPI.Models;
using Microsoft.AspNetCore.Identity;

namespace FinanceManagerAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

        public DbSet<User> Users { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
/*             // One-to-Many: User -> Accounts
            modelBuilder.Entity<User>()
                .HasMany(u => u.Accounts)
                .WithOne(a => a.User)
                .HasForeignKey(a => a.UserId);

            // One-to-Many: Account -> Transactions
            modelBuilder.Entity<Account>()
                .HasMany(a => a.Transactions)
                .WithOne(t => t.Account)
                .HasForeignKey(t => t.AccountId); */

            // ✅ Define Foreign Key for Account (UserId)
            modelBuilder.Entity<Account>()
                .HasOne<User>()              // No navigation property
                .WithMany()
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // ✅ Define Foreign Key for Transaction (AccountId)
            modelBuilder.Entity<Transaction>()
                .HasOne<Account>()           // No navigation property
                .WithMany()
                .HasForeignKey(t => t.AccountId)
                .OnDelete(DeleteBehavior.Cascade);

/*             modelBuilder.Entity<Transaction>()
                .HasOne<User>()           // No navigation property
                .WithMany()
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Cascade);   */  


            // ✅ Hash passwords
            var passwordHasher = new PasswordHasher<User>();

            // ✅ Seed Users
            var user1 = new User
            {
                Id = 1,
                Username = "John Doe",
                Email = "john@example.com",
                PasswordHash = "Password123"// passwordHasher.HashPassword(null, "Password123")
            };

            var user2 = new User
            {
                Id = 2,
                Username = "Jane Smith",
                Email = "jane@example.com",
                PasswordHash = "SecurePass456" //passwordHasher.HashPassword(null, "SecurePass456")
            };

            modelBuilder.Entity<User>().HasData(user1, user2);

            // ✅ Seed Accounts  //  AccountType { Checking, Savings, Debt, Cash }
            var account1 = new Account { Id = 1, Name = "CheckingAccount", Type = AccountType.Checking, Balance = 1500.00m, UserId = 1 };
            var account2 = new Account { Id = 2, Name = "SavingsAccount", Type = AccountType.Savings, Balance = 5000.00m, UserId = 1 };
            var account3 = new Account { Id = 3, Name = "Cash", Type = AccountType.Cash, Balance = 200.00m, UserId = 2 };

            modelBuilder.Entity<Account>().HasData(account1, account2, account3);

            // ✅ Seed Transactions  // TransactionType { Income, Expense, Transfer }
            // DateTime meetingAppt = new DateTime(2018, 4, 4, 16, 0, 0);
            modelBuilder.Entity<Transaction>().HasData(
                new Transaction { Id = 1, AccountId = 1, UserId = 1, Amount = -100.00m, Type = TransactionType.Expense, Description = "Groceries", Date = new DateTime(2025, 5, 5, 16, 0, 0) },
                new Transaction { Id = 2, AccountId = 2, UserId = 1, Amount = 500.00m, Type = TransactionType.Income, Description = "Salary", Date = new DateTime(2025, 4, 4, 9, 0, 0) },
                new Transaction { Id = 3, AccountId = 3, UserId = 2, Amount = 500.00m, Type = TransactionType.Income, Description = "CashGift", Date = new DateTime(2025, 3, 3, 10, 0, 0) }
            );
        }
    }
}
