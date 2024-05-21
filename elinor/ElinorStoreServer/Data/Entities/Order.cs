using ElinorStoreServer.Data.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElinorStoreServer.Data.Entities
{
    public class Order
    {
    

        public int Id { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
        public int Price { get; set; }
        public int Count { get; set; }
        public DateTime CreatedAt { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; }

        public virtual AppUser User { get; set; } = default!;
        public virtual Product Product { get; set; } = default!;
    }
}