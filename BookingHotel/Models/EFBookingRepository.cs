using Microsoft.EntityFrameworkCore.Migrations;

namespace BookingHotel.Models
{
    public class EFBookingRepository : IBookingRepository
    {
        private BookingDbContext context;
        public EFBookingRepository(BookingDbContext ctx)
        {
            context = ctx;
        }
        public IQueryable<Room> Rooms => context.Rooms;
        public IQueryable<Booking> Bookings => context.Bookings;

        public void CreateRoom(Room room)
        {
            context.Rooms.Add(room);
            context.SaveChanges();
        }

        public void UpdateRoom(Room room)
        {
            context.Rooms.Update(room);
            context.SaveChanges();
        }

        public void DeleteRoom(Room room)
        {
            context.Rooms.Remove(room);
            context.SaveChanges();
        }

        public void CreateBooking(Booking booking)
        {
            context.Bookings.Add(booking);
            context.SaveChanges();
        }

        public void UpdateBooking(Booking booking)
        {
            context.Bookings.Update(booking);
            context.SaveChanges();
        }

        public void DeleteBooking(Booking booking)
        {
            context.Bookings.Remove(booking);
            context.SaveChanges();
        }
    }

}
