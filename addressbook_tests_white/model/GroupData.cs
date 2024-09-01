using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AddressbookTestsWhite
{
    public class GroupData : IComparable<GroupData>, IEquatable<GroupData>
    {
    
        public string Name { get; set; }
        public GroupData()
        {
            Name = null;
        }

        public GroupData( string name)
        {
            Name = name;
        }

        public bool Equals(GroupData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return false;
            }
            if (Object.ReferenceEquals(this, other))
            {
                return true;
            }
            return this.Name == other.Name;
        }

        public int CompareTo(GroupData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return 1;
            }
            if (Object.ReferenceEquals(this, other))
            {
                return 0;
            }
            return Name.CompareTo(other.Name);
        }
    }
}

