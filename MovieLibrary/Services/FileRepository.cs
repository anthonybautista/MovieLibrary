using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MovieLibrary.Models;

namespace MovieLibrary.Services
{
    public class FileRepository : IRepository
    {
        public List<Media> Add(string type, string file, List<Media> mediaList)
        {
            IServiceCollection serviceCollection = new ServiceCollection();
            var serviceProvider = serviceCollection
                .AddLogging(x => x.AddConsole())
                .BuildServiceProvider();
            var loggerFactory = serviceProvider.GetService<ILoggerFactory>();
            var logger = loggerFactory.CreateLogger<Program>();

            switch (type)
            {
                case "movie":
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
            
                        int max = mediaList[mediaList.Count - 1].ID + 1;
                        id = max;
                        newMovie = newMovie + id + ",";

                        Console.Write("Enter Movie Title: ");
                        t = Console.ReadLine();

                        // check for duplicate. solution found at:
                        // https://www.codegrepper.com/code-examples/csharp/check+if+class+property+already+exists+c%23
                        bool containsItem = mediaList.Any(item => item.Title.ToLower() == t.ToLower());

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
                        mediaList.Add(m);

                        logger.Log(LogLevel.Information, $"Movie, {t}, Added!");
                        loggerFactory.Dispose();
                    }
                    break;
                case "show":
                    bool moreWriters = true;
                    int showId = 0;
                    string sT;
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
                        
                        int max = mediaList[mediaList.Count - 1].ID + 1;
                        showId = max;
                        newShow = newShow + showId + ",";

                        Console.Write("Enter Show Title: ");
                        sT = Console.ReadLine();

                        // check for duplicate. solution found at:
                        // https://www.codegrepper.com/code-examples/csharp/check+if+class+property+already+exists+c%23
                        bool containsItem = mediaList.Any(item => item.Title.ToLower() == sT.ToLower());

                        if (containsItem)
                        {
                            Console.WriteLine("That show has already been entered!");
                        }
                        else
                        {
                            newShow = newShow + sT + ",";
                            
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

                        Media show = new Show(showId, sT, s, e, w.ToArray());
                        mediaList.Add(show);

                        logger.Log(LogLevel.Information, $"Show, {sT}, Added!");
                        loggerFactory.Dispose();
                    }
                    break;
                case "video":
                    bool moreRegions = true;
                    int videoId = 0;
                    string vT;
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
                        
                        int max = mediaList[mediaList.Count - 1].ID + 1;
                        videoId = max;
                        newVideo = newVideo + videoId + ",";

                        Console.Write("Enter Video Title: ");
                        vT = Console.ReadLine();

                        // check for duplicate. solution found at:
                        // https://www.codegrepper.com/code-examples/csharp/check+if+class+property+already+exists+c%23
                        bool containsItem = mediaList.Any(item => item.Title.ToLower() == vT.ToLower());

                        if (containsItem)
                        {
                            Console.WriteLine("That video has already been entered!");
                        }
                        else
                        {
                            newVideo = newVideo + vT + ",";
                            
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

                        Media v = new Video(videoId, vT, f, l, r.ToArray());
                        mediaList.Add(v);

                        logger.Log(LogLevel.Information, $"Video, {vT}, Added!");
                        loggerFactory.Dispose();
                    }
                    break;
                default:
                    logger.Log(LogLevel.Information, $"Invalid Media Type!");
                    loggerFactory.Dispose();
                    break;
            }

            return mediaList;

        }

        public List<Media> GetAll(string type, string file)
        {
            IServiceCollection serviceCollection = new ServiceCollection();
            var serviceProvider = serviceCollection
                .AddLogging(x => x.AddConsole())
                .BuildServiceProvider();
            var loggerFactory = serviceProvider.GetService<ILoggerFactory>();
            var logger = loggerFactory.CreateLogger<Program>();

            List<Media> media = new List<Media>();

            switch (type)
            {
                case "movie":
                    if (!File.Exists(file))
                    {
                        logger.Log(LogLevel.Information, "File does not exist!");
                        loggerFactory.Dispose();
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

                                    media.Add(m);
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

                                    media.Add(m);
                                }

                            }

                            lineNumber++;
                        }

                        sr.Close();
                        logger.Log(LogLevel.Information, $"{media.Count} movies in file.");
                        loggerFactory.Dispose();
                    }
                    break;
                case "show":
                    if (!File.Exists(file))
                    {
                        logger.Log(LogLevel.Information, "File does not exist!");
                        loggerFactory.Dispose();
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

                                media.Add(show);
                            }

                            lineNumber++;
                        }

                        sr.Close();
                        logger.Log(LogLevel.Information, $"{media.Count} shows in file.");
                        loggerFactory.Dispose();
                    }
                    break;
                case "video":
                    if (!File.Exists(file))
                    {
                        logger.Log(LogLevel.Information, "File does not exist!");
                        loggerFactory.Dispose();
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

                                media.Add(video);
                            }

                            lineNumber++;
                        }

                        sr.Close();
                        logger.Log(LogLevel.Information, $"{media.Count} videos in file.");
                        loggerFactory.Dispose();
                    }
                    break;
                default:
                    logger.Log(LogLevel.Information, $"Invalid Media Type!");
                    loggerFactory.Dispose();
                    break;
            }

            return media;
        }

        public void DisplayAll(List<Media> mediaList)
        {
            foreach (var media in mediaList)
            {
                media.Display();
            }
        }
        
    }
}