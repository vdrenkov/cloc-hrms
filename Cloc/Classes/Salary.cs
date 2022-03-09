﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Cloc.Database.DatabaseQuery;
using static Cloc.Classes.Checker;

namespace Cloc.Classes
{
    internal class Salary
    {
        public static bool HasOvertime(User user)
        {
            int count = 0;
            double totalHours = 0;
            List<string> checks = UserChecks(user, 30);

            foreach (string check in checks)
            {
                string[] results = check.Split(';', ';');
                DateTime checkIn = DateTime.Parse(results[1]);
                DateTime checkOut = DateTime.Parse(results[2]);

                totalHours += (checkOut - checkIn).TotalHours;
                count++;

                if (count >= 30)
                {
                    break;
                }
            }

            if (totalHours > 176)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static double CheckSalary(string UCN)
        {
            double salary = 0, overtime = 0;
            User user = SelectUserQuery(UCN);

            if (HasOvertime(user))
            {
                overtime = 1.5;
            }
            else
            {
                overtime = 1;
            }

            salary = user.TotalHours * user.HourPayment * overtime;
            salary += (salary * user.Percent) / 100;

            return salary;
        }
    }
}