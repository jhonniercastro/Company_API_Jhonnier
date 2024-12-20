using Company_API_Jhonnier.Models;

namespace Company_API_Jhonnier.Interfaces;
public interface IProductsService
{
    Task<ApiResponse<AddProduct>> CreateProducts(AddProduct product);
    Task<ApiResponse<UpdateProduct>> UpdateProducts(UpdateProduct product);
    Task<ApiResponse<bool>> DeleteProducts(int IdProduct);
}