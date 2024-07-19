using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAddressbookTests;

public class ContactForm : IEquatable<ContactForm>, IComparable<ContactForm>
{
    public ContactForm(string lastname, string firstname) 
    {
        Lastname = lastname;
        Firstname = firstname;
    }

    public bool Equals(ContactForm other)
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
    public int CompareTo(ContactForm other)
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
    public string Lastname { get; set; }
    public string Firstname { get; set; }
    public string Id { get; set; }
}