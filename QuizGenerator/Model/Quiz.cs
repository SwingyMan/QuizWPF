using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizGenerator.Model
{
    internal class Quiz
    {
        public int QuizID { get; set; }
        public Question Question { get; set; }
        public Answer answer { get; set; }
    }
}
