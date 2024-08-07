using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quizrunner
{
    public class TestAnswers
    {
        public int id { get; set; }
        public TestQuestions TestQuestions { get; set; }
        public Tests_bd Tests_bd { get; internal set; }
        public int answer_count { get; set; }
        public string answer_text { get; set; }
        public bool is_correct { get; set; }
        
    }
}
