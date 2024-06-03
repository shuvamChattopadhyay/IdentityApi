using IdentityExample.Models;

namespace IdentityExample.Services
{
    public interface IEmployeeServices
    {
        Task<ResponseModel> RegisterEmployee(EmployeesDTO empData);
        Task<ResponseModel> GetEmployeeList();
    }
}
