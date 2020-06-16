using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex04.Menus.Delegates
{
    public delegate void ReportMenuItemClicked(MenuItem i_Item);

    public delegate void ReportBackClicked(MenuItem i_Item);

    public class MenuItem
    {
        private string m_Text;
        private Action<MenuItem> m_Action;
        private List<MenuItem> m_Items;
        private bool m_IsMainMenu;
        private MenuItem m_Prev;

        private event ReportMenuItemClicked m_MenuItemClicked;

        private event ReportBackClicked m_BackClicked;

        public MenuItem(string i_Text, MainMenu i_Menu)
        {
            m_Text = i_Text;
            i_Menu.BecomeListener(this);
        }

        public MenuItem(string i_Text, List<MenuItem> i_Items, MainMenu i_Menu)
        {
            m_Text = i_Text;
            m_Items = i_Items;
            i_Menu.BecomeListener(this);
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

        public event ReportMenuItemClicked ReportMenuItemClicked
        {
            add
            {
                m_MenuItemClicked += value;
            }

            remove
            {
                m_MenuItemClicked -= value;
            }
        }

        public event ReportBackClicked ReportBackClicked
        {
            add
            {
                m_BackClicked += value;
            }

            remove
            {
                m_BackClicked -= value;
            }
        }

        public event Action<MenuItem> Action
        {
            add
            {
                m_Action += value;
            }

            remove
            {
                m_Action -= value;
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
                m_Items[int.Parse(choice) - 1].doWhenMenuItemClicked();
            }
        }

        public void Show()
        {
            Console.WriteLine("======{0}=======", m_Text);
            int i = 1;
            foreach (MenuItem currItem in m_Items)
            {
                Console.WriteLine(i.ToString() + ") " + currItem.m_Text);
                i++;
            }

            string back = "go back";
            if (IsMainMenu == true)
            {
                back = "exit";
            }

            Console.WriteLine("0) To {0}", back);
            getItemChoice();
        }

        private bool checkValidChoice(string i_ChoiceStr)
        {
            bool isValid = true;
            if (int.TryParse(i_ChoiceStr, out int res))
            {
                if (res < 0 || res > m_Items.Count)
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

        public void DoAction()
        {
            m_Action?.Invoke(this);
        }

        private void doWhenMenuItemClicked()
        {
            onMenuItemClicked();
        }

        private void onMenuItemClicked()
        {
            m_MenuItemClicked?.Invoke(this);
        }

        private void doWhenBackClicked()
        {
            onBackClicked();
        }

        private void onBackClicked()
        {
            m_BackClicked?.Invoke(this);
        }
    }
}