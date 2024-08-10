using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToDB;

namespace WebAddressbookTests
{
    // класс для соединения с БД
    public class AddressBookDB : LinqToDB.Data.DataConnection
    {
        // конструктор задача которого обращаться к конструктору базового класса
        // и передавать в качестве параметра название БД
        public AddressBookDB() : base(ProviderName.MySql, 
            @"server=localhost; database=addressbook; port=3306; Uid=root; Pwd=; charset=utf8; Allow Zero Datetime=true") {}
        // для каждой таблицы специальный метод который возвращает таблицу данных
        public ITable<GroupData> Groups
        {
            get { return GetTable<GroupData>(); }
        }
        public ITable<ContactFormData> Contacts
        {
            get { return GetTable<ContactFormData>(); }
        }
        
    }
}

