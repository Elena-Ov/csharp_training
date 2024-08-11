using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Collections.Generic;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupRemovalTests : GroupTestBase
    {
        [Test]
        public void GroupRemovalTest()
        {
            app.Groups.manager.Navigator.GoToGroupsPage();

            if (!app.Groups.IsGroupFound())
            {
                GroupData group = new GroupData("abc");
                group.Header = "fff";
                group.Footer = "ggg";

                app.Groups.CreateGroup(group);
            }

        
            List<GroupData> oldGroups = GroupData.GetAll();
            //сохраняем объект который должен быть удален
            
            GroupData toBeRemoved = oldGroups[0];
            app.Groups.RemoveGroup(toBeRemoved);
            // убеждаемся что количество уменьшилось по сравнению со старым списком
            Assert.AreEqual(oldGroups.Count - 1, app.Groups.GetGroupCount());

            //к моменту проверки группа уже удалена из списка oldGroups, поэтому в нулевой позиции находится другая группа
            //чтобы сравнение работало правильно, нужно ещё до удаления сохранить сравниваемую группу в переменную
            //потом использовать это сохранённое значение
            List<GroupData> newGroups = GroupData.GetAll();
            
            oldGroups.RemoveAt(0);
            Assert.AreEqual(oldGroups, newGroups);

            foreach (GroupData group in newGroups)
            {
                Assert.AreNotEqual(group.Id, toBeRemoved.Id);
            }
        }
    }
}

    