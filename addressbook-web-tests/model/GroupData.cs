using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToDB.Mapping;

namespace WebAddressbookTests
{
    [Table(Name = "group_list")]
    public class GroupData : IEquatable<GroupData>, IComparable<GroupData>
    {

        public GroupData()
        {
        }

        public GroupData(string name)
        {
            Name = name; // присваивание в свойство, тк поле создается автоматически
        }

        // в качестве параметра принимает второй объект типа GroupData other
        public bool Equals(GroupData other)
            // стандартные проверки
        {
            // 1-я если тот объект с которым сравниваем null
            if (Object.ReferenceEquals(other, null))
            {
                return false; // т.к. текущий объект есть 
            }

            // 2-я если это один и тот же объект, т.е. две ссылки указывают на один и тот же объект
            if (Object.ReferenceEquals(this, other))
            {
                return true; //никаких проверок по смыслу делать не надо, мы сравниваем объект сам с собой
            }

            // проверки по смыслу
            return Name == other.Name;

        }

        public override int GetHashCode()
        {
            // согласовываем методы Equals и GetHashCode, вычисляются по именам
            return Name.GetHashCode();
        }

        // метод должен вернуть строковое представление объектов типа GroupData
        public override string ToString()
        {
            return "name = " + Name + "\nheader = " + Header + "\nfooter = " + Footer;
        }

        public int CompareTo(GroupData other)
        {
            //если второй объект с которым мы сравниваем null
            if (Object.ReferenceEquals(other, null))
            {
                return 1; // однозначно текущий объект больше
            }

            // если other != null, то сравнивать с ним можно по смыслу, в нашем случае по именам
            return Name.CompareTo(other.Name);
        }
        
        // для каждого проперти указываем привязку к столбцу
        [Column (Name = "group_name")] 
        public string Name { get; set; }
        
        [Column (Name = "group_header")]
        public string Header { get; set; }
        
        [Column (Name = "group_footer")]

        public string Footer { get; set; }
        
        [Column (Name = "group_id"), PrimaryKey, Identity]
        public string Id { get; set; }

        public static List<GroupData> GetAll()
        {
            using (AddressBookDB db = new AddressBookDB())
            {
                return (from g in db.Groups select g).ToList();
            }
        }
        //метод для получения списка контактов по группе в которую они входят
        public List<ContactFormData> GetContactsByGroup()
        {
            using (AddressBookDB db = new AddressBookDB())
            {
                return (from c in db.Contacts
                    from gcr in db.GCR.Where(p 
                        => p.GroupId == Id && p.ContactId == c.Id)
                    select c).Distinct().ToList();
            }
            
        }
    }
}
