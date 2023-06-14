using ItssProject.Context;
using Microsoft.EntityFrameworkCore;
using System.Data;
using ItssProject.Interfaces;
using ItssProject.Models;
using System;
using static ItssProject.Controllers.UserController;

namespace ItssProject.Services
{
    public class GetDataService : IGetDataService
    {
        private readonly ApplicationContext _applicationContext;
        //private readonly IConfiguration _configuration;
        public GetDataService(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
            //_configuration = configuration;
        }
        public Boolean CheckUserInformation(string userName, string password)
        {
            var user = (from User in _applicationContext.Users.AsNoTracking()
                        where (User.Name == userName && User.Password == password)
                        select User).FirstOrDefault();
            if (user == null)
            {
                return false;
            }
            return true;
        }
        public void AddUser(SignUpRequestModel user)
        {
            try
            {
                if (user.UserName == null || user.Password.Length < 8 || user.Password == null || user.Gmail == null)
                {
                    throw new ArgumentException("Invalid field");
                }
                var newUser = new User();
                newUser.Name = user.UserName;
                newUser.Password = user.Password;
                newUser.Gmail = user.Gmail;
                _applicationContext.Users.Add(newUser);
                _applicationContext.SaveChanges();
            }
            catch
            {
                throw new Exception();
            }

        }
        //public Boolean SignUpUser(string userName, string passWord)
        //{
        //    var Name = (from User in _applicationContext.Users.AsNoTracking()
        //                    where (User.Name == userName)
        //                    select User.Name).FirstOrDefault();
        //    if(Name != null)
        //    {
        //        return false;
        //    }
        //    else
        //    {

        //    } 


        //            }
        //}
        public void DeleteCoffeeShopById(int coffeeShopId)
        {
            try
            {
                var Shop = (from coffeeShop in _applicationContext.CoffeeShops.AsNoTracking()
                            where (coffeeShop.Id == coffeeShopId)
                            select coffeeShop).FirstOrDefault();
                if (Shop == null)
                {
                    Console.WriteLine("CoffeeShop is not exits");
                }
                _applicationContext.CoffeeShops.Remove(Shop);
                _applicationContext.SaveChanges();
            }
            catch
            {
                throw new Exception();
            }
        }
        //public ResponseGetLike GetLikeAndDislikeAtComment()
        //{

        //}
        public List<CoffeeShop> GetCoffeeShopByRequest(string Description, string Name, double Rank, string Address, bool Service)
        {
            try
            {
                var listCoffeeShop = new List<CoffeeShop>();
                listCoffeeShop = (from shop in _applicationContext.CoffeeShops.AsNoTracking()
                                  where (shop.Service == Service && shop.Description == Description && shop.Name == Name && shop.AverageRating == Rank && shop.Address == Address)
                                  select new CoffeeShop
                                  {
                                      Id = shop.Id,
                                      Name = shop.Name,
                                      Address = shop.Address,
                                      Gmail = shop.Gmail,
                                      ContactNumber = shop.ContactNumber,
                                      ImageCover = shop.ImageCover,
                                      AverageRating = shop.AverageRating,
                                      OpenHour = shop.OpenHour,
                                      CloseHour = shop.CloseHour,
                                      Description = shop.Description,
                                      Status = shop.Status,
                                      PostedByUser = shop.PostedByUser,
                                      Approved = shop.Approved
                                  }
                                  ).ToList();
                return listCoffeeShop;
            }
            catch
            {
                throw new Exception();
            }
        }
        public CoffeeShop GetCoffeeShopById(int coffeeId)
        {
            try
            {
                var coffeeShop = (from shop in _applicationContext.CoffeeShops.AsNoTracking()
                                  where (shop.Id == coffeeId)
                                  select new CoffeeShop
                                  {
                                      Id = shop.Id,
                                      Name = shop.Name,
                                      Address = shop.Address,
                                      Gmail = shop.Gmail,
                                      ContactNumber = shop.ContactNumber,
                                      ImageCover = shop.ImageCover,
                                      AverageRating = shop.AverageRating,
                                      OpenHour = shop.OpenHour,
                                      CloseHour = shop.CloseHour,
                                      Description = shop.Description,
                                      Status = shop.Status,
                                      PostedByUser = shop.PostedByUser,
                                      Approved = shop.Approved
                                  }
                                     ).FirstOrDefault();
                return coffeeShop;
            }
            catch
            {
                throw new Exception();
            }
        }
        public void AddInformationOfCoffeeShop(CoffeeShop Model)
        {
            try
            {
                _applicationContext.CoffeeShops.Add(Model);
                _applicationContext.SaveChanges();
            }
            catch
            {
                throw new Exception();
            }
        }
        private List<CoffeeShop> GetCoffeeShop()
        {
            try
            {
                //var listCoffeeShop = _dBRepository.Query(_applicationContext => _applicationContext.Set<CoffeeShop>()
                //.Select(i => new CoffeeShop
                //{
                //    Id = i.Id,
                //    Name = i.Name,
                //    Address = i.Address,
                //    Gmail = i.Gmail,
                //    ContactNumber = i.ContactNumber,
                //    ImageCover = i.ImageCover,
                //    AverageRating = i.AverageRating,
                //    OpenHour = i.OpenHour,
                //    CloseHour = i.CloseHour,
                //    Description = i.Description,
                //    Status = i.Status,
                //    PostedByUser = i.PostedByUser,
                //    Approved = i.Approved
                //})).ToList();
                var listCoffeeShops = (from shop in _applicationContext.CoffeeShops.AsNoTracking()
                                       select new CoffeeShop
                                       {
                                           Id = shop.Id,
                                           Name = shop.Name,
                                           Address = shop.Address,
                                           Gmail = shop.Gmail,
                                           ContactNumber = shop.ContactNumber,
                                           ImageCover = shop.ImageCover,
                                           AverageRating = shop.AverageRating,
                                           OpenHour = shop.OpenHour,
                                           CloseHour = shop.CloseHour,
                                           Description = shop.Description,
                                           Status = shop.Status,
                                           PostedByUser = shop.PostedByUser,
                                           Approved = shop.Approved
                                       }
                                     ).ToList();


                return listCoffeeShops;
            }
            catch
            {
                throw new Exception();
            }
        }
        public List<CoffeeShop> GetCoffeeShopSort(string pullDown)
        {
            try
            {
                var listCoffeeShops = new List<CoffeeShop>();
                if (pullDown == null)
                {
                    return GetCoffeeShop();
                }
                if (pullDown == "Number of comment (count)")
                {
                    var listCoffeeIdWithManyComments = GetIdCoffeeShopHadMostComment();
                    foreach (int i in listCoffeeIdWithManyComments)
                    {
                        var item = GetCoffeeShopById(i);
                        listCoffeeShops.Add(item);
                    }
                    return listCoffeeShops;
                }
                if (pullDown == "Rating")
                {
                    listCoffeeShops = GetCoffeeShop();
                    IQueryable<CoffeeShop> queryableCoffeeShops = listCoffeeShops.AsQueryable();
                    queryableCoffeeShops.OrderByDescending(i => i.AverageRating);
                    return listCoffeeShops;
                }
                if (pullDown == "Address")
                {
                    listCoffeeShops = GetCoffeeShop();
                    IQueryable<CoffeeShop> queryableCoffeeShops = listCoffeeShops.AsQueryable();
                    queryableCoffeeShops.OrderByDescending(i => i.Address);
                    return listCoffeeShops;
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                throw new Exception();
            }
        }
        private List<int> GetIdCoffeeShopHadMostComment()
        {
            try
            {
                var listReview = (from item in _applicationContext.Reviews.AsNoTracking()
                                  select new
                                  {
                                      ReviewId = item.Id,
                                      CoffeeShopId = item.CoffeeId
                                  }).ToList();
                var listCoffeeIdWithManyComments = listReview
            .GroupBy(c => c.CoffeeShopId)
            .OrderByDescending(g => g.Count())
            .Select(g => g.Key)
            .ToList();
                return listCoffeeIdWithManyComments;
            }
            catch
            {
                throw new Exception();
            }
        }
        public void AddBookMark(int userId, int coffeeShopId)
        {
            try
            {
                var UserId = (from user in _applicationContext.Users.AsNoTracking()
                              where (user.Id == userId)
                              select user.Id);
                var CofferShopId = (from coffeeShop in _applicationContext.CoffeeShops.AsNoTracking()
                                    where (coffeeShop.Id == coffeeShopId)
                                    select coffeeShop.Id);
                if (UserId != null && coffeeShopId != null)
                {
                    var bookMark = new BookMark();
                    bookMark.UserId = userId;
                    bookMark.CoffeeId = coffeeShopId;
                    _applicationContext.BookMarks.Add(bookMark);
                    _applicationContext.SaveChanges();
                }
            }
            catch
            {
                throw new Exception();
            }
        }
        public List<CoffeeShop> GetListCoffeBookMarks(int userId)
        {
            try
            {
                var UserId = (from bookMark in _applicationContext.BookMarks.AsNoTracking()
                              where (bookMark.Id == userId)
                              select bookMark.Id);
                if (UserId != null)
                {
                    var listCoffeeShopIdOfBookMarks = _applicationContext.Set<BookMark>().Where(a => a.UserId == userId).Select(a => a.CoffeeId).ToList();
                    List<CoffeeShop> listCoffeeShop = new List<CoffeeShop>();
                    foreach (var item in listCoffeeShopIdOfBookMarks)
                    {
                        var coffeeShop = _applicationContext.Set<CoffeeShop>().Where(a => a.Id == item).Select(a => a);
                        listCoffeeShop.Add((CoffeeShop)coffeeShop);
                    }
                    return listCoffeeShop;
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                throw new Exception();
            }
        }
        public List<Review> GetReview(int coffeeShopId)
        {
            try
            {
                var CoffeeShopId = (from coffeeShop in _applicationContext.CoffeeShops.AsNoTracking()
                                    where (coffeeShop.Id == coffeeShopId)
                                    select coffeeShop.Id);
                if (CoffeeShopId != null)
                {
                    var listReview = (from review in _applicationContext.Reviews.AsNoTracking()
                                      where (review.Id == coffeeShopId)
                                      select new Review
                                      {
                                          Id = review.Id,
                                          CoffeeId = review.CoffeeId,
                                          UserId = review.UserId,
                                          Rating = review.Rating,
                                          Comment = review.Comment,
                                          ReviewAt = review.ReviewAt,
                                          EditAt = review.EditAt
                                      }).ToList();
                    return listReview;
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                throw new Exception();
            }
        }
        public void AddReview(Review review)
        {
            try
            {
                var userId = review.UserId;


                if (review != null)
                {
                    _applicationContext.Reviews.Add(review);
                    _applicationContext.SaveChanges();
                }
            }
            catch
            {
                throw new Exception();
            }
        }
    }
}



