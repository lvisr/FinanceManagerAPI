using System.ComponentModel.DataAnnotations;

namespace FinanceManagerAPI.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string Username { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        public List<Account> Accounts { get; set; } = new();
    }
}
