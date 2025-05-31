using System.ComponentModel.DataAnnotations;

namespace BookingHotel.Models
{
    public class UserProfileModel
    {
        [Display(Name = "Email")]
        public string Email { get; set; } = string.Empty;

        [Display(Name = "New Password")]
        [DataType(DataType.Password)]
        public string? NewPassword { get; set; }

        [Display(Name = "Confirm New Password")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "The password and confirmation password do not match.")]
        public string? ConfirmNewPassword { get; set; }  

        public IList<string> Roles { get; set; } = new List<string>();

        public bool IsAdmin { get; set; }
    }
}
