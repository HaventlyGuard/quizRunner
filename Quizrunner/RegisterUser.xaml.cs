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

namespace Quizrunner
{
    /// <summary>
    /// Логика взаимодействия для RegisterUser.xaml
    /// </summary>
    public partial class RegisterUser : Window
    {
        readonly internal Context db = new Context();

        public RegisterUser()
        {
            InitializeComponent();
            db.Database.EnsureCreated();
        }
        public string Name;
        public string GetName()
        {
            string Name = User_Name.Text;
            return Name;
        }

        public void EnterButton_Click(object sender, RoutedEventArgs e)
        {
            if (GetName() == "")
            {
                mistakeLabel.Content = "Введите логин!";
                return;
            }
            else if (db.Users.FirstOrDefault(x=> x.name==GetName()) is null) 
            {
                mistakeLabel.Content = "Такого имени пользователя нет, возможно вы хотели зарегистрироваться?";
                return;
            }
            else
            {
                this.Hide();
                MainWindow mainWindow = new MainWindow();
                SharedData.Instance.SharedVariable = GetName();
                mainWindow.Show();
                this.Close();
            }
        }
        public void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            if (GetName() == "")
            {
                mistakeLabel.Content = "Введите логин!";
                return;
            }
            else if (db.Users.FirstOrDefault(x => x.name == GetName()) is not null)
            {
                mistakeLabel.Content = "Такой пользователь уже существует";
                return;
            }
            else 
            { 
                Users user = new Users();
                user.name = GetName();
                SharedData.Instance.SharedVariable = GetName();
                db.Users.Add(user);
                db.SaveChanges();
                this.Hide();
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }
        }
    }
}
