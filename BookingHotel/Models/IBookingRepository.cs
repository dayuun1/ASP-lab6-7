namespace BookingHotel.Models
{
    public interface IBookingRepository
    {
        IQueryable<Room> Rooms { get; }
        IQueryable<Booking> Bookings { get; }

        void CreateRoom(Room room);
        void UpdateRoom(Room room);
        void DeleteRoom(Room room);

        void CreateBooking(Booking booking);
        void UpdateBooking(Booking booking);
        void DeleteBooking(Booking booking);
    }

}
