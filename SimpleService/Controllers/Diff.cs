using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace SimpleService.Controllers
{
    /// <summary>
    /// Diff logics
    /// </summary>
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

        /// <summary>
        /// If equal return
        /// If not equal size return
        /// If same size /diffs offsets
        /// </summary>
        /// <returns></returns>
        public StringBuilder Compare()
        {
            List<string> diffprops = new List<string>();

            StringBuilder diffReturn = new StringBuilder(); 
            diffReturn.Append("Results: ");

            JObject leftJSON = JObject.Parse(LeftJSON);
            JObject rightJson = JObject.Parse(RightJSON);

            var leftProperties = leftJSON.Properties().ToList();
            var rightProperties = rightJson.Properties().ToList();

            bool equals = JToken.DeepEquals(leftJSON, rightJson);

            if (equals)
                diffReturn.Append("Equal");
            else
            {
                var missingProperties = leftProperties.Where(expected => rightProperties.Where(actual => actual.Name == expected.Name).Count() == 0);
                if (missingProperties.Count() > 0)
                    missingProperties.ToList().ForEach(prop => {
                        diffReturn.Append("Missing Property: ").Append(prop);
                    });

                AppendDiffValues(leftJSON, rightJson, diffReturn, diffprops);
                AppendOffsets(LeftJSON, RightJSON, diffprops, diffReturn);
            }

            return diffReturn;
        }

        private void AppendDiffValues(JObject lefJSON, JObject rightJSON, StringBuilder diffReturn, List<string> diffprops)
        {
            foreach (KeyValuePair<string, JToken> sourceProperty in lefJSON)
            {
                JProperty targetProp = rightJSON.Property(sourceProperty.Key);

                if (!JToken.DeepEquals(sourceProperty.Value, targetProp.Value))
                {
                    diffReturn.Append("Diff Property: ").Append(sourceProperty.Key).Append(":").Append(sourceProperty.Value);
                    diffprops.Add(sourceProperty.Key);
                }
            }
        }

        private void AppendOffsets(string lefJSON, string rightJSON, List<string> diffprops, StringBuilder diffReturn)
        {
            const int _INITAL_AND_LAST_SPACES_ = 2;
            StringBuilder line = new StringBuilder();
            
            diffprops.ForEach(value =>
            {              
                var resultAux = rightJSON;
                line.Append("\"").Append(value);

                int index = rightJSON.IndexOf(line.ToString());
                if (index > 0) {
                    resultAux = resultAux.Substring(0, index);
                    var lines = resultAux.Count(x => x == ',');
                    diffReturn.Append(" Found differences in line: ").Append(lines + _INITAL_AND_LAST_SPACES_);
                }

                 line.Clear();
            });
        }
    }
}