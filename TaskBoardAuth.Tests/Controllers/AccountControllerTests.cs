using System.Globalization;
using System.Web.Mvc;
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
        public void Register_Will_Call_CreateUser()
        {
            MembershipCreateStatus status = MembershipCreateStatus.Success;
            var staticMembershipServiceMock = new Mock<IStaticMembershipService>();
            staticMembershipServiceMock.Setup(x => x.CreateUser(
                It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(),
                It.IsAny<bool>(), It.IsAny<object>(), out status)).Verifiable();

            var accountController = new AccountController(staticMembershipServiceMock.Object,
                                                          new Mock<IProfileFactoryService>().Object,
                                                          new Mock<IFormsAuthenticationService>().Object);
            accountController.Register(new RegisterModel
            {
                ConfirmPassword = "",
                Email = "",
                FirstName = "",
                LastName = "",
                Password = "",
                UserName = ""
            });
            accountController.ModelState.Add("Isvalid", new ModelState
            {
                Value =
                    new ValueProviderResult(true, "",
                                            CultureInfo.InvariantCulture)
            });
            staticMembershipServiceMock.Verify();

            //var userMock = new Mock<MembershipUser>();
            //userMock.Setup(u => u.ProviderUserKey).Returns(Guid.NewGuid());


            //staticMembershipServiceMock.Setup(x => x.GetUser()).Returns(userMock.Object).Verifiable();

            //var formsAuthMock = new Mock<IFormsAuthenticationService>();
            //formsAuthMock.Setup(x => x.SetAuthCookie(It.IsAny<string>(), It.IsAny<bool>())).Verifiable();

            
        }

        [TestMethod]
        public void Register_Will_Set_FirstName_And_LastName()
        {
            var profileFactoryMock = new Mock<IProfileFactoryService>();
            var accountController = new AccountController(new Mock<IStaticMembershipService>().Object,
                                                          profileFactoryMock.Object,
                                                          new Mock<IFormsAuthenticationService>().Object);
            accountController.Register(new RegisterModel
            {
                ConfirmPassword = "",
                Email = "",
                FirstName = "Ken",
                LastName = "N",
                Password = "",
                UserName = "kenn"
            });
            accountController.ModelState.Add("Isvalid", new ModelState
            {
                Value = new ValueProviderResult(true, "", CultureInfo.InvariantCulture)
            });
            profileFactoryMock.Verify(p => p.SetPropertyValue(It.Is<string>(x => x.Equals("kenn")),
                                                 It.Is<string>(x => x.Equals("LastName")),
                                                 It.Is<string>(x => x.Equals("N"))), Times.Once());
            profileFactoryMock.Verify(p => p.SetPropertyValue(It.Is<string>(x => x.Equals("kenn")),
                                                 It.Is<string>(x => x.Equals("FirstName")),
                                                 It.Is<string>(x => x.Equals("Ken"))), Times.Once());
        }
    }
}