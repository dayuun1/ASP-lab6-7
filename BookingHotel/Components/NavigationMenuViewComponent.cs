using Microsoft.AspNetCore.Mvc;
using BookingHotel.Models;
using BookingHotel.Controllers;

namespace BookingHotel.Components
{
    public class NavigationMenuViewComponent : ViewComponent
    {
        private IBookingRepository repository;

        public NavigationMenuViewComponent(IBookingRepository repo)
        {
            repository = repo;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedClass = HttpContext.Request.Query["roomClass"].ToString();
            var roomClasses = repository.Rooms
                                        .Select(r => r.Class)
                                        .Distinct()
                                        .OrderBy(c => c);
            return View(roomClasses);
        }
    }
}