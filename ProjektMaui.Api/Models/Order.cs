using System.Net.Mail;

namespace ProjektMaui.Api.Models
{
    public enum OrderStatus
    {
        Received,   
        InProgress,    
        Completed,     
        Cancelled      
    }

    public class Order
    {
        public int Id { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public string? Notes { get; set; }

        public OrderStatus Status { get; set; } = OrderStatus.Received;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<Attachment>? Attachments { get; set; }
    }

}
