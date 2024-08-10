using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.DevTools.V85.CSS;
using System.Text.RegularExpressions;

namespace WebAddressbookTests;

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
    public string Firstname { get; set; }
    public string Middlename { get; set; }
    public string Lastname { get; set; }
    public string Nickname { get; set; }
    public string Id { get; set; }
    public string Company { get; set; }
    public string Title { get; set; }
    
    public string Address { get; set; }
    
    public string HomePhone { get; set; }
    
    public string MobilePhone { get; set; }
    public string Fax { get; set; }
    public string WorkPhone { get; set; }
    public string Email { get; set; }
    public string Email2 { get; set; }
    public string Email3 { get; set; }
    public string HomePage { get; set; }
    public string Bday { get; set; }
    public string Bmonth { get; set; }
    public string Byear { get; set; }
    public string Aday { get; set; }
    public string Amonth { get; set; }
    public string Ayear { get; set; }
    
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
        set
        {
            allPhones = value;
        } 
    }
    //метод очищающий от ненужных символов
    private string CleanUp(string phone)
    {
        if (phone == null || phone =="")
        {
            return "";
        }

        return Regex.Replace(phone, "[ -()]", "") + "\n";
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
        set
        {
            allEmails = value;
        } 
    }
    
    public string GetAllEmails()
    {
        string allEmails = "";
        var tempemail = CleanUpEmail(Email);
        if (!string.IsNullOrEmpty(tempemail))
            allEmails = tempemail;
        tempemail = CleanUpEmail(Email2);
        if (!string.IsNullOrEmpty(tempemail))
            allEmails += "\n"+tempemail;
        tempemail = CleanUpEmail(Email3);
        if (!string.IsNullOrEmpty(tempemail))
            allEmails += "\n"+tempemail;
        
        return allEmails.Trim('\n');
    }
    
    private string CleanUpEmail(string mail)
    {
        if (mail == null || mail == "")
        {
            return "";
        }

        return Regex.Replace(mail,"[ ]", "");
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
