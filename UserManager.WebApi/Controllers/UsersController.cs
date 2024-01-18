using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Reflection.Metadata.Ecma335;
using UserManager.Aplication.IRepo;
using UserManager.Data.Context;
using UserManager.ViewModel.User;

namespace UserManager.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepo _userRepo;

        public UsersController(IUserRepo userRepo)
        {
            _userRepo = userRepo;
        }
        [HttpGet]
        public async Task<ActionResult> Get() 
        {
            var result= await _userRepo.GetUsers();
            if (result==null)
            {
                return BadRequest("Not User in db");
            }
            return Ok(result);
        }
        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<ActionResult> Login([FromForm] LoginRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result= await _userRepo.Authencate(request);
            if (result==null)
            {
                return BadRequest("Pass or user is wrong");
            }
            return Ok(new {token=result});
        }
        [HttpPost]
        public async Task<ActionResult> Create([FromForm]CreateUserRequest request) 
        {
 
            var result= await _userRepo.CreateUser(request);
            if (result==null)
            {
                return BadRequest("Lỗi r");
            }
            return Ok(result);
        }
        [HttpGet("/GetByID")]
        public async Task<ActionResult> GetByID([FromQuery]Guid UserID)
        {

            var result = await _userRepo.GetByID(UserID);
            if (result == null)
            {
                return BadRequest("Not Found");
            }
            return Ok(result);
        }
        [HttpPut()]
        public async Task<ActionResult> Update([FromForm] CreateUserRequest User)
        {

            var result = await _userRepo.UpdateUser(User);
            if (result == null)
            {
                return BadRequest("Thất cmn bại");
            }
            return Ok(result);
        } 
        [HttpDelete]
        public async Task<ActionResult> Delete([FromForm] Guid UserID)
        {

            var result = await _userRepo.DeleteUser(UserID);
            if (result == null)
            {
                return BadRequest("Thất cmn bại");
            }
            return Ok(result);
        }

    }
}
