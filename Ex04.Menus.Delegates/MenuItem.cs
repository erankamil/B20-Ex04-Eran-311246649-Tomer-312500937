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
        private List<MenuItem> m_Items;
        private bool m_IsMainMenu;
        private MenuItem m_Prev;

        public event Action m_Action;

        public event ReportMenuItemClicked m_MenuItemClicked;

        public event ReportBackClicked m_BackClicked;

        public MenuItem(string i_Text)
        {
            m_Text = i_Text;
        }

        public void AddToSubMenu(MenuItem i_ItemToAdd)
        {
            if(m_Items == null)
            {
                m_Items = new List<MenuItem>();
            }

            m_Items.Add(i_ItemToAdd);
            if (i_ItemToAdd.Action == null)
            {
                i_ItemToAdd.m_BackClicked += m_BackClicked;
                i_ItemToAdd.m_MenuItemClicked += m_MenuItemClicked;
            }

             i_ItemToAdd.m_Action += m_Action;
        }

        public Action Action
        {
            get
            {
                return m_Action;
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
                if (m_Items[int.Parse(choice) - 1].m_Items == null)
                {
                    m_Items[int.Parse(choice) - 1].doWhenActionClicked();
                }
                else
                {
                    m_Items[int.Parse(choice) - 1].doWhenMenuItemClicked();
                }
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

        private void doWhenActionClicked()
        {
            OnActionClicked();
        }

        protected virtual void OnActionClicked()
        {
            Ex02.ConsoleUtils.Screen.Clear();
            m_Action?.Invoke();
        }

        private void doWhenMenuItemClicked()
        {
            OnMenuItemClicked();
        }

        protected virtual void OnMenuItemClicked()
        {
            m_MenuItemClicked?.Invoke(this);
        }

        private void doWhenBackClicked()
        {
            OnBackClicked();
        }

        protected virtual void OnBackClicked()
        {
            m_BackClicked?.Invoke(this);
        }
    }
}