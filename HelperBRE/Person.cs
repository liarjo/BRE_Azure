using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelperBRE.SampleSubject
{
    public class Person 
    {
        public bool RuleEval { get; set; }
        public double Salary { get; set; }
        double age;
        public double Age
        {
            get
            {
                return age;
            }
            set
            {
                age = value;
            }
        }
    }
}
