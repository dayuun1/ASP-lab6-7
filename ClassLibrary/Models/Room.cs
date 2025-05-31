using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingHotel.Models
{
    public class Room
    {
        [Key]
        public long RoomID { get; set; }

        [Required(ErrorMessage = "Вкажіть номер кімнати")]
        [StringLength(10, ErrorMessage = "Номер кімнати занадто довгий")]
        public string Number { get; set; } = String.Empty;

        [Required(ErrorMessage = "Вкажіть опис кімнати")]
        [StringLength(500, ErrorMessage = "Опис занадто довгий")]
        public string Description { get; set; } = String.Empty;

        [Required(ErrorMessage = "Вкажіть ціну")]
        [Range(0.01, 100000, ErrorMessage = "Ціна має бути в діапазоні від 0.01 до 100000")]
        [Column(TypeName = "decimal(8, 2)")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Вкажіть клас кімнати")]
        [StringLength(50, ErrorMessage = "Клас занадто довгий")]
        public string Class { get; set; } = String.Empty;

        public List<Booking> Bookings { get; set; } = new();
    }
}
