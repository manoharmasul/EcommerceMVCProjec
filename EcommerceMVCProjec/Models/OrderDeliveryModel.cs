namespace EcommerceProject.Models
{
    public class OrderDeliveryModel:BaseModel
    {
        //IsAccepted=@IsAccepted,ModifiedBy=@ModifiedBy,ModifiedDate=@ModifiedDate,AcceptedDate=@AcceptedDate where OrderId

        //DeliveryBoyId,OrderId,OrderStatusId,CreatedBy,CreatedDate,IsDeleted
        public long DeliveryBoyId { get; set; }    
        public long OrderId { get; set; }
        public long OrderStatusId { get; set; }
        public bool IsAccepted { get; set; }
        public DateTime AcceptedDate { get; set; }
        public long Sub_DivisionId { get; set; }
    }
    public class GetOrderDelliveryBoy : BaseModel
    {
        //IsAccepted=@IsAccepted,ModifiedBy=@ModifiedBy,ModifiedDate=@ModifiedDate,AcceptedDate=@AcceptedDate where OrderId

        //DeliveryBoyId,OrderId,OrderStatusId,CreatedBy,CreatedDate,IsDeleted
        public long OrderId { get; set; }
        public string ProductName { get; set; }
        public string DistrictName { get; set; }
        public string Sub_DivisionsName { get; set; }
        public string ShippingAddress { get; set; }
        public DateTime AcceptedDate { get; set; }
    }
}
