using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace Ex04.Menus.Interfaces
{
    class Program
    {
        public static void Main()
        {
            Class1 c = new Class1();
            c.Run();
        }
    }

    class Class1
    {
        public MainMenu m_Menu;

        public  void Run()
        {
            m_Menu = new MainMenu();
            CountCapital count = new CountCapital();
            MenuItem CountCapital = new MenuItem("Count Capital", 1, count, m_Menu);

            ShowVersion version = new ShowVersion();
            MenuItem ShowVersion = new MenuItem("Show Version", 2, version, m_Menu);

            List<MenuItem> digitAndVersion = new List<MenuItem>();
            digitAndVersion.Add(CountCapital);
            digitAndVersion.Add(ShowVersion);

            MenuItem one = new MenuItem("Version and Digits", 1, digitAndVersion, m_Menu);
            m_Menu.m_MainItem.m_Items.Add(one);

            ShowTime time = new ShowTime();
            MenuItem showTime = new MenuItem("Show Time", 1, time, m_Menu);
            ShowDate date = new ShowDate();
            MenuItem showDate = new MenuItem("Show Date", 2, date, m_Menu);

            List<MenuItem> dateAndTime = new List<MenuItem>();
            dateAndTime.Add(showTime);
            dateAndTime.Add(showDate);

            MenuItem two = new MenuItem("Show Date/Time", 2, dateAndTime, m_Menu);
            m_Menu.m_MainItem.m_Items.Add(two);


            m_Menu.Show();

        }

        public struct CountCapital : IAction
        {
            void IAction.DoAction() /// expilict implement 
            {
                Console.WriteLine("The capitals are:");
            }
        }
        public struct ShowTime : IAction
        {
            void IAction.DoAction() /// expilict implement 
            {
                DateTime time = DateTime.Now;

                Console.WriteLine(time.ToShortTimeString());
            }
        }

        public struct ShowDate : IAction
        {
            void IAction.DoAction() /// expilict implement 
            {
                DateTime now = DateTime.Today;

                Console.WriteLine(now.ToShortDateString());
            }
        }

        public struct ShowVersion : IAction
        {
            void IAction.DoAction() /// expilict implement 
            {
                Console.WriteLine("The version is: Version: 20.2.4.30620");
            }
        }

    }
}