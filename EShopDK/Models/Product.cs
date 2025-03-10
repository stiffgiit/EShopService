using System.ComponentModel;

namespace EShopDK.Models
{
    public class Product : BaseModel
    {

        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Ean {  get; set; }

        public decimal Price { get; set; }

        public int Stock { get; set; }

        public string? Sku { get; set; }

        public EShopDK.Models.Category? Category { get; set; }

        public bool Deleted { get; set; }

        public DateTime? created_at { get; set; }

        public Guid? created_by { get; set; }

        public DateTime? updated_at { get; set; }

        public Guid? updated_by { get; set; }

    }
}
