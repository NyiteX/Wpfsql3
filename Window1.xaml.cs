using System;
using System.Collections.Generic;
using System.IO;
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

namespace Wpfsql3
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        //public string passTMP;
        //public string loginTMP;
        public Window1()
        {
            InitializeComponent();
        }

        private void start_btn_Click(object sender, RoutedEventArgs e)
        {
            if(Login_tb.Text != "Login" || Pass_tb.Text != "Password")
            {
                this.Close();
            }
        }

        private void Pass_tb_GotFocus(object sender, RoutedEventArgs e)
        {
            if (Pass_tb.Text == "Password") Pass_tb.Text = "";
        }

        private void Pass_tb_LostFocus(object sender, RoutedEventArgs e)
        {
            if (Pass_tb.Text == "") Pass_tb.Text = "Password";
        }

        private void Login_tb_GotFocus(object sender, RoutedEventArgs e)
        {
            if (Login_tb.Text == "Login") Login_tb.Text = "";
        }

        private void Login_tb_LostFocus(object sender, RoutedEventArgs e)
        {
            if (Login_tb.Text == "") Login_tb.Text = "Login";
        }
    }
}
