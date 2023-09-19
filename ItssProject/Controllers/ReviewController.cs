using ItssProject.Interfaces;
using ItssProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;

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
        [EnableCors("AllowOrigin")]
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
        [EnableCors("AllowOrigin")]
        [HttpPost("addReviewCoffeeShop")]
        public IActionResult AddReviewInfor([FromBody] RequestAddReviewModel Model)
        {
            if(Model == null)
            {
                return BadRequest("Review is null");
            }
            try
            {
                _dataService.AddReview(Model);
                return Ok("Add review is successfully");
            }
            catch
            {
                throw new Exception();
            }
        }
        public class RequestAddReviewModel
        {
            public int UserId { get; set; }
            public int CoffeeId { get; set; }
            public double Rating { get; set; }
            public string? Comment { get; set; }
            public string? ReviewAt { get; set; }
            public string? EditAt { get; set; }
        }
    }

}
