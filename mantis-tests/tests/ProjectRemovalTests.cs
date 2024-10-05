using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Collections.Generic;

namespace mantis_tests;

public class ProjectRemovalTests : TestBase
{
    [Test]
    public void ProjectRemovalTest()
    {
        int indexOfProjectToRemove = 0;
        
        AccountData account = new AccountData()
        { UserName = "administrator", Password = "root" };
        
        ProjectData project = new ProjectData()
        {
            Name = "new_test_project" + GenerateRandomString(30),
            Description = "description_for_test" + GenerateRandomString(30)
        };
        
        List<ProjectData> oldProjects = ProjectData.GetProjectsList(account);
        while (oldProjects.Count <= indexOfProjectToRemove)
        {
            app.API.CreateNewProject(account, project);
            oldProjects = ProjectData.GetProjectsList(account);
        }
        
        ProjectData toBeRemoved = oldProjects[indexOfProjectToRemove];
        
        app.API.RemoveProject(account, toBeRemoved);
        Assert.AreEqual(oldProjects.Count - 1, ProjectData.GetProjectsList(account).Count);

        List<ProjectData> newProjects = ProjectData.GetProjectsList(account);

        oldProjects.Remove(toBeRemoved);
        oldProjects.Sort();
        newProjects.Sort();
        Assert.AreEqual(oldProjects, newProjects);
        
        
        
        /*ProjectData project = new ProjectData()
        {
            Id = "1"//????
        };*/
        
        /*app.Auth.Login(new AccountData("administrator", "root"));
        app.Navigator.GoToManagementPage();
        app.Navigator.GoToProjectManagementPage();
        
        if (!app.Projects.IsProjectFound()) 
        { 
            ProjectData project = new ProjectData
            { Name = GenerateRandomString(30) };
            
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
        }*/
    }
}