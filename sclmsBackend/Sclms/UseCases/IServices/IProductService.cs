using Sclms.DTOS;
using Sclms.Persistence.Modles;

namespace Sclms.UseCases.IServices;


public interface IProductService
{
    Task AddProductAsync(ProductDto product);
    Task<ProductDto> GetProductByIdAsync(long productId);
    Task UpdateProductAsync(Product product);
    Task DeleteProductAsync(long productId);
    Task<List<ProductDto>> GetAllProductAsync();
    Task<List<ProductLookupDto>> GetProductLookup();
}
