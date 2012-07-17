using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskBoardAuth.Controllers;
using TaskBoardAuth.Models;
using TaskBoardAuth.Services;

namespace TaskBoardAuth
{
    public class TaskBoardControllerFactory : DefaultControllerFactory 
    {
        protected override IController GetControllerInstance(System.Web.Routing.RequestContext requestContext, Type controllerType)
        {
            if(controllerType == typeof(TaskBoardController))
                return new TaskBoardController(new TaskBoardService(new TaskManagerContext()), 
                    new StaticMembershipService(), new ProfileFactoryService());

            if(controllerType == typeof(AccountController))
                return new AccountController(new StaticMembershipService(), new ProfileFactoryService());

            return base.GetControllerInstance(requestContext, controllerType);

        }
    }
}