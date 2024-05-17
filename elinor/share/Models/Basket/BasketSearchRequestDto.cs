using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace share.Models.Basket
{
    public class BasketSearchRequestDto
    {
        public int PageSize { get; set; }
        public int PageNo { get; set; }
        public int? Count { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
      /*  public string? CategoryName { get; set; }*/
        public string? ProductName { get; set; }
        public string? UserName { get; set; }
    }
}
