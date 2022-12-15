using HouseRentingSystem.Services.Users;
using Microsoft.AspNetCore.Mvc;
namespace HouseRentingSystem.Web.Areas.Admin.Controllers
{
    public class UsersController : AdminController
    {
        private readonly IUserService users;

        public UsersController(IUserService users)
            => this.users = users;

        [Route("Users/All")]
        public IActionResult All()
        {
            var users = this.users.All();
                return View(users);
        }
    }
}
