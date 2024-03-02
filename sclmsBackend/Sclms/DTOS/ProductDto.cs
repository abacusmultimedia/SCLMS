namespace Sclms.DTOS
{
    public class ProductDto
    {
        public long  Id { get; set; }
        public string? ProductName { get; set; }
        public string? Version { get; set; }
        public string? Description { get; set; }
    }
    public class ProductLookupDto
    {
        public long Id { get; set; }
        public string? Name { get; set; }
    }
}

 