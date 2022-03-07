using System;

namespace MovieLibrary.Models
{
    public class Show : Media
    {
        public int Season;
        public int Episode;
        public string[] Writers;

        // Create a class constructor for the Car class
        public Show(int id, string t, int s, int e, string[] w)
        {
            ID = id;
            Title = t;
            Season = s;
            Episode = e;
            Writers = w;
        }
        
        public override void Display()
        {
            string w = string.Join(",", Writers);
            Console.WriteLine($"Show ID: {ID}");
            Console.WriteLine($"Title: {Title}");
            Console.WriteLine($"Season: {Season}");
            Console.WriteLine($"Episode: {Episode}");
            Console.WriteLine($"Writers: {w}");
        }
    }
}