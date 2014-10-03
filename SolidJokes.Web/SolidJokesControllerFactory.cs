using SolidJokes.Core.DB;
using SolidJokes.Core.Services;
using SolidJokes.Web.Controllers;
using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace SolidJokes.Web {
    public class SolidJokesControllerFactory : DefaultControllerFactory {
        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType) {
            if (controllerType == typeof(HomeController)) {
                var session = new Session();
                //var viewer = new JokeViewer(session);
                var viewer = new JokeViewerLogger(new JokeViewer(session));

                //return new HomeController(viewer);
                // Decorating HomeController with HomeControllerLogger
                return new HomeControllerLogger(new HomeController(viewer));
            }
            // AccountController will still go the normal tightly coupled way
            return base.GetControllerInstance(requestContext, controllerType);
        }
    }
}