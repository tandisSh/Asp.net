﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace share.Models.Order
{
    public class OrderReportByProductRequestDto
    {
        public int PageSize { get; set; }
        public int PageNo { get; set; }
        public int ProductId { get; set; }
      

    }
}
