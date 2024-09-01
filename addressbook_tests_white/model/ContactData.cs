using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressbookTestsWhite
{
    public class ContactData : IEquatable<ContactData>, IComparable<ContactData>
    {
        public string Id { get; set; }
        public string Lastname { get; set; }
        public string Firstname { get; set; }

        public string Middlename { get; set; }
        public string Nickname { get; set; }
        public string Address { get; set; }
        public string HomePhone { get; set; }
        public string MobilePhone { get; set; }
        public string WorkPhone { get; set; }
        public string Email { get; set; }
        public string Email2 { get; set; }
        public string Email3 { get; set; }

        public ContactData() {}
        public ContactData(string lastname, string firstname)
        {
            Lastname = lastname;
            Firstname = firstname;
        }

        public bool Equals(ContactData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return false;
            }
            if (Object.ReferenceEquals(this, other))
            {
                return true;
            }
            return this.Lastname == other.Lastname && this.Firstname == other.Firstname;
        }

        public int CompareTo(ContactData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return 1;
            }
            if (Object.ReferenceEquals(this, other))
            {
                return 0;
            }
            return ("" + this.Lastname + " " + this.Firstname).CompareTo("" + other.Lastname + " " + other.Firstname);
        }

        public override int GetHashCode()
        {
            return ("" + this.Lastname + " " + this.Firstname).GetHashCode();
        }

        public override string ToString()
        {
            return ("contact = " + this.Lastname + " " + this.Firstname);
        }
    } 
}

