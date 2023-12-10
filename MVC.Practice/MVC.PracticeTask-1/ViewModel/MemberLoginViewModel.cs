using System.ComponentModel.DataAnnotations;

namespace MVC.PracticeTask_1.ViewModel
{
    public class MemberLoginViewModel
    {
        [Required]
        [StringLength(maximumLength: 30, MinimumLength = 2)]
        public string UserName { get; set; }
        [Required]
        [StringLength(maximumLength: 20, MinimumLength = 8)]
        [DataType(DataType.Password)]   
        public string Password { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }    
    }
}
