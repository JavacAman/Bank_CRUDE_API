
using IB_Crude_Web_API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;

namespace IB_Crude_Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SendEmailController : ControllerBase
    {
        private static IConfiguration _config;
        //private readonly SmtpSettings _smtpSettings;

        public SendEmailController(IConfiguration configuration)
        {
            _config = configuration;
        }

        public static async void SendEmailAsync(IConfiguration _configuration, SMTPMail smtpmail)
        {
            try
            {
                _config = _configuration;


                string fromEmailAddress = _config["SMTPConfig:fromEmailAddress"];
                var from = new MailAddress(fromEmailAddress, "ValidateSMTPMailAccount");

                string toEmail = smtpmail.ToEmail;
                string CCEmail = smtpmail.CCEmail;
                var Subject = smtpmail.Subject;
                var textBody = smtpmail.TextBody;

                MailMessage mailMessage = new MailMessage
                {
                    From = new MailAddress(fromEmailAddress),
                    Subject = Subject,
                    Body = textBody,
                    IsBodyHtml = true
                };

                if (toEmail != null && toEmail != "")
                {
                    foreach (string tEmail in toEmail.Split(';'))
                    {
                        mailMessage.To.Add(new MailAddress(tEmail.Trim()));
                    }
                }

                if (CCEmail != null && CCEmail != "")
                {
                    foreach (string CEmail in CCEmail.Split(';'))
                    {
                        mailMessage.CC.Add(new MailAddress(CEmail));
                    }
                }

                if (!string.IsNullOrEmpty(smtpmail.AttachmentPath))
                {
                    mailMessage.Attachments.Add(new System.Net.Mail.Attachment(smtpmail.AttachmentPath));
                }
                var smtp = new SmtpClient
                {
                    Host = _config["SMTPConfig:Host"],
                    Port = Convert.ToInt32(_config["SMTPConfig:Port"]),
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(_config["SMTPConfig:NetworkUserId"], _config["SMTPConfig:NetworkPWD"]), ///from my mail id need to remove multiple authenticor from IT Team
                };

                await smtp.SendMailAsync(mailMessage);
                Console.Write("Email sent Successfully \t\n");

                //return Ok("Email sent successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception - {ex.Message}");
                //return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while sending the email.");
            }
        }
    }
}