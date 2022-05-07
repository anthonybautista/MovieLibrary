using System;
using System.Collections.Generic;
using MovieLibrary.DataModels;
using MovieLibrary.Context;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using NLog;
using NLog.Config;
using NLog.Targets;

namespace MovieLibrary.Services
{
    public class DbController : IController
    {

        private Logger log = LogManager.GetCurrentClassLogger();
        public void DisplayAll()
        {

            using (var db = new MovieContext())
            {
                var movies = db.Movies.Select(movie => movie).Include("MovieGenres.Genre").ToList();

                foreach (var movie in movies)
                {
                    Console.WriteLine("Movie ID: " + movie.Id);
                    Console.WriteLine("Title: " + movie.Title);
                    Console.WriteLine("Released: " + movie.ReleaseDate);
                    string g = "Genres: ";
                    int i = 1;

                    foreach (var genre in movie.MovieGenres)
                    {
                        if (i == 1)
                        {
                            g = g + genre.Genre.Name;
                        }
                        else
                        {
                            g = g + ", " + genre.Genre.Name;
                        }

                        i++;
                    }

                    Console.WriteLine(g);
                }
            }

        }

        public void AddUser()
        {
            using (var db = new MovieContext())
            {
                User user = new();
                DbMenu m = new();

                user.Age = m.AgePrompt();
                user.Gender = m.GenderPrompt();
                user.ZipCode = m.ZipPrompt();
                
                string occupation = m.OccupationPrompt();
                
                var occupationMatch = db.Occupations.FirstOrDefault(x => x.Name.ToLower() == occupation.ToLower());
                if (occupationMatch == null)
                {
                    Occupation o = new();
                    o.Name = occupation;
                    db.Occupations.Add(o);
                    user.Occupation = o;
                }
                else
                {
                    user.Occupation = occupationMatch;
                }

                bool confirm = m.AddUserConfirm(user);

                if (confirm)
                {
                    db.Users.Add(user);
                    db.SaveChanges();
                    log.Info("User Added, ID# " + user.Id);
                }
            }
        }

        public void AddReview(int userID, int movieID)
        {
            using (var db = new MovieContext())
            {
                var user = db.Users.FirstOrDefault(x => x.Id == userID);
                var movie = db.Movies.FirstOrDefault(x => x.Id == movieID);

                UserMovie review = new();
                DbMenu m = new();

                review.Rating = m.RatingPrompt();
                review.RatedAt = DateTime.Now;
                review.Movie = movie;
                review.User = user;
                
                bool confirm = m.AddReviewConfirm(review);

                if (confirm)
                {
                    db.UserMovies.Add(review);
                    db.SaveChanges();
                    log.Info("Review Added For: " + review.Movie.Title);
                }
            }
        }

        public void TopByOccupation()
        {
            using (var db = new MovieContext())
            {
                var occupations = db.Occupations.Select(occ => occ)
                                               .OrderBy(occ => occ.Name).ToList();

                foreach (var occupation in occupations)
                {
                    bool valid = true;
                    UserMovie topRated = new UserMovie();
                    try
                    {
                        topRated = db.UserMovies.Where(um => um.User.Occupation == occupation)
                            .OrderByDescending(um => um.Rating).ThenBy(um => um.Movie.Title).First();
                    }
                    catch (InvalidOperationException e)
                    {
                        Console.WriteLine(e.Message);
                        valid = false;
                    }

                    if (valid)
                    {
                        Console.WriteLine("Top Rated Movie For Occupation: " + occupation.Name);
                        Console.WriteLine(topRated.Movie.Title);
                        Console.WriteLine("Rating: " + topRated.Rating);
                        Console.WriteLine("---------------------");
                    }
                    else
                    {
                        Console.WriteLine("Top Rated Movie For Occupation: " + occupation.Name);
                        Console.WriteLine("No ratings yet.");
                        Console.WriteLine("---------------------");
                    }
                    
                
                    
                }
                
            }

        }
        

        public void Search(string searchString)
        {

            using (var db = new MovieContext())
            {
                var movies = db.Movies.Where(x => x.Title.ToLower()
                        .Contains(searchString.ToLower()))
                    .Include("MovieGenres.Genre").ToList();

                foreach (var movie in movies)
                {
                    Console.WriteLine("Movie ID: " + movie.Id);
                    Console.WriteLine("Title: " + movie.Title);
                    Console.WriteLine("Released: " + movie.ReleaseDate);
                    string g = "Genres: ";
                    int i = 1;

                    foreach (var genre in movie.MovieGenres)
                    {
                        if (i == 1)
                        {
                            g = g + genre.Genre.Name;
                        }
                        else
                        {
                            g = g + ", " + genre.Genre.Name;
                        }

                        i++;
                    }

                    Console.WriteLine(g);
                }
            }

        }

        public void Add()
        {
            using (var db = new MovieContext())
            {
                Movie movie = new();
                DbMenu m = new();
                List<Genre> genres = new();
                bool more = true;

                movie.Title = m.TitlePrompt();
                movie.ReleaseDate = m.DatePrompt();

                while (more)
                {
                    string g = m.AddGenrePrompt();
                    if (g == "done")
                    {
                        more = false;
                    }
                    else
                    {
                        var genre = db.Genres.FirstOrDefault(x => x.Name == g);
                        if (genre == null)
                        {
                            Console.WriteLine("Invalid Genre!");
                        }
                        else
                        {
                            genres.Add(genre);
                        }
                    }
                }

                bool confirm = m.AddConfirm(movie, genres);

                if (confirm)
                {
                    db.Movies.Add(movie);
                    foreach (var genre in genres)
                    {
                        var mg = new MovieGenre();
                        mg.Genre = genre;
                        mg.Movie = movie;
                        db.MovieGenres.Add(mg);
                    }

                    db.SaveChanges();
                    log.Info("Movie Added: " + movie.Title);
                }
            }
        }

        public void Update(int movieID)
        {
            using (var db = new MovieContext())
            {
                var search = db.Movies.Where(x => x.Id
                    .Equals(movieID)).Include("MovieGenres").Include("UserMovies").ToList();

                Movie movie = search[0];
                List<Genre> newGenres = new List<Genre>();
                List<Genre> deleteGenres = new List<Genre>();
                    
                DbMenu m = new DbMenu();
                int selection;

                do
                {
                    selection = m.UpdatePrompt(movie, newGenres, deleteGenres);
                    if (selection == 1)
                    {
                        movie.Title = m.TitlePrompt();
                    }
                    else if (selection == 2)
                    {
                        int genreOption = m.GenreOption();

                        if (genreOption == 1)
                        {
                            string newGenre = m.GenrePrompt();
                            var genre = db.Genres.FirstOrDefault(x => x.Name == newGenre);
                            if (genre == null)
                            {
                                Console.WriteLine("Invalid Genre!");
                            }
                            else
                            {
                                newGenres.Add(genre);
                            }
                        } else if (genreOption == 2)
                        {
                            string deleteGenre = m.GenrePrompt();
                            var genre = db.Genres.FirstOrDefault(x => x.Name == deleteGenre);
                            if (genre == null)
                            {
                                Console.WriteLine("Invalid Genre!");
                            }
                            else
                            {
                                deleteGenres.Add(genre);
                            }
                        }
                    } 
                    else if (selection == 3)
                    {
                        DateTime releaseDate = m.DatePrompt();
                        movie.ReleaseDate = releaseDate;
                    }
                } while (selection != 4);

                bool confirm = m.UpdateConfirm(movie, newGenres, deleteGenres);
                if (confirm)
                {
                    foreach (var genre in newGenres)
                    {
                        var mg = new MovieGenre();
                        mg.Genre = genre;
                        mg.Movie = movie;
                        db.MovieGenres.Add(mg);
                    }
                    foreach (var genre in deleteGenres)
                    {
                        var mg = db.MovieGenres.FirstOrDefault(x => x.Movie == movie && x.Genre == genre);
                        if (mg == null)
                        {
                            Console.WriteLine("Genre invalid or not applicable?");
                        }
                        else
                        {
                            db.Remove(mg);
                        }
                    }
                    db.Movies.Update(movie);
                    db.SaveChanges();
                    log.Info("Updates Saved!");
                }
                else
                {
                    log.Info("Update Not Saved!");
                }
            }
        }

        public void Delete(int movieID)
        {
            using (var db = new MovieContext())
            {
                var search = db.Movies.Where(x => x.Id
                    .Equals(movieID)).Include("MovieGenres").Include("UserMovies").ToList();

                Movie movie = search[0];
                    
                DbMenu m = new DbMenu();
                bool confirm = m.DeletePrompt(movie.Title);
                if (confirm)
                {
                    foreach (var mg in movie.MovieGenres)
                    {
                        db.Remove(mg);
                    }
                    foreach (var um in movie.UserMovies)
                    {
                        db.Remove(um);
                    }
                    db.Movies.Remove(movie);
                    db.SaveChanges();
                }
            }
        }
    }
}