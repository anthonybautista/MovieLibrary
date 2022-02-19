// See https://aka.ms/new-console-template for more information
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace MovieLibrary
{
    class Program
    {
        
        class Movie
        {
            public string movieId;
            public string title;
            public string[] genres;

            // Create a class constructor for the Car class
            public Movie(string id, string t, string[] g)
            {
                movieId = id;
                title = t;
                genres = g;
            }
        }

        static void Main(string[] args)
        {
            IServiceCollection serviceCollection = new ServiceCollection();
            var serviceProvider = serviceCollection
                .AddLogging(x => x.AddConsole())
                .BuildServiceProvider();
            var logger = serviceProvider.GetService<ILoggerFactory>().CreateLogger<Program>();

            string file = "movies.csv";
            bool more = true;
            List<Movie> movies = new List<Movie>();
            
            // make sure movie file exists
            if (!File.Exists(file))
            {
                logger.Log(LogLevel.Information, "File does not exist!");
            }
            else
            {
                StreamReader sr = new StreamReader(file);
                int lineNumber = 1;

                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();


                    if (lineNumber > 1)
                    {
                        string[] movieDetails;
                        string[] movieInfo;
                        string id;
                        string t;
                        string[] g;

                        int idx = line.IndexOf('"');
                        if (idx == -1)
                        {
                            // no quote = no comma in movie title
                            // movie details are separated with comma(,)
                            movieDetails = line.Split(',');
                            movieInfo = line.Split(",");
                            id = movieInfo[0];
                            t = movieInfo[1];
                            g = movieInfo[2].Split("|");
                            Movie m = new Movie(id, t, g);

                            movies.Add(m);
                        }
                        else
                        {
                            // quote = comma in movie title
                            // extract the movieId
                            id = line.Substring(0, idx - 1);
                            // remove movieId and first quote from string
                            line = line.Substring(idx + 1);
                            // find the next quote
                            idx = line.IndexOf('"');
                            if (idx == 0)
                            {
                                idx = line.IndexOf(',');
                                // extract the movieTitle
                                t = line.Substring(0, idx);
                            }
                            else
                            {
                                // extract the movieTitle
                                t = line.Substring(0, idx);
                                // remove title and last comma from the string
                                line = line.Substring(idx + 2);
                            }

                            // add genres
                            g = line.Split("|");
                            Movie m = new Movie(id, t, g);

                            movies.Add(m);
                        }

                    }

                    lineNumber++;
                }

                sr.Close();
                /*logger.Log(LogLevel.Information, $"{movies.Count} movies in file.");*/
            }

            try
            {
                do
                {
                    int entry = 0;
                    PrintMenu();
                    entry = Convert.ToInt32(Console.ReadLine());

                    switch (entry)
                    {
                        case 1:
                            AddMovie(movies);
                            break;
                        case 2:
                            DisplayMovies(movies);
                            break;
                        case 3:
                            more = false;
                            break;
                        default:
                            Console.WriteLine("Invalid Entry!");
                            break;
                    }
                } while (more);
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Information, ex.ToString());
            }


            static void PrintMenu()
            {
                Console.WriteLine();
                Console.WriteLine("Movie Library\n" +
                                  "1. Add Movie\n" +
                                  "2. Display All Movies\n" +
                                  "3. Exit\n");
                Console.Write("Select an option: ");
            }

            static void DisplayMovies(List<Movie> movies)
            {
                for (int i = 0; i < movies.Count; i++)
                {
                    string genres = string.Join(",", movies[i].genres);
                    Console.WriteLine($"Movie ID: {movies[i].movieId}");
                    Console.WriteLine($"Title: {movies[i].title}");
                    Console.WriteLine($"Genres: {genres}");
                }
            }

            static void AddMovie(List<Movie> movies)
            {
                IServiceCollection serviceCollection = new ServiceCollection();
                var serviceProvider = serviceCollection
                    .AddLogging(x => x.AddConsole())
                    .BuildServiceProvider();
                var logger = serviceProvider.GetService<ILoggerFactory>().CreateLogger<Program>();
                
                string file = "movies.csv";
                bool moreGenres = true;
                string id;
                string t;
                List<String> g = new List<String>();

                if (!File.Exists(file))
                {
                    logger.Log(LogLevel.Information, "File does not exist!");
                }
                else
                {
                    StreamWriter sw = new StreamWriter(file, true);

                    string newMovie = "";
                    
                    int max = Int32.Parse(movies[movies.Count - 1].movieId) + 1;
                    id = max.ToString();
                    newMovie = newMovie + id + ",";

                    Console.Write("Enter Movie Title: ");
                    t = Console.ReadLine();

                    // check for duplicate. solution found at:
                    // https://www.codegrepper.com/code-examples/csharp/check+if+class+property+already+exists+c%23
                    bool containsItem = movies.Any(item => item.title.ToLower() == t.ToLower());

                    if (containsItem)
                    {
                        Console.WriteLine("That movie has already been entered!");
                    }
                    else
                    {
                        newMovie = newMovie + t;

                        do
                        {
                            Console.Write("Enter movie genres (enter 'done' when finished): ");
                            string genre = Console.ReadLine();
                            if (genre != "done")
                            {
                                newMovie = newMovie + genre + "|";
                                g.Add(genre);
                            }
                            else
                            {
                                moreGenres = false;
                            }
                        } while (moreGenres);
                    }

                    sw.WriteLine();
                    sw.WriteLine(newMovie.Remove(newMovie.Length - 1, 1));
                    sw.Close();

                    Movie m = new Movie(id, t, g.ToArray());
                    movies.Add(m);

                    Console.WriteLine("Movie Added!");
                }
                
            }


        }

    }

}