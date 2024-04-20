using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElinorStoreServer.Data.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public DateTime CreatedAt { get; set; }
        public string ImageFileName { get; set;}
        public int count { get; set; }
        [ForeignKey("category")]
        public int CategoryId { get; set; }

        public virtual Category Category { get; set;}
    }
}
