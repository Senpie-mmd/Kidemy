namespace Kidemy.Application.Statics
{
	public static class CacheKeys
	{
		#region User

		public const string UserPrevCacheKey = "User.";
		public const string UserEmailByUserIdCacheKey = UserPrevCacheKey + "Email.ByUserId.{0}";
		public const string UserNameByUserIdCacheKey = UserPrevCacheKey + "Name.ByUserId.{0}";
        public const string UserMobileByUserIdCacheKey = UserPrevCacheKey + "Mobile.ByUserId.{0}";
		public const string AvatarNameByUserIdCacheKey = UserPrevCacheKey + "AvatarName.ByUserId.{0}";

		#endregion
	}
}
