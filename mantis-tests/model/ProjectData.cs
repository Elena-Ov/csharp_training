using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToDB.Mapping;

namespace mantis_tests
{
    //[Table(Name = "mantis_project_table")]
    public class ProjectData : IEquatable<ProjectData>, IComparable<ProjectData>
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ProjectData() {}
        
        public ProjectData(string name)
        {
            Name = name;
        }
        public ProjectData(string name, string description)
        {
            Name = name;
            Description = description;
        }
        
        public bool Equals(ProjectData other)
            // стандартные проверки
        {
            if (Object.ReferenceEquals(other, null))
            {
                return false; 
            }
            
            if (Object.ReferenceEquals(this, other))
            {
                return true; 
            }
            
            return Name == other.Name;
        }

        public override int GetHashCode()
        {
            // согласовываем методы Equals и GetHashCode, вычисляются по именам
            return Name.GetHashCode();
        }

        // метод должен вернуть строковое представление объектов типа ProjectData
        public override string ToString()
        {
            return "name = " + Name;
        }

        public int CompareTo(ProjectData other)
        {
            //если второй объект с которым мы сравниваем null
            if (Object.ReferenceEquals(other, null))
            {
                return 1; // однозначно текущий объект больше
            }

            // если other != null, то сравнивать с ним можно по смыслу, в нашем случае по именам
            return Name.CompareTo(other.Name);
        }
        /*[Column (Name = "id")] 
        public string Id { get; set; }
        
        [Column (Name = "name")] 
        public string Name { get; set; }
        
        [Column (Name = "status")] 
        public string ProjectStatus { get; set; }
        
        [Column (Name = "enabled")] 
        public string ProjectEnabled { get; set; }
        
        [Column (Name = "view_state")] 
        public string ProjectView { get; set; }
        
        [Column (Name = "access_min")] 
        public string ProjectAccess { get; set; }
        
        [Column (Name = "description")] 
        public string Description { get; set; }
        
        [Column (Name = "inherit_global")] 
        public string ProjectInherit { get; set; }*/
        
        // метод для получения списка проектов из БД
        public static List<ProjectData> GetAll()
        {
            using (BugTrackerDB db = new BugTrackerDB())
            {
                return (from p in db.Projects select p).ToList();
            }
        }
        
        // метод для получения списка проектов через веб сервис
        public static List<ProjectData> GetProjectsList(AccountData account)
        {
            List<ProjectData> projectsList = new List<ProjectData>();
            
            MantisConnect.MantisConnectPortTypeClient client = new MantisConnect.MantisConnectPortTypeClient();
            MantisConnect.ProjectData[] projectsAccessible = client.mc_projects_get_user_accessibleAsync(account.UserName, account.Password).Result;
            foreach (MantisConnect.ProjectData project in projectsAccessible)
            {
                projectsList.Add(new ProjectData()
                {
                    Name = project.name,
                    Id = project.id,
                    Description = project.description
                });
            }

            return projectsList;
        }
    }
}

