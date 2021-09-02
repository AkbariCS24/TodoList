using System.Threading.Tasks;
using TodoList.BusinessLayer.DTOs;

namespace TodoList.BusinessLayer.Contracts
{
    public interface IAccountRepository
    {
        UserDTO AuthenticateUser(LoginDTO loginDTO);
        UserDTO RegisterUser(UserDTO userDTO);
    }
}
