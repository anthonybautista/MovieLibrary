using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MovieLibrary.Models;

namespace MovieLibrary.Services
{

    /// <summary>
    ///     You would need to inject your interfaces here to execute the methods in Invoke()
    ///     See the commented out code as an example
    /// </summary>
    public class MainService : IMainService
    {
        private readonly IRepository _fileService;

        public MainService(IRepository fileService)
        {
            _fileService = fileService;
        }

        public void Invoke()
        {
            IServiceCollection serviceCollection = new ServiceCollection();
            var serviceProvider = serviceCollection
                .AddLogging(x => x.AddConsole())
                .BuildServiceProvider();
            var loggerFactory = serviceProvider.GetService<ILoggerFactory>();
            var logger = loggerFactory.CreateLogger<Program>();

            string movieFile = "moviesJson.txt";
            string showFile = "showsJson.txt";
            string videoFile = "videosJson.txt";
            bool more = true;
            List<Media> movies = _fileService.GetAll("movie", movieFile);
            List<Media> shows = _fileService.GetAll("show", showFile);
            List<Media> videos = _fileService.GetAll("video", videoFile);

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
                                    movies = _fileService.Add("movie",movieFile, movies);
                                    break;
                                case 2:
                                    shows = _fileService.Add("show", showFile, shows);;
                                    break;
                                case 3:
                                    videos = _fileService.Add("video",videoFile, videos);;
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
                                    _fileService.DisplayAll(movies);
                                    break;
                                case 2:
                                    _fileService.DisplayAll(shows);
                                    break;
                                case 3:
                                    _fileService.DisplayAll(videos);
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
        }
    }
}