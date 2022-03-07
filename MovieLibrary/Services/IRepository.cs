using System.Collections.Generic;
using MovieLibrary.Models;

namespace MovieLibrary.Services
{
    public interface IRepository
    {
        List<Media> Add(string type, string file, List<Media> mediaList);
        List<Media> GetAll(string type, string file);
        void DisplayAll(List<Media> mediaList);
    }
}