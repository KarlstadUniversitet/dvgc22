using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;
using Domain;

namespace Repository
{
    public class JsonParser
    {
        List<Application> applicationCodes;
        int JsonElementCount;

        public JsonParser()
        {
            applicationCodes = new List<Application>();
            JsonElementCount = 0;
        }
        /// <summary>
        /// Tar emot en sträng i json-format och parsar ut anmälningskoderna. 
        /// </summary>
        /// <param name="fileText">En sträng i json-format</param>
        /// <returns>Skickar tillbaka i form av array av ints. Blir det fel returneras -1 på första index.</returns>
        public List<Application> ParseJson(string fileText)
        {
            if (IsJsonFileCorrect(fileText))
            {
                if (ParseJsonElementCount(fileText) > 1)
                {
                    int i = 0, stringPointer = 0;
                    while (i < JsonElementCount - 1)
                    {
                        stringPointer = ExtractApplicationCode(fileText, stringPointer);
                        i++;
                    }
                    return applicationCodes;
                }
            }
            applicationCodes.Clear();
            return applicationCodes;
        }

        private int ExtractApplicationCode(string fileText, int stringPointer)
        {
            int start = fileText.IndexOf("values", stringPointer);
            int end = fileText.IndexOf("numberOfValues", start);
            string codeString = Regex.Match(fileText.Substring(start, end - start), @"\d+").Value;
            applicationCodes.Add(new Application(int.Parse(codeString)));
            return fileText.IndexOf("fields", end);
        }

        private int ParseJsonElementCount(string fileText)
        {
            return JsonElementCount = int.Parse(fileText[9].ToString());
        }

        private bool IsJsonFileCorrect(String fileText)
        {
            return (int.TryParse(fileText[9].ToString(), out JsonElementCount)); 
        }
    }
}