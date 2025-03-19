using System.Net.Mail;
using System.Net;

namespace WebSiteBanMoHinh.Areas.Admin.Repository
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true, //bật bảo mật
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("sinhngay21042004@gmail.com", "cysjkighicqmtuic")
            };

            return client.SendMailAsync(
                new MailMessage(from: "sinhngay21042004@gmail.com",
                                to: email,
                                subject,
                                message
                                ));
        } 
    }
}
