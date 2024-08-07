using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quizrunner
{
    public class Results
    { 
        public int id { get; set; }
        public Users Users { get; set; }
        public Tests_bd Tests_bd { get; set; }
        public string result { get; set; }
        public string date { get; set; }
        public string UserName => Users?.name;
        public string TestName => Tests_bd?.test_name;
    }
}
