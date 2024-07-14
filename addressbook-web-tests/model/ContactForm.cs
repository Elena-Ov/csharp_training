namespace WebAddressbookTests;

public class ContactForm : IEquatable<ContactForm>, IComparable<ContactForm>
{
    private string firstname;
    private string lastname;

    public ContactForm(string firstname, string lastname) 
    {
        this.firstname = firstname;
        this.lastname = lastname;
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

        return Firstname == other.Firstname && Lastname == other.Lastname;
    }
    public override int GetHashCode()
    {
        return Firstname.GetHashCode() + Lastname.GetHashCode(); 
    }
    public override string ToString()
    {
        return "firstname = " + Firstname + "lastname = " + Lastname;
    }
    public int CompareTo(ContactForm other)
    {
        if (Object.ReferenceEquals(other, null))
        {
            return 1; 
        }
        
        return Firstname.CompareTo(other.Firstname) + Lastname.CompareTo(other.Lastname);
    }
    public string Firstname
    {
        get
        {
            return firstname;
        }
        set
        {
            firstname = value;
        }
    }

    public string Lastname
    {
        get
        {
            return lastname;
        }
        set
        {
            lastname = value;
        }
    }
}