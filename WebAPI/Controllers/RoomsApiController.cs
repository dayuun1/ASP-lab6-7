using Microsoft.AspNetCore.Mvc;
using BookingHotel.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class RoomsApiController : ControllerBase
    {
        private readonly IBookingRepository _repository;

        public RoomsApiController(IBookingRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("{id}")]
        public IActionResult GetRoom(long id)
        {
            var room = _repository.Rooms.FirstOrDefault(r => r.RoomID == id);
            if (room == null) return NotFound();
            return Ok(room);
        }

        [HttpPost]
        public IActionResult CreateRoom([FromBody] Room room)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _repository.CreateRoom(room);
            return CreatedAtAction(nameof(GetRoom), new { id = room.RoomID }, room);
        }
        [HttpGet]
        public async Task<ActionResult<List<Room>>> GetAllRooms()
        {
            var rooms = await _repository.Rooms.Include(r => r.Bookings).ToListAsync();
            return Ok(rooms);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateRoom(long id, [FromBody] Room updatedRoom)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingRoom = _repository.Rooms.FirstOrDefault(r => r.RoomID == id);
            if (existingRoom == null) return NotFound();

            existingRoom.Number = updatedRoom.Number;
            existingRoom.Description = updatedRoom.Description;
            existingRoom.Price = updatedRoom.Price;
            existingRoom.Class = updatedRoom.Class;

            _repository.UpdateRoom(existingRoom);

            return NoContent(); 
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteRoom(long id)
        {
            var existingRoom = _repository.Rooms.FirstOrDefault(r => r.RoomID == id);
            if (existingRoom == null) return NotFound();

            _repository.DeleteRoom(existingRoom);
            return NoContent();
        }
    }

}
