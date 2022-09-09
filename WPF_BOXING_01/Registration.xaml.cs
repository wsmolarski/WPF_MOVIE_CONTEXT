﻿using System;
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
using static WPF_BOXING_01.MoviesContext;

namespace WPF_BOXING_01
{
    /// <summary>
    /// Interaction logic for Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public string UserConfirmPassword { get; set; }
        public string UserEmail { get; set; }

        public Registration()
        {
            InitializeComponent();
            DataContext = this;
        }
        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            using var db = new MoviesContext();
            var userNames = db.UserLogins
                .Select(q => q.UserName)
                .ToList();
            var emails = db.UserLogins
                .Select(q => q.Email)
                .ToList();
            if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(UserEmail) || string.IsNullOrEmpty(UserPassword))
            {
                var emptyFields = new List<string>();
                if (string.IsNullOrEmpty(UserName))
                {
                    emptyFields.Add("UserName");
                }
                if (string.IsNullOrEmpty(UserEmail))
                {
                    emptyFields.Add("Email");
                }
                if (string.IsNullOrEmpty(UserPassword))
                {
                    emptyFields.Add("Password");
                }
                var emptyFieldsString = string.Join(" ", emptyFields);
                if (emptyFields.Count == 1)
                {
                    MessageBox.Show(emptyFieldsString + " cannot be empty");
                }
                else
                    MessageBox.Show("Field cannot be empty: " + emptyFieldsString + ".");
            }
            else if (userNames.Contains(UserName))
            {
                MessageBox.Show("This username is taken");
            }
            else if (emails.Contains(UserEmail))
            {
                MessageBox.Show("This email is taken");
            }
            else if (UserPassword != UserConfirmPassword)
            {
                MessageBox.Show("Passwords are not the same");
            }
            else if (UserPassword.Contains(UserName))
            {
                MessageBox.Show("Password is too similar to username");
            }
            else
            {

                db.UserLogins.Add(new UserLogin { UserName = UserName, Password = UserPassword, Email = UserEmail, AccountType = "User" });
                db.SaveChanges();
                MessageBox.Show("Registration was succesfull\nYou can login now");
                var _ = new LoginScreen(); // po rejestracji nie zamyka okna tylko przerzuca do login screen
                _.Show();
                Close();
            }
        }
    }
}
