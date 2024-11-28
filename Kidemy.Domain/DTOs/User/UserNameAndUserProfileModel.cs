namespace Kidemy.Domain.DTOs.User
{
    public class UserNameAndUserProfileModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserAvatar{ get; set; }
        public string? Mobile { get; set; }
    }
}
