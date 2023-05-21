namespace EcommerceProject.Models
{
    public class OrderDeliveryModel
    {
        public long DeliveryBoy { get; set; }
        public long OrderId { get; set; }
        public long DeliveryBoyId { get; set; }
        public long AssignByOrAcceptedBy { get; set; }
    }
}
