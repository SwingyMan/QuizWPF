using QuizReader.Model;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace QuizReader.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        #region declarations
        private string _currentTime;
        private DispatcherTimer _timer;
        private TimeSpan time;
        public string CurrentTime { get {return this._currentTime;} set { _currentTime = value; OnPropertyChanged(); } }
        private int counter;
        private bool _activated;
        private bool _startEnabled;
        private bool _stopEnabled;
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
        public ICommand Start { get; set; }
        public ICommand Stop { get; set; }
        public string _pointSum { get; set; }
        public string CurrentAnswer { get; set; }
        public Score score { get; set; }
        public object clicked { get; set; }
        ListQuiz listQuiz { get; set; }
        public bool startEnable { get { return _startEnabled; }set { _startEnabled = value;OnPropertyChanged(); } }
        public bool stopEnable { get { return _stopEnabled; }set { _stopEnabled = value; OnPropertyChanged(); } }
        public bool activate { get { return _activated; } set { _activated = value; OnPropertyChanged(); } }
        public List<string> listed { get { return _listed; } set { _listed = value; OnPropertyChanged(); } }
        public string AnswerAText { get { return _answerAText; } set { _answerAText = value;  OnPropertyChanged(); } }
        public string AnswerBText { get { return _answerBText; }set { _answerBText = value; OnPropertyChanged(); } }
        public string AnswerCText { get { return _answerCText; }set { _answerCText = value; OnPropertyChanged(); } }
        public string AnswerDText { get { return _answerDText; }set{_answerDText = value;OnPropertyChanged();} }
        public string QuestionText { get { return _questionText; }set { _questionText = value; OnPropertyChanged(); } }
        public string PointSum { get { return _pointSum; } set { _pointSum= value; OnPropertyChanged(); } }
        public event PropertyChangedEventHandler? PropertyChanged;
        #endregion
        public MainViewModel() { 
            AnswerAText = "odp:A";
            AnswerBText = "odp:B";
            AnswerCText = "odp:C";
            AnswerDText = "odp:D";
            QuestionText = "Pytanie:";
            activate = false;
            startEnable = false;
            stopEnable =false;
            LoadQuiz = new RelayCommand(loadQuiz);
            AnswerA = new RelayCommand(ButtonA);
            AnswerB = new RelayCommand(ButtonB);
            AnswerC = new RelayCommand(ButtonC);
            AnswerD = new RelayCommand(ButtonD);
            Start = new RelayCommand(StartB);
            Stop = new RelayCommand(StopB);
            listQuiz = new ListQuiz();
            CurrentTime = TimeSpan.Zero.ToString();
            var time = TimeSpan.Zero;
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += timer_Tick;
        }
        void timer_Tick(object sender, EventArgs e)
        {
            time = time.Add(TimeSpan.FromSeconds(1));
            CurrentTime = time.ToString(@"hh\:mm\:ss");
        }

        private void OnPropertyChanged([CallerMemberName]string propertyName = null) {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private void loadQuiz(object obj)
        {
            listQuiz.initialize();
            listed = listQuiz.QuizList.Select(x => x.QuestName).ToList();
            startEnable = true;

        }
        private void StartB(object obj)
        {
            _timer.Start();
            startEnable = false;
            stopEnable = true;
            listed = listQuiz.QuizList.Select(x => x.QuestName).ToList();
            score = new Score(0, listQuiz.QuizList.Count);
            if (listed.Count != 0)
            {
                counter = 0;
                activate = true;
                var first = listQuiz.QuizList.First();
                updateView(first);
            }
        }
        private void StopB(object obj)
        {
            _timer.Stop();
            startEnable = true;
            stopEnable = false;
            MessageBox.Show(score.ToString() + "\n" + "Twój czas: " + time);
            updateView(null);
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
                time = TimeSpan.Zero;
                CurrentTime = time.ToString(@"hh\:mm\:ss");
                _timer.Stop();
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
            button_reaction("559aead08264d5795d3909718cdd05abd49572e84fe55590eef31a88a08fdffd");
        }
        private void ButtonB(object obj)
        {
            button_reaction("df7e70e5021544f4834bbee64a9e3789febc4be81470df629cad6ddb03320a5c");
        }
        private void ButtonC(object obj)
        {
            button_reaction("6b23c0d5f35d1b11f9b683f0b0a617355deb11277d91ae091d399c655b87940d");
        }
        private void ButtonD(object obj)
        {
            button_reaction("3f39d5c348e5b79d06e842c114e6cc571583bbf44e4b0ebfda1a01ec05745d43");
        }
        void button_reaction(string answer)
        {
                if (counter >= listQuiz.QuizList.Count)
                {
                    if (CurrentAnswer.Equals(answer))
                    {
                        score.addScore();
                        MessageBox.Show(score.ToString() + "\n" + "Twój czas: " + time);
                        updateView(null);
                        activate = false;

                        stopEnable = false;
                        startEnable = true;
                    }
                    else
                    {
                        MessageBox.Show(PointSum.ToString() + "\n" + "Twój czas: " + time);
                        updateView(null);
                        activate = false;

                        stopEnable = false;
                        startEnable = true;
                    }
                }
                else
                {
                    if (CurrentAnswer.Equals(answer))
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