namespace MVC5Course.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using ValidationAttribute;

    [MetadataType(typeof(ProductMetaData))]
    public partial class Product
    {
        public int 訂單數量
        {
            get
            {
                //return this.OrderLine.Count;
                //return this.OrderLine.Where(p => p.Qty > 400).Count();
                //return this.OrderLine.Where(p => p.Qty > 400).ToList().Count;
                return this.OrderLine.Count(p => p.Qty > 400); //最佳方式
            }
        }
    }

    public partial class ProductMetaData
    {
        [Required]
        public int ProductId { get; set; }
        [DisplayName("商品名稱")]
        [Required(ErrorMessage = "請輸入商品名稱")]
        [StringLength(200, ErrorMessage = "欄位長度不得大於 200 個字元")]
        [MinLength(3), MaxLength(200)]
        //[商品名稱必須包含Will字串]
        //[RegularExpression("(.+)-(.+)", ErrorMessage = "商品名稱格式錯誤!")]
        public string ProductName { get; set; }
        [DisplayName("商品價格")]
        [Required(ErrorMessage = "請輸入商品價格")]
        [Range(0, 9999, ErrorMessage = "請輸入正確的商品價格")]
        [DisplayFormat(DataFormatString = "{0:0}", ApplyFormatInEditMode = true)]
        public Nullable<decimal> Price { get; set; }
        [DisplayName("是否上架")]
        public Nullable<bool> Active { get; set; }
        [Required]
        [Range(0, 10000, ErrorMessage = "請輸入正確的數量")]
        [DisplayName("商品數量")]
        public Nullable<decimal> Stock { get; set; }
        [DisplayName("建檔時間")]
        public System.DateTime CreatedOn { get; set; }

        public virtual ICollection<OrderLine> OrderLine { get; set; }
    }

    public partial class Product : IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            //if (this.Price > 100 && this.Stock > 5)
            //{
            //    yield return new ValidationResult("price & stock 不合理", new string[] { "price", "stock" });
            //}

            //if (this.OrderLine.Count > 3 && this.Stock == 0)
            //{
            //    yield return new ValidationResult("stock 與訂單數量不符", new string[] { "Order", "stock" });
            //}
            yield break;
        }
    }
}
