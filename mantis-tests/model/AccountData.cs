namespace MantisTests;

public class AccountData
{
    private string userName;
    private string password;
    private string email;
    public AccountData(string username, string password)
    {
        this.userName = username; 
        this.password = password;
    }

    public string Id { get; set; }
    public string UserName 
    { 
        get { return userName; } set { userName = value; }
    }

    public string Password 
    {
        get { return password; } set { password = value; }
    }
    public string Email { get; set; }
    
}
/*{
    public string Name { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
}*/