using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace QuizReader.Model
{
    public class ListQuiz
    {
        public ListQuiz() {
            QuizList = new List<Quiz>(); }
        public List<Quiz> QuizList { get; set; }
        public void initialize()
        {
            QuizList = new List<Quiz>();
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Plik bazy danych (*.db;*.sql)|*.db;*.sql|Wszystkie Pliki (*.*)|*.*";
            if (dialog.ShowDialog() == true)
            {
                SQLiteConnection conn = new SQLiteConnection(@"Data Source=" + dialog.FileName);
                conn.Open();
                SQLiteDataReader reader;
                SQLiteCommand command;
                command = conn.CreateCommand();
                command.CommandText = "select Quest,AnswerA,AnswerB,AnswerC,AnswerD,CorrectAnswer,QuestName from Quiz,Answer,Question where Quiz.AnswerID=Answer.AnswerID and Quiz.QuestionID=Question.QuestionID;";
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string quest = (string)reader["Quest"];
                    string answerA = (string)reader["AnswerA"];
                    string answerB = (string)reader["AnswerB"];
                    string answerC = (string)reader["AnswerC"];
                    string answerD = (string)reader["AnswerD"];
                    string CorrectAnswer = (string)reader["CorrectAnswer"];
                    string QuestName = (string)reader["QuestName"];
                    var x = new Answer(answerA, answerB, answerC, answerD, CorrectAnswer);
                    var y = new Question(quest);
                    var z = new Quiz(y, x, QuestName);
                    QuizList.Add(z);
                }
                conn.Close();
            }
        }
    }
}
