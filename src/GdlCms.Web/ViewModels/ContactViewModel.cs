using System.ComponentModel.DataAnnotations;

namespace GdlCms.Web.ViewModels
{
    public class ContactViewModel
    {
        [Required(ErrorMessage = "Please enter your name!")]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "Please enter your email address!")]
        [EmailAddress(ErrorMessage = "Please enter valid email!")]
        public string Email { get; set; }
        
        public string EmailTitle { get; set; }
        
        [Required(ErrorMessage = "Please enter your message")]
        [MaxLength(ErrorMessage = "Your message must be no longer than 500 characters")]
        public string Message { get; set; }
        
        public int ContactFormId { get; set; }
    }
}