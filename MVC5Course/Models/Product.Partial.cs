namespace MVC5Course.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    [MetadataType(typeof(ProductMetaData))]
    public partial class Product
    {
    }
    
    public partial class ProductMetaData
    {
        [Required]
        public int ProductId { get; set; }
        [DisplayName("商品名稱")]
        [Required(ErrorMessage = "請輸入商品名稱")]
        [StringLength(80, ErrorMessage="欄位長度不得大於 80 個字元")]
        [MinLength(3), MaxLength(30)]
        //[RegularExpression("(.+)-(.+)", ErrorMessage = "商品名稱格式錯誤!")]
        public string ProductName { get; set; }
        [DisplayName("商品價格")]
        [Required(ErrorMessage = "請輸入商品價格")]
        [Range(0, 9999, ErrorMessage = "請輸入正確的商品價格")]
        [DisplayFormat(DataFormatString = "{0:0}",ApplyFormatInEditMode =true)]
        public Nullable<decimal> Price { get; set; }
        [DisplayName("是否上架")]
        public Nullable<bool> Active { get; set; }
        [Required]
        [Range(0, 100, ErrorMessage = "請輸入正確的數量")]
        [DisplayName("商品數量")]
        public Nullable<decimal> Stock { get; set; }
    
        public virtual ICollection<OrderLine> OrderLine { get; set; }
    }
}
