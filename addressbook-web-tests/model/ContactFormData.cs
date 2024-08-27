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
        

        public ContactFormData() {}

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

        // для ContactInformationTests
        private string GetInfoLine(string line1, string line2, string line3, 
            string line4 = null, string line5 = null)
        {
            string readyLine = null;
            if (line1 != null && line1 != "") { readyLine += line1 + "\n"; }
            if (line2 != null && line2 != "") { readyLine += line2 + "\n"; }
            if (line3 != null && line3 != "") { readyLine += line3 + "\n"; }
            if (line4 != null && line4 != "") { readyLine += line4 + "\n"; }
            if (line5 != null && line5 != "") { readyLine += line4 + "\n"; }
            if (readyLine != null && readyLine != "") { readyLine += "\n"; }

            return readyLine;
        }
        // для ContactInformationTests

        public string GetInformationFromIdPage()
        {
            string infoLine = "";
            string fullname = null;
            string addHomePhone = null;
            string addMobilePhone = null;
            string addWorkPhone = null;

            if (this.Firstname != null && this.Firstname != "")
            {
                fullname += this.Firstname;
            }
            if (this.Middlename != null && this.Middlename != "")
            {
                if (fullname != null && fullname != "")
                {
                    fullname += " ";
                }
                fullname += this.Middlename;
            }
            if (this.Lastname != null && this.Lastname != "")
            {
                if (fullname != null && fullname != "")
                {
                    fullname += " ";
                }
                fullname += this.Lastname;
            }

            if (this.HomePhone != null && this.HomePhone != "") { addHomePhone = "H: " + this.HomePhone; }
            if (this.MobilePhone != null && this.MobilePhone != "") { addMobilePhone = "M: " + this.MobilePhone; }
            if (this.WorkPhone != null && this.WorkPhone != "") { addWorkPhone = "W: " + this.WorkPhone; }

            infoLine += GetInfoLine(fullname, this.Nickname, this.Address);
            infoLine += GetInfoLine(addHomePhone, addMobilePhone, addWorkPhone);
            infoLine += GetInfoLine(this.Email, this.Email2, this.Email3);

            return infoLine.Trim('\n');
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
                return (from c in db.Contacts select c).ToList();
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

            return Regex.Replace(phone, "[ ()-]", "") + "\n";
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
                    return (CleanUpFullname(Firstname) + CleanUpFullname(Middlename) + CleanUpFullname(Lastname)).Trim();
                }
            }
            set
            {
                Fullname = value;
            }
        }
        private string CleanUpFullname(string fullname)
        {
            if (fullname == null || fullname =="")
            {
                return "";
            }

            return Regex.Replace(fullname, "[ ]", "") + "\n";
        }*/
    }
}
