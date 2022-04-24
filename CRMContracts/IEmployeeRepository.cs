using CRMEntities.Models;
using CRMServices.RequestFeatures;
using System;
using System.Collections.Generic;

namespace CRMContracts
{
    public interface IEmployeeRepository
    {
        PagedList<Employee> GetEmployees(Guid companyId,EmployeeParameters employeeParameters, bool trackChanges);
        Employee GetEmployee(Guid companyId, Guid id, bool trackChanges);
        void CreateEmployeeForCompany(Guid companyId, Employee employee);
        void DeleteEmployee(Employee employee);

    }
}
