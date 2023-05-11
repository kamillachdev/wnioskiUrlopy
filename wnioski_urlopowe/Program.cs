using System;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Text.RegularExpressions;
using menuSystem;
using holidayRequestSystem;
using database;
using static menuSystem.Menu;
using wnioski_urlopowe;

namespace Program
{
    public class holidayManegement
    {
        static void Main()
        {
            Menu menu = new Menu();
            Sql sql = new Sql();

            string validation = "";
            HolidayRequest holidayRequest = new HolidayRequest();


            Console.WriteLine("LOGOWANIE");
            while (validation != "OK")
            {
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


                holidayRequest.setvalues(name, surname, menu.convertToDate(startDate), menu.convertToDate(endDate));
                validation = holidayRequest.isValid();
                Console.WriteLine(validation);
            }

            Console.WriteLine(menu.printMenu());
            string choice = string.Empty;
            do
            {
                Console.WriteLine("Wybór: ");
                choice = Console.ReadLine();
            } while (menu.ReadChoice(choice) != MenuChoice.CreateHolidayRequest);


            if (sql.actionSql(holidayRequest))
            {
                Console.WriteLine(menu.menuAction(menu.ReadChoice(choice), holidayRequest));
            }
            else
            {
                Console.WriteLine("Nie udało się wysłać wniosku.");
            }
        }
    }
}