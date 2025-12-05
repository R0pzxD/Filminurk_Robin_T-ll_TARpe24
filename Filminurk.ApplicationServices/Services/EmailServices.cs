using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Filminurk.ApplicationServices.Services
{
    internal class EmailServices : IEmailServices
    {
        private readonly IConfiguration configuration;

        public EmailServices(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void SendEmail(EmailDTO dto)
        {
            var email = new MineMessage();
            _configuration.GetSection("EmailUserName").Value = Environment.gmailusername;
            _configuration.GetSection("EmailHost").Value = Environment.smtpaddress;
            _configuration.GetSection("EmailPassword").Value = Environment.gmailappassword;

            email.From.Add(MailboxAddress.Parse(_configuration.GetSection("EmailUserName").Value));
            email.To.Add(MailboxAddress)

        }
    }
}
