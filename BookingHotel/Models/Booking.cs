using System.ComponentModel.DataAnnotations;

namespace BookingHotel.Models
{
    public class Booking : IValidatableObject
    {
        public long BookingID { get; set; }

        [Required(ErrorMessage = "Вкажіть ім’я гостя")]
        [StringLength(100, ErrorMessage = "Ім’я занадто довге")]
        public string GuestName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Вкажіть дату заїзду")]
        public DateTime DateFrom { get; set; }

        [Required(ErrorMessage = "Вкажіть дату виїзду")]
        
        public DateTime DateTo { get; set; }

        [Required]
        public long RoomID { get; set; }

        public Room? Room { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (DateFrom >= DateTo)
            {
                yield return new ValidationResult(
                    "Дата виїзду повинна бути пізніше дати заїзду.",
                    new[] { nameof(DateTo) });
            }
        }
    }
}
