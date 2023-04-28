using QuizReader.Model;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace QuizReader.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private bool _activated;
        private string _answerAText;
        private string _answerBText;
        private string _answerCText;
        private string _answerDText;
        private string _questionText;
        private List<string> _listed;
        public ICommand AnswerA { get; set; }
        public ICommand AnswerB { get; set; }
        public ICommand AnswerC { get; set; }
        public ICommand AnswerD { get; set; }
        public ICommand LoadQuiz { get; set; }
        public bool activate { get { return _activated; } set { _activated = value; OnPropertyChanged(); } }
        public List<string> listed { get { return _listed; } set { _listed = value; OnPropertyChanged(); } }
        public string _pointSum { get; set; }
        public string CurrentAnswer { get; set; }
        public Score score { get; set; }
        public object clicked { get; set; }
        ListQuiz listQuiz { get; set; }
        private int counter;
        public string AnswerAText { get { return _answerAText; } set { _answerAText = value;  OnPropertyChanged(); } }
        public string AnswerBText { get { return _answerBText; }set { _answerBText = value; OnPropertyChanged(); } }
        public string AnswerCText { get { return _answerCText; }set { _answerCText = value; OnPropertyChanged(); } }
        public string AnswerDText { get { return _answerDText; }set{_answerDText = value;OnPropertyChanged();} }
        public string QuestionText { get { return _questionText; }set { _questionText = value; OnPropertyChanged(); } }
        public string PointSum { get { return _pointSum; } set { _pointSum= value; OnPropertyChanged(); } }
        public event PropertyChangedEventHandler? PropertyChanged;
        public MainViewModel() { 
            AnswerAText = "odp:A";
            AnswerBText = "odp:B";
            AnswerCText = "odp:C";
            AnswerDText = "odp:D";
            QuestionText = "Pytanie:";
            activate = false;
            LoadQuiz = new RelayCommand(loadQuiz);
            AnswerA = new RelayCommand(ButtonA);
            AnswerB = new RelayCommand(ButtonB);
            AnswerC = new RelayCommand(ButtonC);
            AnswerD = new RelayCommand(ButtonD);
            listQuiz = new ListQuiz();
        }
        private void OnPropertyChanged([CallerMemberName]string propertyName = null) {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private void loadQuiz(object obj)
        {
            listQuiz.initialize();
            score = new Score(0,listQuiz.QuizList.Count);
            listed = listQuiz.QuizList.Select(x=>x.QuestName).ToList();
            if (listed.Count != 0)
            {

                counter = 0;
                activate = true;
                var first = listQuiz.QuizList.First();
                PointSum = score.ToString();
                AnswerAText = first.answer.AnswerA;
                AnswerBText = first.answer.AnswerB;
                AnswerCText = first.answer.AnswerC;
                AnswerDText = first.answer.AnswerD;
                QuestionText = first.Question.Quest;
                CurrentAnswer = first.answer.CorrectAnswer;
                counter++;
            }
        }
        private void updateView(Quiz item)
        {
            if (item == null)
            {
                PointSum = string.Empty;
                AnswerAText = string.Empty;
                AnswerBText = string.Empty;
                AnswerCText = string.Empty;
                AnswerDText = string.Empty;
                QuestionText = string.Empty;
                CurrentAnswer = string.Empty;
                counter = 0;
                listed = new List<string>();
            }
            else
            {
                PointSum = score.ToString();
                AnswerAText = item.answer.AnswerA;
                AnswerBText = item.answer.AnswerB;
                AnswerCText = item.answer.AnswerC;
                AnswerDText = item.answer.AnswerD;
                QuestionText = item.Question.Quest;
                CurrentAnswer = item.answer.CorrectAnswer;
                counter++;
            }
        }
        private void ButtonA(object obj)
        {
            if (counter >= listQuiz.QuizList.Count)
            {
                if (CurrentAnswer.Equals("A"))
                {
                    score.addScore();
                    MessageBox.Show(score.ToString());
                    updateView(null);
                    activate = false;
                }
                    else
                {
                    MessageBox.Show(PointSum.ToString());
                    updateView(null);
                    activate = false;
                }
            }
            else
            {
                if (CurrentAnswer.Equals("A"))
                {
                    score.addScore();
                    PointSum = score.ToString();
                    updateView(listQuiz.QuizList.ElementAt(counter));
                }
                else
                    updateView(listQuiz.QuizList.ElementAt(counter));
            }
        }
        private void ButtonB(object obj)
        {
            if (counter >= listQuiz.QuizList.Count)
            {
                if (CurrentAnswer.Equals("B"))
                {
                    score.addScore();
                    MessageBox.Show(score.ToString());
                    updateView(null);
                    activate = false;
                }
                else
                {
                    MessageBox.Show(PointSum.ToString());
                    updateView(null);
                    activate = false;
                }
            }
            else
            {
                if (CurrentAnswer.Equals("B"))
                {
                    score.addScore();
                    PointSum = score.ToString();
                    updateView(listQuiz.QuizList.ElementAt(counter));
                }
                else
                    updateView(listQuiz.QuizList.ElementAt(counter));
            }
        }
        private void ButtonC(object obj)
        {
            if (counter >= listQuiz.QuizList.Count)
            {
                if (CurrentAnswer.Equals("C"))
                {
                    score.addScore();
                    MessageBox.Show(score.ToString());
                    updateView(null);
                    activate = false;
                }
                else
                {
                    MessageBox.Show(PointSum.ToString());
                    updateView(null);
                    activate = false;
                }
            }
            else
            {
                if (CurrentAnswer.Equals("C"))
                {
                    score.addScore();
                    PointSum = score.ToString();
                    updateView(listQuiz.QuizList.ElementAt(counter));
                }
                else
                    updateView(listQuiz.QuizList.ElementAt(counter));
            }
        }
        private void ButtonD(object obj)
        {
            if (counter >= listQuiz.QuizList.Count)
            {
                if (CurrentAnswer.Equals("D"))
                {
                    score.addScore();
                    MessageBox.Show(score.ToString());
                    updateView(null);
                    activate = false;
                }
                else
                {
                    MessageBox.Show(PointSum.ToString());
                    updateView(null);
                    activate = false;
                }
            }
            else
            {
                if (CurrentAnswer.Equals("D"))
                {
                    score.addScore();
                    PointSum = score.ToString();
                    updateView(listQuiz.QuizList.ElementAt(counter));
                }
                else
                    updateView(listQuiz.QuizList.ElementAt(counter));
            }
        }
    }
}
