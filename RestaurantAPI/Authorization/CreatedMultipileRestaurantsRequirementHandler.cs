using Microsoft.AspNetCore.Authorization;
using RestaurantAPI.Entities;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RestaurantAPI.Authorization
{
    public class CreatedMultipileRestaurantsRequirementHandler : AuthorizationHandler<CreatedMultipileRestaurantsRequirement>
    {
        private readonly RestaurantDbContext _context;

        public CreatedMultipileRestaurantsRequirementHandler(RestaurantDbContext context)
        {
            _context = context;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CreatedMultipileRestaurantsRequirement requirement)
        {
            var userId = int.Parse(context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);

            var createdRestaurantsCounts = _context
                .Restaurants
                .Count(r => r.CreatedById == userId);

            if (createdRestaurantsCounts >= requirement.MinimumRestaurantsCreated)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
