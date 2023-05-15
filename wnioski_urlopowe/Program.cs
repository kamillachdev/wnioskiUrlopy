using System;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Text.RegularExpressions;
using menuSystem;
using holidayRequestSystem;
using database;
using static menuSystem.Menu;
using MySqlX.XDevAPI.Common;

namespace Program
{
    public class holidayManegement
    {
        static void Main()
        {
            Menu menu = new Menu();
            string nameValidation = "", dateValidation = "";
            HolidayRequest holidayRequest = new HolidayRequest();

            string name = " ", surname = " ";


            Console.WriteLine(menu.printMessages(Messages.startLogin));
            while (nameValidation != "OK")
            {
                Console.WriteLine(menu.printMessages(Messages.insertName));
                name = Console.ReadLine();

                Console.WriteLine(menu.printMessages(Messages.insertSurname));
                surname = Console.ReadLine();

                holidayRequest.setName(name, surname);
                nameValidation = holidayRequest.isNameValid();
                Console.WriteLine(nameValidation);
            }

            while (true)
            {
                Console.WriteLine(menu.printMenu());
                string choice = " ";
                do
                {
                    Console.WriteLine(menu.printMessages(Messages.insertChoice));
                    choice = Console.ReadLine();
                } while (menu.ReadChoice(choice) != MenuChoice.CreateHolidayRequest && menu.ReadChoice(choice) != MenuChoice.showRequests && menu.ReadChoice(choice) != MenuChoice.LogOut);
                
                if(menu.ReadChoice(choice) == MenuChoice.CreateHolidayRequest)
                {
                    
                    string startDateString, endDateString;
                    DateTime startDate, endDate;

                    do
                    {
                        Console.WriteLine(menu.printMessages(Messages.insertStartDate));
                        startDateString = Console.ReadLine();
                    } while (!DateTime.TryParseExact(startDateString, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out startDate));

                    do
                    {
                        Console.WriteLine(menu.printMessages(Messages.insertEndDate));
                        endDateString = Console.ReadLine();
                    } while (!DateTime.TryParseExact(endDateString, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out endDate));


                    holidayRequest.setDate(startDate, endDate);
                }
                Console.WriteLine(menu.menuAction(menu.ReadChoice(choice), holidayRequest));
            }
        }
    }
}