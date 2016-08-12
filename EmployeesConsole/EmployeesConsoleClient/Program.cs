using EmployeesConsoleInterfaces;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Text.RegularExpressions;

namespace EmployeesConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            ChannelFactory<IWCFemployeesService> channelFactory =
                new ChannelFactory<IWCFemployeesService>("EmployeesEndpoint");
            IWCFemployeesService proxy = channelFactory.CreateChannel();


            List<int> empIds = proxy.EmployeesIDs();
            Console.WriteLine("Welcome to Employee Database command line tool\nType 'HELP' to display available commands");
            for (; true;)
            {
                Console.WriteLine();
                Console.Write(">");
                string input = Console.ReadLine().ToLower();
                Regex.Replace(input, @"\s+", "");

                if (input == "help")
                {
                    Console.WriteLine();
                    Console.WriteLine("Possible commands:");
                    Console.WriteLine("'HELP' show all commands\n'IDLIST' list all employee id\n'EMPINFO+id' Display employee information with the given id");
                }

                if (input == "idlist")
                {
                    foreach (var item in empIds)
                    {
                        Console.WriteLine(item);
                    }
                    Console.WriteLine("Number of employees: {0}", proxy.GetEmpCount());
                }

                if (input.Contains("empinfo"))
                {
                    string empnumber = Regex.Match(input, @"\d+").Value;
                    try
                    {
                        var employee = proxy.EmpInfo(Convert.ToInt32(empnumber));
                        string birth = string.Format("{0:d}", employee.BirthDate);
                        Console.WriteLine("name: {0}", employee.Name);
                        Console.WriteLine("date of birth: {0}", birth);
                        Console.WriteLine("id: {0}", employee.EmployeeID);
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Invalid id format");
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Host is closed");
                    }
                }

            }
        }

    }
}

