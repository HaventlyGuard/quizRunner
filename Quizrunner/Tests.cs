using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Quizrunner
{
    public class Tests_bd
    {
        public int id { get; set; }
        public string? test_name { get; set; }
        public ICollection<TestQuestions>? TestQuestions { get; set; }
        public ICollection<Results>? Results { get; set; }

        public Tests_bd()
        {
            TestQuestions = new List<TestQuestions>();
            Results = new List<Results>();
        }
    }


}
