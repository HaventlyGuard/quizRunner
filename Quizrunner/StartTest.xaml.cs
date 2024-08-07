using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Windows.Threading;

namespace Quizrunner
{
    /// <summary>
    /// Логика взаимодействия для Main_quiz.xaml
    /// </summary>
    public partial class StartTest : Window
    {

        readonly internal Context db = new Context();
        public StartTest()
        {
            {
                InitializeComponent();

            }
        }
        private void doTestButton_Click(object sender, RoutedEventArgs e)
        {

            Main_Quiz goToDoTest = new Main_Quiz();
            if (Combobox_Test_Select.SelectedValue is not null)
            {
                this.Hide();
                goToDoTest.TestNameContent(Combobox_Test_Select.SelectedValue.ToString());
                goToDoTest.QuestionContent();
                goToDoTest.Show();
                this.Close();
            }
            else { MessageBox.Show("Пожалуйста выберите тест", "Configuration", MessageBoxButton.OK, MessageBoxImage.Warning); }

        }
        public bool isTestsExist()
        {
            try { db.Tests_bd.Max(x => x.id); }
            catch (System.InvalidOperationException)
            {
                MessageBox.Show("Нет тестов", "Configuration", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            return true;
        }
        public void loadTest()
        {
            Tests_bd? T;
            List<string> Test_Names = new List<string>();
            for (int i = 0; i < db.Tests_bd.Max(x => x.id); i++)
            {
                T = db.Tests_bd.FirstOrDefault(x => x.id == i + 1);
                if (T is not null)
                {
                    Test_Names.Add(T.test_name);
                }
                else
                {
                    continue;
                }
            }
            Combobox_Test_Select.ItemsSource = Test_Names;
        }
        public void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        public void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            number_q.Visibility = Visibility.Hidden;
            Finish_editing.Visibility = Visibility.Hidden;
            Edit.Visibility = Visibility;
            selectNumberQuestions.Visibility = Visibility.Hidden;
            Namequestion.Visibility = Visibility.Hidden;
            doTestButton.Visibility = Visibility;
            Edit_Q.Visibility = Visibility.Hidden;
            title_manage.Visibility = Visibility.Hidden;
            Check_Answer_one.Visibility = Visibility.Hidden;
            Check_Answer_three.Visibility = Visibility.Hidden;
            Check_Answer_four.Visibility = Visibility.Hidden;
            Check_Answer_two.Visibility = Visibility.Hidden;
            Answer_three.Visibility = Visibility.Hidden;
            Answer_four.Visibility = Visibility.Hidden;
            Answer_two.Visibility = Visibility.Hidden;
            Answer_one.Visibility = Visibility.Hidden;
            TestQuestions? T;
            Tests_bd? TT;
            List<int> Test_Ids = new List<int>();
            for (int i = 0; i < 15; i++)
            {
                T = db.TestQuestions.FirstOrDefault(x => x.question_count == i + 1);
                if (T is not null)
                {
                    Test_Ids.Add(T.question_count);
                }
                else
                {
                    continue;
                }
            }
            selectNumberQuestions.ItemsSource = Test_Ids;
        }

        public void selectNumberQuestions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        public void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (Combobox_Test_Select.SelectedValue is null)
            {
                MessageBox.Show("Пожалуйста выберите тест", "Configuration", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            Edit.Visibility = Visibility.Hidden;
            number_q.Visibility = Visibility;
            selectNumberQuestions.Visibility = Visibility;
            Namequestion.Visibility = Visibility;
            Edit_Q.Visibility = Visibility;
            doTestButton.Visibility = Visibility.Hidden;
            Finish_editing.Visibility = Visibility;
            title_manage.Visibility = Visibility;
            Check_Answer_one.Visibility = Visibility;
            Check_Answer_three.Visibility = Visibility;
            Check_Answer_four.Visibility = Visibility;
            Check_Answer_two.Visibility = Visibility;
            Answer_three.Visibility = Visibility;
            Answer_four.Visibility = Visibility;
            Answer_two.Visibility = Visibility;
            Answer_one.Visibility = Visibility;
        }

        public void Createq_Click(object sender, RoutedEventArgs e)
        {
            TextBox[] AnswerControls = { Answer_one, Answer_two, Answer_three, Answer_four };
            CheckBox[] CheckControls = { Check_Answer_one, Check_Answer_two, Check_Answer_three, Check_Answer_four };
            int Test_id = db.Tests_bd.FirstOrDefault(x => x.test_name == Combobox_Test_Select.Text).id;
            int TQ_id = db.TestQuestions.FirstOrDefault(x => x.Tests_bd.id == Test_id && x.question_count == selectNumberQuestions.SelectedIndex + 1).question_count;
            TestQuestions TQ;
            TQ = db.TestQuestions.FirstOrDefault(x => x.Tests_bd.id == Test_id && x.question_count == selectNumberQuestions.SelectedIndex + 1);
            List<TestAnswers> testAnswers = db.TestAnswers.Where(x => x.TestQuestions.question_count == TQ_id && x.Tests_bd.id == Test_id).ToList();
            if (TQ is null)
            {
                MessageBox.Show("Вы не выбрали вопрос", "Configuration", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            for (int i = 0; i < testAnswers.Count; i++)
            {
                if (AnswerControls[i].Text is "")
                {
                    continue;
                }
                testAnswers[i].answer_text = AnswerControls[i].Text;
                testAnswers[i].is_correct = CheckControls[i].IsChecked.Value;
            }
            TQ.question_text = Namequestion.Text;
            db.SaveChanges();
        }

        public void Finish_red_Click(object sender, RoutedEventArgs e)
        {
            Finish_editing.Background = new SolidColorBrush(Colors.LightGreen);
            Edit.Visibility = Visibility;
            number_q.Visibility = Visibility.Hidden;
            selectNumberQuestions.Visibility = Visibility.Hidden;
            Namequestion.Visibility = Visibility.Hidden;
            Edit_Q.Visibility = Visibility.Hidden;
            doTestButton.Visibility = Visibility;
            Finish_editing.Visibility = Visibility.Hidden;
            title_manage.Visibility = Visibility.Hidden;
            Check_Answer_one.Visibility = Visibility.Hidden;
            Check_Answer_three.Visibility = Visibility.Hidden;
            Check_Answer_four.Visibility = Visibility.Hidden;
            Check_Answer_two.Visibility = Visibility.Hidden;
            Answer_three.Visibility = Visibility.Hidden;
            Answer_four.Visibility = Visibility.Hidden;
            Answer_two.Visibility = Visibility.Hidden;
            Answer_one.Visibility = Visibility.Hidden;
        }
    }
}
