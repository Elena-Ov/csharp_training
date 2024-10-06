using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using SimpleBrowser.WebDriver;
using System.Text.RegularExpressions;

namespace mantis_tests;

public class APIHelper : HelperBase
{
    public APIHelper(ApplicationManager manager) : base(manager) { }

    public void CreateNewIssue(AccountData account, ProjectData project, IssueData issueData)
    {
        //объект через который можно обращаться к операциям
        MantisConnect.MantisConnectPortTypeClient client = new MantisConnect.MantisConnectPortTypeClient();
        MantisConnect.IssueData issue = new MantisConnect.IssueData();
        issue.summary = issueData.Summary;
        issue.description = issueData.Description;
        issue.category = issueData.Category;
        issue.project = new MantisConnect.ObjectRef();
        issue.project.id = project.Id;
        
        client.mc_issue_addAsync(account.UserName, account.Password, issue);
    }
    public void CreateNewProject(AccountData account, ProjectData project)
    
    {
        //объект через который можно обращаться к операциям
        MantisConnect.MantisConnectPortTypeClient client = new MantisConnect.MantisConnectPortTypeClient();
        MantisConnect.ProjectData newProject = new MantisConnect.ProjectData();
        newProject.name = project.Name;
        newProject.description = project.Description;
        client.mc_project_addAsync(account.UserName, account.Password, newProject);
    }
    public void RemoveProject(AccountData account, ProjectData project)
    {
        //объект через который можно обращаться к операциям
        MantisConnect.MantisConnectPortTypeClient client = new MantisConnect.MantisConnectPortTypeClient();
        MantisConnect.ProjectData projectToDelete = new MantisConnect.ProjectData();
        projectToDelete.id = project.Id;
        client.mc_project_deleteAsync(account.UserName, account.Password, projectToDelete.id);
    }
}