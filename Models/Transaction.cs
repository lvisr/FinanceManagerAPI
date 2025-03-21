using System.Text.Json.Serialization; // for [JsonIgnore]
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation; // for [ValidateNever]

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

        [JsonIgnore] // Prevent Account from being required in request body and failing Validation when http post Transaction
        [ValidateNever] // Prevents model validation from requiring this field
        public Account Account { get; set; }
    }
}