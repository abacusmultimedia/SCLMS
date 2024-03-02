namespace Sclms.DTOS
{
    public class LicenseDTO
    {

        public long ProductId { get; set; }
        public string UserName { get; set; }

    }
    public class LicenseAddUpdateResponseDTO
    {
        public bool IsSuccessful { get; set; }
        public string? Message { get; set; }

    }

    public class LicenseActivateionDTO
    {
        public long ProductId { get; set; }
        public string UserName { get; set; }
        public string Key { get; set; }

    }
    public class LicenseActivationResponseDTO
    {
        public bool IsSuccessful { get; set; }
        public string? Message { get; set; }

    }

    public class LicenseResponseDTO
    {
        public long Key { get; set; }
        public string? ProductName { get; set; }
        public string? UserName { get; set; }
        public bool IsActive { get; set; }
    }

}
