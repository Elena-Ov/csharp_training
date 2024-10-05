using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using LinqToDB.DataProvider.SqlServer;
using NUnit.Framework;
using OpenQA.Selenium;

namespace mantis_tests
{
    public class ProjectManagementHelper : HelperBase
    {
        protected string baseURL = "http://localhost/mantisbt-2.26.3/";
        // в базовый класс передаем тоже ссылку на manager
        //public AccountData account = new AccountData("administrator", "root");
        public ProjectManagementHelper(ApplicationManager manager) : base(manager)
        {
            //добавила
            //this.manager = manager;
            //driver = manager.Driver;
        }
        public ProjectManagementHelper CreateProject(ProjectData project)
        {
            manager.Navigator.GoToManagementPage();
            manager.Navigator.GoToProjectManagementPage();
            manager.Navigator.GoToProjectCreationPage();
            FillProjectName(project);
            AddProject();
            return this;
            //Wait(100);
            //return !IsElementPresent(By.XPath("//div[@class='alert alert-danger']"));
        }

        public ProjectManagementHelper FillProjectName(ProjectData project)
        {
            //Type(By.Id("project-name"), project.ProjectName);
            Type(By.Name("name"), project.Name);
            return this;
        }

        public ProjectManagementHelper AddProject()
        {
            driver.FindElement(By.XPath("//input[@type='submit']")).Click();
            return this;
        }
        public ProjectManagementHelper RemoveProject(ProjectData project)
        {
            manager.Navigator.GoToManagementPage();
            manager.Navigator.GoToProjectManagementPage();
            ChooseProject(project.Id);
            RemoveProjectButton();
            return this;
            //Wait(100);
            // return !IsElementPresent(By.XPath("//div[@class='alert alert-danger']"));
        }

        // по идентификатору
        public ProjectManagementHelper ChooseProject(string id)
        {
            driver.FindElement(By.XPath("//a[starts-with(@href,'manage_proj_edit_page.php?project_id="+id+"')] ")).Click();
            return this;
        }

        public ProjectManagementHelper RemoveProjectButton()
        {
            driver.FindElement(By.XPath("//*[@id='manage-proj-update-form']/div/div[3]/button[2]")).Click();
            driver.FindElement(By.XPath("//input[@type='submit']")).Click();
            return this;
        }

        public int GetProjectsCount()
        {
            return driver.FindElements(By.XPath("//*[@id='main-container']/div[2]/div[2]/div/div/div[2]/div[2]/div/div[2]/table/tbody/tr")).Count;
            
        }

        public bool IsProjectFound()
        {
            return driver.Url == baseURL + "manage_proj_page.php" &&
                   IsElementPresent(By.XPath("//*[@id='main-container']/div[2]/div[2]/div/div/div[2]/div[2]/div/div[2]/table/tbody/tr"));
        }
    } 
}

