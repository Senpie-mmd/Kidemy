using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.ViewModels.Email;
using Kidemy.Domain.Interfaces;
using Kidemy.Domain.Shared;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Kidemy.Application.Services.Implementations;

public class EmailService : IEmailService
{

    private readonly IEmailSettingService _emailSettingService;
    private readonly IConfiguration _configuration;

    public EmailService(IEmailSettingService emailSettingService, IConfiguration configuration)
    {
        _emailSettingService = emailSettingService;
        _configuration = configuration;
    }

    public async Task<bool> SendEmailAsync(string to, string subject, string body)
    {
        var emailSetting = new EmailViewModel
        {
            From = _configuration["EmailSetting:From"],
            Password = _configuration["EmailSetting:Password"],
            DisplayName = _configuration["EmailSetting:DisplayName"],
            Smtp = _configuration["EmailSetting:Smtp"],
            Port = int.Parse(_configuration["EmailSetting:Port"]),
            EnableSsL = bool.Parse(_configuration["EmailSetting:EnableSsL"])
        };


        var fromEmail = emailSetting.From;
        string password = emailSetting.Password;

        try
        {
            var mail = new MailMessage();

            var smtpServer = new SmtpClient(emailSetting.Smtp);

            mail.From = new MailAddress(fromEmail, emailSetting.DisplayName);

            mail.To.Add(to);

            mail.Subject = subject;

            mail.Body = body;

            mail.IsBodyHtml = true;

            smtpServer.Port = Convert.ToInt32(emailSetting.Port); ;

            smtpServer.Credentials = new NetworkCredential(fromEmail, password);

            smtpServer.EnableSsl = Convert.ToBoolean(emailSetting.EnableSsL);

            smtpServer.Send(mail);
            return true;

        }
        catch (Exception e)
        {
            return false;
        }
    }
    public async Task<bool> SendEmailAsync(List<string> to, string subject, string body)
    {
        var emailSetting = new EmailViewModel
        {
            From = _configuration["EmailSetting:From"],
            Password = _configuration["EmailSetting:Password"],
            DisplayName = _configuration["EmailSetting:DisplayName"],
            Smtp = _configuration["EmailSetting:Smtp"],
            Port = int.Parse(_configuration["EmailSetting:Port"]),
            EnableSsL = bool.Parse(_configuration["EmailSetting:EnableSsL"])
        };

        if (emailSetting != null)
        {

            var fromEmail = emailSetting.From;
            string password = emailSetting.Password;

            try
            {
                var mail = new MailMessage();

                var smtpServer = new SmtpClient(emailSetting.Smtp);

                mail.From = new MailAddress(fromEmail, emailSetting.DisplayName);

                if (to != null && to.Any())
                {
                    foreach (var item in to)
                    {
                        mail.To.Add(item);
                    }

                }

                mail.Subject = subject;

                mail.Body = body;

                mail.IsBodyHtml = true;

                smtpServer.Port = Convert.ToInt32(emailSetting.Port); ;

                smtpServer.Credentials = new NetworkCredential(fromEmail, password);

                smtpServer.EnableSsl = Convert.ToBoolean(emailSetting.EnableSsL);

                smtpServer.Send(mail);
                return true;

            }
            catch (Exception e)
            {
                return false;
            }
        }

        return false;
    }

}
