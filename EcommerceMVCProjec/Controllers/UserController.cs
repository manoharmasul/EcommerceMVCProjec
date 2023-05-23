using EcommerceProject.Models;
using EcommerceProject.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

namespace EcommerceProject.Controllers
{
    public class UserController : Controller
    {
        AttendanceModel at = new AttendanceModel();
        private readonly IAttendaceRepository attendanceRepo;
        private readonly IUserAsyncRepository userasynrepo;
        public UserController(IUserAsyncRepository userasynrepo, IAttendaceRepository attendanceRepo)
        {
            this.userasynrepo = userasynrepo;
            this.attendanceRepo = attendanceRepo;
        }

        public async Task<ActionResult> GetNetworkBySalesManager(long? ret,long? id)
        {
            var myresul = 0;
            if (id == 0 || id == null)
            {
                myresul = (int)ret;
            }
            else
            {
                myresul = (int)id;
            }

            var result=await userasynrepo.GetNetworkBySalesManager(myresul);

            return View(result);
        }
        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AssigLocationToDeliveryBoy(AssiginDeliveryBoyModel assignLocation)
        {
            try
            {
                var Id = HttpContext.Session.GetString("userId");
                var userId = Int32.Parse(Id);
                assignLocation.createdBy = userId;
                var assign = await userasynrepo.AssigLocationToDeliveryBoy(assignLocation);
                var ret = assignLocation.SalesManagerId;

                return RedirectToAction(nameof(GetNetworkBySalesManager), new { ret });
            }
            catch
            {
                return View();
            }
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AssignLocationToManager(AssignLocationModel assignLocation)
        {
            try
            {
                var Id = HttpContext.Session.GetString("userId");
                var userId = Int32.Parse(Id);
                assignLocation.createdBy = userId;
                var assign = await userasynrepo.AssignLocationsToManager(assignLocation);

             
                return RedirectToAction(nameof(GetAllUserModelManagerOther));
            }
            catch
            {
                return View();
            }
        }
        public async Task<ActionResult> GetAllUserModelManagerOther()
        {
            var result = await userasynrepo.GetAllUserModelManagerOther();
            return View(result);
        }
        // GET: UserController
        public async Task<ActionResult> Index()
        {
            var result = await userasynrepo.GetAllUsers();
            return View(result);
        }

        // GET: UserController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UserController/Create
        public ActionResult UserRegistration()
        {
            return View();
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UserRegistration(UserRegistrationModel userregistration)
        {
            try
            {
                var result=await userasynrepo.UserRegistration(userregistration);

                return RedirectToAction(nameof(SetPassword), new { result });
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> AddEmployee()
        {
            var emp = await userasynrepo.GetAllUsersAdd();
            return View(emp);
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddEmployee(UserRegistrationModel userregistration)
        {
            try
            {
                var result = await userasynrepo.AddEmployee(userregistration);

                return RedirectToAction(nameof(SetPassword), new { result });
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> UpdateWalletBalance()
        {
            var Id = HttpContext.Session.GetString("userId");               
            var emp = await userasynrepo.GetUserById(Int32.Parse(Id));
            return View(emp);
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UpdateWalletBalance(UserRegistrationModel userregistration)
        {
            try
            {
                var result = await userasynrepo.AddWalletBalance(userregistration);

                return RedirectToAction(nameof(SetPassword), new { result });
            }
            catch
            {
                return View();
            }
        }

        public ActionResult SetPassword(long result)
        {
            ViewBag.id = result;
            return View();
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SetPassword(UserPasswordModel userPassword)
        {
            try

            {

                var resultt = await userasynrepo.SetPassword(userPassword);
                var uId = HttpContext.Session.GetString("userId");
                if(uId==null)
                {
                    return RedirectToAction(nameof(Login));
                }

                return RedirectToAction(nameof(GetAllUserModelManagerOther));
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(UserLogInModel userloginmodel)
        {
            var user = await userasynrepo.UserLogIn(userloginmodel);
            if (user != null)
            {
                if (user.Password == userloginmodel.Password)
                {
                    HttpContext.Session.SetString("userName", user.UserName);
                    HttpContext.Session.SetString("userId", user.Id.ToString());
                    HttpContext.Session.SetString("userRole", user.Role);
                    ViewBag.user = user.UserName;

                    if (user.Role == "Admin")
                    {

                        at.EmpId = user.Id;
                        attendanceRepo.CheckInOut(at);

                        return RedirectToAction("OrderContSales", "Product");
                    }
                    else
                    {
                        at.EmpId = user.Id;
                        attendanceRepo.CheckInOut(at);

                        return RedirectToAction("Index", "Product");
                    }
                }
                else
                    ViewBag.num = 1;
                return View();
            }
            else
            {
                ViewBag.num = 1;
                return View();
            }
        }

        public IActionResult Logout()
        {
            var uId = HttpContext.Session.GetString("userId");
            at.EmpId = Int32.Parse(uId);
            attendanceRepo.CheckInOut(at);
            HttpContext.Session.Clear();    
           
            return RedirectToAction("LogIn");
        }

        // GET: UserController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UserController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
