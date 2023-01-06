using Microsoft.AspNetCore.Authorization;

namespace RestaurantAPI.Authorization
{
    public class CreatedMultipileRestaurantsRequirement : IAuthorizationRequirement
    {
        public CreatedMultipileRestaurantsRequirement(int minimumRestaurantsCreated)
        {
            MinimumRestaurantsCreated = minimumRestaurantsCreated;
        }

        public int MinimumRestaurantsCreated { get; }
    }
}