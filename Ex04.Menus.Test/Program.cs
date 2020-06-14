using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ex04;


namespace Ex04.Menus.Test
{
    public class Program
    {
        public static void Main()
        {
            Interfaces.MainMenu interfaceMenu = new Interfaces.MainMenu("Main Menu With Intefaces");
            InterFaceMenu(interfaceMenu);

            Ex02.ConsoleUtils.Screen.Clear();
            Console.WriteLine("Changing Menu..");
            System.Threading.Thread.Sleep(2000);
            Ex02.ConsoleUtils.Screen.Clear();

            Delegates.MainMenu delegateMenu = new Delegates.MainMenu("Main Menu With Delegates");
            DelegateMenu(delegateMenu);
        }

        public static void InterFaceMenu(Interfaces.MainMenu i_Menu)
        {
            CountCapital count = new CountCapital();
            Interfaces.MenuItem CountCapital = new Interfaces.MenuItem("Count Capital", 1, count, i_Menu);

            ShowVersion version = new ShowVersion();
            Interfaces.MenuItem ShowVersion = new Interfaces.MenuItem("Show Version", 2, version, i_Menu);

            List<Interfaces.MenuItem> digitAndVersion = new List<Interfaces.MenuItem>();
            digitAndVersion.Add(CountCapital);
            digitAndVersion.Add(ShowVersion);

            Interfaces.MenuItem one = new Interfaces.MenuItem("Version and Digits", 1, digitAndVersion, i_Menu);
            i_Menu.CurrentMenu.MenuItems.Add(one);

            ShowTime time = new ShowTime();
            Interfaces.MenuItem showTime = new Interfaces.MenuItem("Show Time", 1, time, i_Menu);
            ShowDate date = new ShowDate();
            Interfaces.MenuItem showDate = new Interfaces.MenuItem("Show Date", 2, date, i_Menu);

            List<Interfaces.MenuItem> dateAndTime = new List<Interfaces.MenuItem>();
            dateAndTime.Add(showTime);
            dateAndTime.Add(showDate);

            Interfaces.MenuItem two = new Interfaces.MenuItem("Show Date/Time", 2, dateAndTime, i_Menu);
            i_Menu.CurrentMenu.MenuItems.Add(two);


            i_Menu.Show();
        }
       

        public static void DelegateMenu(Delegates.MainMenu i_Menu)
        {
            Delegates.MenuItem countCapital = new Delegates.MenuItem("Count Capital", i_Menu);
            countCapital.Action += CountCapitalAction;

            Delegates.MenuItem showVersion = new Delegates.MenuItem("Show Version", i_Menu);
            showVersion.Action += ShowVersionAction;


            List<Delegates.MenuItem> digitAndVersion = new List<Delegates.MenuItem>();
            digitAndVersion.Add(countCapital);
            digitAndVersion.Add(showVersion);
 
            Delegates.MenuItem one = new Delegates.MenuItem("Version and Digits", digitAndVersion, i_Menu);
            i_Menu.CurrentMenu.MenuItems.Add(one);


            Delegates.MenuItem showTime = new Delegates.MenuItem("Show Time", i_Menu);
            showTime.Action += ShowTimeAction;

            Delegates.MenuItem showDate = new Delegates.MenuItem("Show Date", i_Menu);
            showDate.Action += ShowDateAction;

            List<Delegates.MenuItem> dateAndTime = new List<Delegates.MenuItem>();
            dateAndTime.Add(showTime);
            dateAndTime.Add(showDate);

            Delegates.MenuItem two = new Delegates.MenuItem("Show Date/Time", dateAndTime, i_Menu);
            i_Menu.CurrentMenu.MenuItems.Add(two);

            i_Menu.Show();
        }



        public static void CountCapitalAction(Delegates.MenuItem i_Item)
        {
            Console.WriteLine("Please enter a sentence:");
            string sentence = Console.ReadLine();
            int count = 0;
            for (int i = 0; i < sentence.Length; i++)
            {
                if (char.IsUpper(sentence[i]) == true)
                {
                    count++;
                }
            }
            Console.WriteLine("Number of upper case letters: {0}", count);
        }

        public static void ShowVersionAction(Delegates.MenuItem i_Item)
        {
            Console.WriteLine("The version is: Version: 20.2.4.30620");
        }

        public static void ShowTimeAction(Delegates.MenuItem i_Item)
        {
            DateTime time = DateTime.Now;

            Console.WriteLine(time.ToShortTimeString());
        }

        public static void ShowDateAction(Delegates.MenuItem i_Item)
        {
            DateTime now = DateTime.Today;

            Console.WriteLine(now.ToShortDateString());
        }

        public struct CountCapital : Interfaces.IAction
        {
            void Interfaces.IAction.DoAction() /// expilict implement 
            {
                Console.WriteLine("Please enter a sentence:");
                string sentence = Console.ReadLine();
                int count = 0;
                for(int i=0; i < sentence.Length; i++)
                {
                    if(char.IsUpper(sentence[i]) == true)
                    {
                        count++;
                    }
                }
                Console.WriteLine("Number of upper case letters: {0}", count);
            }
        }

        public struct ShowTime : Interfaces.IAction
        {
            void Interfaces.IAction.DoAction() /// expilict implement 
            {
                DateTime time = DateTime.Now;

                Console.WriteLine(time.ToShortTimeString());
            }
        }

        public struct ShowDate : Interfaces.IAction
        {
            void Interfaces.IAction.DoAction() /// expilict implement 
            {
                DateTime now = DateTime.Today;

                Console.WriteLine(now.ToShortDateString());
            }
        }

        public struct ShowVersion : Interfaces.IAction
        {
            void Interfaces.IAction.DoAction() /// expilict implement 
            {
                Console.WriteLine("The version is: Version: 20.2.4.30620");
            }
        }
    }

}
