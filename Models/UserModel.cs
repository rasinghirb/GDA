using IdentityModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GDA.Models
{
    public class UserModel
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Please enter UserId.")]
        [Display(Name = "User Id")]
        [Range(5100000, 5600000)]
      
        public int UserId { get; set; }
        [Required(ErrorMessage = "Please enter Password.")]
        public string UserPassword { get; set; }
        [Required(ErrorMessage = "Please enter User Role.")]
        public int UserRole { get; set; }
        [Required(ErrorMessage = "Please enter Email.")]
        public string Email { get; set; }
    }
}
