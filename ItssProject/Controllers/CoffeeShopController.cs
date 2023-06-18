using ItssProject.Interfaces;
using ItssProject.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ItssProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoffeeShopController : Controller
    {
        private readonly IGetDataService _dataService;
        public CoffeeShopController(IGetDataService dataService)
        {
            _dataService = dataService;
        }
        [EnableCors("AllowOrigin")]
        [HttpPost("SearchCoffeeShop")]
        public List<CoffeeShop> SearchCoffeeShop([FromBody] RequestCoffeeShopModel Model)
        {
            var Description = Model.Description;
            var Name = Model.Name;
            var Rank = Model.Rank;
            var Address = Model.Address;
            var Service = Model.Service;
            try
            {
                if (Model == null)
                {
                    return null;
                }
                var result = _dataService.GetCoffeeShopByRequest(Description, Name, Rank, Address, Service);
                return result;
            }
            catch
            {
                throw new Exception();
            }
        }
        [EnableCors("AllowOrigin")]
        [HttpPost("GetInfoCoffeeShop/{coffeeId}")]
        public CoffeeShop GetInforCoffeeShopById([FromRoute] int coffeeId)
        {
            try
            {
                if (coffeeId <= 0)
                {
                    return null;
                }
                return _dataService.GetCoffeeShopById(coffeeId);
            }
            catch
            {
                throw new Exception();
            }
        }
        [EnableCors("AllowOrigin")]
        [HttpDelete("DeleteCoffeeShop/{coffeeId}")]
        public IActionResult DeleteCoffeeShopById([FromRoute] int coffeeId)
        {
            try
            {
                if (coffeeId <= 0)
                {
                    return BadRequest("CoffeeId is valid");
                }
                _dataService.DeleteCoffeeShopById(coffeeId);
                return Ok("Delete CoffeeShop is sucessfully");
            }
            catch
            {
                throw new Exception();
            }
        }
        [EnableCors("AllowOrigin")]
        [HttpPost("AddCoffeeShop")]
        public IActionResult AddCoffeeShop([FromBody] CoffeeShop Model)
        {
            try
            {
                if (Model == null)
                {
                    return BadRequest("Please Please fill the shop information");
                }
                _dataService.AddInformationOfCoffeeShop(Model);
                return Ok("Information of coffee shop added sucessfully");
            }
            catch
            {
                throw new Exception();
            }
        }
        [EnableCors("AllowOrigin")]
        [HttpPost("SortCoffeeShopSortBy/{pullDown}")]
        public List<CoffeeShop> SortCoffeeShop([FromRoute]string pullDown)
        {
            try
            {

               return _dataService.GetCoffeeShopSort(pullDown);
            }
            catch
            {
                throw new Exception();
            }
        }
        public class RequestCoffeeShopModel
        {
            public string? Description { get; set; }
            public string? Name { get; set; }
            public double Rank { get; set; }
            public string? Address { get; set; }
            public Boolean Service { get; set; }
        }
    }
}
