using Edunext_API.Helpers;
using Edunext_API.Models;
using Edunext_API.Services;
using Edunext_Model.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Edunext_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserServices userServices;
        private readonly DatabaseContext databaseContext;
        public UsersController(UserServices userServices, DatabaseContext databaseContext)
        {
            this.userServices = userServices;
            this.databaseContext = databaseContext;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<User>>>> GetUsers()
        {
            IEnumerable<User> users = await userServices.GetUsers();
            ApiResponse<IEnumerable<User>> res = new(users, "Get users successfully");
            return Ok(res);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<User>>> GetUserById(int id)
        {
            User? user = await userServices.GetUserById(id);
            ApiResponse<User?> res = new(user, "Get user successfully");
            if (user == null)
            {
                res.Message = $"Not found this user id: {id}";
                return NotFound(res);
            }
            return Ok(res);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<User>>> AddUser(User user)
        {
            ApiResponse<User> res = new(user, "Add user successfully");
            if (user.Id != 0)
            {
                res.Message = "Cannot add this user because id is auto generared!";
                return BadRequest(res);
            }
            try
            {
                await userServices.AddUser(user);
                return Ok(res);
            }
            catch (Exception e)
            {
                res.Message = e.Message;
                return BadRequest(res);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<User>>> UpdateUser(int id, User user)
        {
            ApiResponse<User> res = new(user, "Update user successfully");
            if (id != user.Id)
            {
                res.Message = "Cannot update this user because id is not match!";
                return BadRequest(res);
            }
            try
            {
                await userServices.UpdateUser(user);
                return Ok(res);
            }
            catch (DbUpdateConcurrencyException e)
            {
                res.Message = e.Message;
                return NotFound(res);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<User>>> DeleteUser(int id)
        {
            ApiResponse<User> res = new(null, "Delete user successfully");
            try
            {
                await userServices.DeleteUser(id);
                return Ok(res);
            }
            catch (DbUpdateConcurrencyException e)
            {
                res.Message = e.Message;
                return NotFound(res);
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<ApiResponse<User>>> Login(User user)
        {
            ApiResponse<User> res = new(user, "Login successfully");
            try
            {
                var userLogin = await databaseContext.Users.FirstOrDefaultAsync(u => u.Email == user.Email && u.Password == user.Password);
                if (userLogin == null)
                {
                    res.Message = "Username or password is incorrect!";
                    return BadRequest(res);
                }
                return Ok(res);
            }
            catch (Exception e)
            {
                res.Message = e.Message;
                return BadRequest(res);
            }
        }
        
    }
}
