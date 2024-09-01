using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStack.White;
using TestStack.White.InputDevices;
using TestStack.White.WindowsAPI;
using TestStack.White.UIItems;
using TestStack.White.UIItems.Finders;
using TestStack.White.UIItems.TreeItems;
using TestStack.White.UIItems.WindowItems;
using TestStack.White.UIItems.TableItems;
using System.Windows.Automation;

namespace AddressbookTestsWhite
{
    public class ContactHelper : HelperBase
    {
        public ContactHelper(ApplicationManager manager) : base(manager) { }

        public static string NEWCONTACTWINTITLE = "Contact Editor";

        public void AddNewContact(ContactData contact)
        {
            manager.MainWindow.Get<Button>("uxNewAddressButton").Click();
            Window create = manager.MainWindow.ModalWindow(NEWCONTACTWINTITLE);
            AddContactData(contact, create);
        }

        public void AddContactData(ContactData contact, Window edit_window)
        {
            edit_window.Get<TextBox>("ueFirstNameAddressTextBox").SetValue(contact.Firstname);
            edit_window.Get<TextBox>("ueLastNameAddressTextBox").SetValue(contact.Lastname);
            edit_window.Get<Button>("uxSaveAddressButton").Click();  
        }

        public List<ContactData> GetContactsList()
        {
            List<ContactData> list = new List<ContactData>();
            TableRows tableRows = manager.MainWindow.Get<Table>("uxAddressGrid").Rows;
            foreach (TableRow row in tableRows)
            {
                list.Add(new ContactData(row.Cells[1].Value.ToString(), row.Cells[0].Value.ToString()));
            }
            return list;
        }

        public void RemoveContact(ContactData contact)
        {
            TableRows tableRows = manager.MainWindow.Get<Table>("uxAddressGrid").Rows;
            foreach (TableRow row in tableRows)
            {
                if (row.Cells[1].Value.ToString() == contact.Lastname && row.Cells[0].Value.ToString() == contact.Firstname)
                {
                    row.Click();
                    break;
                }
            }
            manager.MainWindow.Get<Button>("uxDeleteAddressButton").Click();
            Window question = manager.MainWindow.ModalWindow("Question");
            question.Get<Button>(SearchCriteria.ByText("Yes")).Click();
        }
    }
}

