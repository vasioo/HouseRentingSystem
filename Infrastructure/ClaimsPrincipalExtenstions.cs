using System.Security.Claims;

namespace HouseRentingSystem.Infrastructure
{
    public static class ClaimsPrincipalExtenstions
    {
        public static string Id(this ClaimsPrincipal user)
            => user!.FindFirst(ClaimTypes.NameIdentifier).Value;

        public static bool IsAdmin(this ClaimsPrincipal user)
        => user.IsInRole("Administrator");
    }
}
