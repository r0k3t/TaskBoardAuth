using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskBoardAuth.Controllers;
using TaskBoardAuth.Core.Services;
using TaskBoardAuth.Infrastructure;
using TaskBoardAuth.Infrastructure.Repositories;

namespace TaskBoardAuth
{
    public class TaskBoardControllerFactory : DefaultControllerFactory 
    {
        protected override IController GetControllerInstance(System.Web.Routing.RequestContext requestContext, Type controllerType)
        {
            if(controllerType == typeof(TaskBoardController))
                return new TaskBoardController(new TaskBoardRepository(), 
                    new StaticMembershipService(), new ProfileFactoryService());

            if(controllerType == typeof(AccountController))
                return new AccountController(new StaticMembershipService(), new ProfileFactoryService(), new FormsAuthenticationService());

            return base.GetControllerInstance(requestContext, controllerType);

        }
    }
}