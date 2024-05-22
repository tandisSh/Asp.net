using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace share.Models.Basket
{
    public class BasketReportByUserResponseDto
    {
        public string productCategoryName;

        public string ProductName { get; set; }
        public int ProductId { get; set; }
      /*  public int? TotalSum { get; set; }*/
        public string UserId { get; set; }
        public int Count { get; set; }
    }
}
