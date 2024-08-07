using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
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
    /// Логика взаимодействия для Achievements.xaml
    /// </summary>
    public partial class Achievements : Window
    {
        readonly internal Context db = new Context();
        
        public Achievements()
        {
            InitializeComponent();
            fillTable();
        }
        internal void fillTable()
        {
            if (db.Results.ToList() == null) 
            {
                MessageBox.Show("Тест с таким названием уже существует!", "Configuration", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            Results_Table.ItemsSource = db.Results
                                  .Include(r => r.Users)
                                  .Include(r => r.Tests_bd)
                                  .ToList();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
