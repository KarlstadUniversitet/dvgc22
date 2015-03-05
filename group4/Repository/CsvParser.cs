using Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.VisualBasic.FileIO;

namespace Repository
{
    public class CsvParser
    {
        /// Läser in alla produkter från en csv fil och returnerar dem som en lista av Sträng arrayer,
        /// varje position i strängarrayen innehåller ett attribut
        /// </summary>
        /// <param name="csvStream"></param>
        /// <returns></returns>
        public List<String[]> Parse(Stream csvStream)
        {
            List<String[]> result = new List<String[]>();
            StreamReader reader = new StreamReader(csvStream);
            String line;
            while ((line = reader.ReadLine()) != null)
            {
                result.Add(SplitStringIntoFields(line));
            }
            return result;
        }


        /// <summary>
        /// Tar in en sträng och delar upp den i fields, splittar stränger på ','-tecken samt hanterar text inom situationstecken
        /// som en enhet och splittar inte denna.
        /// </summary>
        /// <param name="line">Strängen som ska separeras</param>
        /// <returns>En array av strängar där varje rad är ett field</returns>
        private String[] SplitStringIntoFields(String line)
        {
            using (TextFieldParser parser = new TextFieldParser(new System.IO.StringReader(line)))
            {
                parser.HasFieldsEnclosedInQuotes = true;
                String[] delimiters = {","};
                parser.SetDelimiters(delimiters);
                return parser.ReadFields();
            }
        }
    }
}