using QuizGenerator.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Printing;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using static System.Formats.Asn1.AsnWriter;

namespace QuizGenerator.ViewModel
{
    internal class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private string _questionText;
        private string _answerAText;
        private string _answerBText;
        private string _answerCText;
        private string _answerDText;
        private string _correctAnswer;
        private string _questionName;
        private int _selectedId;
        public string AnswerAText { get { return _answerAText; } set { _answerAText = value; OnPropertyChanged(); } }
        public string AnswerBText { get { return _answerBText; } set { _answerBText = value; OnPropertyChanged(); } }
        public string AnswerCText { get { return _answerCText; } set { _answerCText = value; OnPropertyChanged(); } }
        public string AnswerDText { get { return _answerDText; } set { _answerDText = value; OnPropertyChanged(); } }
        public string QuestionText { get { return _questionText; } set { _questionText = value; OnPropertyChanged(); } }
        public string CorrectAnswer { get { return _correctAnswer; } set { _correctAnswer = value; OnPropertyChanged(); } }
        public string QuestionName { get { return _questionName; } set { _questionName = value; OnPropertyChanged(); } }
        public int SelectedId { get { return _selectedId; } set { _selectedId = value; OnPropertyChanged(); } }

        public ICommand Add { get; set; }
        public ICommand Edit { get; set; }
        public ICommand Delete { get; set; }
        public ICommand Load { get; set; }
        public ICommand Save { get; set; }
        public ICommand SelectedIndexChangedCommand { get; set; }
        private List<string> _listed;
        ListQuiz listQuiz { get; set; }

        private bool _Aradio;
        private bool _Bradio;
        private bool _Cradio;
        private bool _Dradio;
        public bool ARadio { get { return _Aradio; } set { _Aradio = value; OnPropertyChanged(); } }
        public bool BRadio { get { return _Bradio; } set { _Bradio = value; OnPropertyChanged(); } }
        public bool CRadio { get { return _Cradio; } set { _Cradio = value; OnPropertyChanged(); } }
        public bool DRadio { get { return _Dradio; } set { _Dradio = value; OnPropertyChanged(); } }

        public List<string> listed { get { return _listed; } set { _listed = value; OnPropertyChanged(); } }

        public MainViewModel() 
        {
            Add = new RelayCommand(addQuestion);
            Edit = new RelayCommand(editQuestion);
            Delete = new RelayCommand(deleteQuestion);
            Load = new RelayCommand(loadQuiz);
            Save = new RelayCommand(saveQuiz);
            SelectedIndexChangedCommand = new RelayCommand(selection);
            listQuiz = new ListQuiz();
            ARadio = true;
            BRadio = false;
            CRadio = false;
            DRadio = false;
            AnswerAText = string.Empty;
            AnswerBText = string.Empty;
            AnswerCText = string.Empty;
            AnswerDText = string.Empty;
            CorrectAnswer = string.Empty;
            QuestionText = string.Empty;
            listed = new List<string>();
            SelectedId = -1;
        }
        private void setRadio(string answer)
        {
            if(answer == "559aead08264d5795d3909718cdd05abd49572e84fe55590eef31a88a08fdffd")
            {
                ARadio = true; BRadio = false; CRadio = false; DRadio = false;
            }
            else if(answer == "df7e70e5021544f4834bbee64a9e3789febc4be81470df629cad6ddb03320a5c")
            {
                ARadio = false; BRadio = true; CRadio = false; DRadio = false;
            }
            else if (answer == "6b23c0d5f35d1b11f9b683f0b0a617355deb11277d91ae091d399c655b87940d")
            {
                ARadio = false; BRadio = false; CRadio = true; DRadio = false;
            }
            else if (answer == "3f39d5c348e5b79d06e842c114e6cc571583bbf44e4b0ebfda1a01ec05745d43")
            {
                ARadio = false; BRadio = false; CRadio = false; DRadio = true;
            }
            else
            {
                ARadio = true; BRadio = false; CRadio = false; DRadio = false;
            }
        }

        private string getRadio()
        {
            string txt = string.Empty;
            if (ARadio is true)
            {
                txt = "559aead08264d5795d3909718cdd05abd49572e84fe55590eef31a88a08fdffd";
            }
            else if (BRadio is true)
            {
                txt = "df7e70e5021544f4834bbee64a9e3789febc4be81470df629cad6ddb03320a5c";
            }
            else if (CRadio is true)
            {
                txt = "6b23c0d5f35d1b11f9b683f0b0a617355deb11277d91ae091d399c655b87940d";
            }
            else if (DRadio is true)
            {
                txt = "3f39d5c348e5b79d06e842c114e6cc571583bbf44e4b0ebfda1a01ec05745d43";
            }
            return txt;
        }

        private void updateView(Quiz? quest)
        {
            if (quest == null)
            {
                AnswerAText = "A";
                AnswerBText = "B";
                AnswerCText = "C";
                AnswerDText = "D";
                QuestionText = "Treść";
                listed = listQuiz.AsStrings();
                CorrectAnswer = "a";
                QuestionName = "Nazwa";
                setRadio(string.Empty);
            }
            else
            {
                AnswerAText = quest.answer.AnswerA;
                AnswerBText = quest.answer.AnswerB;
                AnswerCText = quest.answer.AnswerC;
                AnswerDText = quest.answer.AnswerD;
                QuestionText = quest.Question.Quest;
                CorrectAnswer = quest.answer.CorrectAnswer;
                QuestionName = quest.QuestName;
                listed = listQuiz.AsStrings();
                setRadio(CorrectAnswer);
            }
        }
        private bool check()
        {
            if (AnswerAText.Trim() == string.Empty) return false;
            if (AnswerBText.Trim() == string.Empty) return false;
            if (AnswerCText.Trim() == string.Empty) return false;
            if (AnswerDText.Trim() == string.Empty) return false;
            if (CorrectAnswer.Trim() == string.Empty) return false;
            if (QuestionName.Trim() == string.Empty) return false;
            if (QuestionText.Trim() == string.Empty) return false;
            if (ARadio == false && BRadio == false && CRadio == false && DRadio == false) return false;
            return true;
        }
        private void addQuestion(object obj)
        {
            if (check())
            {
                if(listed.Contains(QuestionName))
                {
                    MessageBox.Show("Takie pytanie już istnieje, zmień nazwę!");
                }
                else
                {
                    CorrectAnswer = getRadio();
                    Answer answer = new Answer(AnswerAText, AnswerBText, AnswerCText, AnswerDText, CorrectAnswer);
                    Question quest = new Question(QuestionText);
                    Quiz quiz = new Quiz(quest, answer, QuestionName);
                    listQuiz.Add(quiz);
                    updateView(quiz);
                }
            }
        }

        private void editQuestion(object obj)
        {
            if (check())
            {
                if (listed.Contains(QuestionName))
                {
                    MessageBox.Show("Takie pytanie już istnieje, zmień nazwę!");
                }
                else
                {
                    CorrectAnswer = getRadio();
                    Answer answer = new Answer(AnswerAText, AnswerBText, AnswerCText, AnswerDText, CorrectAnswer);
                    Question quest = new Question(QuestionText);
                    Quiz quiz = new Quiz(quest, answer, QuestionName);
                    listQuiz.Edit(quiz, SelectedId);
                    updateView(quiz);
                }
            }
        }

        private void deleteQuestion(object obj) 
        {
            listQuiz.Remove(SelectedId);
            updateView(null);
        }

        private void loadQuiz(object obj) 
        {
            listQuiz.initialize();
            if(listQuiz.QuizList.Count > 0)
            {
                var first = listQuiz.QuizList.First();
                updateView(first);
            }
            else
            {
                updateView(null);
            }
        }

        private void saveQuiz(object obj) 
        {
            listQuiz.save(listQuiz);
        }
        private void selection(object obj) 
        {
            updateView(listQuiz.OnIndex(SelectedId));
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
