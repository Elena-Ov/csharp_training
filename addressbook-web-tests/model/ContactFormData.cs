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
    private string allPhones;
    private string allEmails;
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
    public string Lastname { get; set; }
    public string MiddleName { get; set; }
    public string Firstname { get; set; }
    public string NickName { get; set; }
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
    
    /*public string AllEmails
    {
        get
        {
            if (allEmails != null)
            {
                return allEmails;
            }
            else
            {
                return (CleanUpEmail(Email) + CleanUpEmail(Email2) + CleanUpEmail(Email3)).Trim();
            }
        }
        set
        {
            allEmails = value;
        } 
    }
    private string CleanUpEmail(string mail)
    {
        if (mail == null || mail == "")
        {
            return "";
        }

        return Regex.Replace(mail,"[ ]", "") + "\n";
    }*/
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
    
    private string GetAllEmails()
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
}