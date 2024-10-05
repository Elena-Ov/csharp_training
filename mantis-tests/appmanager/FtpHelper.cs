using System.IO;
using System.Net.FtpClient;

namespace mantis_tests;

public class FtpHelper : HelperBase
{
    private FtpClient client;
    //конструктор - в качестве параматра FtpHelper принимает ApplicationManager manager
    // обращается к конструктору базового класса и передает ему manager, который там будет заполнен
    public FtpHelper(ApplicationManager manager) : base(manager)
    {
        client = new FtpClient();
        // адрес сервера
        client.Host = "localhost";
        client.Credentials = new System.Net.NetworkCredential("mantis", "mantis");
        client.Connect();
    }
    //методы помощники
    // для того чтобы временно прятать config файл
    public void BackupFile(String path)
    {
        String backupPath = path + ".bak";
        if (client.FileExists(backupPath))
        {
            return;
        }
        client.Rename(path, backupPath);
    }
    //востановление config файла из резервной копии
    public void RestoreBackupFile(String path)
    {
        String backupPath = path + ".bak";
        if (!client.FileExists(backupPath))
        {
            return;
        }
        if (client.FileExists(path))
            //удаление
        {
            client.DeleteFile(path);
        }
        //восстановление
        client.Rename(backupPath, path);
    }

    public void Upload(String path, Stream localFile)
    {
        if (client.FileExists(path))
            //удаление
        {
            client.DeleteFile(path);
        }
        // чтение данных из локального Stream и запись в удаленный
        //создаем удаленный Stream
        // в конце запись автом -ки закроется благодаря using
        using (Stream ftpStream = client.OpenWrite(path))
        {
            //размер буффера
            byte[] buffer = new byte[8 * 1024];
            // читаем данные
            //первый парам - буффер в который будут читаться данные, сдвиг - 0
            // кол-во байт которые будем читать=размеру буффера
            // Read() - возвращает количество байт которые реально были прочитаны
            int count = localFile.Read(buffer, 0, buffer.Length);
            while (count > 0)
            {
                //записываем данные в удаленный поток
                // count - показывает сколько у нас в наличии байт
                ftpStream.Write(buffer, 0, count);
                //читаем и записываем данные пока они не закончились
                count = localFile.Read(buffer, 0, buffer.Length);
            }
        }
    }
}