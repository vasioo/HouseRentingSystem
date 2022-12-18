using HouseRentingSystem.Infrastructure;
using HouseRentingSystem.Models.Agents;
using HouseRentingSystem.Services.Agents;
using HouseRentingSystem.Services.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HouseRentingSystem.Controllers
{
    public class AgentsController : Controller
    {

        private readonly IAgentService agents;
        private readonly IUserService users;

        public AgentsController(IAgentService agents, IUserService users)
        {
            this.agents = agents;
            this.users = users;
        }

        [Authorize]
        public IActionResult Become()
        {
            if (this.agents.ExistsById(this.User.Id()))
            {
                return BadRequest();
            }
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Become(BecomeAgentFormModel model)
        {
            var userId = this.User.Id();
            if (this.agents.ExistsById(userId))
            {
                return BadRequest();
            }
            if (this.agents.AgentWithPhoneNumberExists(model.PhoneNumber!))
            {
                ModelState.AddModelError(nameof(model.PhoneNumber)
                    , "This phone number already exists");
            }
            if (this.users.UserHasRents(userId))
            {
                ModelState.AddModelError("Error", "You should not have any rents in order to become an agent");
            }
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            this.agents.Create(userId, model.PhoneNumber);

            TempData["message"] = "You have successfully became an agent";
            return RedirectToAction(nameof(HousesController.All), "Houses");
        }
    }
}
