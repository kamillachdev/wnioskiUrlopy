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
            menu.menuAction(menu.ReadChoice(menu.getChoice()));
        }
    }
}