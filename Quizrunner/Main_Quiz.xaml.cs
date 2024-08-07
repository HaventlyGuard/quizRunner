using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace Quizrunner
{
    /// <summary>
    /// Логика взаимодействия для Main_quiz.xaml
    /// </summary>
    public partial class Main_Quiz : Window
    {
        readonly public Context db = new Context();
        public Main_Quiz()
        {
            InitializeComponent();
        }
        
        public void Button_Click(object sender, RoutedEventArgs e)
        {

            this.Hide();
            StartTest mainWindow = new StartTest();
            mainWindow.loadTest();
            mainWindow.Show();
            this.Close();
        }
        public void TestNameContent(string name)
        {
            TestName.Content = name;
        }
        public void alreadyAnswered()
        {
            List<Rectangle> rectangles = getRectangles();
            List<Button> Answers = getAnsButt();
            if (AnsweredQuestion.IndexOf(currentQuestionNumber) != -1) //что текуший номер вопроса, который мы выбрали содержится в вопроса
            {
                for (int i = 0; i < Answers.Count; i++)
                {
                    rectangles[i].Stroke = Brushes.Black;
                    Answers[i].IsEnabled = false;
                }
            }

            else
            {
                for (int i = 0; i < Answers.Count; i++)
                {
                    rectangles[i].Stroke = Brushes.Black;
                    Answers[i].IsEnabled = true;
                }
            }

        }
        public void QuestionContent()
        {
            List<Button> Qustions_Buttons = getAnsButt();
            int Test_id = getTestId();
            int TQ_id = getTQ_Id();
            NumberQuestion.Content = $"Вопрос {currentQuestionNumber}: {db.TestQuestions.Where(x => x.Tests_bd.id == Test_id).FirstOrDefault(x => x.question_count == currentQuestionNumber).question_text}";
            List<TestAnswers> testAnswers = db.TestAnswers.Where(x => x.TestQuestions.question_count == TQ_id && x.Tests_bd.id == Test_id).ToList();
            ButtonVisibility();
            for (int i = 0; i < Qustions_Buttons.Count; i++)
            {
                Qustions_Buttons[i].Content = testAnswers[i].answer_text;
            }
            if (currentQuestionNumber == db.TestQuestions.Where(x => x.Tests_bd.id == Test_id).Max(x => x.question_count))
            {
                ConfirmEnd.Visibility = Visibility.Visible;
            }
        }
        public List<Button> getAnsButt()
        {
            List<Button> buttons = new();
            buttons = [QuestionButton1, QuestionButton2, QuestionButton3, QuestionButton4];
            return buttons;
        }
        public List<Rectangle> getRectangles()
        {
            List<Rectangle> rectangles = new();
            rectangles = [l1,l2,l3,l4];
            return rectangles;
        }
        public int getTestId()
        {
            return db.Tests_bd.FirstOrDefault(x => x.test_name == TestName.Content).id;
        }
        public int getMaxCountQ()
        {
            int Test_id = getTestId();
            return db.TestQuestions.Where(x => x.Tests_bd.id == Test_id).Max(x => x.question_count);
        }
        public int getTQ_Id()
        {
            int Test_id = getTestId();
            return db.TestQuestions.FirstOrDefault(x => x.Tests_bd.id == Test_id && x.question_count == currentQuestionNumber).question_count;
        }
        public void ButtonVisibility()
        {
            if (currentQuestionNumber == getMaxCountQ())
            {
                ButtonArrowRight.IsEnabled = false;
                ButtonArrowRight.Opacity = 0.25;
            }
            else
            {
                ButtonArrowRight.IsEnabled = true;
                ButtonArrowRight.Opacity = 1;
            }

            if (currentQuestionNumber == 1)
            { 
                ButtonArrowLeft.IsEnabled = false;
                ButtonArrowLeft.Opacity = 0.25;
            }
            else
            {
               ButtonArrowLeft.IsEnabled = true;
                ButtonArrowLeft.Opacity = 1;
            }
            if (currentQuestionNumber == getMaxCountQ())
            {
                ConfirmEnd.Visibility = Visibility.Visible;
            }
        }
        public int pressedButton(object sender, List<Button> Buttons)
        {
            Button clickedButton = (Button)sender;
            string buttonName = clickedButton.Name;
            return Buttons.IndexOf(clickedButton);
        }
        public void anyButtonClicked(object sender, RoutedEventArgs e)
        {
            List<Button> LefTRight = [ButtonArrowLeft, ButtonArrowRight];
            int buttonIndex = pressedButton(sender, LefTRight);
            if (buttonIndex == 0) { currentQuestionNumber--; }
            else { currentQuestionNumber++; }
            QuestionContent();
            ButtonVisibility();
            alreadyAnswered();
        }
        public void ConfirmEnd_Click(object sender, RoutedEventArgs e)
        {
            Name = SharedData.Instance.SharedVariable;
            Results results = new Results();
            int User_id = db.Users.FirstOrDefault(x=> x.name == Name).id;
            int Test_id = db.Tests_bd.FirstOrDefault(x => x.test_name == TestName.Content).id;
            results.Users = db.Users.FirstOrDefault(Users => Users.id == User_id);
            results.Tests_bd = db.Tests_bd.FirstOrDefault(Tests => Tests.id == Test_id);
            results.result = countRightAnswers.ToString() + "/" + db.TestQuestions.Where(x => x.Tests_bd.id == Test_id).Max(x => x.question_count).ToString();
            results.date = DateTime.Now.ToShortDateString() +" "+ DateTime.Now.ToString("HH:mm:ss");
            db.Results.Add(results);
            db.SaveChanges();
            this.Hide();
            MainWindow goToMainWindow = new MainWindow();
            goToMainWindow.Show();
            this.Close();
        }
        
        public void answerButtonClicked(object sender, RoutedEventArgs e)
        {
            List<Button> Answers = getAnsButt();
            int Test_id = getTestId();
            int TQ_id = getTQ_Id();
            TestAnswers TA = db.TestAnswers.FirstOrDefault(x => x.Tests_bd.id == Test_id && x.TestQuestions.question_count == TQ_id && x.is_correct == true);
            List<TestAnswers> testAnswers = db.TestAnswers.Where(x => x.TestQuestions.question_count == TQ_id && x.Tests_bd.id == Test_id).ToList();
            List<int> RA = db.TestAnswers.Where(x => x.TestQuestions.question_count == TQ_id && x.Tests_bd.id == Test_id && x.is_correct == true).Select(x => x.answer_count).ToList(); 
            List <Rectangle> rectangles = getRectangles();
            int buttonIndex = pressedButton(sender, Answers) + 1;
            if (RA.IndexOf(db.TestAnswers.FirstOrDefault(x => x.TestQuestions.question_count == TQ_id && x.answer_count == buttonIndex).answer_count) != -1) 
            {
                rectangles[buttonIndex-1].Stroke = Brushes.Green;
                countRightAnswers++;
            }
            else 
            {
                rectangles[buttonIndex-1].Stroke = Brushes.DarkRed;
                foreach (int i in RA)
                {
                    rectangles[i-1].Stroke = Brushes.Green;
                }
            }
            for (int i = 0; i < Answers.Count; i++)
            {
                Answers[i].IsEnabled = false;
            }
            AnsweredQuestion.Add(currentQuestionNumber);
        }
        List<int> AnsweredQuestion = [];
        int countRightAnswers = 0;
        public int currentQuestionNumber = 1;
    }
}