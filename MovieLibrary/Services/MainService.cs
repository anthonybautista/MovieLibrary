using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MovieLibrary.Context;
using MovieLibrary.Models;

namespace MovieLibrary.Services
{

    /// <summary>
    ///     You would need to inject your interfaces here to execute the methods in Invoke()
    ///     See the commented out code as an example
    /// </summary>
    public class MainService : IMainService
    {
        //private readonly IRepository _fileService;
        private readonly IController _dbService;

      /*  public MainService(IRepository fileService)
        {
            _fileService = fileService;
        } */
        
        public MainService(IController dbService)
        {
            _dbService = dbService;
        }

        public void Invoke()
        {
            IServiceCollection serviceCollection = new ServiceCollection();
            var serviceProvider = serviceCollection
                .AddLogging(x => x.AddConsole())
                .BuildServiceProvider();
            var loggerFactory = serviceProvider.GetService<ILoggerFactory>();
            var logger = loggerFactory.CreateLogger<Program>();

            bool more = true;

            try
            {
                do
                {
                    int entry = 0;
                    PrintMenu();
                    entry = Convert.ToInt32(Console.ReadLine());
                    
                    logger.Log(LogLevel.Information, $"User selected {entry}");
                    loggerFactory.Dispose();

                    if (entry == 5)
                    {
                        more = false;
                    }
                    else if (entry == 4)
                    {
                        Console.Write("Enter a word to search: ");
                        string searchString = Console.ReadLine();
                        _dbService.Search(searchString);

                    } 
                    else if (entry == 3)
                    {
                        Console.Write("Enter a Movie ID to delete: ");
                        int movieID = Convert.ToInt32(Console.ReadLine());
                        _dbService.Delete(movieID);
                    }
                    else if (entry == 2)
                    {
                        Console.Write("Enter a Movie ID to update: ");
                        int movieID = Convert.ToInt32(Console.ReadLine());
                        _dbService.Update(movieID);
                    }
                    else if (entry == 1)
                    {
                        _dbService.Add();
                    }
                    else
                    {
                        Console.WriteLine("Invalid Selection.");
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
                                  "1. Add Movie\n" +
                                  "2. Update Movie\n" +
                                  "3. Delete Movie\n" +
                                  "4. Search\n" +
                                  "5. Exit\n");
                Console.Write("Select an option: ");
            }
            
        }
    }
}