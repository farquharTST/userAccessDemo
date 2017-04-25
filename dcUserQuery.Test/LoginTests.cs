using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using dcUserQuery;
using System.DirectoryServices;

namespace dcUserQuery.Test
{
    [TestClass]
    public class LoginTests
    {
        /// <summary>
        /// Returns all the AD DS groups of which the current user is a member.
        /// </summary>
        [TestMethod]
        public void UserInfo_ReturnsDomainGroups()
        {
            var uInfo = new UserInfo();
            var groups = uInfo.DomainGroups;
            var displayName = uInfo.DisplayName;
            var authGroups = uInfo.AuthDomainGroups;

            Assert.IsNotNull(groups);
            Assert.IsNotNull(authGroups);
        }



        [TestMethod]
        public void ValidUser_IsMemberofGroups_ReturnsGroupList()
        {
            var userQuery = new DcUserQuery();
            var groupList = userQuery.GetMyGroups();
            Assert.IsNotNull(groupList);
            Assert.IsTrue(groupList.Count > 0);
        }

        #region Private Methods

        #endregion
    }


}
