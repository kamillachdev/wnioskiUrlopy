using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wnioski_urlopowe
{
    internal class StartMenu
    {
        public string printMenu()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("START MENU");
            sb.AppendLine("1. Zaloguj się");
            sb.AppendLine("2. Zarejestruj się");
            return sb.ToString();
        }

        public enum MenuChoice { Undefined, Login, Register};

        public MenuChoice ReadChoice(string choice)
        {
            switch (choice)
            {
                case "1":
                    return MenuChoice.Login;
                case "2":
                    return MenuChoice.Register;
                default:
                    return MenuChoice.Undefined;
            }
        }



        public string menuAction(MenuChoice menuChoice)
        {
            string output = " ";
            if (menuChoice == MenuChoice.Login)
            {
                output = login();
            }
            else if (menuChoice == MenuChoice.Register) 
            {
                output = register();
            }
            else
            {
                output = "error";
            }
            return output;
        }

        private string login()
        {
            string output = " ";

            return output;
        }

        private string register()
        {
            string output = " ";
            return output;
        }
    }
}
