using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using Unity;
using VoteManagerAPI.Contracts;
using VoteManagerAPI.Models;
using VoteManagerAPI.Services;

namespace VoteManagerAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var container = new UnityContainer();

            // Define Dependency Concrete types here
            container.RegisterType<ISessionService, SessionService>();
            container.RegisterType<IOrderOfBusinessService, OrderOfBusinessService>();
            container.RegisterType<IMotionService, MotionService>();
            container.RegisterType<IAmendmentService, AmendmentService>();
            container.RegisterType<IVoteService, VoteService>();
            container.RegisterType<IRuleService, RuleService>();

            config.DependencyResolver = new UnityResolver(container);

            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
