using Barnamenevisan.Localizing.Shared;
using Kidemy.Domain.Enums.BankAccount;
using System.ComponentModel.DataAnnotations;

namespace Kidemy.Domain.Models.BankAccountCard
{
    public class BankAccountCard : AuditBaseEntity<int>
    {
        public int UserId { get; set; }

        public string BankName { get; set; }

        public string OwnerName { get; set; }


        public string CardNumber { get; set; }

        
        public string ShabaNumber { get; set; }

        
        public string AccountNumber { get; set; }

        public BankAccountCardStatus Status { get; set; }

        public string? Description { get; set; }

        public bool IsDefault { get; set; }

        public bool IsDelete { get; set; }
    }
}
