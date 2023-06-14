using ItssProject.Models;
using System.Net;
using System.Xml.Linq;
using static ItssProject.Controllers.UserController;

namespace ItssProject.Interfaces
{
    public interface IGetDataService
    {
        Boolean CheckUserInformation(string userName, string password);
        CoffeeShop GetCoffeeShopById(int IdShop);
        void DeleteCoffeeShopById(int IdShop);
        List<CoffeeShop> GetCoffeeShopByRequest(string Description, string Name, double Rank, string Address, bool Service);
        void AddInformationOfCoffeeShop(CoffeeShop Model);
        List<CoffeeShop> GetCoffeeShopSort(string pullDown);
        void AddBookMark(int userId, int coffeeShopId);
        List<Review> GetReview(int coffeeShopId);
        List<CoffeeShop> GetListCoffeBookMarks(int userId);
        void AddReview(Review review);
        void AddUser(SignUpRequestModel user);
        //ResponseGetLike GetLikeAndDislikeAtComment();
    }
}
