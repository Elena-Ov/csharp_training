namespace WebAddressbookTests;

// указываем что этот класс наследует IEquatable
// его можно сравнивать с другими объектами типа GroupData
// определяем функцию которая реализует это сравнение public bool Equals()
// добавляем второй стандартный метод GetHashCode()
public class GroupData : IEquatable<GroupData>, IComparable<GroupData>
{
    private string name;
    private string header = ""; 
    private string footer = ""; 
    public GroupData(string name)
    {
        this.name = name;
    }
    // в качестве параметра принимает второй объект типа GroupData other
    public bool Equals(GroupData other)
    // стандартные проверки
    {
        // 1-я если тот объект с которым сравниваем null
        if (Object.ReferenceEquals(other, null))
        {
            return false;// т.к. текущий объект есть 
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
        return "name = " + Name;
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

    public string Name
    {
        get
        {
            return name;
        }
        set
        {
            name = value;
        }
    }
    public string Header
    {
        get
        {
            return header;
        }
        set
        {
            header = value;
        }
    }
    public string Footer
    {
        get
        {
            return footer;
        }
        set
        {
            footer = value;
        }
    }
}
