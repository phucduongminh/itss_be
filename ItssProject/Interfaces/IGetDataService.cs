using ItssProject.Models;
using System.Net;
using System.Xml.Linq;
using static ItssProject.Controllers.CoffeeShopController;
using static ItssProject.Controllers.UserController;

namespace ItssProject.Interfaces
{
    public interface IGetDataService
    {
        Boolean CheckUserInformation(string userName, string password);
        CoffeeShop GetCoffeeShopById(int IdShop);
        void DeleteCoffeeShopById(int IdShop);
        List<CoffeeShop> GetCoffeeShopByRequest(string Status, string Name, string Address, bool Service);
        void AddInformationOfCoffeeShop(RequestAddCoffeeShopModel Model);
        List<CoffeeShop> GetCoffeeShopSort(string pullDown);
        void AddBookMark(int userId, int coffeeShopId);
        List<Review> GetReview(int coffeeShopId);
        List<CoffeeShop> GetListCoffeBookMarks(int userId);
        void AddReview(Review review);
        void AddUser(SignUpRequestModel user);
        //ResponseGetLike GetLikeAndDislikeAtComment();
    }
}
