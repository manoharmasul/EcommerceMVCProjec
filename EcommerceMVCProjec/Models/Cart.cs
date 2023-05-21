namespace EcommerceProject.Models
{
    public class Cart:BaseModel
    {
       // Id,ProductId,UserId,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate,IsDeleted
        public long Id { get; set; }
        public long ProductId { get; set; }
        public long UserId { get; set; }
    }
  
}
