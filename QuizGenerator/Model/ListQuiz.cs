using Microsoft.Win32;
using System;
using System.Data.SQLite;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data.SqlClient;

namespace QuizGenerator.Model
{
    public class ListQuiz
    {
        public ListQuiz()
        {
            QuizList = new List<Quiz>();
        }
        public List<Quiz> QuizList { get; set; }
        public void Add(Quiz quiz)
        {
            QuizList.Add(quiz);
        }
        public void Remove(int i)
        {
            if(i != -1)
            {
                QuizList.RemoveAt(i);
            }
        }
        public void Edit(Quiz quiz, int id)
        {
            if (id != -1)
                QuizList[id] = quiz;
        }
        public Quiz? OnIndex(int i) 
        {
            if(i != -1)
            {
                return QuizList[i];
            }
            else
            {
                return null;
            }
        }

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
        public void save(ListQuiz list)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Plik bazy danych (*.db;*.sql)|*.sql;*.db";
            if (dialog.ShowDialog() == true)
            {
                SQLiteConnection.CreateFile(dialog.FileName);
                SQLiteConnection conn = new SQLiteConnection(@"Data Source=" + dialog.FileName);
                conn.Open();
                string sql1 = "CREATE TABLE Answer(AnswerID INTEGER PRIMARY KEY, AnswerA TEXT, AnswerB TEXT, AnswerC TEXT, AnswerD TEXT, CorrectAnswer TEXT)";
                string sql2 = "CREATE TABLE Question(QuestionID INTEGER PRIMARY KEY, Quest TEXT)";
                string sql3 = "CREATE TABLE Quiz(QuestionID INTEGER, AnswerID INTEGER, QuestName TEXT, FOREIGN KEY (QuestionId) REFERENCES Question(questionid), FOREIGN KEY (AnswerId) REFERENCES Answer(answerid))";
                SQLiteCommand cmd = conn.CreateCommand();
                cmd.CommandText = sql1;
                cmd.ExecuteNonQuery();
                cmd.CommandText = sql2;
                cmd.ExecuteNonQuery();
                cmd.CommandText = sql3;
                cmd.ExecuteNonQuery();
                int i = 1;
                foreach(Quiz quiz in QuizList)
                {
                    string sql_insert1 = "INSERT INTO Answer VALUES ({0}, \"{1}\", \"{2}\", \"{3}\", \"{4}\", \"{5}\")";
                    string sql_insert1_f = string.Format(sql_insert1, i.ToString(), quiz.answer.AnswerA, quiz.answer.AnswerB, quiz.answer.AnswerC, quiz.answer.AnswerD, quiz.answer.CorrectAnswer);
                    string sql_insert2 = "INSERT INTO Question VALUES ({0}, \"{1}\")";
                    string sql_insert2_f = string.Format(sql_insert2, i.ToString(), quiz.Question.Quest);
                    string sql_insert3 = "INSERT INTO Quiz VALUES ({0}, {1}, \"{2}\")";
                    string sql_insert3_f = string.Format(sql_insert3, i.ToString(), i.ToString(), quiz.QuestName);
                    SQLiteCommand cmd_insert = conn.CreateCommand();
                    cmd_insert.CommandText = sql_insert1_f;
                    cmd_insert.ExecuteNonQuery();
                    cmd_insert.CommandText = sql_insert2_f; 
                    cmd_insert.ExecuteNonQuery();
                    cmd_insert.CommandText = sql_insert3_f; 
                    cmd_insert.ExecuteNonQuery();
                    i++;
                }
                conn.Close();
            }
        }
        public List<string> AsStrings()
        {
            var list = new List<string>();
            foreach(Quiz q in QuizList)
            {
                list.Add(q.ToString());
            }
            return list;
        }
}
}
