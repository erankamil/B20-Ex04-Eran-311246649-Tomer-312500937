﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex04.Menus.Interfaces
{
    public interface IAction
    {
        void DoAction();
    }

    public interface IClickedListener
    {
        void WasClicked(MenuItem i_Item);
    }

    public interface IBackWasClickedLisenter
    {
        void BackClicked(MenuItem i_MenuItem);
    }

    public class MenuItem
    {
        private int m_ItemIndex;
        private string m_Text;
        private IAction m_Action;
        private List<MenuItem> m_Items;
        private Notifier<IClickedListener> m_ClickNotifier;
        private Notifier<IBackWasClickedLisenter> m_BackClickedNotifier;
        private bool m_IsMainMenu;
        private MenuItem m_Prev;

        public MenuItem(string i_Text, int i_ItemIndex, IAction i_Action)
        {
            m_Text = i_Text;
            m_ItemIndex = i_ItemIndex;
            m_Action = i_Action;
            initializeNotifiers();
        }

        public MenuItem(string i_Text, int i_ItemIndex = 0)
        {
            m_Text = i_Text;
            m_ItemIndex = i_ItemIndex;
            initializeNotifiers();
        }

        private void initializeNotifiers()
        {
            m_ClickNotifier = new Notifier<IClickedListener>();
            m_BackClickedNotifier = new Notifier<IBackWasClickedLisenter>();
        }

        public void AddToSubMenu(MenuItem i_Item)
        {
            if(m_Items == null)
            {
                m_Items = new List<MenuItem>();
            }

            m_Items.Add(i_Item);
            if(i_Item.Action == null)
            {
                IBackWasClickedLisenter backListener = this.BackClickedNotifier.Listerners.First();
                i_Item.BackClickedNotifier.AddListeners(backListener);
            }

            IClickedListener Clickedlistener = this.MenuItemClickedNotifier.Listerners.First();
            i_Item.MenuItemClickedNotifier.AddListeners(Clickedlistener);
        }

        public bool IsMainMenu
        {
            get
            {
                return m_IsMainMenu;
            }

            set
            {
                m_IsMainMenu = value;
            }
        }

        public string Text
        {
            get
            {
                return m_Text;
            }

            set
            {
                m_Text = value;
            }
        }

        public List<MenuItem> MenuItems
        {
            get
            {
                return m_Items;
            }

            set
            {
                m_Items = value;
            }
        }
        
        public IAction Action
        {
            get
            {
                return m_Action;
            }
        }

        public Notifier<IClickedListener> MenuItemClickedNotifier
        {
            get
            {
                return m_ClickNotifier;
            }
        }

        public Notifier<IBackWasClickedLisenter> BackClickedNotifier
        {
            get
            {
                return m_BackClickedNotifier;
            }
        }

        public MenuItem PrevMenuItem
        {
            get
            {
                return m_Prev;
            }

            set
            {
                m_Prev = value;
            }
        }

        private void getItemChoice()
        {
            string choice;
            do
            {
                Console.WriteLine(@"Please enter your choice(by index) or back\exit");
                choice = Console.ReadLine();
            }
            while (!checkValidChoice(choice));

            if (choice == "0")
            {
                doWhenBackClicked();
            }
            else
            {
                m_Items[int.Parse(choice) - 1].doWhenClicked(m_Items[int.Parse(choice) - 1], int.Parse(choice));
            }
        }

        internal void Show()
        {
            Console.WriteLine("======{0}=======", m_Text);
            foreach (MenuItem currItem in m_Items)
            {
                Console.WriteLine(currItem.m_ItemIndex.ToString() + ") " + currItem.m_Text);
            }

            string back = "go back";
            if(IsMainMenu == true)
            {
                back = "exit";
            }

            Console.WriteLine("0) To {0}", back);
            getItemChoice();
        }
        
        private bool checkValidChoice(string i_ChoiceStr)
        {
           bool isValid = true;
           if(int.TryParse(i_ChoiceStr, out int res))
            {
                if(res < 0 || res > m_Items.Count)
                {
                    isValid = false;
                    Console.WriteLine("Index out of range! Min {0} Max {1}", 0, m_Items.Count);
                }
            }
           else
            {
                isValid = false;
                Console.WriteLine("Choice must be index from the menu");
            }

            return isValid;
        }

        private void doWhenClicked(MenuItem i_Item, int i_ChoiceIndex)
        {
             m_ClickNotifier.NotifyAllListerners(i_Item, i_ChoiceIndex);
        }

        private void doWhenBackClicked()
        {
            m_BackClickedNotifier.NotifyAllListerners(this, 0);
        }
    }

    public class Notifier<T>
    {
        private List<T> m_Listeners;

        public List<T> Listerners
        {
            get
            {
                return m_Listeners;
            }
        }

        public void AddListeners(T i_Listener)
        {
            if(m_Listeners == null)
            {
                m_Listeners = new List<T>();
            }

            m_Listeners.Add(i_Listener);
        }

        public void RemoveListener(T i_Listener)
        {
            m_Listeners.Remove(i_Listener);
        }

        public void NotifyAllListerners(MenuItem i_Item, int indexChoice)
        {
            foreach(T listener in m_Listeners)
            {
                if(indexChoice != 0)
                {
                    (listener as IClickedListener).WasClicked(i_Item);
                }
                else
                {
                    (listener as IBackWasClickedLisenter).BackClicked(i_Item);
                }
            }
        }
    }
}