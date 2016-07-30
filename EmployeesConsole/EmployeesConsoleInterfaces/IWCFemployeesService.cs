using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace EmployeesConsoleInterfaces
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IWCFemployeesService" in both code and config file together.
    [ServiceContract]
    public interface IWCFemployeesService
    {
        [OperationContract]
        List<int> EmployeesIDs();
        [OperationContract]
        EmployeeInfo EmpInfo(int id);
    }
}
