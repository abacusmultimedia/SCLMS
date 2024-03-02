using Microsoft.AspNetCore.Identity;
using NuGet.Protocol.Plugins;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sclms.Persistence.Modles
{

    public class Product
    {
        public long  Id { get; set; }
        public string? ProductName { get; set; }
        public ICollection<ProductVersion>  Version { get; set; }
        public string? Description { get; set; }
        
    }
    public class ProductVersion
    {
        public long Id { get; set; }         
        public string? Title { get; set; }
        public string? ReleaseNote { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
    }


    public class License
    {
        public long Id { get; set; }
        public long ProductId { get; set; }
        public string Key { get; set; }
        public string UserId { get; set; }
        public bool IsDistributed { get; set; }
        public bool IsActive { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }
        [ForeignKey("UserId")]
        public AppUsers User { get; set; }
    }
 


    public class Events
    {
        public long ID { get; set; }
        public string? EventTitle { get; set; }
        public long Start { get; set; }
        public long End { get; set; }
        public string ? BackgroundColor { get; set; }
    }

    public class ChatMessage
    {
        public long ID { get; set; }
        public DateTime DateTime { get; set; }
        public string? MessageText { get; set; }

        public string? SenderID { get; set; }
        public string? ReceiverID { get; set; }

        [ForeignKey("SenderID")]
        public AppUsers? Sender { get; set; }
        [ForeignKey("ReceiverID")]
        public AppUsers? Receiver { get; set; }       

    }
    public class AppUsers :IdentityUser
    { 
    }



   
}
