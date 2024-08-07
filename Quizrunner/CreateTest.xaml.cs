using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace Quizrunner
{


    /// <summary>
    /// Логика взаимодействия для CreateTest.xaml
    /// </summary>


    public partial class CreateTest : Window
    {
        readonly public Context db = new Context();

        public CreateTest()
        {
            InitializeComponent();
        }
        public void PressInputNameButton(object sender, RoutedEventArgs e)
        {
            string Name = InputTestName.Text;
            Tests_bd tests = new Tests_bd();
            if (db.Tests_bd.FirstOrDefault(x=>x.test_name == Name) is not null)
            {
                MessageBox.Show("Тест с таким названием уже существует!", "Configuration", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (Name != null && Name.Length == 0)
            {
                MessageBox.Show("Пожалуйста введите имя теста", "Configuration", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                tests.test_name = Name;
                Kl_question.Visibility = Visibility;
                SelectNumberQuestions.Visibility = Visibility;
                InputTestName.IsReadOnly = true;
                db.Tests_bd.Add(tests);
                db.SaveChanges();
            }

        }
        public void Button_Click(object sender, RoutedEventArgs e)
        {
            if(InputTestName.Text == "")
            {
                MessageBox.Show("Вы не создали вопрос", "Configuration", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            for (int i = 0; i < combobox_select.Items.Count; i++)
            {
                if (db.Tests_bd.FirstOrDefault(x => x.test_name == InputTestName.Text).TestQuestions.FirstOrDefault(x => x.question_count == i + 1) is null) // не правильно написанное исключение прога падает нет достпупа к полю с null
                {
                    MessageBox.Show("Вы заполнили не все вопросы", "Configuration", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }
            this.Hide();
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        public void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            combobox_select.Items.Clear();
            int index;
            index = SelectNumberQuestions.SelectedIndex + 1;

            for (int i = 0; i < index; i++)
            {
                combobox_select.Items.Add(i + 1);
            }
            combobox_select.Visibility = Visibility;
            Choose_num_question.Visibility = Visibility;
        }

        public void Combobox_ch_2(object sender, SelectionChangedEventArgs e)
        {
            Namequestion.Visibility = Visibility;
            Createq.Visibility = Visibility;
            title_manage.Visibility = Visibility;
            Check_Answer_one.Visibility = Visibility;
            Check_Answer_three.Visibility = Visibility;
            Check_Answer_four.Visibility = Visibility;
            Check_Answer_two.Visibility = Visibility;
            Answer_three.Visibility = Visibility;
            Answer_four.Visibility = Visibility;
            Answer_two.Visibility = Visibility;
            Answer_one.Visibility = Visibility;
            Namequestion.Clear();
            Answer_one.Clear();
            Answer_four.Clear();
            Answer_two.Clear();
            Answer_three.Clear();
            Check_Answer_one.IsChecked = false;
            Check_Answer_three.IsChecked = false;
            Check_Answer_four.IsChecked = false;
            Check_Answer_two.IsChecked  = false;
        }



        public void PressCreateq(object sender, RoutedEventArgs e)
        {
            TestQuestions testQuestions = new TestQuestions();
            TextBox[] AnswerControls = { Answer_one, Answer_two, Answer_three, Answer_four };
            CheckBox[] CheckControls = { Check_Answer_one, Check_Answer_two, Check_Answer_three, Check_Answer_four };
            Tests_bd? T;
            TestQuestions? TQ;
            T = db.Tests_bd.FirstOrDefault(x => x.test_name == InputTestName.Text);
            
            if (Answer_four.Text == "" || Answer_three.Text=="" || Answer_two.Text==""|| Answer_one.Text=="")
            {
                MessageBox.Show("Пожалуйста введите ответы на вопрос", "Configuration", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (Check_Answer_four.IsChecked == false && Check_Answer_three.IsChecked == false && Check_Answer_two.IsChecked == false && Check_Answer_one.IsChecked == false)
            {
                MessageBox.Show("Пожалуйста выберете хотя бы один правильный ответ", "Configuration", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (db.Tests_bd.FirstOrDefault(x=>x.test_name==InputTestName.Text).TestQuestions.FirstOrDefault(x=>x.question_count==combobox_select.SelectedIndex+1)is not null) 
            {
                MessageBox.Show("Вы пытаетесь добавить вопрос который уже был добавлен в Тест", "Configuration", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (T is not null)
            {
                testQuestions.question_text = Namequestion.Text;
                testQuestions.question_count = combobox_select.SelectedIndex + 1;
                testQuestions.Tests_bd = T;
                db.TestQuestions.Add(testQuestions);
                db.SaveChanges();
            }
            TQ = db.TestQuestions.FirstOrDefault(x => x.question_count == combobox_select.SelectedIndex + 1);
            if (TQ is not null)
            {
                TestAnswers[] answers = new TestAnswers[4];

                for (int i = 0; i < 4; i++)
                {
                    TestAnswers testAnswer = new TestAnswers();
                    testAnswer.answer_text = AnswerControls[i].Text;
                    testAnswer.is_correct = CheckControls[i].IsChecked.Value;
                    testAnswer.TestQuestions = TQ;
                    testAnswer.Tests_bd = T;
                    testAnswer.answer_count = i + 1;
                    db.Add(testAnswer);
                    answers[i] = testAnswer;
                }
                db.SaveChanges();
            }
        }
    }
}
