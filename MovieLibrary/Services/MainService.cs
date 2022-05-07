using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MovieLibrary.Context;
using MovieLibrary.Models;
using NLog;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;

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
        private readonly IMenu _menu;
        private Logger logger = LogManager.GetCurrentClassLogger();

      /*  public MainService(IRepository fileService)
        {
            _fileService = fileService;
        } */
        
        public MainService(IController dbService, IMenu programMenus)
        {
            _dbService = dbService;
            _menu = programMenus;
        }

        public void Invoke()
        {

            bool more = true;

            try
            {
                do
                {
                    int entry = 0;
                    _menu.MainMenu();
                    try
                    {
                        entry = Convert.ToInt32(Console.ReadLine());
                        logger.Info($"User selected {entry}");
                    }
                    catch (Exception e)
                    {
                        logger.Info(e.ToString());
                    }

                    if (entry == 7)
                    {
                        more = false;
                    } 
                    else if (entry == 6)
                    {
                        bool moreTwo = true;
                        do 
                        {
                            _menu.UserMenu();
                            try
                            {
                                entry = Convert.ToInt32(Console.ReadLine());
                                logger.Info($"User selected {entry}");
                            }
                            catch (Exception e)
                            {
                                logger.Info(e.ToString());
                            }
        
                            if (entry == 4)
                            {
                                moreTwo = false;
                            }
                            else if (entry == 3)
                            {
                                _dbService.TopByOccupation();
                            }
                            else if (entry == 2)
                            {
                                Console.Write("Enter a User ID: ");
                                int userID = Convert.ToInt32(Console.ReadLine());
                                Console.Write("Enter a Movie ID to rate: ");
                                int movieID = Convert.ToInt32(Console.ReadLine());
                                _dbService.AddReview(userID, movieID);
        
                            } 
                            else if (entry == 1)
                            {
                                _dbService.AddUser();
                            }
                            else
                            {
                                Console.WriteLine("Invalid Selection.");
                            }
        
                        } while (moreTwo);
                    }
                    else if (entry == 5)
                    {
                        _dbService.DisplayAll();
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
                logger.Info(ex.ToString());
            }
            
        }
    }
}