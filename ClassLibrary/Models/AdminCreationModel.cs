using System.ComponentModel.DataAnnotations;

namespace BookingHotel.Models
{
    public class AdminCreationModel
    {
        [Required]
        [EmailAddress]
        public string NewAdminEmail { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string NewAdminPassword { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        [Compare("NewAdminPassword", ErrorMessage = "Password and confirmation do not match.")]
        public string ConfirmNewAdminPassword { get; set; } = string.Empty;
    }
}
