using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC5Course.Models
{
    public class ProductVIewModel
    {
        public int ProductId { get; set; }
        [Required(ErrorMessage = "商品名稱必填")]
        [StringLength(10, ErrorMessage = "商品名稱不得大於10個字元")]
        public string ProductName { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:N0}")]
        public Nullable<decimal> Price { get; set; }
        [Required]
        public Nullable<decimal> Stock { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderLine> OrderLine { get; set; }
    }
}