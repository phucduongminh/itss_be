using ItssProject.Models;
using System.Net;
using System.Xml.Linq;
using static ItssProject.Controllers.CoffeeShopController;
using static ItssProject.Controllers.ReviewController;
using static ItssProject.Controllers.SubCoffeeShopController;
using static ItssProject.Controllers.UserController;

namespace ItssProject.Interfaces
{
    public interface IGetDataService
    {
        Boolean CheckUserInformation(string userName, string password);
        CoffeeShop GetCoffeeShopById(int IdShop);
        void DeleteCoffeeShopById(int IdShop);
        void DeleteSubCoffeeShopById(int IdShop);
        List<CoffeeShop> GetCoffeeShopByRequest(string Name, string Address, bool Service);
        void AddInformationOfCoffeeShop(RequestAddCoffeeShopModel Model);
        void AddInformationOfSubCoffeeShop(RequestAddSubCoffeeShopModel Model);
        List<CoffeeShop> GetCoffeeShopSort(string pullDown);
        List<SubCoffeeShop> GetSubCoffeeShopSort(string pullDown);
        void AddBookMark(int userId, int coffeeShopId);
        void DeleteBookMarkById(int userId, int coffeeShopId);
        List<Review> GetReview(int coffeeShopId);
        List<CoffeeShop> GetListCoffeBookMarks(int userId);
        void AddReview(RequestAddReviewModel Model);
        void AddUser(SignUpRequestModel user);
        int GetUserIdByUserName(string userName);
        string GetUserNameByUserId(int userId);
        void EditShopInformation(CoffeeShop updatedCoffeeShop);
        void ApproveCoffeeShopById(int coffeeShopId);

        //ResponseGetLike GetLikeAndDislikeAtComment();
    }
}
