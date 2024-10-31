using JobScial.BAL.Models;
using JobScial.BAL.Repositorys.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using MimeKit;
using System.Text;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using static System.Net.Mime.MediaTypeNames;

namespace JobScial.BAL.Repositorys.Implementations
{
    public class EmailRepository : IEmailRepository
    {   
        public readonly EmailConfiguration _emailConfig;
        public EmailRepository(EmailConfiguration emailConfig) => _emailConfig = emailConfig;
        
        
        public void SendEmail(Message message)
        {
            var emailMessage = CreateEmailMessage(message);
            Send(emailMessage);
        }

        private MimeMessage CreateEmailMessage(Message message)
        {
            var emailMessage = new MimeMessage();

            // Kiểm tra và thêm địa chỉ email người gửi
            if (!string.IsNullOrEmpty(_emailConfig.From))
            {
                emailMessage.From.Add(new MailboxAddress("email", _emailConfig.From));
            }
            else
            {
                throw new ArgumentNullException(nameof(_emailConfig.From), "Địa chỉ email người gửi không được để trống.");
            }

            // Kiểm tra và thêm địa chỉ email người nhận
            if (message.To != null)
            {
                emailMessage.To.AddRange(message.To);
            }
            else
            {
                throw new ArgumentNullException(nameof(message.To), "Không có địa chỉ email người nhận được cung cấp.");
            }

            // Thêm chủ đề và nội dung email
            emailMessage.Subject = message.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = message.Content };

            return emailMessage;
        }
        /*var emailMessage = new MimeMessage();
        emailMessage.From.Add(new MailboxAddress("email", _emailConfig.From));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = message.Content };

            return emailMessage;*/

        private void Send(MimeMessage mailMessage)
        {
            using var client = new SmtpClient();
            try
            {
                client.Connect(_emailConfig.SmtpServer, _emailConfig.Port, true);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                client.Authenticate(_emailConfig.UserName, _emailConfig.Password);

                client.Send(mailMessage);
            }
            catch
            {
                //log an error message or throw an exception or both.
                throw;
            }
            finally
            {
                client.Disconnect(true);
                client.Dispose();
            }
        }
    }
}
