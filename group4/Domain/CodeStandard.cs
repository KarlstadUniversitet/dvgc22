using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Domain
{
    public class CodeStandard
    {
        public int property { get; set; }
        
        /// <summary>
        /// Example
        /// </summary>
        /// <param name="i">not null</param>
        public void exampleMethod(int i)
        {
            this.property = 3;

            string emptyString = String.Empty;
        }
        
        /// <summary>
        /// Loops
        /// </summary>
        public void loops()
        {
            int j = 0;

            while (j == 1)
            {
                // do stuff
            }

            for (int i = 0; i < 10; i++)
                j = i;
        }
    }
}