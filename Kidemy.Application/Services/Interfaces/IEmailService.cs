public interface IEmailService
{
    public Task<bool> SendEmailAsync(string to, string subject, string body);
    public Task<bool> SendEmailAsync(List<string> to, string subject, string body);
}
