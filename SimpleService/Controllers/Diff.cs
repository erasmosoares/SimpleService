using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleService.Controllers
{
    public class Diff
    {
        private static Diff instance;

        private Diff() { }

        public static Diff Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Diff();
                }
                return instance;
            }
        }

        public string LeftJSON { get; set; }
        public string RightJSON { get; set; }

        public IEnumerable<JProperty> Compare()
        {
            JObject leftJSON = JObject.Parse(LeftJSON);
            JObject rightJson = JObject.Parse(RightJSON);

            var leftProperties = leftJSON.Properties().ToList();
            var rightProperties = rightJson.Properties().ToList();

            var missingProps = leftProperties.Where(expected => rightProperties.Where(actual => actual.Name == expected.Name).Count() == 0);

            return missingProps;
        }
    }
}