using CRMContracts.Email;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmailService.Configuration
{
    public class EmailConfiguration : IEmailConfiguration
    {
        private readonly IConfiguration _configuration;

        public EmailConfiguration(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Mail => _configuration.GetSection("MailSettings").GetSection("Mail").Value;
        public string DisplayName => _configuration.GetSection("MailSettings").GetSection("DisplayName").Value;
        public string Password => _configuration.GetSection("MailSettings").GetSection("Password").Value;
        public string Host => _configuration.GetSection("MailSettings").GetSection("Host").Value;
        public string Port => _configuration.GetSection("MailSettings").GetSection("Port").Value;
    }
}
