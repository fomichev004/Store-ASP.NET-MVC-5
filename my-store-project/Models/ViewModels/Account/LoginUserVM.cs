using my_store_project.Models.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace my_store_project.Models.ViewModels.Account
{
    public class LoginUserVM
    {        
        [Required]
        [DisplayName ("User Name")]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [DisplayName ("Remember Me")]
        public string RememberMe { get; set; }
    }
}
 