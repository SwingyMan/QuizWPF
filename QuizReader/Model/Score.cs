using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations.Model;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizReader.Model
{
    public class Score
    {
        public int currentScore { get; set; }
        public int maxScore { get; set; }
        public override string ToString()
        {
            return "Obecny wynik " + currentScore + "/" + maxScore; 
        }
        public Score(int currentScore, int maxScore)
        {
            this.currentScore = currentScore;
            this.maxScore = maxScore;
        }
        public void addScore()
        {
            currentScore += 1;
        }
    }
}
