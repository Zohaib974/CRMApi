﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CRMServices.DataTransferObjects
{
     public class UserForRegistrationDto
    {
        public string FullName { get; set; }
        [Required(ErrorMessage = "Username is required")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public ICollection<string> Roles { get; set; }
    }

}
