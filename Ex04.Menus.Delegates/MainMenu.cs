using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex04.Menus.Delegates
{
    public class MainMenu
    {
        private MenuItem m_MainItem;

        public MainMenu(string i_Text)
        {
            m_MainItem = new MenuItem(i_Text, this);
            m_MainItem.IsMainMenu = true;
            m_MainItem.PrevMenuItem = null;
            m_MainItem.MenuItems = new List<MenuItem>();
        }

        public MenuItem CurrentMenuItem
        {
            get
            {
                return m_MainItem;
            }

            set
            {
                m_MainItem = value;
            }
        }

        private void MenuItem_WasClicked(MenuItem i_Item)
        {
            Ex02.ConsoleUtils.Screen.Clear();
            if (i_Item.MenuItems != null)
            {
                m_MainItem = i_Item;
                i_Item.Show();
            }
            else
            {
                i_Item.DoAction();
                System.Threading.Thread.Sleep(2000);
                Ex02.ConsoleUtils.Screen.Clear();
            }
        }

        private void Back_WasClicked(MenuItem i_Item)
        {
            if (i_Item.IsMainMenu == true)
            {
                m_MainItem = null;
            }
            else
            {
                m_MainItem = i_Item.PrevMenuItem;
                Ex02.ConsoleUtils.Screen.Clear();
            }
        }

        public void BecomeListener(MenuItem i_Item)
        {
            i_Item.ReportMenuItemClicked += MenuItem_WasClicked;
            i_Item.ReportBackClicked += Back_WasClicked;
        }

        public void Show()
        {
            while (m_MainItem != null)
            {
                m_MainItem.Show();
            }
        }
    }
}
