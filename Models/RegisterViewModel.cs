using System.ComponentModel.DataAnnotations;
using BletExamIdeas;

namespace BletExamIdeas.Models
{
    public class RegisterViewModel : BaseEntity
    {
        [Key]
        
        [Required]
        [MinLength(2)]
        [Display(Name = "Name")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Name can only contain letters")]
        public string Name { get; set; }

        [Required]
        [MinLength(2)]
        [Display(Name = "Alias")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Alias can only contain letters")]
        
        public string Alias { get; set; }
 
        [Required]
        [EmailAddress]
        public string Email { get; set; }
 
        [Required]
        [MinLength(8)]
        public string Password { get; set; }
       
       
        [Required]
        [MinLength(8)]
        [Compare("Password", ErrorMessage = "Password and confirmation must match.")]
        [Display(Name = "Confirm Password")]
        public string CPassword { get; set; }
    }


        public class Login : BaseEntity
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string loginEmail { get; set; }

        [Required]
        [MinLength(8)]
        [Display(Name = "Password")]        
        public string loginPassword { get; set; }
    }

        public class LogReg
    {
        public RegisterViewModel registerUser { get; set;}
        public Login loginUser { get; set; }
    }
}