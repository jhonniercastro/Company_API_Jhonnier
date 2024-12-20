using Company_API_Jhonnier.Interfaces;
using Company_API_Jhonnier.Models;
using Company_API_Jhonnier.Repositories;

namespace Company_API_Jhonnier.Services;
public class ProductsService : IProductsService
{
    private readonly IProductsRepository _productsRepository;
    public ProductsService(IProductsRepository productsRepository)
    {
        _productsRepository = productsRepository;
    }

    public async Task<ApiResponse<AddProduct>> CreateProducts(AddProduct product)
    {
        try
        {
            if (await _productsRepository.ValidateProduct(product.name) > 0)
            {
                return new ApiResponse<AddProduct>
                {
                    Success = false,
                    Message = "El producto ya existe.",
                    Data = product
                };
            }

            int idUsuario = await _productsRepository.AddProduct(product);
            if (idUsuario > 0)
            {
                return new ApiResponse<AddProduct>
                {
                    Success = true,
                    Message = "Producto creado correctamente.",
                    Data = product
                };
            }
            else
            {
                return new ApiResponse<AddProduct>
                {
                    Success = false,
                    Message = "Ha ocurrido un error al crear el producto.",
                    Data = product
                };
            }
        }
        catch (Exception ex)
        {
            return new ApiResponse<AddProduct>
            {
                Success = false,
                Message = ex.Message,
                Data = product
            };
        }
    }
    public async Task<ApiResponse<UpdateProduct>> UpdateProducts(UpdateProduct product)
    {
        try
        {
            if (product.idProduct < 1)
            {
                return new ApiResponse<UpdateProduct>
                {
                    Success = false,
                    Message = "Id del producto invalido.",
                    Data = product
                };
            }


            if (await _productsRepository.ValidateProduct(product.name, product.idProduct) > 0)
            {
                return new ApiResponse<UpdateProduct>
                {
                    Success = false,
                    Message = "El producto ya existe.",
                    Data = product
                };
            }

            int idProduct = await _productsRepository.UpdateProduct(product);
            if (idProduct > 0)
            {
                return new ApiResponse<UpdateProduct>
                {
                    Success = true,
                    Message = "Producto editado correctamente.",
                    Data = product
                };
            }
            else
            {
                return new ApiResponse<UpdateProduct>
                {
                    Success = false,
                    Message = "Ha ocurrido un error al editar el producto.",
                    Data = product
                };
            }

        }
        catch (Exception ex)
        {
            return new ApiResponse<UpdateProduct>
            {
                Success = false,
                Message = ex.Message,
                Data = product
            };
        }
    }
    public async Task<ApiResponse<bool>> DeleteProducts(int IdProduct)
    {
        try
        {
            int idProduct = await _productsRepository.DeleteProduct(IdProduct);
            if (idProduct > 0)
            {
                return new ApiResponse<bool>
                {
                    Success = true,
                    Message = "Producto eliminado correctamente.",
                    Data = true
                };
            }
            else
            {
                return new ApiResponse<bool>
                {
                    Success = false,
                    Message = "Ha ocurrido un error al eliminar el producto.",
                    Data = false
                };
            }
        }
        catch (Exception ex)
        {
            return new ApiResponse<bool>
            {
                Success = false,
                Message = ex.Message,
                Data = false
            };
        }
    }
}
