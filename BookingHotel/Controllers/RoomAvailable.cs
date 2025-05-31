using BookingHotel.Hubs;
using BookingHotel.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BookingHotel.Controllers
{
    [Authorize]
    public class RoomAvailableController : Controller
    {
        private readonly IBookingRepository _repository;
        private readonly IHubContext<RoomHub> _hubContext;

        public RoomAvailableController(IBookingRepository repository, IHubContext<RoomHub> hubContext)
        {
            _repository = repository;
            _hubContext = hubContext;
        }

        public IActionResult Index()
        {
            var rooms = _repository.Rooms.Include(r => r.Bookings).ToList();
            return View(rooms);
        }

        [HttpPost]
        public async Task<IActionResult> BookRoom(int roomId)
        {
            var booking = new Booking
            {
                RoomID = roomId,
                DateFrom = DateTime.Today,
                DateTo = DateTime.Today.AddDays(1)
            };

            _repository.CreateBooking(booking); 
            var rooms = _repository.Rooms.Include(r => r.Bookings).ToList();
            var roomAvailability = rooms.Select(r => new
            {
                r.RoomID,
                r.Number,
                IsAvailable = !_repository.Bookings.Any(b =>
                    b.RoomID == r.RoomID &&
                    b.DateFrom <= DateTime.Today &&
                    b.DateTo > DateTime.Today)
            });

            await _hubContext.Clients.All.SendAsync("UpdateRoomAvailability", roomAvailability);

            return Ok();
        }

    }
}
