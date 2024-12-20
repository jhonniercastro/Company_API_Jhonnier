using Company_API_Jhonnier.Interfaces;
using Company_API_Jhonnier.Models;
using Company_API_Jhonnier.Repositories;
using System.Text.RegularExpressions;

namespace Company_API_Jhonnier.Services;
public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task<ApiResponse<AddUser>> CreateUser(AddUser user)
    {
        try
        {
            if (user.Roles.Count() == 0)
            {
                return new ApiResponse<AddUser>
                {
                    Success = false,
                    Message = "No se han adjuntado roles.",
                    Data = user
                };
            }
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            if (Regex.IsMatch(user.Email, pattern))
            {
                int bdRoles = await _userRepository.ValidarRoles(user.Roles.Distinct().ToList());
                if (bdRoles == user.Roles.Distinct().ToList().Count())
                {
                    if (await _userRepository.ValidateUser(user.Nombre, user.Email) > 0)
                    {
                        return new ApiResponse<AddUser>
                        {
                            Success = false,
                            Message = "El usuario o correo ya existe.",
                            Data = user
                        };
                    }

                    int idUsuario = await _userRepository.AddUser(user);
                    if (idUsuario > 0)
                    {
                        foreach (var item in user.Roles)
                        {
                            int RolId = 0;
                            await _userRepository.AddUserInRoles(idUsuario, RolId);
                        }
                        return new ApiResponse<AddUser>
                        {
                            Success = true,
                            Message = "Usuario creado correctamente.",
                            Data = user
                        };
                    }
                    else
                    {
                        return new ApiResponse<AddUser>
                        {
                            Success = false,
                            Message = "Ha ocurrido un error al crear el usuario.",
                            Data = user
                        };
                    }
                }
                else
                {
                    return new ApiResponse<AddUser>
                    {
                        Success = false,
                        Message = "Uno o mas roles no existen.",
                        Data = user
                    };
                }
            }
            else
            {
                return new ApiResponse<AddUser>
                {
                    Success = false,
                    Message = "Correo invalido.",
                    Data = user
                };
            }
        }
        catch (Exception ex)
        {
            return new ApiResponse<AddUser>
            {
                Success = false,
                Message = ex.Message,
                Data = user
            };
        }
    }

    public async Task<ApiResponse<UpdateUser>> UpdateUser(UpdateUser user)
    {
        try
        {
            if (user.IdUser < 1)
            {
                return new ApiResponse<UpdateUser>
                {
                    Success = false,
                    Message = "Id del usuario invalido.",
                    Data = user
                };
            }


            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            if (Regex.IsMatch(user.Email, pattern))
            {
                int bdRoles = await _userRepository.ValidarRoles(user.Roles.Distinct().ToList());
                if (bdRoles == user.Roles.Distinct().ToList().Count())
                {
                    if (await _userRepository.ValidateUser(user.Nombre, user.Email, user.IdUser) > 0)
                    {
                        return new ApiResponse<UpdateUser>
                        {
                            Success = false,
                            Message = "El usuario o correo ya existe.",
                            Data = user
                        };
                    }

                    int idUsuario = await _userRepository.UpdateUser(user);
                    if (idUsuario > 0)
                    {
                        foreach (var item in user.Roles)
                        {
                            int RolId = 0;
                            await _userRepository.AddUserInRoles(idUsuario, RolId);
                        }
                        return new ApiResponse<UpdateUser>
                        {
                            Success = true,
                            Message = "Usuario editado correctamente.",
                            Data = user
                        };
                    }
                    else
                    {
                        return new ApiResponse<UpdateUser>
                        {
                            Success = false,
                            Message = "Ha ocurrido un error al editar el usuario.",
                            Data = user
                        };
                    }
                }
                else
                {
                    return new ApiResponse<UpdateUser>
                    {
                        Success = false,
                        Message = "Uno o mas roles no existen.",
                        Data = user
                    };
                }
            }
            else
            {
                return new ApiResponse<UpdateUser>
                {
                    Success = false,
                    Message = "Correo invalido.",
                    Data = user
                };
            }
        }
        catch (Exception ex)
        {
            return new ApiResponse<UpdateUser>
            {
                Success = false,
                Message = ex.Message,
                Data = user
            };
        }
    }
}
