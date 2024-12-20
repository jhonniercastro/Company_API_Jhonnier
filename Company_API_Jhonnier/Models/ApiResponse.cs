namespace Company_API_Jhonnier.Models;
public class ApiResponse<T>
{
    public bool Success { get; set; } // Indica si la operación fue exitosa
    public string Message { get; set; } // Mensaje explicativo
    public T Data { get; set; } // Datos opcionales de la respuesta
}