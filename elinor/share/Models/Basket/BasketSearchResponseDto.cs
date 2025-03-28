﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace share.Models.Basket
{
    public class BasketSearchResponseDto
    {
        public string ProductName { get; set; }
        public int count { get; set; }
        public int Price { get; set; }
        public DateTime CreatedAt { get; set; }
        public string ProductImageFileName { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public int ProductId { get; set; }
        public string Description { get; set; }
    }
}
