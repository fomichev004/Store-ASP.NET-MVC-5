using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using my_store_project.Models.Data;

namespace my_store_project.Models.ViewModels.Account
{
    public class UserProfileVM
    {
        public UserProfileVM()
        {
        }

        public UserProfileVM(UserDTO row)
        {
            Id = row.Id;
            FirstName = row.FirstName;
            LastName = row.LastName;
            EmailAddress = row.EmailAddress;
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
        [DataType (DataType.EmailAddress)]
        [DisplayName ("Email")]
        public string EmailAddress { get; set; }
        [Required]
        [DisplayName ("User Name")]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }        
        [DisplayName ("Confirm Password")]
        public string ConfirmPassword { get; set; }
    }
}
