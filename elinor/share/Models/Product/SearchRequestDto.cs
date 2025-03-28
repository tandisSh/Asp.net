﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace share.Models.Product
{
    public class SearchRequestDto
    {
        public int PageSize { get; set; }
        public int PageNo { get; set; }
        public int? count { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string? CategoryName { get; set; }
        public string? ProductName { get; set; }
        public int? MinPrice { get; set; }
        public int? Maxprice { get; set; }
        public string? SortBy { get; set; }
    }
}
