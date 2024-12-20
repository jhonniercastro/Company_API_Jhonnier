using Company_API_Jhonnier.Models;

namespace Company_API_Jhonnier.Interfaces;
public interface IProductsRepository
{
    Task<int> AddProduct(AddProduct product);
    Task<int> ValidateProduct(string Nombre, int idProduct = 0);
    Task<int> UpdateProduct(UpdateProduct product);
    Task<int> DeleteProduct(int IdProduct);
}
