using System;
using System.Collections.Generic;
using MovieLibrary.DataModels;
using MovieLibrary.Context;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace MovieLibrary.Services
{
    public class DbMenu
    {
        public bool DeletePrompt(string movieTitle)
        {
            Console.WriteLine($"Are you sure you want to delete {movieTitle}?");
            Console.Write($"Enter 'yes' to confirm:");
            string confirm = Console.ReadLine();

            if (confirm == "yes")
            {
                return true;
            }

            return false;
        }

        public int UpdatePrompt(Movie movie, List<Genre> newGenres, List<Genre> deleteGenres)
        {
            Console.WriteLine($"Movie #{movie.Id}");
            Console.WriteLine($"Title: {movie.Title}");
            Console.WriteLine($"Release Date: {movie.ReleaseDate}");
            Console.WriteLine("Genres: ");
            foreach (var genre in movie.MovieGenres)
            {
                Console.WriteLine(genre.Genre.Name);
            }
            Console.WriteLine("Add Genres: ");
            foreach (var genre in newGenres)
            {
                Console.WriteLine(genre.Name);
            }
            Console.WriteLine("Delete Genres: ");
            foreach (var genre in deleteGenres)
            {
                Console.WriteLine(genre.Name);
            }

            Console.Write("Would you like to update (1)Title, (2)Genres, (3)Release Date, or are you (4)done? ");
            int selection = Convert.ToInt32(Console.ReadLine());

            return selection;
        }
        
        public bool UpdateConfirm(Movie movie, List<Genre> newGenres, List<Genre> deleteGenres)
        {
            Console.WriteLine($"Movie #{movie.Id}");
            Console.WriteLine($"Title: {movie.Title}");
            Console.WriteLine($"Release Date: {movie.ReleaseDate}");
            Console.WriteLine("Genres: ");
            foreach (var genre in movie.MovieGenres)
            {
                Console.WriteLine(genre.Genre.Name);
            }
            Console.WriteLine("Add Genres: ");
            foreach (var genre in newGenres)
            {
                Console.WriteLine(genre.Name);
            }
            Console.WriteLine("Delete Genres: ");
            foreach (var genre in deleteGenres)
            {
                Console.WriteLine(genre.Name);
            }

            Console.Write("Enter 'yes' to confirm these changes: ");
            string confirm = Console.ReadLine();

            if (confirm == "yes")
            {
                return true;
            }

            return false;
        }

        public string TitlePrompt()
        {
            Console.Write("Enter a Title: ");
            string newTitle = Console.ReadLine();

            return newTitle;
        }
        
        public DateTime DatePrompt()
        {
            Console.Write("Enter a Release Date (mm/dd/yyyy): ");
            DateTime newDate = DateTime.Parse(Console.ReadLine());

            return newDate;
        }

        public int GenreOption()
        {
            Console.Write("Would you like to (1)Add or (2)Remove a genre? ");
            int selection = Convert.ToInt32(Console.ReadLine());

            return selection;
        }
        
        public string GenrePrompt()
        {
            Console.Write("Enter a Genre: ");
            string newGenre = Console.ReadLine();

            return newGenre;
        }
        
        public string AddGenrePrompt()
        {
            Console.Write("Enter a Genre or 'done': ");
            string newGenre = Console.ReadLine();

            return newGenre;
        }
        
        public bool AddConfirm(Movie movie, List<Genre> genres)
        {
            Console.WriteLine("New Movie:");
            Console.WriteLine($"Title: {movie.Title}");
            Console.WriteLine($"Release Date: {movie.ReleaseDate}");
            Console.WriteLine("Genres: ");
            foreach (var genre in genres)
            {
                Console.WriteLine(genre.Name);
            }

            Console.Write("Enter 'yes' to add this movie: ");
            string confirm = Console.ReadLine();

            if (confirm == "yes")
            {
                return true;
            }

            return false;
        }
    }
}