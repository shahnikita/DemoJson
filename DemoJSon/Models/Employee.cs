using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoJSon.Models
{
    public class Employee
    {
        [JsonProperty(PropertyName="na")]
        public string Name { get; set; }
    }
}