using System.Collections.Generic;
using Moq;
using TaskBoardAuth.Models;
using TaskBoardAuth.Services;

namespace TaskBoardAuth.Tests
{
    public class TaskBoardControllerTestsMockHelper
    {
        public static Mock<ITaskBoardService> SetupUpServiceMocks(Mock<ITaskBoardService> mockService)
        {
            mockService.Setup(x => x.GetTaskBoardModel(It.IsAny<int>())).Returns(new TaskBoardModel
                                                                                     {
                                                                                         Project = new Project
                                                                                                       {
                                                                                                           ProjectId = 4,
                                                                                                       },
                                                                                         Tasks = new List<Task>
                                                                                                     {
                                                                                                         new Task(),
                                                                                                         new Task(),
                                                                                                         new Task(),
                                                                                                         new Task()
                                                                                                     }
                                                                                     });
            mockService.Setup(x => x.GetProjects()).Returns(new List<Project>
                                                                {
                                                                    new Project
                                                                        {
                                                                            Name = "Project Three"
                                                                        },
                                                                    new Project
                                                                        {
                                                                            Name = "Project Two"
                                                                        }
                                                                });

            mockService.Setup(x => x.SaveNewTask(It.IsAny<Task>())).Returns((Task t) =>
                                                                                {
                                                                                    t.TaskId = 4;
                                                                                    return t;
                                                                                });
            return mockService;
        }
    }
}