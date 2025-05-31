namespace BookingHotel.Models.ViewModels
{
    public class ListViewModel
    {
        public IEnumerable<Room> Rooms { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}
