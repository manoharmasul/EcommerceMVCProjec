namespace EcommerceProject.Models
{
    public class Product:BaseModel
    {
        //Id,ProductName,ImageUrl,Price,Description,Specification,TypeId,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate,IsDeleted
        public long Id { get; set; }
        public string ProductName { get; set; }
        public string ImageUrl { get; set; }
        public double SellingPrice { get; set; }
        public string Description { get; set; }
        public string Specification { get; set; }
        public long TypeId { get; set; }
       
    }
    public class ProductAdd
    {
        //Id,ProductName,ImageUrl,Price,Description,Specification,TypeId,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate,IsDeleted
        public long Id { get; set; }
        public string ProductName { get; set; }
        public string ImageUrl { get; set; }
        public double SellingPrice { get; set; }
        public double BuyPrice { get; set; }
        public string Description { get; set; }
        public string Specification { get; set; }
        public long TypeId { get; set; }
        public long OrderCount { get; set; }
       
    }
    public class GetProductTypes
    {
        public long Id { get; set; }
        public string ProductType { get; set; }
    }
    public class GetProducts
    {
        //Id,ProductName,ImageUrl,Price,Description,Specification,TypeId,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate,IsDeleted
        public List<ProductAdd> Productlist { get; set; }
        public long OrderCount { get; set; }
        public List<GetProductTypes> ProdTypeList { get; set; }
    }
    public class GetProductsOrderCount
    {
        //Id,ProductName,ImageUrl,Price,Description,Specification,TypeId,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate,IsDeleted   
        public long OrderCount { get; set; }   
        public long OrderPendings { get; set; }
        public long ToDaysOrder { get; set; }
        public long TotalSales { get; set; }     
        public long TotalProfite { get; set; }     
       public List<GetProductsavailability> proavailability { get; set; }    
       public List<GetTopSellingItems> gettopselingitems { get; set; }    

    }
    public class GetProductsavailability
    {
        //Id,ProductName,ImageUrl,Price,Description,Specification,TypeId,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate,IsDeleted   
       public string ProductType { get; set; }  
       public long Available { get; set; }   


    }
    public class GetTopSellingItems
    {
        //Id,ProductName,ImageUrl,Price,Description,Specification,TypeId,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate,IsDeleted   
        public string ProductName { get; set; }
        public long NoOfSales { get; set; }


    }
}
