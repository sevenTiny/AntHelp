﻿using Microsoft.Owin.Hosting;
using Newtonsoft.Json;
using Owin;
using QX_Frame.App.WebApi.Extends;
using QX_Frame.WebAPI.config;
using System;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Dispatcher;

namespace QX_Frame.Web.Srv
{
    /**
     * author:qixiao
     * time:2017-1-31 19:20:27
     **/
    class Program
    {
        static void Main(string[] args)
        {
            //string baseAddress = "http://localhost:4999/";    //localhost visit
            string baseAddress = "http://+:4999/";              //all internet environment visit  
            try
            {
                WebApp.Start<StartUp>(url: baseAddress);
                Console.WriteLine("BaseIpAddress is " + baseAddress);
                Console.WriteLine("\nApplication Started !");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            for (;;)
            {
                Console.ReadLine();
            }
        }
    }
    //the start up configuration
    class StartUp
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            HttpConfiguration config = new HttpConfiguration();
            //跨域配置 //need reference from nuget
            config.EnableCors(new EnableCorsAttribute("*", "*", "*"));
            //enabing attribute routing
            config.MapHttpAttributeRoutes();
            // Web API Convention-based routing.
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional },
                 namespaces: new string[] { "QX_Frame.WebAPI.Controllers" }
            );
            config.Services.Replace(typeof(IHttpControllerSelector), new WebAPI.WebApiControllerSelector(config));

            //if config the global filter input there need not write the attributes
            config.Filters.Add(new App.WebApi.Filters.ExceptionAttribute_DG());

            new ClassRegisters(); //register ioc menbers

            //SwaggerConfig.Register(config);
            appBuilder.UseWebApi(config);
        }
    }
}
