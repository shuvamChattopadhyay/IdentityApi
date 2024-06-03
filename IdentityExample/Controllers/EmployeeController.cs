using IdentityExample.Models;
using IdentityExample.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace IdentityExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController(IEmployeeServices employeeServices) : ControllerBase
    {
        private readonly IEmployeeServices _employee = employeeServices;

        [HttpPost]
        [Route("RegisterEmployee")]
        public async Task<ResponseModel> RegisterEmployee(EmployeesDTO empData)
        {
            ResponseModel response = new ResponseModel();
            if (empData != null)
            {
                response = await _employee.RegisterEmployee(empData);
            }
            else
            {
                response.statusCode = HttpStatusCode.OK;
                response.message = "Data is empty!";
            }
            return response;
        }

        [HttpGet]
        [Route("GetEmployeeList")]
        public async Task<ResponseModel> GetEmployeeList()
        {
            ResponseModel response = new ResponseModel();
            response = await _employee.GetEmployeeList();
            return response;
        }
    }
}
