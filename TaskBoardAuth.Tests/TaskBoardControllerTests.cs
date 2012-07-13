using System.Collections.Generic;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TaskBoardAuth.Controllers;
using TaskBoardAuth.Models;
using TaskBoardAuth.Services;

namespace TaskBoardAuth.Tests
{
    [TestClass]
    public class TaskBoardControllerTests
    {
        private TaskBoardController controller;
        private ITaskBoardService service;
        private Mock<ITaskBoardService> mockService;

        [TestInitialize]
        public void Setup()
        {
            mockService = new Mock<ITaskBoardService>();
            mockService = TaskBoardControllerTestsMockHelper.SetupUpServiceMocks(mockService);

            service = mockService.Object;
            controller = new TaskBoardController(service);
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
                                          CreatedById = 1,
                                          LocationLeft = 1,
                                          Description = "",
                                          LocationTop = 1,
                                          Name = "xx",
                                          ProjectId = 1,
                                          TaskOwnerId = 1,
                                          UpdatedById = 1
                                      });
            mockService.Verify(x => x.SaveNewTask(It.IsAny<Task>()), Times.Once());
            Assert.AreEqual(TaskStatus.Backlog, ((Task) result.Data).TaskStatus);

        }

    }
}