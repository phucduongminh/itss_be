﻿using ItssProject.Interfaces;
using ItssProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;

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
            var Name = Model.Name;
            var Address = Model.Address;
            var Service = Model.Service;
            try
            {
                if (Model == null)
                {
                    return null;
                }
                var result = _dataService.GetCoffeeShopByRequest(Name, Address, Service);
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
        public IActionResult AddCoffeeShop([FromBody] RequestAddCoffeeShopModel Model)
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
        public List<CoffeeShop> SortCoffeeShop([FromRoute] string pullDown)
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
        [EnableCors("AllowOrigin")]
        [HttpPost("EditCoffeeShop")]
        public IActionResult EditCoffeeShop([FromBody] CoffeeShop Model)
        {
            try
            {
                if (Model == null)
                {
                    return BadRequest("Please Please fill the shop information");
                }
                _dataService.EditShopInformation(Model);
                return Ok("Information of coffee shop edited sucessfully");
            }
            catch
            {
                throw new Exception();
            }
        }
        [EnableCors("AllowOrigin")]
        [HttpPost("Approve/{coffeeId}")]
        public IActionResult ApproveCoffeeShopById([FromRoute] int coffeeId)
        {
            try
            {
                if (coffeeId <= 0)
                {
                    return BadRequest("CoffeeId is valid");
                }
                _dataService.ApproveCoffeeShopById(coffeeId);
                return Ok("Approve CoffeeShop is sucessfully");
            }
            catch
            {
                throw new Exception();
            }
        }
        public class RequestCoffeeShopModel
        {
            public string? Name { get; set; }
            public string? Address { get; set; }
            public Boolean Service { get; set; }
        }
        public class RequestAddCoffeeShopModel
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
            public Boolean Status { get; set; }
            public int? PostedByUser { get; set; }
            public int? Approved { get; set; }
        }    
    }
}
