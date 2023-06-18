using ItssProject.Interfaces;
using ItssProject.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ItssProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookMarkController : Controller
    {
        private readonly IGetDataService _dataService;
        public BookMarkController(IGetDataService dataService)
        {
            _dataService = dataService;
        }
        [EnableCors("AllowOrigin")]
        [HttpPost("{UserId}/AddBookMark/{CoffeeShopId}")]
        public IActionResult AddBookMark([FromRoute] int UserId, [FromRoute] int CoffeeShopId)
        {
            try
            {
                if (UserId < 1 || CoffeeShopId < 1)
                {
                    return BadRequest("Id is not valid");
                }
                _dataService.AddBookMark(UserId, CoffeeShopId);
                return Ok("Add BookMark is successfully");
            }
            catch
            {
                throw new Exception();
            }
        }
        [EnableCors("AllowOrigin")]
        [HttpPost("{UserId}/getListBookMark")]
        public List<CoffeeShop> GetBookMarks([FromRoute] int UserId)
        {
            try
            {
                if (UserId < 1)
                {
                    return null;
                }
                return _dataService.GetListCoffeBookMarks(UserId);
            }
            catch
            {
                throw new Exception();
            }
        }
    }
}
