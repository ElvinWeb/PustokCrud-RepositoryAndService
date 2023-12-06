using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC.Practice.PustokMVC.Core.Models
{
    public class Slide : BaseEntity
    {
       
        [Required]
        [StringLength(maximumLength: 50)]
        public string Title { get; set; }
        [Required]
        [StringLength(maximumLength: 100)]
        public string Description { get; set; }
        [Required]
        [StringLength(maximumLength: 30)]
        public string BtnText { get; set; }
        [Required]
        [StringLength(maximumLength: 100)]
        public string RedirectUrl { get; set; }   
        
        [StringLength(maximumLength: 100)]
        public string? ImgUrl { get; set; }
        [NotMapped]
        public IFormFile? Image { get; set; }

    }
}
