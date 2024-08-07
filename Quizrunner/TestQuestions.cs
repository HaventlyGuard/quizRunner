using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quizrunner
{
    public class TestQuestions
    {
        public int id { get; set; }
        public string question_text { get; set; }
        public int question_count { get; set; }
        public Tests_bd Tests_bd { get; set; }
        public ICollection<TestAnswers>? TestAnswers { get; set; }
        public TestQuestions()
        {
            TestAnswers = new List<TestAnswers>();
        }
    }
}
