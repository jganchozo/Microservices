using SendGrid;
using SendGrid.Helpers.Mail;
using ServiceShop.Messenger.Email.SendGridLibrary.Interface;
using ServiceShop.Messenger.Email.SendGridLibrary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceShop.Messenger.Email.SendGridLibrary.Implement
{
    public class SendGridEmail : IEmail
    {
        public async Task<ResponseModel> Send(SendGridData data)
        {
            try
            {
                var sendGridClient = new SendGridClient(data.SendGridAPIKey);
                var to = new EmailAddress(data.To, data.Name);
                var subject = data.Subject;
                var sender = new EmailAddress("jganchozo@outlook.com", "Jose Manuel");
                var body = data.Body;
                var message = MailHelper.CreateSingleEmail(sender, to, subject, body, body);

                var result = await sendGridClient.SendEmailAsync(message);

                return new ResponseModel(true, null);
            }
            catch (Exception ex)
            {
                return new ResponseModel(false, ex.Message);
            }
        }
    }
}
