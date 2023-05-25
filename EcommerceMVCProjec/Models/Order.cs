
namespace EcommerceProject.Models
{
    public class Order : BaseModel
    {
        //Id,CustomerId,ProductId,TotalAmmount,OrderStatus,BillingAddress,ShippingAddress,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate,IsDeleted
        public long Id { get; set; }
        public long CustomerId { get; set; }
        public long ProductId { get; set; }
        public double TotalAmmount { get; set; }
        public long OrderStatusId { get; set; }
        public string BillingAddress { get; set; }
        public string ShippingAddress { get; set; }
        public long Quantity { get; set; }
        public string MobileNo { get; set; }


    }
    public class GetOrder : BaseModel
    {
        //Id,CustomerId,ProductId,TotalAmmount,OrderStatus,BillingAddress,ShippingAddress,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate,IsDeleted

        public long SrNo { get; set; }
        public long Id { get; set; }
        public string ImageUrl { get; set; }
        public string ProductName { get; set; }
        public double TotalAmmount { get; set; }
        public string OrderStatus { get; set; }
        public string BillingAddress { get; set; }
        public string ShippingAddress { get; set; }
        public long Quantity { get; set; }
        public string MobileNo { get; set; }
        public DateTime OrderDate { get; set; }

    }
    public class UpdateOrderStatus
    {
        //Id,CustomerId,ProductId,TotalAmmount,OrderStatus,BillingAddress,ShippingAddress,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate,IsDeleted
        public long Id { get; set; }
        public long OrderStatusId { get; set; }
        public long ModifiedBy { get; set; }

    }
    public class GetAllOrdersForAdmin
    {
        //Id,CustomerId,ProductId,TotalAmmount,OrderStatus,BillingAddress,ShippingAddress,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate,IsDeleted
        //ProductId,ProductName,OrderCounts,OrderStatus,OrderQuantity,Available_Products
        //SrNo,ProductId,ProductName,OrderCounts,OrderStatus,OrderQuantity,Available_Products
        public long SrNo { get; set; }
        public long ProductId { get; set; }
        public string ProductName { get; set; }
        public long OrderCounts { get; set; }
        public string OrderStatus { get; set; }
        public long OrderQuantity { get; set; }
        public long Available_Products { get; set; }
        public long Sub_DivisionId { get; set; }
        public string Sub_DivisionsName { get; set; }
        public List<DeliveryBoyListModel> DeliveryBoys { get; set; }


    }
    public class UpdateOrderBillingAddress
    {
        //CustomerId,ProductId,TotalAmmount,OrderStatus,BillingAddress,ModifiedBy
        // ProductId,ProductName,OrderCounts,OrderStatus,OrderQuantity,Available_Products


        public long ProductId { get; set; }
        public string ProductName { get; set; }
        public long OrderCounts { get; set; }
        public string OrderStatus { get; set; }
        public string BillingAddress { get; set; }
        public long OrderStatusId { get; set; }
        public long OrderQuantity { get; set; }
        public long Available_Products { get; set; }
        public long ModifiedBy { get; set; }
        public long? DeliveryBoyId { get; set; }
        public long? CheckId { get; set; }
        public long? Sub_DivisionId { get; set; }
        public DateTime ModifiedDate { get; set; }
        public List<DeliveryBoyListModel> DeliveryBoyList { get; set; }

    }
    public class AddOrderItems : BaseModel
    {
        //Id,CustomerId,ProductId,TotalAmmount,OrderStatus,BillingAddress,ShippingAddress,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate,IsDeleted
        public long Id { get; set; }
        public long CustomerId { get; set; }
        public long ProductId { get; set; }
        public double TotalAmmount { get; set; }
        public long OrderStatusId { get; set; }
        public string BillingAddress { get; set; }
        public string ShippingAddress { get; set; }
        public long Quantity { get; set; }
        public long DistrictId { get; set; }
        public string DistrictName { get; set; }
        public long Sub_DivisionId { get; set; }
        public string MobileNo { get; set; }
        public long resultreturn { get; set; }
        public List<DistrictModel> districtModels { get; set; }
        public List<SubDivisionModel> subDivisionModel { get; set; }

    }
    public class DistrictModel
    {
        public long Id { get; set; }
        public string DistrictName { get; set; }
    }
    public class SubDivisionModel
    {
        public long Id { get; set; }
        public string Sub_DivisionsName { get; set; }
        public long DistrictId { get; set; }
    }
}
