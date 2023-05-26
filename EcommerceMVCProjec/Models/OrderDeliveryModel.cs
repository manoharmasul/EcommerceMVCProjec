namespace EcommerceProject.Models
{
    public class OrderDeliveryModel:BaseModel
    {
        //DeliveryBoyId,OrderId,OrderStatusId,CreatedBy,CreatedDate,IsDeleted
        public long DeliveryBoyId { get; set; }    
        public long OrderId { get; set; }
        public long OrderStatusId { get; set; }
        public bool IsAccepted { get; set; }
        public long Sub_DivisionId { get; set; }
    }
}
