using Dapper;
using EcommerceProject.Context;
using EcommerceProject.Models;
using EcommerceProject.Repository.Interface;

namespace EcommerceProject.Repository
{
    public class ProductAsyncRepository:IProductAsyncRepository
    {
        private readonly DapperContext context;
        public ProductAsyncRepository(DapperContext context)
        {
            this.context = context;
        }

        public async Task<long> AddNewProduct(ProductAdd product)
        {
            //Id,ProductName,ImageUrl,Price,Description,Specification,TypeId,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate,IsDeleted

            var query = @"insert into tblProducts (ProductName,ImageUrl,SellingPrice,BuyPrice,Description,Specification,TypeId,CreatedDate,IsDeleted) values(@ProductName,@ImageUrl,@SellingPrice,@BuyPrice,@Description,@Specification,@TypeId,GetDate(),0)";
            using(var connection=context.CreateConnection())
            {
                var result=await connection.ExecuteAsync(query,product);

                return result;
            }
        }

        public async Task<GetProducts> GetAllProducts()
        {
            GetProducts dashData=new GetProducts();

            List<ProductAdd> prodlist=new List<ProductAdd>();    

            var queryprod= @"select Id,ProductName,ImageUrl,SellingPrice,Description,Specification,TypeId  from tblProducts where IsDeleted=0";

            var queryprodtype = "select * from tblProductType ";

            var OrderCount = "Select Count(*) from tblOrder where IsDeleted=0";


            using (var connection=context.CreateConnection())
            {
                var products=await connection.QueryAsync<ProductAdd>(queryprod);

                prodlist=products.ToList();

                var prodtype = (await connection.QueryAsync<GetProductTypes>(queryprodtype)).ToList();

                var ordcount =await connection.QuerySingleOrDefaultAsync<long>(OrderCount);             

                dashData.Productlist= products.ToList();
                dashData.OrderCount =ordcount;
                dashData.ProdTypeList = prodtype;

                return dashData;

            }
        }

        public async Task<ProductAdd> GetProductById(long id)
        {
            var query = @"select * from tblProducts where IsDeleted=0 and Id=@Id";
            using (var connection = context.CreateConnection())
            {
                 var result=await connection.QueryFirstOrDefaultAsync<ProductAdd>(query, new {Id=id});
                return result;
            }
        }

        public async Task<List<ProductAdd>> GetProductBySearchText(string searchtext)
        {
            var query = @"select * from tblProducts where (ProductName like '%'+@SearchText+'%' or @SearchText='')";
            
            using(var connection = context.CreateConnection())
            {
                var prod=await connection.QueryAsync<ProductAdd>(query, new { @SearchText=searchtext});

                return prod.ToList();

            }  

        }

        public async Task<List<ProductAdd>> GetProductByType(string category)
        {
            var query = @"select * from tblProducts p inner join tblProductType pt on p.TypeId=pt.Id where pt.productType=@productType";

            using(var connection=context.CreateConnection())
            {
                var result=await connection.QueryAsync<ProductAdd>(query, new { productType= category });
                return result.ToList();
            }
        }

        public async Task<int> InsertDemo(Demo demo)
        {
            var result = 0;
            var query = @"insert into tblDemo(Demo,SellingPrice)values(@Demo,@SellingPrice) ";
           using(var connections=context.CreateConnection())
            {
              
               
                    result = await connections.ExecuteAsync(query, demo);   
                
                return result;
            }
        }

        public async Task<GetProductsOrderCount> ProductsOrderCount()
        {
            GetProductsOrderCount getProductsOrderCount = new GetProductsOrderCount();
            var queryprodcont = @"select pt.ProductType,COUNT(p.TypeId) as Available from tblProductType pt 
                        inner join tblProducts p on pt.Id=p.TypeId group by p.typeid,pt.productType";

            var queryOrderCount = @"Select Count(*) from tblOrder where IsDeleted=0";

            var queryOrderPending = @"select Count(*) from tblOrder where IsDeleted=0 and OrderStatusId=1";

            var querytotatSell = @"select IsNull(Sum(TotalAmmount),0) from tblOrder where IsDeleted=0";

            var querytotatBuyprice = @"select IsNUll(Sum(p.BuyPrice),0) from tblOrder o 
                                    inner join tblProducts p on o.ProductId=p.Id
                                    where o.IsDeleted=0";

            var querytotalSellprice = @"select IsNull(Sum(p.SellingPrice),0) from tblOrder o 
                                    inner join tblProducts p on o.ProductId=p.Id
                                    where o.IsDeleted=0";

            var queryToDaysOrders = @"select Count(*) from tblOrder where IsDeleted=0 And CONVERT(DATE,CreatedDate) =  CONVERT(DATE, GETDATE())";
            var querytopselingproduct = @"select p.ProductName,Count(o.Id) as NoOfSales from tblOrder o 
                                     inner join tblProducts p on o.ProductId=p.Id group by p.ProductName order by NoOfSales desc";

            using (var connection=context.CreateConnection())
            {

                var prodcount = await connection.QueryAsync<GetProductsavailability>(queryprodcont);
                var orderscount = await connection.QuerySingleOrDefaultAsync<long>(queryOrderCount);
                var orderpending = await connection.QuerySingleOrDefaultAsync<long>(queryOrderPending);
                var totasell=await connection.QuerySingleOrDefaultAsync<long>(querytotatSell);
                var totalBuy=await connection.QuerySingleOrDefaultAsync<long>(querytotatBuyprice);
                var totalSell=await connection.QuerySingleOrDefaultAsync<long>(querytotalSellprice);
                var todaysorders = await connection.QuerySingleOrDefaultAsync<long>(queryToDaysOrders);
                var topselprod = await connection.QueryAsync<GetTopSellingItems>(querytopselingproduct);

                getProductsOrderCount.proavailability = prodcount.ToList();
                getProductsOrderCount.OrderCount = orderscount;
                getProductsOrderCount.OrderPendings = orderpending;
                getProductsOrderCount.TotalSales = totasell;
                getProductsOrderCount.ToDaysOrder=todaysorders;
                getProductsOrderCount.TotalProfite=totasell-totalBuy;
                getProductsOrderCount.gettopselingitems=topselprod.ToList();

                return getProductsOrderCount;   
            }

        }

        public Task<long> UpdateProduct(ProductAdd product)
        {
            throw new NotImplementedException();
        }
    }
}
