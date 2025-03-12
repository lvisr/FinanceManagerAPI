namespace FinanceManagerAPI.Models
{
    public enum AccountType { Checking, Savings, Debt, Cash }

    public class Account
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public AccountType Type { get; set; }
        public decimal Balance { get; set; } = 0;
        public int UserId { get; set; }
        public User User { get; set; }
        public List<Transaction> Transactions { get; set; } = new();
    }
}
