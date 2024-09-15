using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToDB;

namespace MantisTests

{
    // класс для соединения с БД
    public class BugTrackerDB : LinqToDB.Data.DataConnection
{
    // конструктор задача которого обращаться к конструктору базового класса
    // и передавать в качестве параметра название БД
    // указываем connection stream для связи с бд
    public BugTrackerDB() : base(ProviderName.MySql, 
        @"server=localhost; 
database=bugtracker; port=3306; Uid=root; Pwd=; charset=utf8; Allow Zero Datetime=true") {}        
    // метод который извлекает данные из нужной таблицы
    public ITable<ProjectData> Projects { get { return this.GetTable<ProjectData>(); } }
}
    
}