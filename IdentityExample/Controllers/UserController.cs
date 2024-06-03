using IdentityExample.Models;
using IdentityExample.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace IdentityExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserServices _userServices;
        public UserController(IUserServices userServices)
        {
            _userServices = userServices;
        }

        [HttpPost]
        [Route("CreateUser")]
        [AllowAnonymous]
        public async Task<ResponseModel> CreateUser(UserDTO userData)
        {
            ResponseModel responseModel = new ResponseModel();
            
            if(userData != null)
            {
                //Create hash of the given password and store only hashvalue in the database
               responseModel = await _userServices.CreateUserAsync(userData);
            }
            else
            {
                responseModel.statusCode = System.Net.HttpStatusCode.InternalServerError;
                responseModel.message = "Data can not be null";
                responseModel.responseData = null;
            }
         
            return responseModel;
        }

        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
        public async Task<ResponseModel> Login(LoginRequest login)
        {
            ResponseModel responseModel = new ResponseModel();
            if(login != null)
            {
                responseModel = await _userServices.Login(login);
            }
            else
            {
                responseModel.statusCode=System.Net.HttpStatusCode.InternalServerError;
                responseModel.message = "Login data can not be null";
                responseModel.responseData = null;
            }
            return responseModel;
        }


        [HttpGet]
        [Route("GetUserList")]
        public async Task<ResponseModel> GetUserList()
        {
            ResponseModel responseModel = new ResponseModel();
            responseModel = await _userServices.GetUserList();
            return responseModel;

        }

        [HttpPost]
        [Route("AddRole")]
        [AllowAnonymous]
        public async Task<ResponseModel> AddRole(string role)
        {
            ResponseModel response = new ResponseModel();
            response = await _userServices.CreateRoles(role);
            return response;
        }
    }
}
