﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebBacklink.Areas.Admin.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Enter Your UserName")]
        public string UserName { set; get; }
        [Required(ErrorMessage = "Enter Your Password")]
        public string Password { set; get; }
        public bool RememberMe { set; get; }
    }
}