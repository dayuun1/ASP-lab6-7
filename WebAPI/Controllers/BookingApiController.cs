using BookingHotel.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class BookingsController : ControllerBase
    {
        private readonly IBookingRepository _repository;

        public BookingsController(IBookingRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var bookings = _repository.Bookings.Include(b => b.Room).ToList();
            return Ok(bookings);
        }

        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            var booking = _repository.Bookings.Include(b => b.Room).FirstOrDefault(b => b.BookingID == id);
            if (booking == null)
                return NotFound();
            return Ok(booking);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Booking booking)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _repository.CreateBooking(booking);
            return CreatedAtAction(nameof(Get), new { id = booking.BookingID }, booking);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] Booking booking)
        {
            if (id != booking.BookingID)
                return BadRequest("Booking ID mismatch");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _repository.UpdateBooking(booking);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var booking = _repository.Bookings.FirstOrDefault(b => b.BookingID == id);
            if (booking == null)
                return NotFound();

            _repository.DeleteBooking(booking);
            return NoContent();
        }
    }
}
