namespace FinanceManagerAPI.Models
{
    public enum TransactionType { Income, Expense, Transfer }

    public class Transaction
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public TransactionType Type { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public int AccountId { get; set; }
        public Account Account { get; set; }
    }
}
