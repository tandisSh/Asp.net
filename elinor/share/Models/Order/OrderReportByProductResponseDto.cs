using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace share.Models.Order
{
    public class OrderReportByProductResponseDto
    {
        public string ProductName { get; set; }
        public string ProductCategoryName { get; set; }
        public int ProductId { get; set; }
        public int? TotalSum { get; set; }
        public int Count { get; set; }
    }
}
