﻿using Cloc.Classes;
using Cloc.Pages;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using static Cloc.Database.SelectQuery;

namespace Cloc
{
    /// <summary>
    /// Interaction logic for BossWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        public AdminWindow()
        {
            InitializeComponent();
            PreviewKeyDown += new KeyEventHandler(HandleEsc);
            Admin.Navigate(new MainPage());
        }

        private void ExitCurrentSession()
        {
            if (!Database.InsertQuery.AddLogQuery(Session.UserToken.GetLoginData(), "Изход от системата."))
            {
                MessageBox.Show("Неуспешен запис на активността.");
            }

            Session.UserToken.RemoveLoginData();
            ActivityPage.count = ActivityPage.COUNT;

            StartupWindow sw = new();
            Close();
            sw.Show();
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                ExitCurrentSession();
            }
        }

        private void ButtonMain_Click(object sender, RoutedEventArgs e)
        {
            Admin.Navigate(new MainPage());
        }

        private void ButtonAdminOptions_Click(object sender, RoutedEventArgs e)
        {
            AdminOptionsPage aop = new();

            if (aop.ComboBoxUsers != null)
            {
                aop.ComboBoxUsers.Items.Clear();
            }

            List<Person> people = SelectAllPeopleQuery();

            foreach (Person person in people)
            {
                aop.ComboBoxUsers.Items.Add(person.Name + " " + person.Surname + ", " + person.UCN);
            }

            Admin.Navigate(aop);
        }

        private void ButtonAddUser_Click(object sender, RoutedEventArgs e)
        {
            AddUserPage aup = new();

            if (aup.ComboBoxPosition != null)
            {
                foreach (WorkPosition value in Enum.GetValues(typeof(WorkPosition)))
                {
                    string position = Person.TranslateFromWorkPosition(value);
                    aup.ComboBoxPosition.Items.Add(position);
                }
            }

            Admin.Navigate(aup);
        }

        private void ButtonReports_Click(object sender, RoutedEventArgs e)
        {
            ReportsPage rp = new();

            if (rp.ComboBoxFilter != null)
            {
                rp.ComboBoxFilter.Items.Clear();
                rp.ComboBoxFilter.Items.Add("Всички потребители");

                List<Person> people = SelectAllPeopleQuery();

                foreach (Person person in people)
                {
                    rp.ComboBoxFilter.Items.Add(person.Name + " " + person.Surname + ", " + person.UCN);
                }
            }
            Admin.Navigate(rp);
        }

        private void ButtonProfile_Click(object sender, RoutedEventArgs e)
        {
            ProfilePage pp = new();

            if (pp.UserLabel != null)
            {
                pp.UserLabel.Visibility = Visibility.Hidden;
            }

            if (pp.AdminLabel != null)
            {
                pp.AdminLabel.Visibility = Visibility.Visible;
            }

            if (pp.ComboBoxPerson != null)
            {
                pp.ComboBoxPerson.Visibility = Visibility.Visible;
                pp.ComboBoxPerson.Items.Clear();
            }

            List<Person> people = SelectAllPeopleQuery();

            foreach (Person person in people)
            {
                pp.ComboBoxPerson.Items.Add(person.Name + " " + person.Surname + ", " + person.UCN);
            }

            Admin.Navigate(pp);
        }

        private void ButtonActivity_Click(object sender, RoutedEventArgs e)
        {
            ActivityPage ap = new();

            if (ap.UserLabel != null)
            {
                ap.UserLabel.Visibility = Visibility.Visible;
            }

            if (ap.ComboBoxUser != null)
            {
                ap.ComboBoxUser.Visibility = Visibility.Visible;
                ap.ComboBoxUser.Items.Clear();
            }

            List<Person> people = SelectAllPeopleQuery();

            foreach (Person person in people)
            {
                ap.ComboBoxUser.Items.Add(person.Name + " " + person.Surname + ", " + person.UCN);
            }

            Admin.Navigate(ap);
        }

        private void ButtonExit_Click(object sender, RoutedEventArgs e)
        {
            ExitCurrentSession();
        }
    }
}
