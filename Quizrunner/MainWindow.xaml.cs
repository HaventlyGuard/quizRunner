using Microsoft.EntityFrameworkCore.Storage;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Quizrunner
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        readonly internal Context db = new Context();
        public MainWindow()
        {
            InitializeComponent();
            //int wid_one = 0;
            Style buttonStyle = new Style(typeof(Button));
            buttonStyle.Setters.Add(new Setter { Property = Control.FontFamilyProperty, Value = new FontFamily("Verdana") });
            // buttonStyle.Setters.Add(new Setter {  });
            buttonStyle.Setters.Add(new Setter(ButtonHelper.BorderCornerRadiusProperty, new CornerRadius(20)));

            Start_button.Style = buttonStyle;
            Start_button.Style = buttonStyle;
            // label_st.Width = Start_button.Width;
        }
        public static class ButtonHelper
        {
            public static CornerRadius GetBorderCornerRadius(DependencyObject obj)
            {
                return (CornerRadius)obj.GetValue(BorderCornerRadiusProperty);
            }

            public static void SetBorderCornerRadius(DependencyObject obj, CornerRadius value)
            {
                obj.SetValue(BorderCornerRadiusProperty, value);
            }

            public static readonly DependencyProperty BorderCornerRadiusProperty =
                DependencyProperty.RegisterAttached("BorderCornerRadius", typeof(CornerRadius), typeof(ButtonHelper), new PropertyMetadata(new CornerRadius(8)));
        }

        public void Start_button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Hide();
            CreateTest goToCreateTest = new CreateTest();
            goToCreateTest.Show();
            this.Close();
        }

        public void Create_new_game_Click(object sender, RoutedEventArgs e)
        {
            StartTest goToSelectTest = new StartTest();
            if (!goToSelectTest.isTestsExist())
                return;
            this.Hide();
            goToSelectTest.loadTest();
            goToSelectTest.Show();
            this.Close();
        }

        private void Title_btn_left_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        public void Achievements_window(object sender, RoutedEventArgs e)
        {
            Achievements achievements = new Achievements();
            this.Hide();
            achievements.Show();
            this.Close();
        }
    }

}
