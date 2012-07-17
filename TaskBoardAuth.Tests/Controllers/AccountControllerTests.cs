using System;
using System.Configuration;
using System.Globalization;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Profile;
using System.Web.Security;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TaskBoardAuth.Controllers;
using TaskBoardAuth.Models;
using TaskBoardAuth.Services;

namespace TaskBoardAuth.Tests.Controllers
{
    [TestClass]
    public class AccountControllerTests
    {
        [TestMethod]
        [Ignore]
        public void Register_Will_Set_UserProfile_First_And_Last_Name()
        {
            var profileBaseMock = new Mock<ProfileBase>();
            profileBaseMock.Setup(x => x.Save()).Verifiable("Save was not called");
            var userMock = new Mock<MembershipUser>();
            userMock.Setup(u => u.ProviderUserKey).Returns(Guid.NewGuid());

            var staticMembershipServiceMock = new Mock<IStaticMembershipService>();
            staticMembershipServiceMock.Setup(x => x.GetUser()).Returns(userMock.Object);
            
            MembershipCreateStatus status = MembershipCreateStatus.Success;
            
            staticMembershipServiceMock.Setup(x => x.CreateUser(
                It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(),
                It.IsAny<bool>(), It.IsAny<object>(), out status)).Verifiable();

            //var accountController = new AccountController(staticMembershipServiceMock.Object);
            //accountController.Register(new RegisterModel
            //                               {
            //                                   ConfirmPassword = "",
            //                                   Email = "",
            //                                   FirstName = "",
            //                                   LastName = "",
            //                                   Password = "",
            //                                   UserName = ""
            //                               });
            //accountController.ModelState.Add("Isvalid", new ModelState()
            //                                                {
            //                                                    Value = new ValueProviderResult(true, "", CultureInfo.InvariantCulture)
            //                                                });
            //profileBaseMock.Verify();
            //staticMembershipServiceMock.Verify();

        }
    }
}
