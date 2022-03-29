using System;
using System.Collections.Generic;
using System.Linq;
using MovieLibrary.Models;

namespace MovieLibrary.Services
{
    public class DataContext
    {
        public List<Media> Movies { get; set; }
        public List<Media> Shows { get; set; }
        public List<Media> Videos { get; set; }

        private readonly JsonRepository Repository;

        public DataContext()
        {
            Repository = new JsonRepository();
            Movies = Repository.GetAll("movie", "moviesJson.txt");
            Shows = Repository.GetAll("show", "showsJson.txt");
            Videos = Repository.GetAll("video", "videosJson.txt");
        }
        
        public void SearchAll(string searchString)
        {
            List<Media> matches = new();

            Media movie = Movies.FirstOrDefault(x => x.Title.ToLower().Contains(searchString.ToLower()));
            if (movie != null)
            {
                matches.Add(movie);
            }

            Media show = Shows.FirstOrDefault(x => x.Title.ToLower().Contains(searchString.ToLower()));
            if (show != null)
            {
                matches.Add(show);
            }

            Media video = Videos.FirstOrDefault(x => x.Title.ToLower().Contains(searchString.ToLower()));
            if (video != null)
            {
                matches.Add(video);
            }

            foreach (var media in matches)
            {
                media.Display();
                Console.WriteLine();
            }
            
            Console.WriteLine("Total Matches: " + matches.Count);
        }

        
    }
}