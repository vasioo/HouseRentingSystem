using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HouseRentingSystem.Web.Areas.Admin.Controllers
{
    [Area(AreaName)]
    [Authorize(Roles = "Administrator")]
    public class AdminController:Controller
    {
    }
}
