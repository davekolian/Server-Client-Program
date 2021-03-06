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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;
using System.Data;
using System.Net;
using System.Net.Sockets;
using SimpleTCP;

namespace Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void CheckLogin(string username, string password) {
            string checkUser = null;
            string checkPassword = null;
            int isLogged = 0;

            String connectionString = "datasource = localhost; username = root; password =; database = loginnames";
            MySqlConnection connection = new MySqlConnection(connectionString);
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM users WHERE username = '" + username + "';", connection);
            MySqlDataReader reader;

            try
            {
                connection.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    checkUser = reader[0].ToString();
                    checkPassword = reader[1].ToString();
                    isLogged = (reader[2].ToString() == "1" ? 1 : 0);
                }

                reader.Close();

                if (checkUser != null && checkUser.Equals(username) && checkPassword.Equals(password) && isLogged == 0)
                {
                    MySqlCommand upd = new MySqlCommand("UPDATE users SET isLogged='1' WHERE username='" + username + "';", connection);
                    upd.ExecuteNonQuery();
                    OpenChatWindow();
                }
                else
                {
                    if (isLogged == 1) MessageBox.Show("User already logged in!");
                    else
                    {
                        MessageBox.Show("Incorrect user or pass!");
                        this.usernameBox.Text = "";
                        this.passwordBox.Password = "";
                    }
                }

                connection.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Login (object sender, RoutedEventArgs e)
        {
            String user = this.usernameBox.Text;
            String pass = this.passwordBox.Password;

            CheckLogin(user, pass);
        }

        private void ClearUsername(object sender, RoutedEventArgs e)
        {
            this.usernameBox.Text = ""; 
        }

        private void ClearPassword(object sender, MouseButtonEventArgs e)
        {
            this.passwordBox.Password = "";
        }

        private void UserForm(object sender, RoutedEventArgs e)
        {
            NewUserForm newUser = new NewUserForm();
            newUser.Show();
            Close();
        }

        private void OpenChatWindow()
        {
            ChatWindow chatWindow = new ChatWindow(this.usernameBox.Text);
            chatWindow.Show();
            Close();
        }
    }
}
