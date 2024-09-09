namespace MantisTests;

public class AccountData
{
    private string name;
    private string password;
    private string email;
    public AccountData(string username, string password)
    {
        this.name = username; 
        this.password = password;
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

    public string Password 
    {
        get
        {
            return password;
        }
        set
        {
            password = value;
        }
    }
    public string Email { get; set; }
}
/*{
    public string Name { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
}*/