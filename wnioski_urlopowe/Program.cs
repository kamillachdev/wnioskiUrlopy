using System;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Text.RegularExpressions;
using menuSystem;
using holidayRequestSystem;
using database;
using static menuSystem.Menu;

namespace Program
{
    public class holidayManegement
    {        
        static void Main()
        {
            Menu menu = new Menu();
            Console.WriteLine(menu.printMenu());

            Console.WriteLine("Wybór: ");
            string choice = Console.ReadLine();

            Console.WriteLine("Podaj imię: ");
            string name = Console.ReadLine();

            Console.WriteLine("Podaj nazwisko: ");
            string surname = Console.ReadLine();

            string startDate = "";
            string endDate = "";

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

            string validation = HolidayRequest.isValid(name, surname, menu.convertToDate(startDate), menu.convertToDate(endDate));
            if (validation == "OK")
            {
                HolidayRequest holidayRequest = new HolidayRequest(name, surname, menu.convertToDate(startDate), menu.convertToDate(endDate));
                Console.WriteLine(menu.menuAction(menu.ReadChoice(choice), holidayRequest));
            }
            else
            {
                Console.WriteLine(validation);

            }
        }
    }
}