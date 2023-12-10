using System.ComponentModel.DataAnnotations;

namespace MVC.PracticeTask_1.ViewModel
{
    public class MemberRegisterViewModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Fullname { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }  
        [Required]      
        public string BirthDate { get; set; }
        [Required]
        [StringLength(maximumLength: 30, MinimumLength = 8)]
        [DataType(DataType.Password)]   
        public string Password { get; set; }
        [Required]
        [StringLength(maximumLength: 30, MinimumLength = 8)]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }    
    }
}
