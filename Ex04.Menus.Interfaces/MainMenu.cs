using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex04.Menus.Interfaces
{
    public class MainMenu : IClickedListener, IBackWasClickedLisenter
    {
        private MenuItem m_MainItem;

        public MainMenu(string i_Text)
        {
            initializeMainMenuItem(i_Text);
        }

        private void initializeMainMenuItem(string i_Text)
        {
            m_MainItem = new MenuItem(i_Text, 0);
            m_MainItem.IsMainMenu = true;
            m_MainItem.PrevMenuItem = null;
            m_MainItem.BackClickedNotifier.AddListeners(this);
            m_MainItem.MenuItemClickedNotifier.AddListeners(this);
        }

        public void AddToMainMenu(MenuItem i_Item)
        {
            if(m_MainItem.MenuItems == null)
            {
                m_MainItem.MenuItems = new List<MenuItem>();
            }

            m_MainItem.MenuItems.Add(i_Item);
            if(i_Item.Action == null)
            {
                i_Item.BackClickedNotifier.AddListeners(this);
            }

            i_Item.MenuItemClickedNotifier.AddListeners(this);
            i_Item.PrevMenuItem = this.m_MainItem;
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

        void IClickedListener.WasClicked(MenuItem i_Item)
        {
            Ex02.ConsoleUtils.Screen.Clear();
            if (i_Item.MenuItems != null)
            {
                m_MainItem = i_Item;
                i_Item.Show();
            }
            else
            {
                i_Item.Action.DoAction();
                System.Threading.Thread.Sleep(2000);
                Ex02.ConsoleUtils.Screen.Clear();
            }
        }

        void IBackWasClickedLisenter.BackClicked(MenuItem i_MenuItem)
        {
            if (i_MenuItem.IsMainMenu == true)
            {
                m_MainItem = null;
            }
            else
            {
                m_MainItem = i_MenuItem.PrevMenuItem;
                Ex02.ConsoleUtils.Screen.Clear();
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
