using OpaqueMail;

namespace MantisTests;

public class MailHelper : HelperBase
{
    public MailHelper(ApplicationManager manager) : base(manager) {}
    //метод будет читать последнее письмо и возвращать пустой текст
    public String GetLastMail(AccountData account)
    {
        //ждем 15 сек и каждую сек проверяем есть ли письмо
        for (int i = 0; i < 20; i++)
        {
            Pop3Client pop3 = 
                new Pop3Client("localhost", 110, account.Name, account.Password, false);
            // устанавливаем соединение с сервером, каждый раз тк данные кешируются
            pop3.Connect();
            pop3.Authenticate();
            if (pop3.GetMessageCount() > 0)
            {
                MailMessage message = pop3.GetMessage(1);
                string body = message.Body;
                pop3.DeleteMessage(1);
                return body;
            }
            else
            {
                System.Threading.Thread.Sleep(3000);
            }
        }
        //письмо так и не пришло
        return null;
    }
}