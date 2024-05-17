using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace share.Models.Basket
{
    public class BasketAddRequestDto
    {
        /// <summary>
        /// شناسه کاربر
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// تعداد محصول
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// شناسه محصول
        /// </summary>
        public int ProductId { get; set; }

    }
}
