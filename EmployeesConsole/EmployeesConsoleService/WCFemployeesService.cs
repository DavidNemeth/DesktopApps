using EmployeesConsoleInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace EmployeesConsoleService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "WCFemployeesService" in both code and config file together.
    public class WCFemployeesService : IWCFemployeesService
    {
        public List<int> EmployeesIDs()
        {
            List<int> empIdList = new List<int>();
            try
            {
                using (EmployeesModel db = new EmployeesModel())
                {
                    var emps = from e in db.Employees
                               select e.EmployeeID;
                    empIdList = emps.ToList();
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
                using(EmployeesModel db = new EmployeesModel())
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
    }
}
