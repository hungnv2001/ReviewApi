using Microsoft.AspNetCore.Identity;
using UserManager.Aplication.IRepo;
using UserManager.Data.Entities;
using UserManager.ViewModel.User;
using Microsoft.EntityFrameworkCore;
using UserManager.Data.Context;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace UserManager.Aplication.Repositories
{
    public class UserRepo : IUserRepo
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signinManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IConfiguration _config;
        private readonly MyDbContext _context;

        public UserRepo(UserManager<User>userManager, MyDbContext context, SignInManager<User> signInManager, RoleManager<Role> roleManager, IConfiguration config)
        {
            _userManager = userManager;
            _context = context;
            _signinManager= signInManager;
            _roleManager= roleManager;
            _config = config;
        }
        public async Task<string> Authencate(LoginRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null) return null;

            var result = await _signinManager.PasswordSignInAsync(user, request.Password, request.RememberMe, true);

            if (!result.Succeeded)
            {
                return null;
            }

            var roles = await _userManager.GetRolesAsync(user);
            var claims = new[]
            {
        new Claim(ClaimTypes.Email, user.Email),
        new Claim(ClaimTypes.GivenName, user.Name),
        new Claim(ClaimTypes.Role, string.Join(";", roles)),
        new Claim(ClaimTypes.Name, request.UserName)
    };

            // Lấy chuỗi khóa từ appsettings.json
            var keyString = _config["Tokens:Key"];

            // Chuyển đổi chuỗi khóa sang mảng byte và đảm bảo có độ dài đủ (256 bits)
            var keyBytes = Encoding.UTF8.GetBytes(keyString);
            //if (keyBytes.Length < 32)
            //{
            //    Array.Resize(ref keyBytes, 32);
            //}

            var key = new SymmetricSecurityKey(keyBytes);
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Tokens:Issuer"],
                _config["Tokens:Issuer"],
                claims,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async  Task<bool> CheckPassWord(string userName, string password)
        {
            var user = await _userManager.FindByIdAsync(userName);
            //ccheck user tồn tại
            if (user == null)
            {
                
                return false;
            }

            // Kiểm tra mật khẩu hiện tại
            var passwordCheck = await _userManager.CheckPasswordAsync(user, password);

            if (!passwordCheck)
            {
              
                return false;
            }
            return true;
        }
        public async Task<UserViewModel> CreateUser(CreateUserRequest request)
        {
            var user = new User()
            {
                Email = request.Email,
                Name = request.Name,
               UserName= request.UserName,
               
            };
            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded) 
            {
                return new UserViewModel
                {
                    Email = request.Email,
                    Name = request.Name,
                    UserName = request.UserName,

                };
            }
            return null;
        }

        public async Task<bool> DeleteUser(Guid ID)
        {
            var user = await _userManager.FindByIdAsync(ID.ToString());
            //ccheck user tồn tại
            if (user == null)
            {

                return false;
            }
            var x= await _userManager.DeleteAsync(user);
            return true;

        }

        public async Task<UserViewModel> GetByID(Guid ID)
        {
          //  var result = await _userManager.FindByIdAsync(ID.ToString());
           User result = await _context.Users.FindAsync(ID);
          if(result != null)
            {
                return new UserViewModel
                {
                    ID = result.Id,
                    Email = result.Email,
                    Name = result.Name,
                    UserName = result.UserName,

                }; ;
            }
            return null;
        }

        public async Task<List<UserViewModel>> GetUsers()
        {
                var lstUser = await _userManager.Users.ToListAsync();
          //  var lstUser =await  _context.Users.ToListAsync();
            var lstUserVM = lstUser.Select(p => new UserViewModel
            {
                ID=p.Id,
                Email = p.Email,
                Name = p.Name,
                UserName = p.UserName,

            });
           

            return  lstUserVM.ToList();
            
            
        }

        public async Task<bool> UpdateUser(CreateUserRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);

            if (user == null)
            {
                return false;
            }

            // Cập nhật thông tin người dùng
            user.Email = request.Email;
            user.Name = request.Name;
            var result = await _userManager.UpdateAsync(user);
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result2 = await _userManager.ResetPasswordAsync(user, token, request.Password);
            if (result.Succeeded)
            {
                return true;
            }
            return false;
        }
    }
}
