namespace EShopDK.Models
{
    public class BaseModel
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public bool Deleted { get; set; }

        public DateTime? created_at { get; set; }

        public Guid? created_by { get; set; }

        public DateTime? updated_at { get; set; }

        public Guid? updated_by { get; set; }

    }
}
