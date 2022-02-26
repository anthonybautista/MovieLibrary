using System;

namespace MovieLibrary.Models
{
    public class Movie : Media
    {
        public string[] Genres;

        // Create a class constructor for the Car class
        public Movie(int id, string t, string[] g)
        {
            ID = id;
            Title = t;
            Genres = g;
        }
        
        public override void Display()
        {
            string g = string.Join(",", Genres);
            Console.WriteLine($"Movie ID: {ID}");
            Console.WriteLine($"Title: {Title}");
            Console.WriteLine($"Genres: {g}");
        }
    }
}