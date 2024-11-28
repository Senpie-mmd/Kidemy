using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kidemy.Application.ViewModels.WithrawRequest
{
    public class RejectWithdrawRequestViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Description")]
        public string? Description { get; set; }
    }
}
