using System.Collections.Generic;
using System.ServiceModel;


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
