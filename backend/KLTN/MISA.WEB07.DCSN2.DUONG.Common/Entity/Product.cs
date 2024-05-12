using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static System.Net.Mime.MediaTypeNames;

namespace KLTN.Common.Entity
{
    [Table("product")]
    public class Product
    {
        [Key]
        public Guid? product_id { get; set; }
        public string? product_name { get; set; }
        public string? product_code { get; set; }
        public string? product_image_url { get; set; }
        public decimal? product_price { get; set; }
        public string? description { get; set; }
        public decimal? rental_price_day { get; set; }
        public decimal? rental_price_week { get; set; }
        public decimal? rental_price_month { get; set; }

    }
}
