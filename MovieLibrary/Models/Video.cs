using System;

namespace MovieLibrary.Models
{
    public class Video : Media
    {
        public string Format;
        public int Length;
        private int[] Regions;

        // Create a class constructor for the Car class
        public Video(int id, string t, string f, int l, int[] r)
        {
            ID = id;
            Title = t;
            Format = f;
            Length = l;
            Regions = r;
        }
        
        public override void Display()
        {
            string r = string.Join(",", Regions);
            Console.WriteLine($"Show ID: {ID}");
            Console.WriteLine($"Title: {Title}");
            Console.WriteLine($"Format: {Format}");
            Console.WriteLine($"Length: {Length}");
            Console.WriteLine($"Regions: {r}");
        }
    }
}