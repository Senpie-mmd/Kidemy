using Kidemy.Application.Tools;
using System.ComponentModel.DataAnnotations;

namespace Kidemy.Application.Convertors
{
    public static class Calulate
    {
        public static string GetDiffreneceBetweenDateTimes(this DateTime createdDate)
        {

            var now = DateTime.Now;

            TimeSpan timeDifference = now - createdDate;

            int totalDays = (int)timeDifference.TotalDays;
            int totalHours = (int)timeDifference.TotalHours;
            int totalMinutes = (int)timeDifference.TotalMinutes;
            int totalYears = (int)totalDays / 365;

            if (totalYears == 0)
            {
                if (totalDays == 0)
                {
                    if (totalHours == 0)
                    {
                        if (totalMinutes == 0)
                        {
                            return "1 " + TimeDefference.MinsAgo.GetEnumName();
                        }

                        return totalMinutes + " " + TimeDefference.MinsAgo.GetEnumName();
                    }

                    return totalHours + " " + TimeDefference.HoursAgo.GetEnumName();
                }

                return totalDays + " " + TimeDefference.DaysAgo.GetEnumName();
            }

            return totalYears + " " + TimeDefference.YearsAgo.GetEnumName();
        }

        public static double GetAvrage(this List<int> Scores)
        {
            if (!Scores.Any()) return 0;

            double average = Scores.Average();

            return Math.Round(average, 1);
        }

        #region Utilities 
        public enum TimeDefference
        {
            [Display(Name = "DaysAgo")]
            DaysAgo,
            [Display(Name = "HoursAgo")]
            HoursAgo,
            [Display(Name = "MinsAgo")]
            MinsAgo,
            [Display(Name = "YearsAgo")]
            YearsAgo
        }
        #endregion
    }
}
