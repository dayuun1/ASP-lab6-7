using Microsoft.AspNetCore.Mvc;
using BookingHotel.Models;
using BookingHotel.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace BookingHotel.Controllers
{
        public class HomeController : Controller
        {
            private IBookingRepository repository;

            public HomeController(IBookingRepository repo)
            {
                repository = repo;
            }
        [Authorize]
        public IActionResult Index(string roomClass, int page = 1)
        {
            var filteredRooms = repository.Rooms
                .Where(r => roomClass == null || r.Class == roomClass);

            int pageSize = 2;

            var model = new ListViewModel
            {
                Rooms = filteredRooms
                            .OrderBy(r => r.RoomID)
                            .Skip((page - 1) * pageSize)
                            .Take(pageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = filteredRooms.Count()
                }
            };

            return View(model);
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Details(long id)
        {
            var room = repository.Rooms.FirstOrDefault(r => r.RoomID == id);
            if (room == null) return NotFound();
            return View(room);
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Create(Room room)
        {
            if (ModelState.IsValid)
            {
                repository.CreateRoom(room);
                return RedirectToAction("Index");
            }
            return View(room);
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(long id)
        {
            var room = repository.Rooms.FirstOrDefault(r => r.RoomID == id);
            if (room == null) return NotFound();
            return View(room);
        }

        [HttpPost]
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(Room room)
        {
            if (ModelState.IsValid)
            {
                var existingRoom = repository.Rooms.FirstOrDefault(r => r.RoomID == room.RoomID);
                if (existingRoom != null)
                {
                    existingRoom.Number = room.Number;
                    existingRoom.Description = room.Description;
                    existingRoom.Price = room.Price;
                    existingRoom.Class = room.Class;
                    repository.UpdateRoom(existingRoom);
                }
                return RedirectToAction("Index");
            }

            return View(room);
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(long id)
        {
            var room = repository.Rooms.FirstOrDefault(r => r.RoomID == id);
            if (room == null) return NotFound();
            return View(room);
        }

        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteConfirmed(long id)
        {
            var room = repository.Rooms.FirstOrDefault(r => r.RoomID == id);
            if (room != null)
            {
                repository.DeleteRoom(room);
            }
            return RedirectToAction("Index");
        }
    }
}

