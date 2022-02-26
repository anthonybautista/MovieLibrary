// See https://aka.ms/new-console-template for more information
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MovieLibrary.Models;

namespace MovieLibrary
{
    class Program
    {
        
        static void Main(string[] args)
        {
            IServiceCollection serviceCollection = new ServiceCollection();
            var serviceProvider = serviceCollection
                .AddLogging(x => x.AddConsole())
                .BuildServiceProvider();
            var loggerFactory = serviceProvider.GetService<ILoggerFactory>();
            var logger = loggerFactory.CreateLogger<Program>();

            string movieFile = "movies.csv";
            string showFile = "shows.csv";
            string videoFile = "videos.csv";
            bool more = true;
            List<Media> movies = new List<Media>();
            List<Media> shows = new List<Media>();
            List<Media> videos = new List<Media>();


            movies = readMovies(movieFile);
            shows = readShows(showFile);
            videos = readVideos(videoFile);
            

            try
            {
                do
                {
                    int entry = 0;
                    PrintMenu();
                    entry = Convert.ToInt32(Console.ReadLine());
                    
                    logger.Log(LogLevel.Information, $"User selected {entry}");
                    loggerFactory.Dispose();

                    if (entry == 3)
                    {
                        more = false;
                    }
                    else
                    {
                        PrintMediaMenu();
                        int mediaSelection = 0;
                        mediaSelection = Convert.ToInt32(Console.ReadLine());

                        logger.Log(LogLevel.Information, $"User selected {mediaSelection}");
                        loggerFactory.Dispose();

                        if (entry == 1)
                        {
                            switch (mediaSelection)
                            {
                                case 1:
                                    AddMovie(movies);
                                    break;
                                case 2:
                                    AddShow(shows);
                                    break;
                                case 3:
                                    AddVideo(videos);
                                    break;
                                default:
                                    Console.WriteLine("Invalid Entry!");
                                    break;
                            }
                        }
                        else
                        {
                            switch (mediaSelection)
                            {
                                case 1:
                                    DisplayMedia(movies);
                                    break;
                                case 2:
                                    DisplayMedia(shows);
                                    break;
                                case 3:
                                    DisplayMedia(videos);
                                    break;
                                default:
                                    Console.WriteLine("Invalid Entry!");
                                    break;
                            }
                        }
                    }

                } while (more);
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Information, ex.ToString());
                loggerFactory.Dispose();
            }


            static void PrintMenu()
            {
                Console.WriteLine();
                Console.WriteLine("Movie Library\n" +
                                  "1. Add Media\n" +
                                  "2. Display Media\n" +
                                  "3. Exit\n");
                Console.Write("Select an option: ");
            }
            
            static void PrintMediaMenu()
            {
                Console.WriteLine();
                Console.WriteLine("Select Media Type\n" +
                                  "1. Movie\n" +
                                  "2. Show\n" +
                                  "3. Video\n");
                Console.Write("Select an option: ");
            }

            static void DisplayMedia(List<Media> mediaList)
            {
                for (int i = 0; i < mediaList.Count; i++)
                {
                    mediaList[i].Display();
                }
            }

            static void AddMovie(List<Media> movies)
            {
                IServiceCollection serviceCollection = new ServiceCollection();
                var serviceProvider = serviceCollection
                    .AddLogging(x => x.AddConsole())
                    .BuildServiceProvider();
                var loggerFactory = serviceProvider.GetService<ILoggerFactory>();
                var logger = loggerFactory.CreateLogger<Program>();
                
                string file = "movies.csv";
                bool moreGenres = true;
                int id;
                string t;
                List<String> g = new List<String>();

                if (!File.Exists(file))
                {
                    logger.Log(LogLevel.Information, "File does not exist!");
                    loggerFactory.Dispose();
                }
                else
                {
                    StreamWriter sw = new StreamWriter(file, true);

                    string newMovie = "";
                    
                    int max = movies[movies.Count - 1].ID + 1;
                    id = max;
                    newMovie = newMovie + id + ",";

                    Console.Write("Enter Movie Title: ");
                    t = Console.ReadLine();

                    // check for duplicate. solution found at:
                    // https://www.codegrepper.com/code-examples/csharp/check+if+class+property+already+exists+c%23
                    bool containsItem = movies.Any(item => item.Title.ToLower() == t.ToLower());

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

                    Media m = new Movie(id, t, g.ToArray());
                    movies.Add(m);

                    logger.Log(LogLevel.Information, $"Movie, {t}, Added!");
                    loggerFactory.Dispose();
                }
                
            }
            
            static void AddShow(List<Media> shows)
            {
                IServiceCollection serviceCollection = new ServiceCollection();
                var serviceProvider = serviceCollection
                    .AddLogging(x => x.AddConsole())
                    .BuildServiceProvider();
                var loggerFactory = serviceProvider.GetService<ILoggerFactory>();
                var logger = loggerFactory.CreateLogger<Program>();
                
                string file = "shows.csv";
                bool moreWriters = true;
                int id = 0;
                string t;
                int s = 0;
                int e = 0;
                List<String> w = new List<String>();

                if (!File.Exists(file))
                {
                    logger.Log(LogLevel.Information, "File does not exist!");
                    loggerFactory.Dispose();
                }
                else
                {
                    StreamWriter sw = new StreamWriter(file, true);

                    string newShow = "";
                    
                    int max = shows[shows.Count - 1].ID + 1;
                    id = max;
                    newShow = newShow + id + ",";

                    Console.Write("Enter Show Title: ");
                    t = Console.ReadLine();

                    // check for duplicate. solution found at:
                    // https://www.codegrepper.com/code-examples/csharp/check+if+class+property+already+exists+c%23
                    bool containsItem = shows.Any(item => item.Title.ToLower() == t.ToLower());

                    if (containsItem)
                    {
                        Console.WriteLine("That show has already been entered!");
                    }
                    else
                    {
                        newShow = newShow + t + ",";
                        
                        Console.Write("Enter Show Season: ");
                        s = Convert.ToInt32(Console.ReadLine());
                        
                        newShow = newShow + s + ",";
                        
                        Console.Write("Enter Show Episode: ");
                        e = Convert.ToInt32(Console.ReadLine());
                        
                        newShow = newShow + s + ",";

                        do
                        {
                            Console.Write("Enter show writers (enter 'done' when finished): ");
                            string writer = Console.ReadLine();
                            if (writer != "done")
                            {
                                newShow = newShow + writer + "|";
                                w.Add(writer);
                            }
                            else
                            {
                                moreWriters = false;
                            }
                        } while (moreWriters);
                    }

                    sw.WriteLine();
                    sw.WriteLine(newShow.Remove(newShow.Length - 1, 1));
                    sw.Close();

                    Media show = new Show(id, t, s, e, w.ToArray());
                    shows.Add(show);

                    logger.Log(LogLevel.Information, $"Show, {t}, Added!");
                    loggerFactory.Dispose();
                }
                
            }

            static void AddVideo(List<Media> videos)
            {
                IServiceCollection serviceCollection = new ServiceCollection();
                var serviceProvider = serviceCollection
                    .AddLogging(x => x.AddConsole())
                    .BuildServiceProvider();
                var loggerFactory = serviceProvider.GetService<ILoggerFactory>();
                var logger = loggerFactory.CreateLogger<Program>();
                
                string file = "videos.csv";
                bool moreRegions = true;
                int id = 0;
                string t;
                string f = "";
                int l = 0;
                List<Int32> r = new List<Int32>();

                if (!File.Exists(file))
                {
                    logger.Log(LogLevel.Information, "File does not exist!");
                    loggerFactory.Dispose();
                }
                else
                {
                    StreamWriter sw = new StreamWriter(file, true);

                    string newVideo = "";
                    
                    int max = videos[videos.Count - 1].ID + 1;
                    id = max;
                    newVideo = newVideo + id + ",";

                    Console.Write("Enter Video Title: ");
                    t = Console.ReadLine();

                    // check for duplicate. solution found at:
                    // https://www.codegrepper.com/code-examples/csharp/check+if+class+property+already+exists+c%23
                    bool containsItem = videos.Any(item => item.Title.ToLower() == t.ToLower());

                    if (containsItem)
                    {
                        Console.WriteLine("That video has already been entered!");
                    }
                    else
                    {
                        newVideo = newVideo + t + ",";
                        
                        Console.Write("Enter Video Format: ");
                        f = Console.ReadLine();
                        
                        newVideo = newVideo + f + ",";
                        
                        Console.Write("Enter Video Length: ");
                        l = Convert.ToInt32(Console.ReadLine());
                        
                        newVideo = newVideo + l + ",";

                        do
                        {
                            Console.Write("Enter movie regions (enter 'done' when finished): ");
                            string region = Console.ReadLine();
                            if (region != "done")
                            {
                                newVideo = newVideo + region + "|";
                                r.Add(Int32.Parse(region));
                            }
                            else
                            {
                                moreRegions = false;
                            }
                        } while (moreRegions);
                    }

                    sw.WriteLine();
                    sw.WriteLine(newVideo.Remove(newVideo.Length - 1, 1));
                    sw.Close();

                    Media v = new Video(id, t, f, l, r.ToArray());
                    videos.Add(v);

                    logger.Log(LogLevel.Information, $"Video, {t}, Added!");
                    loggerFactory.Dispose();
                }
                
            }

        }

        static List<Media> readMovies(string movieFile)
        {
            IServiceCollection serviceCollection = new ServiceCollection();
            var serviceProvider = serviceCollection
                .AddLogging(x => x.AddConsole())
                .BuildServiceProvider();
            var loggerFactory = serviceProvider.GetService<ILoggerFactory>();
            var logger = loggerFactory.CreateLogger<Program>();
            
            List<Media> movies = new List<Media>();
            
            // make sure movie file exists
            if (!File.Exists(movieFile))
            {
                logger.Log(LogLevel.Information, "File does not exist!");
                loggerFactory.Dispose();
            }
            else
            {
                StreamReader sr = new StreamReader(movieFile);
                int lineNumber = 1;

                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();

                    if (lineNumber > 1)
                    {

                        string[] movieInfo;
                        int id;
                        string t;
                        string[] g;

                        int idx = line.IndexOf('"');
                        if (idx == -1)
                        {
                            // no quote = no comma in movie title
                            movieInfo = line.Split(",");
                            id = Int32.Parse(movieInfo[0]);
                            t = movieInfo[1];
                            g = movieInfo[2].Split("|");
                            Media m = new Movie(id, t, g);

                            movies.Add(m);
                        }
                        else
                        {
                            // quote = comma in movie title
                            // extract the movieId
                            id = Int32.Parse(line.Substring(0, idx - 1));
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
                logger.Log(LogLevel.Information, $"{movies.Count} movies in file.");
                loggerFactory.Dispose();
            }

            return movies;
        }
        
        static List<Media> readShows(string showFile)
        {
            IServiceCollection serviceCollection = new ServiceCollection();
            var serviceProvider = serviceCollection
                .AddLogging(x => x.AddConsole())
                .BuildServiceProvider();
            var loggerFactory = serviceProvider.GetService<ILoggerFactory>();
            var logger = loggerFactory.CreateLogger<Program>();
            
            List<Media> shows = new List<Media>();
            
            // make sure show file exists
            if (!File.Exists(showFile))
            {
                logger.Log(LogLevel.Information, "File does not exist!");
                loggerFactory.Dispose();
            }
            else
            {
                StreamReader sr = new StreamReader(showFile);
                int lineNumber = 1;

                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();

                    if (lineNumber > 1)
                    {
                        string[] showInfo;
                        int id;
                        string t;
                        int s;
                        int e;
                        string[] w;

                        // no quote = no comma in movie title
                        showInfo = line.Split(",");
                        id = Int32.Parse(showInfo[0]);
                        t = showInfo[1];
                        s = Int32.Parse(showInfo[2]);
                        e = Int32.Parse(showInfo[3]);
                        w = showInfo[4].Split("|");
                        Media show = new Show(id, t, s, e, w);

                        shows.Add(show);
                    }

                    lineNumber++;
                }

                sr.Close();
                logger.Log(LogLevel.Information, $"{shows.Count} shows in file.");
                loggerFactory.Dispose();
            }

            return shows;
        }
        
        static List<Media> readVideos(string videoFile)
        {
            IServiceCollection serviceCollection = new ServiceCollection();
            var serviceProvider = serviceCollection
                .AddLogging(x => x.AddConsole())
                .BuildServiceProvider();
            var loggerFactory = serviceProvider.GetService<ILoggerFactory>();
            var logger = loggerFactory.CreateLogger<Program>();
            
            List<Media> videos = new List<Media>();
            
            // make sure video file exists
            if (!File.Exists(videoFile))
            {
                logger.Log(LogLevel.Information, "File does not exist!");
                loggerFactory.Dispose();
            }
            else
            {
                StreamReader sr = new StreamReader(videoFile);
                int lineNumber = 1;

                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();

                    if (lineNumber > 1)
                    {
                        string[] videoInfo;
                        int id;
                        string t;
                        string f;
                        int l;
                        string[] regions;
                        List<Int32> r = new List<Int32>();

                        // no quote = no comma in movie title
                        videoInfo = line.Split(",");
                        id = Int32.Parse(videoInfo[0]);
                        t = videoInfo[1];
                        f = videoInfo[2];
                        l = Int32.Parse(videoInfo[3]);
                        regions = videoInfo[4].Split("|");
                        for (int i = 0; i < regions.Length; i++)
                        {
                            r.Add(Int32.Parse(regions[i]));
                        }
                        Media video = new Video(id, t, f, l, r.ToArray());

                        videos.Add(video);
                    }

                    lineNumber++;
                }

                sr.Close();
                logger.Log(LogLevel.Information, $"{videos.Count} videos in file.");
                loggerFactory.Dispose();
            }

            return videos;
        }

    }

}