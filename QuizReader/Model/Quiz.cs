﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizReader.Model
{
   public class Quiz
    {
        public int QuizID { get; set; }
        public Question Question { get; set; }
        public Answer answer { get; set; }
        public string QuestName { get; set; }
        public Quiz(Question question, Answer answer, string QuestName)
        {
            this.QuestName = QuestName;
            Question = question;
            this.answer = answer;
        }
    }
}
