using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace MantisTests;

   [TestFixture]
public class ProjectCreationTests : TestBase
{
    [Test]
    public void ProjectCreationTest()
    {
        app.Auth.Login(new AccountData("administrator", "root"));
        List<ProjectData> oldProjects = ProjectData.GetAll();
        var project = new ProjectData { ProjectName = GenerateRandomString(30) };
        app.Projects.CreateProject(project);
        Assert.AreEqual(oldProjects.Count + 1, app.Projects.GetProjectsCount(1));

        List<ProjectData> newProjects = ProjectData.GetAll();

        oldProjects.Add(project);
        oldProjects.Sort();
        newProjects.Sort();

        Assert.AreEqual(oldProjects, newProjects);
    }
    
    /*// реализуем метод генерации тестовых данных
    public static IEnumerable<ProjectData> RandomProjectDataProvider()
    {
        // создаем список
        List<ProjectData> projects = new List<ProjectData>();
        //заполняем его данными, которые будем генерировать
        // 3 разных тестовых набора
        for (int i = 0; i < 3; i++)
        {
            //максимальная длина строки, которую мы хотим сгенерировать
            projects.Add(new ProjectData(GenerateRandomString(30))
            {});
        }

        return projects;
    }
    
    [Test, TestCaseSource("RandomProjectDataProvider")]
    
    public void ProjectCreationTest(ProjectData project)
    {
        app.Auth.Login(new AccountData("administrator", "root"));
        List<ProjectData> oldProjects = ProjectData.GetAll();

        app.Projects.CreateProject(project);
        Assert.AreEqual(oldProjects.Count + 1, app.Projects.GetProjectsCount(1));

        List<ProjectData> newProjects = ProjectData.GetAll();

        oldProjects.Add(project);
        oldProjects.Sort();
        newProjects.Sort();

        Assert.AreEqual(oldProjects, newProjects);
    }*/
}