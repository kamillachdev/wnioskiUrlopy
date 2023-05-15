using System;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Text.RegularExpressions;
using menuSystem;
using holidayRequestSystem;
using database;
using static menuSystem.Menu;
using MySqlX.XDevAPI.Common;
using wnioski_urlopowe;
using static wnioski_urlopowe.Login;
using manageDatabase;

namespace Program
{
    public class holidayManegement
    {
        static void Main()
        {
            Login login = new Login();
            Menu menu = new Menu();
            string nameValidation = "", dateValidation = "";
            HolidayRequest holidayRequest = new HolidayRequest();

            string name = " ", surname = " ";


            Console.WriteLine(login.printMessages(loginMessages.startLogin));
            while (nameValidation != "OK")
            {
                Console.WriteLine(login.printMessages(loginMessages.insertName));
                name = Console.ReadLine();

                Console.WriteLine(login.printMessages(loginMessages.insertSurname));
                surname = Console.ReadLine();

                holidayRequest.setName(name, surname);
                nameValidation = holidayRequest.isNameValid();
                Console.WriteLine(nameValidation);
            }

            using (var ctx = new HolidayContext())
            {
                var stud = new User() { UserID = holidayRequest.createId(), Name = holidayRequest.getName(), Surname = holidayRequest.getSurname() };

                ctx.Users.Add(stud);
                ctx.SaveChanges();
            }

            /*
            while (true)
            {
                Console.WriteLine(menu.printMenu());
                string choice = " ";
                do
                {
                    Console.WriteLine(menu.printMessages(menuMessages.insertChoice));
                    choice = Console.ReadLine();
                } while (menu.ReadChoice(choice) != MenuChoice.CreateHolidayRequest && menu.ReadChoice(choice) != MenuChoice.showRequests && menu.ReadChoice(choice) != MenuChoice.LogOut);
                
                if(menu.ReadChoice(choice) == MenuChoice.CreateHolidayRequest)
                {
                    
                    string startDateString, endDateString;
                    DateTime startDate, endDate;

                    while (dateValidation != "OK")
                    {
                        Console.WriteLine(menu.printMessages(menuMessages.insertStartDate));
                        startDateString = Console.ReadLine();

                        Console.WriteLine(menu.printMessages(menuMessages.insertEndDate));
                        endDateString = Console.ReadLine();

                        if(DateTime.TryParseExact(startDateString, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out startDate) && DateTime.TryParseExact(endDateString, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out endDate))
                        {
                            holidayRequest.setDate(startDate, endDate);
                            dateValidation = holidayRequest.isDateValid();
                            Console.WriteLine(dateValidation);
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
                Console.WriteLine(menu.menuAction(menu.ReadChoice(choice), holidayRequest));
            
            }
            */
        }
    }
}