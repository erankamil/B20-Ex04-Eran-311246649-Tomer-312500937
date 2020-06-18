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
            initializeMainMenuItem(i_Text);
        }

        private void initializeMainMenuItem(string i_Text)
        {
            m_MainItem = new MenuItem(i_Text);
            m_MainItem.IsMainMenu = true;
            m_MainItem.PrevMenuItem = null;
            m_MainItem.MenuItems = new List<MenuItem>();
            m_MainItem.m_BackClicked += M_MainItem_ReportBackClicked;
            m_MainItem.m_MenuItemClicked += M_MainItem_ReportMenuItemClicked;
            m_MainItem.m_Action += M_MainItem_m_Action;
        }

        private void M_MainItem_m_Action()
        {
            System.Threading.Thread.Sleep(2000);
            Ex02.ConsoleUtils.Screen.Clear();
        }

        public void AddToMainMenu(MenuItem i_Item)
        {
            m_MainItem.MenuItems.Add(i_Item);
            i_Item.m_BackClicked += M_MainItem_ReportBackClicked;
            i_Item.m_MenuItemClicked += M_MainItem_ReportMenuItemClicked;
            i_Item.m_Action += I_Item_m_Action;
            i_Item.PrevMenuItem = this.m_MainItem;
        }

        private void I_Item_m_Action()
        {
            System.Threading.Thread.Sleep(2000);
            Ex02.ConsoleUtils.Screen.Clear();
        }

        private void M_MainItem_ReportMenuItemClicked(MenuItem i_Item)
        {
            Ex02.ConsoleUtils.Screen.Clear();
            m_MainItem = i_Item;
            i_Item.Show();
        }

        private void M_MainItem_ReportBackClicked(MenuItem i_Item)
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

        public void Show()
        {
            while (m_MainItem != null)
            {
                m_MainItem.Show();
            }
        }
    }
}
