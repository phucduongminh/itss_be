using ItssProject.Interfaces;
using ItssProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;

namespace ItssProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubCoffeeShopController : Controller
    {
        private readonly IGetDataService _dataService;
        public SubCoffeeShopController(IGetDataService dataService)
        {
            _dataService = dataService;
        }
        [EnableCors("AllowOrigin")]
        [HttpDelete("DeleteSubCoffeeShop/{coffeeId}")]
        public IActionResult DeleteSubCoffeeShopById([FromRoute] int coffeeId)
        {
            try
            {
                if (coffeeId <= 0)
                {
                    return BadRequest("CoffeeId is valid");
                }
                _dataService.DeleteSubCoffeeShopById(coffeeId);
                return Ok("Delete SubCoffeeShop is sucessfully");
            }
            catch
            {
                throw new Exception();
            }
        }
        [EnableCors("AllowOrigin")]
        [HttpPost("AddSubCoffeeShop")]
        public IActionResult AddSubCoffeeShop([FromBody] RequestAddSubCoffeeShopModel Model)
        {
            try
            {
                if (Model == null)
                {
                    return BadRequest("Please Please fill the shop information");
                }
                _dataService.AddInformationOfSubCoffeeShop(Model);
                return Ok("Information of coffee shop added sucessfully");
            }
            catch
            {
                throw new Exception();
            }
        }
        [EnableCors("AllowOrigin")]
        [HttpPost("SortSubCoffeeShopSortBy/{pullDown}")]
        public List<SubCoffeeShop> SortSubCoffeeShop([FromRoute] string pullDown)
        {
            try
            {

                return _dataService.GetSubCoffeeShopSort(pullDown);
            }
            catch
            {
                throw new Exception();
            }
        }
        public class RequestSubCoffeeShopModel
        {
            public string? Name { get; set; }
            public string? Address { get; set; }
            public string? Status { get; set; }
            public Boolean Service { get; set; }
        }
        public class RequestAddSubCoffeeShopModel
        {
            public string? Name { get; set; }
            public string? Address { get; set; }
            public string? Gmail { get; set; }
            public int? ContactNumber { get; set; }
            public string? ImageCover { get; set; }
            public double? AverageRating { get; set; }
            public string OpenHour { get; set; }
            public string CloseHour { get; set; }
            public Boolean Service { get; set; }
            public string? Description { get; set; }
            public string? Status { get; set; }
            public int? PostedByUser { get; set; }
            public int? Approved { get; set; }
        }    
    }
}
