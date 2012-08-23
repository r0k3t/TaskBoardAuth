using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Security;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TaskBoardAuth.Controllers;
using TaskBoardAuth.Core.Interfaces;
using TaskBoardAuth.Core.Models;

namespace TaskBoardAuth.Tests.Controllers
{
    [TestClass]
    public class TaskBoardControllerTests
    {
        private TaskBoardController controller;
        private ITaskBoardRepository service;
        private Mock<ITaskBoardRepository> mockService;
        private Mock<IStaticMembershipService> mockStaticMembershipService;
        private Mock<IProfileFactoryService> mockProfileFactoryService;
        private Guid fakeProviderUserKey;

        [TestInitialize]
        public void Setup()
        {
            mockService = new Mock<ITaskBoardRepository>();
            mockService = TaskBoardControllerTestsMockHelper.SetupUpServiceMocks(mockService);

            fakeProviderUserKey = Guid.NewGuid();
            var userMock = new Mock<MembershipUser>();
            userMock.Setup(u => u.ProviderUserKey).Returns(fakeProviderUserKey);
            userMock.Setup(u => u.UserName).Returns("kenn");

            mockStaticMembershipService = new Mock<IStaticMembershipService>();
            mockStaticMembershipService.Setup(x => x.GetUser()).Returns(userMock.Object);

            mockProfileFactoryService = new Mock<IProfileFactoryService>();
            mockProfileFactoryService.Setup(x => x.GetUserProfile(It.IsAny<string>())).Returns(new UserProfile());
            mockProfileFactoryService.Setup(x => x.GetPropertyValue(It.IsAny<string>(), It.IsAny<string>())).Returns("test value");

            service = mockService.Object;
            controller = new TaskBoardController(service, mockStaticMembershipService.Object, mockProfileFactoryService.Object);
        }

        [TestMethod]
        public void Index_Will_Return_A_List_Of_Active_Projects()
        {
            var view = controller.Index();
            Assert.AreEqual(view.ViewData.Model.GetType(), typeof(List<Project>));
        }

        [TestMethod]
        public void TaskBoard_Will_Get_A_TaskBoardModel_As_Model_For_A_Given_Project_Id()
        {
            var view = controller.TaskBoard(1);
            Assert.AreEqual(view.ViewData.Model.GetType(), typeof(TaskBoardModel));
        }

        [TestMethod]
        public void CreateTask_Will_Set_Status_To_Backlog_And_Call_SaveNewTask()
        {
            var result = controller.CreateTask(new Task
                                      { 
                                          Description = "",
                                          Name = "xx",
                                          ProjectId = 1,
                                      });
            mockService.Verify(x => x.SaveNewTask(It.IsAny<Task>()), Times.Once());
            Assert.AreEqual(TaskStatus.Backlog, ((Task) result.Data).TaskStatus);
            Assert.AreEqual(12, ((Task)result.Data).LocationLeft);
            Assert.AreEqual(190, ((Task)result.Data).LocationTop);

        }

        [TestMethod] 
        public void CreatProject_Will_Return_A_Project_As_A_JsonResult()
        {
            var result = controller.CreateProject(new Project
                                                      {
                                                          Name = "test name",
                                                          Description = "test description"
                                                      });
            mockService.Verify(x => x.SaveProject(It.IsAny<Project>()), Times.Once());
            Assert.AreEqual(fakeProviderUserKey, ((Project)result.Data).OwnerId);
        }

        [TestMethod]
        public void CreateProject_Wont_Attempt_To_Create_A_Project_With_A_Blank_Or_Null_Name()
        {
            var result = controller.CreateProject(new Project());
            mockService.Verify(x => x.SaveProject(It.IsAny<Project>()), Times.Never());
            Assert.AreEqual("Please supply a name for this project.", ((Project)result.Data).Name);
        }

        [TestMethod]
        public void CloseProject_Will_Call_Close_On_Selected_ProjectId()
        {
            const int projectId = 5;
            const string expectedMsg = "Project Id: 5 successfully closed.";
            mockService.Setup(x => x.CloseProject(projectId)).Returns(new OperationStatus
                                                                         {
                                                                             Success = true
                                                                         });
            var result = controller.CloseProject(5);
            Assert.AreEqual(expectedMsg, result.Data.ToString());
            mockService.Verify(x => x.CloseProject(It.IsAny<int>()), Times.Once());
        }

        [TestMethod]
        public void UpdateStatus_Will_Call_Repository_UpdateStatus()
        {
            var task = new Task();
            controller.UpdateTaskStatus(task);
            mockService.Verify(x => x.UpdateStatus(It.IsAny<Task>()), Times.Once());
        }
    }
}