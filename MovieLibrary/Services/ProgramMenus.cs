using System;

namespace MovieLibrary.Services;

public class ProgramMenus : IMenu
{
    public void MainMenu()
    {
        Console.WriteLine();
        Console.WriteLine("Movie Library\n" +
                          "1. Add Movie\n" +
                          "2. Update Movie\n" +
                          "3. Delete Movie\n" +
                          "4. Search\n" +
                          "5. Display All Movies\n" +
                          "6. User Menu\n" +
                          "7. Exit");
        Console.Write("Select an option: ");
    }
    
    public void UserMenu()
    {
        Console.WriteLine();
        Console.WriteLine("User Menu\n" +
                          "1. Add User\n" +
                          "2. Add Review\n" +
                          "3. Top Movies by Occupation\n" +
                          "4. Exit");
        Console.Write("Select an option: ");
    }
}