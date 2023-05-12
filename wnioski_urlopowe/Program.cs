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

        public static (DateTime start, DateTime end) GetHolidayDates()
        {
            Menu menu = new Menu();
            string startDateString, endDateString;
            DateTime startDate, endDate;

            do
            {
                Console.WriteLine(menu.PrintNextStep(4));
                startDateString = Console.ReadLine();
            } while (!DateTime.TryParseExact(startDateString, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out startDate));

            do
            {
                Console.WriteLine(menu.PrintNextStep(5));
                endDateString = Console.ReadLine();
            } while (!DateTime.TryParseExact(endDateString, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out endDate));

            return (startDate, endDate);
        }

        static void Main()
        {
            Menu menu = new Menu();
            string nameValidation = "", dateValidation = "";
            HolidayRequest holidayRequest = new HolidayRequest();

            string name = " ", surname = " ", startDate = " ", endDate = " ";


            Console.WriteLine(menu.PrintNextStep(0));
            while (nameValidation != "OK")
            {
                Console.WriteLine(menu.PrintNextStep(1));
                name = Console.ReadLine();

                Console.WriteLine(menu.PrintNextStep(2));
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
                    Console.WriteLine(menu.PrintNextStep(3));
                    choice = Console.ReadLine();
                } while (menu.ReadChoice(choice) != MenuChoice.CreateHolidayRequest && menu.ReadChoice(choice) != MenuChoice.showRequests && menu.ReadChoice(choice) != MenuChoice.LogOut);
                Console.WriteLine(menu.menuAction(menu.ReadChoice(choice), holidayRequest));
            }
        }
    }
}