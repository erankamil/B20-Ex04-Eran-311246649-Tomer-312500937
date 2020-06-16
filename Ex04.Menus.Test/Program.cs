using System;
using System.Collections.Generic;

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

        internal static void InterFaceMenu(Interfaces.MainMenu i_Menu)
        {
            CountCapital count = new CountCapital();
            Interfaces.MenuItem CountCapital = new Interfaces.MenuItem("Count Capital", 1, count, i_Menu);

            ShowVersion version = new ShowVersion();
            Interfaces.MenuItem ShowVersion = new Interfaces.MenuItem("Show Version", 2, version, i_Menu);

            List<Interfaces.MenuItem> listDigitAndVersion = new List<Interfaces.MenuItem>();
            listDigitAndVersion.Add(CountCapital);
            listDigitAndVersion.Add(ShowVersion);

            Interfaces.MenuItem digitAndVersion = new Interfaces.MenuItem("Version and Digits", 1, listDigitAndVersion, i_Menu);
            i_Menu.CurrentMenuItem.MenuItems.Add(digitAndVersion);

            ShowTime time = new ShowTime();
            Interfaces.MenuItem showTime = new Interfaces.MenuItem("Show Time", 1, time, i_Menu);
            ShowDate date = new ShowDate();
            Interfaces.MenuItem showDate = new Interfaces.MenuItem("Show Date", 2, date, i_Menu);

            List<Interfaces.MenuItem> listDateAndTime = new List<Interfaces.MenuItem>();
            listDateAndTime.Add(showTime);
            listDateAndTime.Add(showDate);

            Interfaces.MenuItem dateAndTime = new Interfaces.MenuItem("Show Date/Time", 2, listDateAndTime, i_Menu);
            i_Menu.CurrentMenuItem.MenuItems.Add(dateAndTime);

            dateAndTime.PrevMenuItem = i_Menu.CurrentMenuItem;
            digitAndVersion.PrevMenuItem = i_Menu.CurrentMenuItem;

            i_Menu.Show();
        }
       
        internal static void DelegateMenu(Delegates.MainMenu i_Menu)
        {
            Delegates.MenuItem countCapital = new Delegates.MenuItem("Count Capital", i_Menu);
            countCapital.Action += CountCapitalAction;

            Delegates.MenuItem showVersion = new Delegates.MenuItem("Show Version", i_Menu);
            showVersion.Action += ShowVersionAction;

            List<Delegates.MenuItem> listVersionAndDigit = new List<Delegates.MenuItem>();
            listVersionAndDigit.Add(countCapital);
            listVersionAndDigit.Add(showVersion);

            Delegates.MenuItem versionAndDigit = new Delegates.MenuItem("Version and Digits", listVersionAndDigit, i_Menu);
            i_Menu.CurrentMenuItem.MenuItems.Add(versionAndDigit);

            versionAndDigit.PrevMenuItem = i_Menu.CurrentMenuItem;

            Delegates.MenuItem showTime = new Delegates.MenuItem("Show Time", i_Menu);
            showTime.Action += ShowTimeAction;

            Delegates.MenuItem showDate = new Delegates.MenuItem("Show Date", i_Menu);
            showDate.Action += ShowDateAction;

            List<Delegates.MenuItem> listDateAndTime = new List<Delegates.MenuItem>();
            listDateAndTime.Add(showTime);
            listDateAndTime.Add(showDate);

            Delegates.MenuItem dateAndTime = new Delegates.MenuItem("Show Date/Time", listDateAndTime, i_Menu);
            i_Menu.CurrentMenuItem.MenuItems.Add(dateAndTime);

            dateAndTime.PrevMenuItem = i_Menu.CurrentMenuItem;

            i_Menu.Show();
        }

        internal static void CountCapitalAction(Delegates.MenuItem i_Item)
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

        internal static void ShowVersionAction(Delegates.MenuItem i_Item)
        {
            Console.WriteLine("The version is: Version: 20.2.4.30620");
        }

        internal static void ShowTimeAction(Delegates.MenuItem i_Item)
        {
            DateTime time = DateTime.Now;

            Console.WriteLine(time.ToShortTimeString());
        }

        internal static void ShowDateAction(Delegates.MenuItem i_Item)
        {
            DateTime now = DateTime.Today;

            Console.WriteLine(now.ToShortDateString());
        }

        internal struct CountCapital : Interfaces.IAction
        {
            void Interfaces.IAction.DoAction() /// expilict implement 
            {
                Console.WriteLine("Please enter a sentence:");
                string sentence = Console.ReadLine();
                int count = 0;
                for(int i = 0; i < sentence.Length; i++)
                {
                    if(char.IsUpper(sentence[i]) == true)
                    {
                        count++;
                    }
                }

                Console.WriteLine("Number of upper case letters: {0}", count);
            }
        }

        internal struct ShowTime : Interfaces.IAction
        {
            void Interfaces.IAction.DoAction() /// expilict implement 
            {
                DateTime time = DateTime.Now;

                Console.WriteLine(time.ToShortTimeString());
            }
        }

        internal struct ShowDate : Interfaces.IAction
        {
            void Interfaces.IAction.DoAction() /// expilict implement 
            {
                DateTime now = DateTime.Today;

                Console.WriteLine(now.ToShortDateString());
            }
        }

        internal struct ShowVersion : Interfaces.IAction
        {
            void Interfaces.IAction.DoAction()  
            {
                Console.WriteLine("The version is: Version: 20.2.4.30620");
            }
        }
    }
}
