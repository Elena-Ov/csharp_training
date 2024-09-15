using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToDB.Mapping;

namespace MantisTests
{
    [Table(Name = "mantis_project_table")]
    public class ProjectData : IEquatable<ProjectData>, IComparable<ProjectData>
    {
        public ProjectData() {}
        
        public ProjectData(string projectName)
        {
            ProjectName = projectName;
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
            
            return ProjectName == other.ProjectName;
        }

        public override int GetHashCode()
        {
            // согласовываем методы Equals и GetHashCode, вычисляются по именам
            return ProjectName.GetHashCode();
        }

        // метод должен вернуть строковое представление объектов типа ProjectData
        public override string ToString()
        {
            return "name = " + ProjectName;
        }

        public int CompareTo(ProjectData other)
        {
            //если второй объект с которым мы сравниваем null
            if (Object.ReferenceEquals(other, null))
            {
                return 1; // однозначно текущий объект больше
            }

            // если other != null, то сравнивать с ним можно по смыслу, в нашем случае по именам
            return ProjectName.CompareTo(other.ProjectName);
        }
        [Column (Name = "id")] 
        public string Id { get; set; }
        
        [Column (Name = "name")] 
        public string ProjectName { get; set; }
        
        [Column (Name = "status")] 
        public string ProjectStatus { get; set; }
        
        [Column (Name = "enabled")] 
        public string ProjectEnabled { get; set; }
        
        [Column (Name = "view_state")] 
        public string ProjectView { get; set; }
        
        [Column (Name = "access_min")] 
        public string ProjectAccess { get; set; }
        
        [Column (Name = "inherit_global")] 
        public string ProjectInherit { get; set; }
        
        public static List<ProjectData> GetAll()
        {
            using (BugTrackerDB db = new BugTrackerDB())
            {
                return (from p in db.Projects select p).ToList();
            }
        }
    }
}

