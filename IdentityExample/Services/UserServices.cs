using IdentityExample.Models;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace IdentityExample.Services
{
    public class UserServices : IUserServices
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly EmployeeDbContext _db;
        private readonly IConfiguration _configuration;
        public UserServices(UserManager<IdentityUser> userManager, EmployeeDbContext db, IConfiguration configuration, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _db = db;  
            _configuration = configuration;
            _roleManager = roleManager;
        }
        public async Task<ResponseModel> CreateUserAsync(UserDTO userData)
        {
            ResponseModel responseModel = new ResponseModel();

            using (var transaction = _db.Database.BeginTransaction())
            {
                try
                {

                    var identityUser = new IdentityUser()
                    {
                        UserName = userData.UserName,
                        Email = userData.Email,
                        PhoneNumber = userData.Phone,
                    };


                    IdentityResult result = await _userManager.CreateAsync(identityUser, userData.password);
                    if (result.Succeeded)
                    {
                        bool isExist = await _roleManager.RoleExistsAsync(userData.UserRole);
                        if (isExist)
                        {
                            IdentityResult result_role = await _userManager.AddToRoleAsync(identityUser, userData.UserRole);
                            if (result_role.Succeeded)
                            {
                                /*saving the same data in the userdata table*/
                                UserData data = new UserData()
                                {
                                    UserName = userData.UserName,
                                    Email = userData.Email,
                                    Phone = userData.Phone,
                                    Department = userData.Department,
                                    Address = userData.Address,
                                    DateofBirth = userData.DateofBirth,
                                    UserUniqueId = userData.UserUniqueId,
                                    UserRole = userData.UserRole,
                                    password = userData.password
                                    
                                };
                                var response_data = _db.UserData.Add(data);
                                _db.SaveChanges();
                                responseModel.statusCode = System.Net.HttpStatusCode.OK;
                                responseModel.message = "User successfully created";
                                responseModel.responseData = null;

                                //Commiting the transaction
                                transaction.Commit();
                            }
                        }
                        else
                        {
                            responseModel.statusCode = System.Net.HttpStatusCode.InternalServerError;
                            responseModel.message = "Entered role does not match with the existing role in database!";
                            responseModel.responseData = null;
                        }
                    }
                    else
                    {
                        responseModel.statusCode = System.Net.HttpStatusCode.InternalServerError;
                        responseModel.message = result.ToString();
                        responseModel.responseData = null;
                    }


                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                }
            }
                
            
            return responseModel;
        }

        public async Task<ResponseModel> Login(LoginRequest login)
        {
            ResponseModel responseModel = new ResponseModel();
            string JWT_tokenValue;
            try
            {
                var identityUser = await _userManager.FindByEmailAsync(login.username);
                if(identityUser is null)
                {
                    responseModel.statusCode=System.Net.HttpStatusCode.NotFound;
                    responseModel.message = "User not found";
                    responseModel.responseData = null;
                }
                else
                {
                    var passwordCheckResult = await _userManager.CheckPasswordAsync(identityUser, login.password);
                    if (passwordCheckResult)
                    {
                        JWT_tokenValue = await GenerateTokenString(login, identityUser);
                        responseModel.statusCode = System.Net.HttpStatusCode.OK;
                        responseModel.message = "Logged in successfully";
                        responseModel.responseData = JWT_tokenValue;
                    }
                    else
                    {
                        responseModel.statusCode = System.Net.HttpStatusCode.OK;
                        responseModel.message = "Password not matched";
                        responseModel.responseData = null;
                    }
                }
                
            }
            catch(Exception ex)
            {

            }
            return responseModel;
        }

        
        public async Task<ResponseModel> GetUserList()
        {
            ResponseModel responseModel = new ResponseModel();
            try
            {
                List<IdentityUser> userList = _db.Users.ToList();
                
                responseModel.statusCode = System.Net.HttpStatusCode.OK;
                responseModel.message = "Success";
                responseModel.responseData = userList;
               
               
            }
            catch(Exception ex)
            {

            }
            return responseModel;
        }

        public async Task<string> GenerateTokenString(LoginRequest login, IdentityUser user)
        {
            string tokenString = string.Empty;
            try
            {   
                var role = await _userManager.GetRolesAsync(user);


                //Claim contain different type of data which can be used in authorization
                var claims = new List<Claim>()
                {
                new Claim(ClaimTypes.Email, login.username),
                new Claim(ClaimTypes.Role, "Admin")
                };

                SecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt:Key").Value));
                SigningCredentials SigningCred = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);
                SecurityToken SecurityToken = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(60),
                    issuer: _configuration.GetSection("Jwt:Issuer").Value,
                    audience: _configuration.GetSection("Jwt:Audience").Value,
                    signingCredentials: SigningCred
                    );

                tokenString = new JwtSecurityTokenHandler().WriteToken(SecurityToken);
            }
            catch(Exception ex)
            {

            }
            
            return tokenString;
        }

       public async Task<ResponseModel> CreateRoles(string roleName)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                if (!string.IsNullOrWhiteSpace(roleName))
                {
                    var result = await _roleManager.CreateAsync(new IdentityRole
                    {
                        Name = roleName
                        //Id= new Guid().ToString(),
                        //ConcurrencyStamp = new Guid().ToString(),
                        //NormalizedName = roleName.ToUpper()

                    });
                    if (result.Succeeded)
                    {
                        response.statusCode = System.Net.HttpStatusCode.OK;
                        response.message = "Role created successfully";
                        response.responseData = null;
                    }
                    else
                    {
                        response.statusCode = System.Net.HttpStatusCode.OK;
                        response.message = "Something went wrong";
                        response.responseData = null;
                    }
                }
            }
            catch(Exception ex)
            {

            }
            return response;
        }
    }
}
