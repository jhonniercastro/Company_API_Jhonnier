using Company_API_Jhonnier.Models;

namespace Company_API_Jhonnier.Interfaces;
public interface IUserRepository
{
    Task<int> AddUser(AddUser user);
    Task<int> ValidarRoles(List<string> roles);
    Task<int> AddUserInRoles(int UserId, int RolId);
    Task<int> ValidateUser(string Nombre, string Email, int idUser = 0);
    Task<int> UpdateUser(UpdateUser user);
}
