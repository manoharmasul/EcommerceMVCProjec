using EcommerceProject.Models;
using EcommerceProject.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceProject.Controllers
{
    public class AttendanceController : Controller
    {
        private readonly IAttendaceRepository attendanceRepo;
        public AttendanceController(IAttendaceRepository attendaceRepository)
        {
            attendanceRepo=attendaceRepository;
        }
        // GET: AttendanceController
        public async Task<ActionResult> GetAttendanceRecord(string? UserName,DateTime? FromDate,DateTime? ToDate)
        {
            var result = await attendanceRepo.GetAllAttendancesByEmpId(UserName, FromDate, ToDate);
            return View(result);
        }

        // GET: AttendanceController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AttendanceController/Create
        public ActionResult CheckInOut()
        {
            return View();
        }

        // POST: AttendanceController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CheckInOut(AttendanceModel attendance)
        {
            try
            {
                var result =await attendanceRepo.CheckInOut(attendance);
                var uId = HttpContext.Session.GetString("userId");
                var UserId = Int32.Parse(uId);
                attendance.EmpId = UserId;
                attendance.CreatedBy = UserId;
                attendance.ModifiedBy = UserId;
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AttendanceController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AttendanceController/Edit/5
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

        // GET: AttendanceController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AttendanceController/Delete/5
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
