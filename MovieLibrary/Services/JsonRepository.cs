using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MovieLibrary.Models;
using Newtonsoft.Json;
using System.Linq;

namespace MovieLibrary.Services
{
    public class JsonRepository : IRepository
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

                        int max = mediaList[mediaList.Count - 1].ID + 1;
                        id = max;

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

                            do
                            {
                                Console.Write("Enter movie genres (enter 'done' when finished): ");
                                string genre = Console.ReadLine();
                                if (genre != "done")
                                {
                                    g.Add(genre);
                                }
                                else
                                {
                                    moreGenres = false;
                                }
                            } while (moreGenres);
                        }
                        
                        Media m = new Movie(id, t, g.ToArray());
                        mediaList.Add(m);
                        
                        string json = JsonConvert.SerializeObject(m);

                        sw.WriteLine();
                        sw.WriteLine(json);
                        sw.Close();

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
                        
                        int max = mediaList[mediaList.Count - 1].ID + 1;
                        showId = max;

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
                            
                            Console.Write("Enter Show Season: ");
                            s = Convert.ToInt32(Console.ReadLine());
                            
                            Console.Write("Enter Show Episode: ");
                            e = Convert.ToInt32(Console.ReadLine());

                            do
                            {
                                Console.Write("Enter show writers (enter 'done' when finished): ");
                                string writer = Console.ReadLine();
                                if (writer != "done")
                                {
                                    w.Add(writer);
                                }
                                else
                                {
                                    moreWriters = false;
                                }
                            } while (moreWriters);
                        }
                        
                        Media show = new Show(showId, sT, s, e, w.ToArray());
                        mediaList.Add(show);
                        
                        string json = JsonConvert.SerializeObject(show);

                        sw.WriteLine();
                        sw.WriteLine(json);
                        sw.Close();

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

                        int max = mediaList[mediaList.Count - 1].ID + 1;
                        videoId = max;

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
                            
                            Console.Write("Enter Video Format: ");
                            f = Console.ReadLine();
                            
                            Console.Write("Enter Video Length: ");
                            l = Convert.ToInt32(Console.ReadLine());

                            do
                            {
                                Console.Write("Enter movie regions (enter 'done' when finished): ");
                                string region = Console.ReadLine();
                                if (region != "done")
                                {
                                    r.Add(Int32.Parse(region));
                                }
                                else
                                {
                                    moreRegions = false;
                                }
                            } while (moreRegions);
                        }

                        Media v = new Video(videoId, vT, f, l, r.ToArray());
                        mediaList.Add(v);
                        
                        string json = JsonConvert.SerializeObject(v);
                        
                        sw.WriteLine();
                        sw.WriteLine(json);
                        sw.Close();

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
                            
                            Movie m = JsonConvert.DeserializeObject<Movie>(line);

                            media.Add(m);
                            
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
                            
                            Show s = JsonConvert.DeserializeObject<Show>(line);
                            
                            media.Add(s);
                            
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
                            
                            Video v = JsonConvert.DeserializeObject<Video>(line);

                            media.Add(v);

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