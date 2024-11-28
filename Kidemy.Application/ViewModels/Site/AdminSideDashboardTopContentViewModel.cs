using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kidemy.Application.ViewModels.Site
{
    public class AdminSideDashboardTopContentViewModel
    {
        public int TicketCount { get; set; }

        public int UserCount { get; set; }

        public int ContactUsFormCount { get; set; }

        public int TodayUsersCount { get; set; }
          
        public int NewWithdrawRequestCount { get; set; }

        public int NewCourseRequestCount { get; set; }

        public int InProgressMasterRequestCount { get; set; }


    }
}
