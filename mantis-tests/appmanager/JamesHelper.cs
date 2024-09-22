using MinimalisticTelnet;

namespace MantisTests;

public class JamesHelper : HelperBase
{
    public JamesHelper(ApplicationManager manager) : base(manager){}

    public void Add(AccountData account)
    {
        if (Verify(account))
        {
            return;
        }
        TelnetConnection telnet = LoginToJames();
        telnet.WriteLine("adduser" + account.UserName + " " + account.Password);
        System.Console.Out.WriteLine(telnet.Read());
    }

    public void Delete(AccountData account) 
    {
        if (! Verify(account))
        {
            return;
        }
        TelnetConnection telnet = LoginToJames();
        telnet.WriteLine("deluser" + account.UserName);
        System.Console.Out.WriteLine(telnet.Read());
    }

    public bool Verify(AccountData account)
    {
        TelnetConnection telnet = LoginToJames();
        telnet.WriteLine("verify" + account.UserName + " " + account.Password);
        String s = telnet.Read();
        System.Console.Out.WriteLine(s);
        return ! s.Contains("does not exist");
    }
    private TelnetConnection LoginToJames()
    {
        // хост и порт на котором находится почтовый сервер
        TelnetConnection telnet= new TelnetConnection("localhost", 4555);
        //читаем текст который James вывел на консоль
        System.Console.Out.WriteLine(telnet.Read());
        telnet.WriteLine("root");
        System.Console.Out.WriteLine(telnet.Read());
        telnet.WriteLine("root");
        System.Console.Out.WriteLine(telnet.Read());
        return telnet;
    }
}