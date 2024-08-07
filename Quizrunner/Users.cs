using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quizrunner
{

    public class Users
    {
        public int id { get; set; }
        public string name { get; set; }
        public ICollection<Results>? Results { get; set; }
        public Users()
        {
            Results = new List<Results>();
        }
    }
}
