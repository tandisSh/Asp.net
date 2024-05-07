using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace share.Models.Product
{
    public class ProductAddRequestDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public DateTime CreatedAt { get; set; }
        public string ImageFileName { get; set; }
        public int count { get; set; }
        public int CategoryId { get; set; }

    }
}
