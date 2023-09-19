using ItssProject.Context;
using Microsoft.EntityFrameworkCore;
using System.Data;
using ItssProject.Interfaces;
using ItssProject.Models;
using System;
using static ItssProject.Controllers.UserController;
using static ItssProject.Controllers.CoffeeShopController;
using static ItssProject.Controllers.ReviewController;
using static ItssProject.Controllers.SubCoffeeShopController;
using System.Globalization;

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
        public void ApproveCoffeeShopById(int coffeeShopId)
        {
            try
            {
                var shop = _applicationContext.CoffeeShops.FirstOrDefault(coffeeShop => coffeeShop.Id == coffeeShopId);
                if (shop == null)
                {
                    Console.WriteLine("CoffeeShop does not exist");
                    return;
                }

                shop.Approved = 1;
                _applicationContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw;
            }
        }

        public void DeleteSubCoffeeShopById(int coffeeShopId)
        {
            try
            {
                var Shop = (from subcoffeeShop in _applicationContext.SubCoffeeShops.AsNoTracking()
                            where (subcoffeeShop.Id == coffeeShopId)
                            select subcoffeeShop).FirstOrDefault();
                if (Shop == null)
                {
                    Console.WriteLine("CoffeeShop is not exits");
                }
                _applicationContext.SubCoffeeShops.Remove(Shop);
                _applicationContext.SaveChanges();
            }
            catch
            {
                throw new Exception();
            }
        }
        public void DeleteBookMarkById(int userId, int coffeeShopId)
        {
            try
            {
                var Bookmark = (from bookMark in _applicationContext.BookMarks.AsNoTracking()
                              where (bookMark.UserId == userId && bookMark.CoffeeId == coffeeShopId)
                              select bookMark).FirstOrDefault();
                if (Bookmark != null)
                {
                    //Console.WriteLine("CoffeeShop is not exits");
                    _applicationContext.BookMarks.Remove(Bookmark);
                    _applicationContext.SaveChanges();
                }
            }
            catch
            {
                throw new Exception();
            }
        }
        //public ResponseGetLike GetLikeAndDislikeAtComment()
        //{

        //}
        public List<CoffeeShop> GetCoffeeShopByRequest(string Name, string Address, bool Service)
        {
            try
            {
                var listCoffeeShop = new List<CoffeeShop>();

                //DateTime dateTime1 = DateTime.ParseExact(Hour, "H:mm", null);

                listCoffeeShop = (from shop in _applicationContext.CoffeeShops.AsNoTracking()
                                  where (shop.Service == Service
                                         //&& DateTime.ParseExact(shop.CloseHour, "H:mm", null) > dateTime1
                                         //&& shop.OpenHour== Hour
                                         && shop.Name.ToLower().Contains(Name.ToLower())
                                         && shop.Address.ToLower().Contains(Address.ToLower()))
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
                                      Service = shop.Service,
                                      Description = shop.Description,
                                      Status = shop.Status,
                                      PostedByUser = shop.PostedByUser,
                                      Approved = shop.Approved
                                  }).ToList();

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
                                      Service = shop.Service,
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

        public void EditShopInformation(CoffeeShop updatedCoffeeShop)
        {
            try
            {
                // Lấy thông tin cửa hàng cà phê dựa trên coffeeId
                var coffeeShop = _applicationContext.CoffeeShops.FirstOrDefault(shop => shop.Id == updatedCoffeeShop.Id);
                if (coffeeShop == null)
                {
                    throw new ArgumentException("Invalid coffee shop ID");
                }

                // Cập nhật thông tin mới chỉ khi các trường không phải là null
                if (updatedCoffeeShop.Name != null)
                {
                    coffeeShop.Name = updatedCoffeeShop.Name;
                }
                if (updatedCoffeeShop.Address != null)
                {
                    coffeeShop.Address = updatedCoffeeShop.Address;
                }
                if (updatedCoffeeShop.Gmail != null)
                {
                    coffeeShop.Gmail = updatedCoffeeShop.Gmail;
                }
                if (updatedCoffeeShop.Approved != null)
                {
                    coffeeShop.Approved = updatedCoffeeShop.Approved;
                }
                if (updatedCoffeeShop.Status != null)
                {
                    coffeeShop.Status = updatedCoffeeShop.Status;
                }
                if (updatedCoffeeShop.AverageRating != null)
                {
                    coffeeShop.AverageRating = updatedCoffeeShop.AverageRating;
                }
                if (updatedCoffeeShop.CloseHour != null)
                {
                    coffeeShop.CloseHour = updatedCoffeeShop.CloseHour;
                }
                if (updatedCoffeeShop.OpenHour != null)
                {
                    coffeeShop.OpenHour = updatedCoffeeShop.OpenHour;
                }
                if (updatedCoffeeShop.Service != null)
                {
                    coffeeShop.Service = updatedCoffeeShop.Service;
                }
                if (updatedCoffeeShop.ContactNumber != null)
                {
                    coffeeShop.ContactNumber = updatedCoffeeShop.ContactNumber;
                }
                if (updatedCoffeeShop.Description != null)
                {
                    coffeeShop.Description = updatedCoffeeShop.Description;
                }
                if (updatedCoffeeShop.ImageCover != null)
                {
                    coffeeShop.ImageCover = updatedCoffeeShop.ImageCover;
                }
                if (updatedCoffeeShop.PostedByUser != null)
                {
                    coffeeShop.PostedByUser = updatedCoffeeShop.PostedByUser;
                }

                // Lưu thay đổi vào cơ sở dữ liệu
                _applicationContext.SaveChanges();
            }
            catch
            {
                throw new Exception();
            }
        }

        public void AddInformationOfCoffeeShop(RequestAddCoffeeShopModel Model)
        {
            try
            {
                if (Model.Name == null || Model.Address == null)
                {
                    throw new ArgumentException("Invalid field");
                }
                var newCoffeeShop = new CoffeeShop();
                newCoffeeShop.Name = Model.Name;
                newCoffeeShop.Address = Model.Address;
                newCoffeeShop.Gmail = Model.Gmail;
                newCoffeeShop.Approved = Model.Approved;
                newCoffeeShop.Status = Model.Status;
                newCoffeeShop.AverageRating = Model.AverageRating;
                newCoffeeShop.CloseHour = Model.CloseHour;
                newCoffeeShop.OpenHour = Model.OpenHour;
                newCoffeeShop.Service = Model.Service;
                newCoffeeShop.ContactNumber = Model.ContactNumber;
                newCoffeeShop.Description = Model.Description;
                newCoffeeShop.ImageCover = Model.ImageCover;
                newCoffeeShop.PostedByUser = Model.PostedByUser;
                _applicationContext.CoffeeShops.Add(newCoffeeShop);
                _applicationContext.SaveChanges();
            }
            catch
            {
                throw new Exception();
            }
        }

        public void AddInformationOfSubCoffeeShop(RequestAddSubCoffeeShopModel Model)
        {
            try
            {
                if (Model.Name == null || Model.Address == null)
                {
                    throw new ArgumentException("Invalid field");
                }
                var newCoffeeShop = new SubCoffeeShop();
                newCoffeeShop.Name = Model.Name;
                newCoffeeShop.Address = Model.Address;
                newCoffeeShop.Gmail = Model.Gmail;
                newCoffeeShop.Approved = Model.Approved;
                newCoffeeShop.Status = Model.Status;
                newCoffeeShop.AverageRating = Model.AverageRating;
                newCoffeeShop.CloseHour = Model.CloseHour;
                newCoffeeShop.OpenHour = Model.OpenHour;
                newCoffeeShop.Service = Model.Service;
                newCoffeeShop.ContactNumber = Model.ContactNumber;
                newCoffeeShop.Description = Model.Description;
                newCoffeeShop.ImageCover = Model.ImageCover;
                newCoffeeShop.PostedByUser = Model.PostedByUser;
                _applicationContext.SubCoffeeShops.Add(newCoffeeShop);
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
                                           Service = shop.Service,
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
        private List<SubCoffeeShop> GetSubCoffeeShop()
        {
            try
            {
                var listCoffeeShops = (from shop in _applicationContext.SubCoffeeShops.AsNoTracking()
                                       select new SubCoffeeShop
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
                                           Service = shop.Service,
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
                if (pullDown == "All")
                {
                    listCoffeeShops = GetCoffeeShop();
                    IQueryable<CoffeeShop> queryableCoffeeShops = listCoffeeShops.AsQueryable();
                    queryableCoffeeShops = queryableCoffeeShops.OrderBy(i => i.Id);
                    return queryableCoffeeShops.ToList();
                }
                if (pullDown == "Number of comment")
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
                    queryableCoffeeShops = queryableCoffeeShops.OrderByDescending(i => i.AverageRating);
                    return queryableCoffeeShops.ToList();
                }
                if (pullDown == "RatingDown")
                {
                    listCoffeeShops = GetCoffeeShop();
                    IQueryable<CoffeeShop> queryableCoffeeShops = listCoffeeShops.AsQueryable();
                    queryableCoffeeShops = queryableCoffeeShops.OrderBy(i => i.AverageRating);
                    return queryableCoffeeShops.ToList();
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

        public List<SubCoffeeShop> GetSubCoffeeShopSort(string pullDown)
        {
            try
            {
                var listCoffeeShops = new List<SubCoffeeShop>();
                if (pullDown == "All")
                {
                    listCoffeeShops = GetSubCoffeeShop();
                    IQueryable<SubCoffeeShop> queryableCoffeeShops = listCoffeeShops.AsQueryable();
                    queryableCoffeeShops = queryableCoffeeShops.OrderBy(i => i.Id);
                    return queryableCoffeeShops.ToList();
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
                    var listCoffeeShopIdOfBookMarks = _applicationContext.Set<BookMark>().Where(a => a.UserId == userId).Select(a => a.CoffeeId).Distinct().ToList();
                    List<CoffeeShop> listCoffeeShop = new List<CoffeeShop>();
                    foreach (var item in listCoffeeShopIdOfBookMarks)
                    {
                        var coffeeShop = (from CoffeeShop in _applicationContext.CoffeeShops.AsNoTracking()
                                          where(CoffeeShop.Id == item)
                                          select CoffeeShop).FirstOrDefault();
                        //var coffeeShop = _applicationContext.Set<CoffeeShop>().Where(a => a.Id == item).Select(a => a);
                        //listCoffeeShop.Add((CoffeeShop)coffeeShop);
                        listCoffeeShop.Add(coffeeShop);
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
                                      where (review.CoffeeId == coffeeShopId)
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
        public void AddReview(RequestAddReviewModel Model)
        {
            try
            {
                if (Model != null)
                {
                    var review = new Review();
                    review.UserId = Model.UserId;
                    review.CoffeeId = Model.CoffeeId;
                    review.Rating = Model.Rating;
                    review.Comment = Model.Comment;
                    review.ReviewAt = Model.ReviewAt;
                    review.EditAt = Model.EditAt;
                    _applicationContext.Reviews.Add(review);
                    _applicationContext.SaveChanges();
                }
            }
            catch
            {
                throw new Exception();
            }
        }
        public int GetUserIdByUserName(string userName)
        {
            try
            {
                var userId = (from User in _applicationContext.Users
                              where (User.Name == userName)
                              select User.Id).FirstOrDefault();
                return userId;
            }
            catch
            {
                throw new Exception();
            }
        }
        public string GetUserNameByUserId(int userId)
        {
            try
            {
                var userName = (from User in _applicationContext.Users
                              where (User.Id == userId)
                              select User.Name).FirstOrDefault();
                return userName;
            }
            catch
            {
                throw new Exception();
            }
        }
    }
}



