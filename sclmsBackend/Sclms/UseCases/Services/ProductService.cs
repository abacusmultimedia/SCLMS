using Sclms.DTOS;
using Sclms.Persistence.Modles;
using Sclms.UseCases.IServices;

public class ProductService : IProductService
{
    private readonly IRepository<Product> _productRepository;

    public ProductService(IRepository<Product> productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task AddProductAsync(ProductDto product)
    {
        var version = new List<ProductVersion>
        {
            new ProductVersion
            {ReleaseNote = "",
            Title = product.Version

            }
        };
        var entity = new Product
        {
            ProductName = product.ProductName,
            Description = product.Description,
            Version = version
        };
        await _productRepository.AddAsync(entity);
    }

    public async Task<List<ProductDto>> GetAllProductAsync()
    {
        var response = _productRepository.GetAllAsync();

        return response.Select(x => new ProductDto
        {
            Description = x.Description,
            Id = x.Id,
            ProductName = x.ProductName,
            //Version = x.Version.Select(r=> new ProductVersion
            //{
            //    Title = r.Title
            //}).ToList()
        }).ToList();

    }
    public async Task<List<ProductLookupDto>> GetProductLookup()
    {
        var response = _productRepository.GetAllAsync();

        return response.Select(x => new ProductLookupDto
        {
            Id = x.Id,
            Name = x.ProductName
        }).ToList();
    }



    public async Task<ProductDto> GetProductByIdAsync(long productId)
    {
        var product = await _productRepository.GetByIdAsync(productId);
        return new ProductDto
        {
            ProductName = product.ProductName,
            Id = product.Id,
            Description = product.Description,
        };
    }

    public async Task UpdateProductAsync(Product product)
    {
        await _productRepository.UpdateAsync(product);
    }

    public async Task DeleteProductAsync(long productId)
    {
        var product = await _productRepository.GetByIdAsync(productId);
        if (product != null)
        {
            await _productRepository.DeleteAsync(product);
        }
    }

}
