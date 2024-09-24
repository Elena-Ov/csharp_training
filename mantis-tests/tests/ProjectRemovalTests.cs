using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Collections.Generic;

namespace MantisTests;

public class ProjectRemovalTests : TestBase
{
    [Test]
    public void ProjectRemovalTest()
    {
        app.Auth.Login(new AccountData("administrator", "root"));
        app.Navigator.GoToManagementPage();
        app.Navigator.GoToProjectManagementPage();
        
        if (!app.Projects.IsProjectFound()) 
        { 
            ProjectData project = new ProjectData
            { ProjectName = GenerateRandomString(30) };
            
            app.Projects.CreateProject(project);
        }
       
        List<ProjectData> oldProjects = ProjectData.GetAll();
        
        ProjectData toBeRemoved = oldProjects[0];
        app.Projects.RemoveProject(toBeRemoved);
        Assert.AreEqual(oldProjects.Count - 1, app.Projects.GetProjectsCount());

        List<ProjectData> newProjects = ProjectData.GetAll();

        oldProjects.RemoveAt(0);

        Assert.AreEqual(oldProjects, newProjects);
        foreach (ProjectData project in newProjects)
        {
            Assert.AreNotEqual(project.Id, toBeRemoved.Id);
        }
    }
}