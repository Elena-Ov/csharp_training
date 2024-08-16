using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.DevTools.V85.CSS;
using System.Text.RegularExpressions;
using LinqToDB.Mapping;

namespace WebAddressbookTests

{
    [Table(Name = "addressbook")]

    public class ContactFormData : IEquatable<ContactFormData>, IComparable<ContactFormData>
    {
        public string allPhones;

        public string allEmails;
        //public string birthDay;
        //public string anniversary;

        public ContactFormData()
        {
        }

        public ContactFormData(string lastname, string firstname)
        {
            Lastname = lastname;
            Firstname = firstname;
        }

        public bool Equals(ContactFormData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return false;
            }

            if (Object.ReferenceEquals(this, other))
            {
                return true;
            }

            return Lastname == other.Lastname && Firstname == other.Firstname;
        }

        public override int GetHashCode()
        {
            return Lastname.GetHashCode() + Firstname.GetHashCode();
        }

        public override string ToString()
        {
            return "lastname  = " + Lastname + "\t" + "firstname = " + Firstname;
        }

        public int CompareTo(ContactFormData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return 1;
            }

            var result = Lastname.CompareTo(other.Lastname);
            if (result == 0)
                result = Firstname.CompareTo(other.Firstname);
            return result;
        }

        //поле создаются автоматически
        [Column (Name = "firstname")]
        public string Firstname { get; set; }
        
        [Column (Name = "middlename")]
        public string Middlename { get; set; }
        
        [Column (Name = "lastname")]
        public string Lastname { get; set; }
        
        [Column (Name = "nickname")]
        public string Nickname { get; set; }
        
        [Column (Name = "id"), PrimaryKey, Identity]
        public string Id { get; set; }
        
        [Column (Name = "company")]
        public string Company { get; set; }
        
        [Column (Name = "title")]
        public string Title { get; set; }

        [Column (Name = "address")]
        public string Address { get; set; }

        [Column (Name = "homepage")]
        public string HomePhone { get; set; }
        
        [Column (Name = "mobile")]
        public string MobilePhone { get; set; }
        
        [Column (Name = "fax")] 
        public string Fax { get; set; }
        
        [Column (Name = "work")]
        public string WorkPhone { get; set; }
        
        [Column (Name = "email")]
        public string Email { get; set; }
        
        [Column (Name = "email2")]
        public string Email2 { get; set; }
        
        [Column (Name = "email3")]
        public string Email3 { get; set; }
        
        [Column (Name = "homepage")]
        public string HomePage { get; set; }
        
        [Column (Name = "bday")]
        public string Bday { get; set; }
        
        [Column (Name = "bmonth")]
        public string Bmonth { get; set; }
        
        [Column (Name = "byear")]
        public string Byear { get; set; }
        
        [Column (Name = "aday")]
        public string Aday { get; set; }
        
        [Column (Name = "amonth")]
        public string Amonth { get; set; }
        
        [Column (Name = "ayear")]
        public string Ayear { get; set; }
        
        [Column (Name = "deprecated")]
        public string Deprecated { get; set; }
        
        public static List<ContactFormData> GetAllContacts()
        {
            using (AddressBookDB db = new AddressBookDB())
            {
                return (from c in db.Contacts.Where(x => x.Deprecated == "0000-00-00 00:00:00") select c).ToList();
            }
        }

        // расписываем полностью так как будем клеить строки - обратная проверка
        public string AllPhones

        {
            get
            {
                if (allPhones != null)
                {
                    return allPhones;
                }
                else
                {
                    return (CleanUp(HomePhone) + CleanUp(MobilePhone) + CleanUp(WorkPhone)).Trim();
                }
            }
            set { allPhones = value; }
        }

        //метод очищающий от ненужных символов
        private string CleanUp(string phone)
        {
            if (phone == null || phone == "")
            {
                return "";
            }

            return Regex.Replace(phone, "[()-]", "") + "\n";
        }

        public string AllEmails
        {
            get
            {
                if (allEmails != null)
                {
                    return allEmails;
                }
                else
                {
                    return GetAllEmails();
                }
            }
            set { allEmails = value; }
        }

        public string GetAllEmails()
        {
            string allEmails = "";
            var tempemail = CleanUpEmail(Email);
            if (!string.IsNullOrEmpty(tempemail))
                allEmails = tempemail;
            tempemail = CleanUpEmail(Email2);
            if (!string.IsNullOrEmpty(tempemail))
                allEmails += "\n" + tempemail;
            tempemail = CleanUpEmail(Email3);
            if (!string.IsNullOrEmpty(tempemail))
                allEmails += "\n" + tempemail;

            return allEmails.Trim('\n');
        }

        private string CleanUpEmail(string mail)
        {
            if (mail == null || mail == "")
            {
                return "";
            }

            return Regex.Replace(mail, "[ ]", "");
        }
        // склеиваем полное имя - обратная проверка
        /*public string Fullname

        {
            get
            {
                if (Fullname != null)
                {
                    return Fullname;
                }
                else
                {
                    return (CleanUpName(Firstname) + CleanUpName(Middlename) + CleanUpName(Lastname)).Trim();
                }
            }
            set
            {
                Fullname = value;
            }
        }
        private string CleanUpName(string name)
        {
            if (name == null || name =="")
            {
                return "";
            }

            return Regex.Replace(name, "[ ]", "") + "\n";
        }
        // если год не указан ???
        public string BirthDay

        {
            get
            {
                if (birthDay != null)
                {
                    return birthDay;
                }
                else
                {
                    return Bday + Bmonth + CleanUpYear(Byear);
                }
            }
            set { birthDay = value; }
        }

        public string Anniversary

        {
            get
            {
                if (anniversary != null)
                {
                    return anniversary;
                }
                else
                {
                    return Aday + Amonth + CleanUpYear(Ayear);
                }
            }
            set { anniversary = value; }
        }
        private string CleanUpYear(string year)
        {
            if (year == null || year =="")
            {
                return "";
            }

            return Regex.Replace(year, "[ ]", "") + "\n";
        }*/
    }
}
