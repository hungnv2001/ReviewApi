using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManager.ViewModel.User;

namespace UserManager.Aplication.IRepo
{
    public interface IUserRepo
    {
        Task<List<UserViewModel>> GetUsers();
        Task<UserViewModel> GetByID(Guid ID);
        Task<UserViewModel> CreateUser(CreateUserRequest request);
        Task<bool> UpdateUser(CreateUserRequest user);
        Task<bool> DeleteUser(Guid ID);
        Task<string> Authencate(LoginRequest request);
    }
}
