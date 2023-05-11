using System;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Text.RegularExpressions;
using menuSystem;
using holidayRequestSystem;
using database;
using static menuSystem.Menu;
using wnioski_urlopowe;
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

            string name = " ", surname = " ", startDate = " ", endDate = " ";


            Console.WriteLine("LOGOWANIE");
            while (nameValidation != "OK")
            {
                Console.WriteLine("Podaj imię: ");
                name = Console.ReadLine();

                Console.WriteLine("Podaj nazwisko: ");
                surname = Console.ReadLine();

                holidayRequest.setName(name, surname);
                nameValidation = holidayRequest.isNameValid();
                Console.WriteLine(nameValidation);
            }

            Console.WriteLine(menu.printMenu());
            string choice = string.Empty;
            do
            {
                Console.WriteLine("Wybór: ");
                choice = Console.ReadLine();
            } while (menu.ReadChoice(choice) != MenuChoice.CreateHolidayRequest && menu.ReadChoice(choice) != MenuChoice.showRequests);

            if (choice == "2")
            {
                Console.WriteLine(menu.menuAction(menu.ReadChoice(choice), holidayRequest));
            }
            else
            {
                while (dateValidation != "OK")
                {
                    do
                    {
                        Console.WriteLine("Podaj datę początkową w formacie dd-MM-yyyy: ");
                        startDate = Console.ReadLine();
                    } while (!menu.isDateValid(startDate));

                    do
                    {
                        Console.WriteLine("Podaj datę końcową w formacie dd-MM-yyyy: ");
                        endDate = Console.ReadLine();
                    } while (!menu.isDateValid(endDate));


                    holidayRequest.setDate(menu.convertToDate(startDate), menu.convertToDate(endDate));
                    dateValidation = holidayRequest.isDateValid();
                    Console.WriteLine(dateValidation);
                }
                Console.WriteLine(menu.menuAction(menu.ReadChoice(choice), holidayRequest));
            }
        }
    }
}