using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain;

namespace Repository
{
    public class CodeHandler
    {
        private string urlSearchBegin = "https://se.timeedit.net/web/kau/db1/schema_kau/objects.txt?max=15&fr=t&partajax=t&im=f&sid=3&l=sv&search_text=";
        private string urlSearchEnd = "&types=199";
        private string urlGetBegin = "https://se.timeedit.net/web/kau/db1/schema_kau/s.csv?sid=3&object=";
        private string urlGetEnd = "&type=Alla&p=-6.n%2C24.n";//Bugg i time edit, detta funkar iaf ;) (TimeEdit hoppar 1,5 år bakåt och 2 år framåt)

        int applicationCode;

        private Filehandler fileHandler;
        private JsonObjParser jsonParser;
        private List<Application> Applications;

        public CodeHandler() 
        {
            fileHandler = new Filehandler();
            jsonParser = new JsonObjParser();
            Applications = new List<Application>();
        }
        /// <summary>
        /// Tar emot en sträng med en kurskod eller en anmälningskod. Är det en kurskod anropas jsonParser för att hämta ut anmälningskoderna. Är det en anmälningskod redan
        /// läggs den in i retur-Listen.
        /// </summary>
        /// <param name="code">En sträng med en kurskod eller en anmälningskod</param>
        /// <returns>Returnerar en List av Applications med anmälningskoderna. Blir det fel returneras en tom lista.</returns>
        public List<Application> GetApplicationCodeList(string code)
        {
            if (!String.IsNullOrEmpty(code))
            {
                return GetApplicationCodesFromCourseCode(code);
            }
            Applications.Clear();
            return Applications;
        }


        public List<Application> ConvertStringsToApplications(string applicationCodes)
        {
            int appCode,id=0;
            List<Application> result = new List<Application>();
            foreach (String applicationCode in SplitString(applicationCodes))
                if (int.TryParse(applicationCode, out appCode))
                {
                    result.Add(new Application(appCode));
                    result.ElementAt(id).ID = id++;
                }
            return result;
        }

        public string GetScheduleURL(int applicationCode)
        {
            return urlGetBegin + applicationCode + urlGetEnd;
        }

        private List<Application> GetApplicationCodesFromCourseCode(string courseCode)
        {
            string jsonSearch = urlSearchBegin + courseCode + urlSearchEnd;
            string fileText = fileHandler.ReadFile(fileHandler.GetFileFromUrl(jsonSearch));
            return jsonParser.ParseJson(fileText);
        }
        private List<string> SplitString(string courseCode)
        {
            char[] delimiters = { ',' };
            List<String> result = courseCode.Split(delimiters).ToList<String>();
            return result;
        }

        private bool IsCourseCode(string code)
        {
            return (Char.IsLetter(code[0]) || !int.TryParse(code, out applicationCode));
        }

        private bool IsApplicationCode(string code)
        {
            return (int.TryParse(code, out applicationCode));
        }

        private List<Application> GetApplicationCodesFromString(String code)
        {
            Applications.Add(new Application(int.Parse(code)));
            return Applications;
        }


        
    }
}