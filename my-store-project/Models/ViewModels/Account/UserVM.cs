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
    public class UserVM
    {   
        public UserVM()
        {
        }

        public UserVM(UsertDTO row)
        {
            Id = row.Id;
            FirstName = row.FirstName;
            LastName = row.LastName;
            EmailAdress = row.EmailAdress;
            Username = row.Username;
            Password = row.Password;
        }

        public int Id { get; set; }
        [Required]
        [DisplayName ("First Name")]
        public string FirstName { get; set; }
        [Required]
        [DisplayName ("Last Name")]
        public string LastName { get; set; }
        [Required]
        [DataType(DataType.EmailAdress)]
        [DisplayName ("Email")]
        public string EmailAdress { get; set; }
        [Required]
        [DisplayName ("User Name")]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [DisplayName ("Confirm Password")]
        public string ConfirmPassword { get; set; }
    }
}
 