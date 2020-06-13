using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex04.Menus.Interfaces
{
    public class MainMenu : IClickedListener , IBackWasClickedLisenter
    {
        private readonly string r_MenuName = "Main Menu";
        public MenuItem m_MainItem;
        private MenuItem m_Prev;

        public MainMenu()
        {
            m_MainItem = new MenuItem(r_MenuName, this);
            m_MainItem.m_Items = new List<MenuItem>();
            m_Prev = null;
        }

        void IClickedListener.WasClicked(MenuItem i_Item)
        {
            Ex02.ConsoleUtils.Screen.Clear();
            if (i_Item.m_Items != null)
            {
                m_Prev = m_MainItem;
                m_MainItem = i_Item;
                i_Item.Show();
            }
            else
            {
                i_Item.m_Action.DoAction();
                System.Threading.Thread.Sleep(2000);
                Ex02.ConsoleUtils.Screen.Clear();
            }
        }

        void IBackWasClickedLisenter.BackClicked()
        {
            if (m_MainItem.m_Text == r_MenuName)
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
    
    public interface IClickedListener
    {
        void WasClicked(MenuItem i_Item);
    }

    public interface IBackWasClickedLisenter
    {
        void BackClicked();
    }
}
