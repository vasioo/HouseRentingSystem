using HouseRentingSystem.Infrastructure;
using HouseRentingSystem.Models.Houses;
using HouseRentingSystem.Services.Agents;
using HouseRentingSystem.Services.Houses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace HouseRentingSystem.Controllers
{
    public class HousesController : Controller
    {
        private readonly IHouseService houses;
        private readonly IAgentService agents;

        public HousesController(IHouseService houses, IAgentService agents)
        {
            this.houses = houses;
            this.agents = agents;
        }

        public IActionResult All([FromQuery]AllHousesQueryModel query)
        {
            var queryResult = this.houses.All(
                query.Category,
                query.SearchTerm,
                query.Sorting,
                query.CurrentPage,
                AllHousesQueryModel.HousesPerPage);

            query.TotalHousesCount  = queryResult.TotalHousesCount;
            query.Houses = queryResult.Houses;

            var houseCategories = this.houses.AllCategoriesNames();
            query.Categories = houseCategories;

            return View(query);
        }

        [Authorize]
        public IActionResult Mine() => View(new AllHousesQueryModel());

        public IActionResult Details(int id) => View();

        [Authorize]
        public IActionResult Add()
        {
            if (!this.agents.ExistsById(this.User.Id()))
            {
                return RedirectToAction(nameof(AgentsController.Become), "Agents");
            }
            return View(new HouseFormModel
            {
                Categories = this.houses.AllCategories()
            });
        }

        [Authorize]
        [HttpPost]
        public IActionResult Add(HouseFormModel model)
        {
            if (!this.agents.ExistsById(this.User.Id()))
            {
                return RedirectToAction(nameof(AgentsController.Become), "Agents");
            }
            if (!this.houses.CategoryExists(model.CategoryId))
            {
                this.ModelState.AddModelError(nameof(model.CategoryId),
                    "No such category");
            }
            if (!ModelState.IsValid)
            {
                model.Categories = this.houses.AllCategories();
                return View(model);
            }
            var agentId = this.agents.GetAgentId(this.User.Id());

            var newHouseId = this.houses.Create(model.Title, model.Address, model.Description,
                model.ImageUrl, model.PricePerMonth, model.CategoryId, agentId);

            return RedirectToAction(nameof(Details), new { id = newHouseId });
        }

        [Authorize]
        public IActionResult Edit(int id)
            => View(new HouseFormModel());

        [Authorize]
        [HttpPost]
        public IActionResult Edit(int id, HouseFormModel house)
        {
            return RedirectToAction(nameof(Details), new { id = "1" });
        }

        [Authorize]
        public IActionResult Delete(int id)
            => View(new HouseDetailsViewModel());

        [Authorize]
        [HttpPost]
        public IActionResult Delete(HouseDetailsViewModel house)
        {
            return RedirectToAction(nameof(All));
        }

        [Authorize]
        [HttpPost]
        public IActionResult Rent(int id)
        {
            return RedirectToAction(nameof(Mine));
        }

        [Authorize]
        [HttpPost]
        public IActionResult Leave(int id)
        {
            return RedirectToAction(nameof(Mine));
        }
    }
}
