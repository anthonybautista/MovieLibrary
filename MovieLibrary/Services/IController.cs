using MovieLibrary.DataModels;

namespace MovieLibrary.Services
{
    public interface IController
    {
        void Add();
        void Delete(int movieID);
        void Update(int movieID);
        void Search(string searchString);
    }
}