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
        private MenuItem m_Prev;

        public MainMenu(string i_Text)
        {
            m_MainItem = new MenuItem(i_Text, this);
            m_MainItem.IsMainMenu = true;
            m_MainItem.MenuItems = new List<MenuItem>();
            m_Prev = null;
        }

        public MenuItem CurrentMenu
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
                m_Prev = m_MainItem;
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

        void IBackWasClickedLisenter.BackClicked()
        {
            if (m_Prev == m_MainItem)
            {
                m_MainItem = null;
            }
            else
            {
                m_MainItem = m_Prev;
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
