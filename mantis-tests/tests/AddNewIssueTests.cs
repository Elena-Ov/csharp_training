namespace mantis_tests;

[TestFixture]

public class AddNewIssueTests : TestBase
{
    [Test]
    public void AddNewIssueTest()
    {
        AccountData account = new AccountData()
        {
            UserName = "administrator", Password = "root"
        };
        ProjectData project = new ProjectData()
        {
            Id = "1"
        };
        IssueData issue = new IssueData()
        {
            Summary = "some short text",
            Description = "some long text",
            Category = "General"
        };
        app.API.CreateNewIssue(account, project, issue);
    }
}