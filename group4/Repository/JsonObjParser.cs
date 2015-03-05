using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Newtonsoft.Json;

namespace Repository
{
    public class JsonObjParser
    {
        List<Application> applicationCodes;
        JsonObject jsonObject;

        public JsonObjParser()
        {
            applicationCodes = new List<Application>();
        }

        public void parseText(string textToParse)
        {
            jsonObject = JsonConvert.DeserializeObject<JsonObject>(textToParse);  
        }

        public object ObjectCount { get { return jsonObject.Count; } }

        public List<Application> ParseJson(string textToParse)
        {
            jsonObject = JsonConvert.DeserializeObject<JsonObject>(textToParse);

            for (int i = 0; i < jsonObject.Count; i++)                                 //tidigare --> (int i = 0; i < jsonObject.Count - 1; i++ )
            {                                                                              //måste vart en fulhack sedan den hoppar sista
                if (jsonObject.Records[i].Fields.Length > 3)                               //<------???? vad gör den??? verkar inte ha någon verkan
                {
                    Application application = new Application();
                    application.ID = jsonObject.Ids[i];
                    application.Code = jsonObject.Records[i].Fields[0].ValuesAsInteger[0];
                    application.CourseCode = jsonObject.Records[i].Fields[1].Values[0];
                    application.CourseName = jsonObject.Records[i].Fields[2].Values[0];
                    applicationCodes.Add(application);
                }
            }

            return applicationCodes;
        }
    }
}
