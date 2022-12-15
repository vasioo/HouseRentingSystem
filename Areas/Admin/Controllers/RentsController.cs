using HouseRentingSystem.Services.Rents;
using Microsoft.AspNetCore.Mvc;

namespace HouseRentingSystem.Web.Areas.Admin.Controllers
{
    public class RentsController : AdminController
    {
        private readonly IRentService rents;

        public RentsController(IRentService rents)
            => this.rents = rents;

        [Route("Rents/All")]
        public IActionResult All()
        {
            var rents = this.rents.All();
            return View(rents);
        }
    }
}
