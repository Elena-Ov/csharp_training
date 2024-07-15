namespace WebAddressbookTests;

public class ContactForm : IEquatable<ContactForm>, IComparable<ContactForm>
{
    private string firstname;
    private string lastname;

    public ContactForm(string lastname, string firstname) 
    {
        this.lastname = lastname;
        this.firstname = firstname;
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
        
        return Firstname.CompareTo(other.Lastname) + Lastname.CompareTo(other.Firstname);
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
}