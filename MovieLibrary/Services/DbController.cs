using System;
using System.Collections.Generic;
using MovieLibrary.DataModels;
using MovieLibrary.Context;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace MovieLibrary.Services
{
    public class DbController : IController
    {
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
                    Console.WriteLine("Updates Saved!");
                }
                else
                {
                    Console.WriteLine("Update Not Saved!");
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