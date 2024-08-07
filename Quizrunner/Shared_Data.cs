using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quizrunner
{
    public class SharedData
    {
        private static SharedData _instance;
        public static SharedData Instance => _instance ?? (_instance = new SharedData());
        private SharedData() { }
        public string SharedVariable { get; set; }
    }
}
