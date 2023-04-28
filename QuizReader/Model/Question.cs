using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizReader.Model
{
    public class Question
    {
        public int QuestionID { get; set; }
        public string Quest { get; set; }
        public Question(string Quest)
        { this.Quest = Quest; }
    }
}
