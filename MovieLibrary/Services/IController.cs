using MovieLibrary.DataModels;

namespace MovieLibrary.Services
{
    public interface IController
    {
        void Add();
        void Delete(int movieID);
        void Update(int movieID);
        void Search(string searchString);
        void DisplayAll();
        void AddUser();
        void AddReview(int userID, int movieID);
        void TopByOccupation();
    }
}