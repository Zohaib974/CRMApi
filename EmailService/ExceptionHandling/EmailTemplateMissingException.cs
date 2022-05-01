using System;
using System.Collections.Generic;
using System.Text;

namespace EmailService.ExceptionHandling
{
    public class EmailTemplateMissingException : Exception
    {
        public EmailTemplateMissingException(string path)
            : base("Email template is missing at path: " + path)
        {

        }
    }
}
