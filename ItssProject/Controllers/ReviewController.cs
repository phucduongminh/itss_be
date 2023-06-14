using ItssProject.Interfaces;
using ItssProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ItssProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : Controller
    {
        private readonly IGetDataService _dataService;
        public ReviewController(IGetDataService dataService)
        {
            _dataService = dataService;
        }
        [HttpGet("getReviewCoffeeShop/{coffeeShopId}")]
        public List<Review> GetReview([FromRoute] int coffeeShopId)
        {
            try
            {
                if(coffeeShopId < 0)
                {
                    return null;
                }
                return _dataService.GetReview(coffeeShopId);
            }
            catch
            {
                throw new Exception();
            }

        }
        [HttpPost("addReviewCoffeeShop")]
        public IActionResult AddReview([FromBody] Review review)
        {
            if(review == null)
            {
                return BadRequest("Review is null");
            }
            try
            {
                _dataService.AddReview(review);
                return Ok("Add review is successfully");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

}
