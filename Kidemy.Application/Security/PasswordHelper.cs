using Kidemy.Domain.Shared;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Kidemy.Application.Security
{
	public static class PasswordHelper
	{
		public static string EncodePasswordSHA256(this string pass)
		{
			Byte[] originalBytes;
			Byte[] encodedBytes;

			var provider = new SHA256CryptoServiceProvider();

			originalBytes = ASCIIEncoding.Default.GetBytes(pass);
			encodedBytes = provider.ComputeHash(originalBytes);

			//Convert encoded bytes back to a 'readable' string   
			return Convert.ToBase64String(encodedBytes);
		}

		public static Result PasswordIsValid(this string Password, int minLength = 8, bool requiredUpperCase = true, bool requiredLowerCase = true)
		{
			if (string.IsNullOrWhiteSpace(Password))
			{
				return Result.Failure(ErrorMessages.PasswordRequiredError);
			}

			if (Password.Length < minLength)
			{
				return Result.Failure(ErrorMessages.PasswordMinLengthError);
			}

			if (requiredUpperCase && !Regex.Match(Password, "^(?=.*[A-Z]).+$").Success)
			{
				return Result.Failure(ErrorMessages.PasswordRequiredUpperCaseError);
			}

			if (requiredLowerCase && !Regex.Match(Password, "^(?=.*[a-z]).+$").Success)
			{
				return Result.Failure(ErrorMessages.PasswordRequiredLowerCaseError);
			}

			return Result.Success();
		}

	}
}
