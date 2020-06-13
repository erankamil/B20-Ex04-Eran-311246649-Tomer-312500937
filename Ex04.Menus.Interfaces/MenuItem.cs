using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex04.Menus.Interfaces
{
    public class MenuItem
    {
        private int m_ItemIndex;
        public string m_Text;
        public IAction m_Action;
        public  List<MenuItem> m_Items;
        private Notifier<IClickedListener> m_ClickNotifier;
        private Notifier<IBackWasClickedLisenter> m_BackClickedNotifier;

        public MenuItem(MainMenu i_MainListener)
        {
            m_Text = MainMenu.sr_MenuName;
            initializeListener(i_MainListener);
        }

        public MenuItem(string i_Text,int i_ItemIndex, IAction i_Action, MainMenu i_MainListener)
        {
            m_Text = i_Text;
            m_ItemIndex = i_ItemIndex;
            m_Action = i_Action;
            initializeListener(i_MainListener);
        }

        public MenuItem(string i_Text, int i_ItemIndex, List<MenuItem> i_Items, MainMenu i_MainListener)
        {
            m_Text = i_Text;
            m_ItemIndex = i_ItemIndex;
            m_Items = i_Items;
            initializeListener(i_MainListener);
        }

        private void initializeListener(MainMenu i_MainListener)
        {
            m_ClickNotifier = new Notifier<IClickedListener>();
            m_ClickNotifier.AddListeners(i_MainListener);
            m_BackClickedNotifier = new Notifier<IBackWasClickedLisenter>();
            m_BackClickedNotifier.AddListeners(i_MainListener);
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
            {// the menu tell his menuItem he was choosen
                m_Items[int.Parse(choice) - 1].doWhenClicked(m_Items[int.Parse(choice) - 1], int.Parse(choice));
            }
        }

        public void Show()
        {
            Console.WriteLine("======{0}=======", m_Text);
            foreach (MenuItem currItem in m_Items)
            {
                Console.WriteLine(currItem.m_ItemIndex.ToString() + ") " + currItem.m_Text);
            }
            string back = "go back";
            if(m_Text == MainMenu.sr_MenuName)
            {
                back = "exit";
            }
            Console.WriteLine("0) To {0}",back);
            getItemChoice();
        }
        
        private bool checkValidChoice(string i_ChoiceStr)
        {
            bool isValid = true;
           if(int.TryParse(i_ChoiceStr,out int res))
            {
                if(res < 0 || res > m_Items.Count)
                {
                    isValid = false;
                    Console.WriteLine("Index out of range! Min {0} Max {1}",0 , m_Items.Count);
                }
            }
           else
            {
                isValid = false;
                Console.WriteLine("Choice must be index from the menu");
            }
            return isValid;
        }

        // MenuItem notifies the system(MainMenu) he was clicked
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

        public void NotifyAllListerners(MenuItem i_Item,int indexChoice)
        {
            foreach(T listener in m_Listeners)
            {
                if(indexChoice != 0)
                {
                    (listener as IClickedListener).WasClicked(i_Item);
                }
                else
                {
                    (listener as IBackWasClickedLisenter).BackClicked();
                }
            }
        }
    }

    public interface IAction
    {
        void DoAction();
    }


}




