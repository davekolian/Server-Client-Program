﻿using SimpleTCP;
using System;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Forms;
using System.IO;
using MySql.Data.MySqlClient;

namespace Client
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class ChatWindow : Window
    {
        private string username;
        Boolean ifFileSelected = false; //boolean tells us if browse button is selected
        FileInfo fi; //stores file name and extension for the file to be saved

        public ChatWindow() //Default constructor
        {
            InitializeComponent();
        }

        public ChatWindow(string usr)
        {
            InitializeComponent();

            username = usr;
            this.Title = usr;

            string currentPath = "D:\\Desktop\\Server-Client Program\\Client\\Files";

            if (!Directory.Exists(currentPath + "/" + username))
            {
                Directory.CreateDirectory(currentPath + "/" + username);
            }
        }

        SimpleTcpClient client;

        private void ChatWindowLoaded(object sender, EventArgs e)
        {
            client = new SimpleTcpClient();
            client.StringEncoder = Encoding.UTF8;
            client.DataReceived += Client_DataReceived;

            txtStatus.IsReadOnly = true;
            txtHost.IsReadOnly = true;
            txtPort.IsReadOnly = true;
        }

        private void btnConnect_Click(object sender, RoutedEventArgs e)
        {
            btnConnect.IsEnabled = false; //prevents us from clicking the button
            client.Connect(txtHost.Text, Convert.ToInt32(txtPort.Text));
            client.Write("                         " + username + " just joined!\n");
        }

       

        private void Client_DataReceived(object sender, SimpleTCP.Message e)
        {
            if (e.MessageString.Length > 4 && e.MessageString.Contains("list"))
            {
                String msg = e.MessageString.Substring(4, e.MessageString.Length - 4);
                string[] listClients = new string[msg.Split(':').Length - 2];
                int cp = 0;
                for (int i = 0; i < msg.Split(':').Length; i++)
                {
                    if (msg.Split(':')[i] != "" && msg.Split(':')[i] != this.username) {
                        listClients[cp] = msg.Split(':')[i];
                        cp++;
                    }
                }
                
                listbox.Items.Dispatcher.Invoke((Action)delegate ()
                {                      
                       listbox.ItemsSource = listClients;
                });               
            }
            else
            {
                txtStatus.Dispatcher.Invoke((Action)delegate ()
                {
                    if (e.MessageString.Contains("just joined") && !e.MessageString.Contains(username))
                    {
                        txtStatus.Text += e.MessageString;
                    }
                    else if (e.MessageString.Contains("just joined") && e.MessageString.Contains(username))
                    {
                        txtStatus.Text += "";
                    }
                    else
                    {
                        txtStatus.Text += e.MessageString;
                    }
                });

            }

            if (e.MessageString.Equals("All"))
            {
                txtStatus.Dispatcher.Invoke((Action)delegate ()
                {
                    txtStatus.Text += "";
                });

            }
        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            if (btnConnect.IsEnabled)
            {
                System.Windows.MessageBox.Show("Please connect to the local server!");
            }
            else if (listbox.SelectedIndex != -1)
            {
                if (txtMessage.Text.Length > 0 && !ifFileSelected && !txtMessage.Text.Equals(" ") && !txtMessage.Text.Equals("\n"))
                {
                    client.WriteLine(listbox.SelectedItem.ToString().ToLower() + "#" + username + ": " + txtMessage.Text);
                }
                else if (ifFileSelected)
                {
                    byte[] b1 = File.ReadAllBytes(op.FileName);
                    client.Write(listbox.SelectedItem.ToString() + "#File#" + fi.Name + "#");
                    client.Write(b1);
                    ifFileSelected = false;
                }
                else
                {
                    System.Windows.MessageBox.Show("Please type something or choose a file to send!");
                }
            }
            else
            {
                System.Windows.MessageBox.Show("Please select someone to send the message to!");
            }

            txtMessage.Text = "";
            
        }

        OpenFileDialog op;
        
        private void btnBrowse_Click(object sender, RoutedEventArgs e) //Function for Button called Browse
        {
            if (!btnConnect.IsEnabled) //if button is not pressed
            {
                op = new OpenFileDialog();
                if (op.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    fi = new FileInfo(op.FileName);
                    ifFileSelected = true;
                    txtMessage.Text = "Sending " + fi.Name;
                }
            } else
            {
                System.Windows.MessageBox.Show("Please connect to the local server!");
            }
        }
        
        private void txtMessage_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!btnConnect.IsEnabled) //if button is not pressed
                client.WriteLine("list");
            else
                System.Windows.MessageBox.Show("Please connect to the local server!");

        }

        private void Window_Closed(object sender, EventArgs e)
        {
            if (!btnConnect.IsEnabled) //if button is not pressed and if we we are connected
            {
                client.Write(username + ":closed");
                client.TcpClient.Close();
            }

            String connectionString = "datasource = localhost; username = root; password =; database = loginnames";
            MySqlConnection connection = new MySqlConnection(connectionString);

            try
            {
                connection.Open();
                MySqlCommand upd = new MySqlCommand("UPDATE users SET isLogged='0' WHERE username='" + username + "';", connection);
                upd.ExecuteNonQuery();

                connection.Close();

            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
        }
    }
}
