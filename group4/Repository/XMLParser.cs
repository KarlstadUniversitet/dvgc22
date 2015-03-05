using Domain;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;

namespace Repository
{
    public class XMLParser
    {


        public static List<Lecture> FileParser(String fileName)
        {
            XmlTextReader reader = new XmlTextReader(fileName);
            reader.WhitespaceHandling = WhitespaceHandling.None;
            List<Lecture> tentor = new List<Lecture>();

            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    if (reader.Name == "item")
                    {
                        Lecture tenta = new Lecture();
                        DateTime startTime = new DateTime();
                        DateTime endTime = new DateTime();
                        reader.Read();
                        while (!reader.Name.Equals("item"))
                        {
                            if (reader.IsStartElement())
                            {
                                switch (reader.Name)
                                {
                                    case "title":
                                        reader.Read();
                                        if (reader.Value == " -  - ")
                                        {
                                            return new List<Lecture>();
                                        }
                                        startTime=AddDate(startTime, reader.Value);
                                        endTime=AddDate(endTime, reader.Value);
                                        tenta.info = reader.Value;
                                        break;
                                    case "description":
                                        reader.Read();
                                        ParseDescription(tenta, reader.Value, startTime, endTime);
                                        break;
                                    default: break;
                                }
                            }

                            if (!reader.Read())
                            {
                                break;
                            }
                        }
                        tentor.Add(tenta);

                    }
                }
            }

            return tentor;
        }
        //viewer discretion is advised
        private static DateTime AddDate(DateTime dt, String date)
        {
            String[] dateArray = date.Split('-');
            dt=dt.AddYears(Convert.ToInt32(dateArray[0])-1);
            dt=dt.AddMonths(Convert.ToInt32(dateArray[1])-1);
            dt = dt.AddDays(Convert.ToInt32(dateArray[2])-1);

            return dt;
        }

        //Benämning: Arkivkunskap I - Regelverket&lt;br&gt;Kurs: ARGA01&lt;br&gt;Sal: 11B 140&lt;br&gt;Tid: 0815-1315
        public static bool ParseDescription(Lecture tenta, string text, DateTime startTime, DateTime endTime)
        {
            Match course = Regex.Match(text, @"Kurs: (\w{6})");
            tenta.course = course.Groups[1].Value;

            Match room = Regex.Match(text, @"Sal: (\w{2,3}\s*\d{3})");
            tenta.classroom = room.Groups[1].Value;

            Match info = Regex.Match(text, @"- (.*?)<br>");
            tenta.info = "Tentamen: " + info.Groups[1].Value;
         
            Match time = Regex.Match(text, @"Tid: (\d{2})(\d{2})-(\d{2})(\d{2})");
            startTime = startTime.AddHours(Convert.ToDouble(time.Groups[1].Value));
            startTime = startTime.AddMinutes(Convert.ToDouble(time.Groups[2].Value));
            endTime = endTime.AddHours(Convert.ToDouble(time.Groups[3].Value));
            endTime = endTime.AddMinutes(Convert.ToDouble(time.Groups[4].Value));
            tenta.startTime = startTime;
            tenta.endTime = endTime;

            Match courseName = Regex.Match(text, @"Benämning: (.*?) - ");
            Application app = new Application();
            app.CourseName = courseName.Groups[1].Value;
            app.CourseCode = tenta.course;
            tenta.application = app;



            return true;
        }

    }
}
