using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class ColorHandler
    {
        private List<Color> colors;

        public ColorHandler()
        {
            colors = new List<Color>();
            colors.Add(new Color{ Application = new Application(1), Colour="lectureBreak" });
        }

        public void AddColor(Lecture lecture)
        {
            colors.Add(CreateColor(lecture));
        }

        private Color CreateColor(Lecture lecture)
        {
            return new Color() { Colour = GetColorID(), Application = lecture.application };
        }

        public string GetSavedColor(Lecture lecture)
        {
            string result = null;
            foreach (Color c in colors)   
                if (lecture.application.Code == c.Application.Code)
                    result = c.Colour;
            return result;
        }

        private string GetColorID()
        {
            return "color" + GetNextColorID().ToString();
        }

        private int GetNextColorID()
        {
            int result = colors.Count();
            if (result > 59)
                result = 1;
            return result;
        }
    }
}
