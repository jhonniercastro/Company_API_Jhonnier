using Company_API_Jhonnier.Models;

namespace Company_API_Jhonnier.Interfaces;
public interface IUserService
{
    Task<ApiResponse<AddUser>> CreateUser(AddUser user);
    Task<ApiResponse<UpdateUser>> UpdateUser(UpdateUser user);
}
