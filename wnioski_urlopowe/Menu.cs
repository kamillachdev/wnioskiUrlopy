using holidayRequestSystem;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace menuSystem
{
    internal class Menu
    {
        public string printMenu()
        {
            string menu = "1. Nowy wniosek urlopowy";
            return menu;
        }

        public enum MenuChoice { Undefined, CreateHolidayRequest };

        public MenuChoice ReadChoice(string choice)
        {
            switch(choice)
            {
                case "1":
                    return MenuChoice.CreateHolidayRequest;
                default:
                    return MenuChoice.Undefined;
            }   
        }

        public void menuAction(MenuChoice menuChoice)
        {
            if(menuChoice == MenuChoice.CreateHolidayRequest) 
            {
                HolidayRequest holidayRequest = new HolidayRequest(getName(), getSurname(), getStartDate(), getEndDate());
                Console.WriteLine(holidayRequest.GetSummary());
            }
            else
            {
                Console.WriteLine("Nie ma takiej opcji!");
            }
        }

        public string getName()
        {
            Console.WriteLine("Podaj imię: ");
            string name = Console.ReadLine();
            return name;
        }

        public string getSurname()
        {
            Console.WriteLine("Podaj nazwisko: ");
            string surname = Console.ReadLine();
            return surname;
        }

        public DateTime getStartDate()
        {
            DateTime startDate;

            while (true)
            {
                Console.WriteLine("Podaj datę początkową w formacie dd-MM-yyyy: ");

                if (!DateTime.TryParseExact(Console.ReadLine(), "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out startDate))
                {
                    Console.WriteLine("Nieprawidłowy format daty.");
                    continue;
                }
                break;
            }
            return startDate;
        }

        public DateTime getEndDate()
        {
            DateTime endDate;

            while (true)
            {
               Console.WriteLine("Podaj datę końcową w formacie dd-MM-yyyy: ");

               if (!DateTime.TryParseExact(Console.ReadLine(), "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out endDate))
               {
                    Console.WriteLine("Nieprawidłowy format daty.");
                    continue;
               }
               break;
            }
            return endDate;
        }

        public string getChoice()
        {
            Console.WriteLine("Wybór: ");
            string choice = Console.ReadLine();
            return choice;
        }
    }
}
