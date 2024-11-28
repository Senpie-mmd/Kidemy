using Kidemy.Domain.Enums.BankAccount;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kidemy.Application.ViewModels.BankAccountCard
{
    public class ChangeStatusBankAccountCardViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Description")]
        public string? Description { get; set; }

        [Display(Name = "BankAccountCardStatus")]
        public BankAccountCardStatus Status { get; set; }
    }
}
