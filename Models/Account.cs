using System.Text.Json.Serialization; // for [JsonIgnore]
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation; // for [ValidateNever]

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

        [JsonIgnore] // Prevent User from being required in request body and failing Validation when http post Account
        [ValidateNever] // Prevents model validation from requiring this field
        public User User { get; set; }
        public List<Transaction> Transactions { get; set; } = new();
    }
}
