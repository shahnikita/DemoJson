using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace DemoJSon.Filters
{
   public class JsonFilter : ActionFilterAttribute
    {
        public string Parameter { get; set; }
        public Type JsonDataType { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Request.ContentType.Contains("application/json"))
            {
                filterContext.HttpContext.Request.InputStream.Position = 0;
                string json;
                using (var reader = new StreamReader(filterContext.HttpContext.Request.InputStream))
                {
                     json = reader.ReadToEnd();
                }

                var result = JsonConvert.DeserializeObject(json, JsonDataType);
                filterContext.ActionParameters[Parameter] = result;
            }
        }
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            Log("OnActionExecuted", filterContext.RouteData);
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            Log("OnResultExecuting", filterContext.RouteData);
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            Log("OnResultExecuted", filterContext.RouteData);
        }


        private void Log(string methodName, RouteData routeData)
        {
            var controllerName = routeData.Values["controller"];
            var actionName = routeData.Values["action"];
            var message = String.Format("{0} controller:{1} action:{2}", methodName, controllerName, actionName);
            Debug.WriteLine(message, "Action Filter Log");
        }
        
    }


}