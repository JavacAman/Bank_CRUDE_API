//using IB_Crude_Web_API.Models;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Configuration;
//using System;
//using System.Linq;

//namespace IB_Crude_Web_API
//{
//    public class EmailService
//    {

//        private readonly IConfiguration _configuration;

//        public EmailService(IConfiguration configuration)
//        {
//            _configuration = configuration;
//        }

//        public void SendEmail(string toEmail, string createdBy, string noteNumber, string secretaryEmail = null)
//        {
//            var objMail = _context.EmailTemplates.FirstOrDefault(t => t.MailFor == "ATRCreator Review");

//            if (objMail != null)
//            {
//                SMTPMail objsmtp = new SMTPMail();
//                objsmtp.ToEmail = toEmail;
//                if (secretaryEmail != null)
//                {
//                    objsmtp.CCEmail = secretaryEmail + ";" + createdBy;
//                }
//                else
//                {
//                    objsmtp.CCEmail = createdBy;
//                }
//                string modifiedSubject = objMail.Subject.Replace("XXXX", noteNumber);
//                objsmtp.Subject = modifiedSubject;
//                int linkIndex = objMail.Body.IndexOf(_configuration["SMTPConfig:EnoteView"]);
//                string modifiedLink = _configuration["SMTPConfig:EnoteView"] + e_AtrCreator.noteId.ToString();
//                string IntraNetLink = _configuration["SMTPConfig:EnoteViewIntraNet"] + e_AtrCreator.noteId.ToString();
//                string modifiedBody = objMail.Body
//                     .Replace("CCCC", toEmail)
//                     .Replace("DDDD", createdBy);

//                // Code to send email using SMTPMail (implementation assumed to be elsewhere)
//            }
//        }
//    }
//}

