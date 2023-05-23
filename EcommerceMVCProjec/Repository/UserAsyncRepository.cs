using Dapper;
using EcommerceProject.Context;
using EcommerceProject.Models;
using EcommerceProject.Repository.Interface;

namespace EcommerceProject.Repository
{
    public class UserAsyncRepository : IUserAsyncRepository
    {
        private readonly DapperContext context;
        public UserAsyncRepository(DapperContext context)
        {
            this.context = context;
        }

        public Task<long> EditUser(UserRegistrationModel userregistration)
        {
            throw new NotImplementedException();
        }

        public async Task<List<UserRegistrationModel>> GetAllUsers()
        {
            var query = @"select u.Id,UserName,EmailId,MobileNo,Gender,Role from tblUser u 
                            inner join tblUserType ut on u.RoleId=ut.Id
                            where IsDeleted=0 and RoleId in(1,2,3)";
            using(var connection=context.CreateConnection())
            {
                var result=await connection.QueryAsync<UserRegistrationModel>(query);
                return result.ToList();
            }
        }

        public async Task<GetUserLogInModel> UserLogIn(UserLogInModel usermodel)
        {
            var query = @"select U.Id as Id,U.UserName,U.FirstName,U.LastName,	
                        U.EmailId,U.MobileNo,
                        U.Password,U.CreatedBy,	
                        U.CreatedDate,U.ModifiedBy,U.ModifiedDate,U.IsDeleted,U.DateOfBirth,
                        U.Gender,U.RoleId as RoleId,	
                        U.WalletBalance,UT.Role
                        from tblUser U inner join tblUserType UT on U.RoleId=UT.Id
                        where (UserName=@UserName or EmailId=@UserName) 
                        and Password=@Password and IsDeleted=0";
            using (var connection = context.CreateConnection())
            {
                var result = await connection.QueryAsync<GetUserLogInModel>(query, new {UserName=usermodel.UserName,Password=usermodel.Password,EmailId=usermodel.UserName});
                return result.FirstOrDefault();
            }
        }

        public async Task<UserRegistrationModel> GetUserById(long id)
        {
            var query = @"select * from tblUser Where Id=@Id";
            using(var connection=context.CreateConnection())
            {
                var result = await connection.QueryFirstOrDefaultAsync<UserRegistrationModel>(query, new { Id = id });
                
                return result;
            }
        }

        public async Task<long> SetPassword(UserPasswordModel userpassoword)
        {
            var query = @"update tblUser set Password=@Password,CreatedBy=@CreatedBy where Id=@Id";
            using(var connection=context.CreateConnection())
            {
                var result=await connection.ExecuteAsync(query, new {Password=userpassoword.Password,CreatedBy=userpassoword.Id,Id=userpassoword.Id});
                return result;
            }
        }

        public async Task<long> UserRegistration(UserRegistrationModel userregistration)
        {
            var query = "insert into tblUser(UserName,FirstName,LastName,EmailId,MobileNo,DateOfBirth,Gender,CreatedDate,IsDeleted,RoleId) values(@UserName,@FirstName,@LastName,@EmailId,@MobileNo,@DateOfBirth,@Gender,GetDate(),0,@RoleId);SELECT CAST(SCOPE_IDENTITY() as int)";

            using (var connection = context.CreateConnection())
            {
               
                userregistration.RoleId = 5; 

                var checkmobile = await connection.QueryFirstOrDefaultAsync(@"select * from tblUser Where IsDeleted=0 and MobileNo=@MobileNo", new { MobileNo = userregistration.MobileNo });
                if (checkmobile != null)
                {
                    return -1;//Mobile No Already Present
                }
                var checkemail = await connection.QueryFirstOrDefaultAsync(@"select * from tblUser where IsDeleted=0 and EmailId=@EmailId", new { EmailId = userregistration.EmailId });
                if (checkemail != null)
                {
                    return -2; //Email Id Already Present
                }
                string ss = userregistration.FirstName + userregistration.LastName;
                var checkuname = await connection.QueryFirstOrDefaultAsync
                    (
                    @"select * from tblUser where IsDeleted=0 and UserName=@UserName",
                   new {UserName=ss});
                if (checkuname == null)
                {
                    userregistration.UserName = userregistration.FirstName + userregistration.LastName;
                }
                if (checkuname != null)
                {
                    for (int i = 0; i < 10; i++)
                    {
                        Random rnd = new Random();
                        int xx = rnd.Next(999);
                        ss += xx;
                        var checkagain = await connection.QueryFirstOrDefaultAsync
                           (@"select * from tblUser where UserName=@UserName and IsDeleted=0", new { UserName = ss });
                        if (checkagain == null)
                        {
                            userregistration.UserName = ss;
                            break;
                        }

                    }

                }
                var result = await connection.QuerySingleAsync<long>(query, userregistration);
                return result;
            }

        }

        public async Task<long> AddEmployee(UserRegistrationModel userregistration)
        {
            var query = "insert into tblUser(UserName,FirstName,LastName,EmailId,MobileNo,DateOfBirth,Gender,CreatedDate,IsDeleted,RoleId) values(@UserName,@FirstName,@LastName,@EmailId,@MobileNo,@DateOfBirth,@Gender,GetDate(),0,@RoleId);SELECT CAST(SCOPE_IDENTITY() as int)";
           

            using (var connection = context.CreateConnection())
            {
                userregistration.RoleId = userregistration.RoleId;

                var checkmobile = await connection.QueryFirstOrDefaultAsync(@"select * from tblUser Where IsDeleted=0 and MobileNo=@MobileNo", new { MobileNo = userregistration.MobileNo });
                if (checkmobile != null)
                {
                    return -1;//Mobile No Already Present
                }
                var checkemail = await connection.QueryFirstOrDefaultAsync(@"select * from tblUser where IsDeleted=0 and EmailId=@EmailId", new { EmailId = userregistration.EmailId });
                if (checkemail != null)
                {
                    return -2; //Email Id Already Present
                }
                string ss = userregistration.FirstName + userregistration.LastName;
                var checkuname = await connection.QueryFirstOrDefaultAsync
                    (
                    @"select * from tblUser where IsDeleted=0 and UserName=@UserName",
                   new { UserName = ss });
                if (checkuname == null)
                {
                    userregistration.UserName = userregistration.FirstName + userregistration.LastName;
                }
                if (checkuname != null)
                {
                    for (int i = 0; i < 10; i++)
                    {
                        Random rnd = new Random();
                        int xx = rnd.Next(999);
                        ss += xx;
                        var checkagain = await connection.QueryFirstOrDefaultAsync
                           (@"select * from tblUser where UserName=@UserName and IsDeleted=0", new { UserName = ss });
                        if (checkagain == null)
                        {
                            userregistration.UserName = ss;
                            break;
                        }

                    }

                }
               
                var result = await connection.QuerySingleAsync<long>(query, userregistration);
               
                return result;

            }
        }

        public async Task<UserRegistrationModel> GetAllUsersAdd()
        {
            UserRegistrationModel urm = new UserRegistrationModel();
            var query = @"select Id as RoleId,Role as RoleType from tblUserType";
            using(var connection=context.CreateConnection())
            {
                var result = await connection.QueryAsync<UserTypes>(query);
                urm.userTypes = result.ToList();
                return urm;
            }
        }

        public async Task<long> AddWalletBalance(UserRegistrationModel addwalletbalace)
        {
            var query = @"Update tblUser set WalletBalance=@WalletBalance where Id=@Id";
            using (var connection = context.CreateConnection())
            {
                var result = await connection.ExecuteAsync(query, addwalletbalace);
                return result;
            }
        }

        public async Task<GetAllUserModelManagerOther> GetAllUserModelManagerOther()
        {
            GetAllUserModelManagerOther getallmodelmanager=new GetAllUserModelManagerOther();
            var querymanager = @"select u.Id,UserName,EmailId,MobileNo,Gender,Role,
                                isnull((select DistrictName from tblDistrict d inner join tblAssignLocation l on d.Id=l.DistrictId
                                where SalesManagerId=u.Id),'No Data Found..!') as DistrictName from tblUser u 
                                inner join tblUserType ut on u.RoleId=ut.Id
                                where u.IsDeleted=0 and u.RoleId in(3)";

            var queryallemp = @"select u.Id,UserName,EmailId,MobileNo,Gender,Role from tblUser u 
                         inner join tblUserType ut on u.RoleId=ut.Id
                         where IsDeleted=0 and RoleId in(3,4)";
            var queryalldist = @"select * from tblDistrict";
            using (var connection = context.CreateConnection())
            {
                var managerlist = await connection.QueryAsync<UserRegistrationModel>(querymanager);
                var employeelist = await connection.QueryAsync<UserRegistrationModel>(queryallemp);
                var alldistemp = await connection.QueryAsync<DistrictModel>(queryalldist);
                getallmodelmanager.managerlist=managerlist.ToList();
                getallmodelmanager.allemployeelist=employeelist.ToList();
                getallmodelmanager.alldistrictModels = alldistemp.ToList();

                return getallmodelmanager;
            }
        }

        public async Task<long> AssignLocationsToManager(AssignLocationModel assignLocation)
        {
            var queryinsert = @"insert into tblAssignLocation(SalesManagerId, DistrictId, CreatedBy, CreatedDate, IsDeleted)
                               values(@SalesManagerId, @DistrictId, @CreatedBy, @CreatedDate,0)";

            using(var connection = context.CreateConnection())
            {
                assignLocation.CreatedDate = DateTime.Now;


                if(assignLocation.DistrictId==0)
                {
                    return 1;
                }
                    var result=await connection.ExecuteAsync(queryinsert,assignLocation);

                return result;

                
            }
        }

        public async Task<List<GetNetworkSalesManagerModel>> GetNetworkBySalesManager(long? salesmagerId)
        {
            var saleid = salesmagerId;
            GetNetworkSalesManagerModel objnetmodel = new GetNetworkSalesManagerModel();
            List<GetNetworkSalesManagerModel> getnetworkmodel=new List<GetNetworkSalesManagerModel>();
            var query = @"select u.Id,u.UserName,EmailId,MobileNo,
                        u.RoleId,ut.Role,sd.Id as Sub_DivisionsId,
                        sd.Sub_DivisionsName,(select (SalesManagerId)) as SalesManagerId
                        from tblUser u 
                        inner join tblAssignDeliveryBoyTeam adbt on u.Id=adbt.DeliveryBoyId
                        inner join tblUserType ut on u.RoleId=ut.Id
                        inner join tblSub_Divisions sd on sd.Id=adbt.Sub_DivisionId
                        where SalesManagerId=@SalesManagerId";

            var querydist = @"
                 select d.Id as Id,d.DistrictName as DistrictName from tblDistrict d 
                 inner join tblAssignLocation l on d.Id=l.DistrictId where l.SalesManagerId=@SalesManagerId";

            var querysubdiv = @"select * from tblSub_Divisions";

            var querydeliveryboy = @"select Id,UserName As UserName from tblUser 
                                    where Id not in (select DeliveryBoyId 
                                    from tblAssignDeliveryBoyTeam where IsDeleted=0) 
                                    and RoleId not in (1,2,3,5)";

            using (var connection=context.CreateConnection())
            {
                var distlist = await connection.QueryAsync<DistrictModel>(querydist, new { SalesManagerId= salesmagerId });
                var subdivlist = await connection.QueryAsync<SubDivisionModel>(querysubdiv);
                var delivaryboys = await connection.QueryAsync<UserRegistrationModel>(querydeliveryboy);
               
                var result = await connection.QueryAsync<GetNetworkSalesManagerModel>(query, new { SalesManagerId = salesmagerId });
                if (result.Count() > 0)
                {
                    foreach (var item in result)
                    {
                        item.districtmodel = distlist.ToList();
                        item.subdivisionlist = subdivlist.ToList();
                        item.deliveryboylist = delivaryboys.ToList();
                        break;
                    }
                    getnetworkmodel = result.ToList();
                   return getnetworkmodel;
                }
                else
                {
                    objnetmodel.districtmodel=distlist.ToList();
                    objnetmodel.subdivisionlist=subdivlist.ToList();
                    objnetmodel.deliveryboylist =delivaryboys.ToList();
                 
                        objnetmodel.SalesManagerId = (long)saleid;
                    

                    getnetworkmodel.Add(objnetmodel);

                    return getnetworkmodel;
                }

             
            }
        }

        public async Task<long> AssigLocationToDeliveryBoy(AssiginDeliveryBoyModel assigndeliveryboymodel)
        {
            var queryassign = @"insert into tblAssignDeliveryBoyTeam
            (SalesManagerId,DeliveryBoyId,Sub_DivisionId,CreatedBy,CreatedDate,IsDeleted)
            Values(@SalesManagerId,@DeliveryBoyId,@Sub_DivisionId,@CreatedBy,@CreatedDate,@IsDeleted)";
            using(var connection=context.CreateConnection())
            {
                if(assigndeliveryboymodel.Sub_DivisionId==0)
                {
                    return 1;
                }
                assigndeliveryboymodel.CreatedDate = DateTime.Now;
                var result = await connection.ExecuteAsync(queryassign, assigndeliveryboymodel);
                return result;
            }
        }
    }
}
