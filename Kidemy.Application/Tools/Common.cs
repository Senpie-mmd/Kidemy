using Microsoft.Extensions.Localization;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Kidemy.Application.Tools
{
    public static class CommonTools
    {
        #region Fields

        private static IStringLocalizer _localizer;

        #endregion

        #region Constructor

        public static void SetLocalizer(IStringLocalizer localizer)
        {
            _localizer = localizer;
        }

        #endregion

        public static string GetEnglishNumbers(this string s)
            => s.Replace("۰", "0").Replace("۱", "1").Replace("۲", "2").Replace("۳", "3").Replace("۴", "4")
                .Replace("۵", "5").Replace("۶", "6").Replace("۷", "7").Replace("۸", "8").Replace("۹", "9");

        public static string GetPersianNumbers(this string s)
            => s.Replace("0", "۰").Replace("1", "۱").Replace("2", "۲").Replace("3", "۳").Replace("4", "۴")
                .Replace("5", "۵").Replace("6", "۶").Replace("7", "۷").Replace("8", "۸").Replace("9", "۹");

        public static string GetEnumName(this Enum myEnum)
        {
            var enumDisplayName = myEnum.GetType()
                .GetMember(myEnum.ToString())
                .FirstOrDefault();

            if (enumDisplayName != null)
                return _localizer[enumDisplayName.GetCustomAttribute<DisplayAttribute>().GetName()];

            return "";
        }

        public static string ToPersianString(this int value)
        {
            switch (value)
            {
                case 1:
                    return "یک";
                case 2:
                    return "دو";
                case 3:
                    return "سه";
                case 4:
                    return "چهار";
                case 5:
                    return "پنج";
                case 6:
                    return "شش";
                case 7:
                    return "هفت";
                case 8:
                    return "هشت";
                case 9:
                    return "نه";
                case 10:
                    return "ده";
            }

            return "صفر";
        }

        public static decimal FixPrice(this string price)
        {
            if (string.IsNullOrEmpty(price))
                return 0;

            if (int.TryParse(price.Replace(",", ""), out int outPrice))
                return outPrice;
            else
                return 0;
        }

        public static bool IsNull(this object obj)
            => obj == null;

        public static string FixPhoneNumberFormat(this string phoneNumber)
        {
            if (phoneNumber.IsNull())
                return phoneNumber;

            if (!phoneNumber.StartsWith("98")) return phoneNumber;

            var index = phoneNumber.IndexOf("98");

            if (index == 0)
                phoneNumber = "0" + phoneNumber[2..];

            return phoneNumber;
        }

        public static string ConvertToEnglishNumber(this string persianStr)
        {
            Dictionary<char, char> LettersDictionary = new Dictionary<char, char>
            {
                ['۰'] = '0',
                ['۱'] = '1',
                ['۲'] = '2',
                ['۳'] = '3',
                ['۴'] = '4',
                ['۵'] = '5',
                ['۶'] = '6',
                ['۷'] = '7',
                ['۸'] = '8',
                ['۹'] = '9'
            };
            if (persianStr == null) return persianStr;
            foreach (var item in persianStr.Where(item => !item.IsDigitEnglish()).Where(item => item != '.'))
            {
                try
                {
                    persianStr = persianStr.Replace(item, LettersDictionary[item]);
                }
                catch (Exception)
                {
                    continue;
                }
            }

            return persianStr;
        }

        public static bool IsDigitEnglish(this char str)
        {
            List<char> englishDigits = new List<char>()
            {
                '1', '2', '3', '4', '5', '6', '7', '8', '9', '0'
            };
            return englishDigits.Contains(str);
        }

        public static bool IsMobile(this string mobile)
        {
            return Regex.IsMatch(mobile, @"^(98|0)?(۹۸|۰)?(9|۹)[۰-۹0-9]{9}?$");
        }

        public static bool IsEmail(this string email)
        {
            return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }

        public static string GetPersianName(this bool value)
            => value ? "بله" : "خیر";

        public static string FixEmail(this string email)
            => email.Trim().ToLower();

        public static string GenerateUniqueStringCode()
        => Guid.NewGuid().ToString().Replace("-", "");

        public static int GenerateRandomIntegerCode()
            => new Random().Next(100000, 999999);

        public static string GenerateRandomNumericCode(int length)
        {
            Random random = new Random();

            const string chars = "0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static string GenerateRandomString(int length)
        {
            Random random = new Random();

            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static string RemoveFristZerosTheRefId(this string refId)
        {
            if (string.IsNullOrEmpty(refId))
                return "0";
            else
                return Convert.ToInt32(refId).ToString();

        }

        public static string GetCourseDetailsFilesPath(this string unFormatedAddress, int courseId)
        {
            return string.Format(unFormatedAddress, courseId);
        }
    }
}
