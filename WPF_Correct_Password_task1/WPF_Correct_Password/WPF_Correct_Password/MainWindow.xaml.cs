using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;

namespace WPF_Correct_Password
{
    //Task 1 - create a program to check the correctness of the entered password
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            bool isFirstLetter; 
            bool isLastLetter;

            string password = PasswordBox.Text; //reading a string from PasswordBox
            string name = NameBox.Text; //reading a string from NameBox

            //1.check password for availability Upper and Lower symbol
            //2.check password by regular expressions for availability only initial symbol
            //3.checking the password for the presence of a letter on first and last position
            //4.password length check

            if ((password.Count(Char.IsUpper) > 0 && (password.Count(Char.IsLower) > 0))  
                && (Regex.IsMatch(password, @"^[a-zA-Z0-9_\.\-]*$")) && 
                (isFirstLetter = Char.IsLetter(password[0])) && (isLastLetter = Char.IsLetter(password[password.Length - 1]))
                && (password.Length > 7 && password.Length < 26))
            {
                MessageBox.Show(name + ' ' + "your password has been verified");
            }
            else
            {
                MessageBox.Show(name + ' ' + "your password was not checked for correct");
            }
        }
    }
}