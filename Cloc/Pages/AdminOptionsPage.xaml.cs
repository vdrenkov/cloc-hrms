﻿using Cloc.AdditionalWindows;
using Cloc.Classes;
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

namespace Cloc.Pages
{
    /// <summary>
    /// Interaction logic for BossOptionsPage.xaml
    /// </summary>
    public partial class AdminOptionsPage : Page
    {
        public AdminOptionsPage()
        {
            InitializeComponent();
        }

        private bool DeleteUser()
        {
            bool flag = false;

            if (ComboBoxUsers != null && ComboBoxUsers.SelectedItem != null)
            {
                string userInfo = ComboBoxUsers.SelectedItem.ToString();
                string[] split = userInfo.Split(", ");

                if (Session.UserToken.GetLoginData() != split[1])
                {
                    if (Database.DatabaseQuery.DeleteWorkerQuery(split[1]))
                    {
                        flag = true;

                        Logger.AddLog(Session.UserToken.GetLoginData(), "Изтриване профила на " + split[0] + ".");
                    }
                }
                else
                {
                    MessageBox.Show("Не може да изтриете администраторския акаунт!");
                }
            }
            else
            {
                MessageBox.Show("Не сте избрали потребител.");
            }

            return flag;
        }

        private void AddUserButton_Click(object sender, RoutedEventArgs e)
        {
            SetupWindow sw = new();

            sw.LabelServer.Visibility = Visibility.Hidden;
            sw.LabelUser.Visibility = Visibility.Hidden;
            sw.LabelPassword.Visibility = Visibility.Hidden;
            sw.LabelPort.Visibility = Visibility.Hidden;

            sw.TextBoxServer.Visibility = Visibility.Hidden;
            sw.TextBoxUser.Visibility = Visibility.Hidden;
            sw.PasswordBoxDBPassword.Visibility = Visibility.Hidden;
            sw.TextBoxPort.Visibility = Visibility.Hidden;

            sw.ButtonAddUser.Visibility = Visibility.Visible;
            sw.ButtonSetup.Visibility = Visibility.Hidden;

            if (sw.ComboBoxPosition != null)
            {
                sw.ComboBoxPosition.Visibility = Visibility.Visible;
                foreach (WorkPosition value in Enum.GetValues(typeof(WorkPosition)))
                {
                    string position = Person.TranslateFromWorkPosition(value);
                        sw.ComboBoxPosition.Items.Add(position);
                }
            }

            sw.Show();
        }

        private void DeleteUserButton_Click(object sender, RoutedEventArgs e)
        {
            if (DeleteUser())
            {
                MessageBox.Show("Избраният потребител беше изтрит успешно.");
            }
        }

        private void ChangeDataButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CheckSalaryButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
