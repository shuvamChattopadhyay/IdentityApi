using IdentityExample.Models;
using Microsoft.EntityFrameworkCore;

namespace IdentityExample.Services
{
    public class EmployeeServices(IConfiguration configuration, EmployeeDbContext db) : IEmployeeServices
    {
        private readonly EmployeeDbContext _db = db;
        private readonly IConfiguration _configuration = configuration;

        public async Task<ResponseModel> GetEmployeeList()
        {
            ResponseModel response = new ResponseModel();
            try
            {

            }
            catch(Exception ex)
            {

            }
        }

        public async Task<ResponseModel> RegisterEmployee(EmployeesDTO empData)
        {
            ResponseModel responseModel = new ResponseModel();
            
            try
            {
                Employees emp = new Employees()
                {
                    Full_Name = empData.Full_Name,
                    Address = empData.Address,
                    Mobile_no = empData.Mobile_no,
                    DepartmentId = empData.DepartmentId,
                    DateOfBirth = empData.DateOfBirth,
                    UserId = empData.UserId
                };

                var retData = _db.Employees.Add(emp);
                _db.SaveChanges();
                responseModel.statusCode = System.Net.HttpStatusCode.OK;
                responseModel.message = "Employee Registered Successfullt!";
                responseModel.responseData = retData;

            }
            catch (Exception ex) 
            {
                responseModel.statusCode = System.Net.HttpStatusCode.OK;
                responseModel.message = ex.Message;
                responseModel.responseData = null;
            }
            return responseModel;
        }
    }
}
