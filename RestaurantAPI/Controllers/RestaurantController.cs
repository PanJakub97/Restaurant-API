using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Entities;
using RestaurantAPI.Models;
using RestaurantAPI.Services;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace RestaurantAPI.Controllers
{
    [Route("api/restaurant")]
    [ApiController] //For validation
    [Authorize] // All actions require query http
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantService _restaurantService;

        public RestaurantController(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }

        [HttpPut("{id}")]
        public ActionResult Update([FromBody] UpdateRestaurantDto dto, [FromRoute] int id)
        {
            //[ApiController] - this automatically calling this excerpt of code - validation

            //if(!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            _restaurantService.Update(id, dto);

            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            _restaurantService.Delete(id);

            return NotFound();
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Manager")] // this Authorization has priority over attribute in defitnition of class 
        public ActionResult CreateRestaurant([FromBody] CreateRestaurantDto dto)
        {
            var userId = int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);
            var id = _restaurantService.Create(dto);

            return Created($"/api/restaurant/{id}", null);
        }

        [HttpGet]
        //[Authorize(Policy = "HasNationality")] // Extra policy which has been added on my own in STARTUP
        //[Authorize(Policy = "AtLeast20")]
        //[Authorize(Policy = "CreatedAtLeast2Restaurants")]
        [AllowAnonymous]
        public ActionResult<IEnumerable<RestaurantDto>> GetAll([FromQuery]RestaurantQuery query)
        {
            var restaurantsDtos = _restaurantService.GetAll(query);

            return Ok(restaurantsDtos);
        }

        [HttpGet("{id}")]
        [AllowAnonymous] // you can do this without AUTHORIZATION HEADER
        public ActionResult<RestaurantDto> Get([FromRoute] int id)
        {
            var resturant = _restaurantService.GetById(id);

            return Ok(resturant);
        }

    }
}

//"email": "testkubski@test.com",
//"password": "password",
//"confirmPassword": "password",
//"nationality": "greek",
//"dateOfBirth": "1977-05-12",
//"roleId": 2,
//"firstName": "Jar",
//"lastName": "max"