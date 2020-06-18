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
            Interfaces.MenuItem digitAndVersion = new Interfaces.MenuItem("Version and Digits", 1);
            i_Menu.AddToMainMenu(digitAndVersion);

            CountCapital count = new CountCapital();
            Interfaces.MenuItem countCapital = new Interfaces.MenuItem("Count Capital", 1, count);
            digitAndVersion.AddToSubMenu(countCapital);

            ShowVersion version = new ShowVersion();
            Interfaces.MenuItem showVersion = new Interfaces.MenuItem("Show Version", 2, version);
            digitAndVersion.AddToSubMenu(showVersion);

            Interfaces.MenuItem dateAndTime = new Interfaces.MenuItem("Show Date/Time", 2);
            i_Menu.AddToMainMenu(dateAndTime);

            ShowTime time = new ShowTime();
            Interfaces.MenuItem showTime = new Interfaces.MenuItem("Show Time", 1, time);
            dateAndTime.AddToSubMenu(showTime);

            ShowDate date = new ShowDate();
            Interfaces.MenuItem showDate = new Interfaces.MenuItem("Show Date", 2, date);
            dateAndTime.AddToSubMenu(showDate);

            i_Menu.Show();
        }
       
        internal static void DelegateMenu(Delegates.MainMenu i_Menu)
        {
            Delegates.MenuItem versionAndDigit = new Delegates.MenuItem("Version and Digits");
            i_Menu.AddToMainMenu(versionAndDigit);

            Delegates.MenuItem countCapital = new Delegates.MenuItem("Count Capital");
            countCapital.m_Action += CountCapitalAction;
            versionAndDigit.AddToSubMenu(countCapital);
        
            Delegates.MenuItem showVersion = new Delegates.MenuItem("Show Version");
            showVersion.m_Action += ShowVersionAction;
            versionAndDigit.AddToSubMenu(showVersion);

            Delegates.MenuItem dateAndTime = new Delegates.MenuItem("Show Date/Time");
            i_Menu.AddToMainMenu(dateAndTime);

            Delegates.MenuItem showTime = new Delegates.MenuItem("Show Time");
            showTime.m_Action += ShowTimeAction;
            dateAndTime.AddToSubMenu(showTime);

            Delegates.MenuItem showDate = new Delegates.MenuItem("Show Date");
            showDate.m_Action += ShowDateAction;
            dateAndTime.AddToSubMenu(showDate);

            i_Menu.Show();
        }

        internal static void CountCapitalAction()
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

        internal static void ShowVersionAction()
        {
            Console.WriteLine("The version is: Version: 20.2.4.30620");
        }

        internal static void ShowTimeAction()
        {
            DateTime time = DateTime.Now;

            Console.WriteLine(time.ToShortTimeString());
        }

        internal static void ShowDateAction()
        {
            DateTime now = DateTime.Today;

            Console.WriteLine(now.ToShortDateString());
        }

        internal struct CountCapital : Interfaces.IAction
        {
            void Interfaces.IAction.DoAction() 
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
            void Interfaces.IAction.DoAction() 
            {
                DateTime time = DateTime.Now;

                Console.WriteLine(time.ToShortTimeString());
            }
        }

        internal struct ShowDate : Interfaces.IAction
        {
            void Interfaces.IAction.DoAction()
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
