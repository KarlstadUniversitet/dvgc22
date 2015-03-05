using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Application
    {
        public int ID { get; set; }
        public int Code { get; set; }
        public String CourseCode { get; set; }
        public String CourseName { get; set; }



        public Application()
            : this(0)
        { }

        public Application(int appcode)
            : this(appcode, String.Empty)
        { }

        public Application(int applicationCode, string courseCode)
            : this(applicationCode, courseCode, String.Empty)
        { }

        public Application(int applicationCode, string courseCode, string courseName)
        {
            Code = applicationCode;
            CourseCode = courseCode;
            CourseName = courseName;
        }

    }
}
