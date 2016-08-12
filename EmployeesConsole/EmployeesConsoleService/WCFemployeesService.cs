using EmployeesConsoleInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;

namespace EmployeesConsoleService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class WCFemployeesService : IWCFemployeesService
    {
        private int empCount = 0;
        public List<int> EmployeesIDs()
        {

            List<int> empIdList = new List<int>();
            try
            {
                using (EmployeesModel db = new EmployeesModel())
                {
                    foreach (var item in db.Employees)
                    {
                        empIdList.Add(item.EmployeeID);
                        empCount++;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            return empIdList;
        }
        public EmployeeInfo EmpInfo(int id)
        {
            EmployeeInfo empinfo = null;
            try
            {
                using (EmployeesModel db = new EmployeesModel())
                {
                    Employee matchingemp = db.Employees.FirstOrDefault(e => e.EmployeeID == id);
                    empinfo = new EmployeeInfo();
                    empinfo.EmployeeID = matchingemp.EmployeeID;
                    empinfo.BirthDate = matchingemp.BirthDate;
                    empinfo.Name = matchingemp.FirstName + " " + matchingemp.LastName;
                }

            }
            catch (Exception)
            {
                Console.WriteLine("Employee id not found.");
            }
            return empinfo;
        }

        public int GetEmpCount()
        {
            return empCount;
        }
    }
}
