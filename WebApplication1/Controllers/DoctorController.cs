using WebApplication1.Infrastructure;
using WebApplication1.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using WebApplication1.ViewModels;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
	public class DoctorController : Controller
	{
        private HospitalDbContext db = new HospitalDbContext();



        // GET: /Doctors/
        public ActionResult Index(string docDept, string searchstring)
        {
            var deptList = new List<Enums.HospitalDepartment>();
            deptList = Enum.GetValues(typeof(Enums.HospitalDepartment))
                                    .Cast<Enums.HospitalDepartment>()
                                    .ToList();
            ViewBag.docDept = new SelectList(deptList);

            var doctors = from d in db.Doctors
                          select d;

            if (!string.IsNullOrEmpty(searchstring))
            {
                doctors = doctors.Where(s => s.Name.Contains(searchstring));
            }

            if (!string.IsNullOrEmpty(docDept))
            {
                doctors = doctors.Where(x => x.Department.ToString() == docDept);
            }
            return View(doctors);
        }
        // GET: /Doctors/UpcomingAppointments/5
        [Authorize(Roles = "Admin, Doctor")]
        public ActionResult UpcomingAppointments(string id, string SearchString)
        {
            int n;
            bool isInt = int.TryParse(id, out n);
            if (!isInt)
            {


                var user = db.ApplicationUsers.First(u => u.UserName == id);
                var model = new EditUserViewModel(user);
                DoctorModel doctor = db.Doctors.FirstOrDefault(u => u.Name == user.Name);
                if (doctor == null)
                {
                    return View("Error");
                }
                if (!String.IsNullOrEmpty(SearchString))
                {
                    doctor.Appointments = doctor.Appointments.Where(s => s.ApplicationUser.Name.ToUpper().Contains(SearchString.ToUpper())).ToList();
                }
                doctor.Appointments.Sort();
                return View(doctor);
            }
            else
            {
                if (!User.IsInRole("Admin"))
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                DoctorModel doctor = db.Doctors.Find(n);
                if (doctor == null)
                {
                    return View("Error");
                }
                doctor.Appointments.Sort();
                return View(doctor);
            }
        }

        // GET: /Doctors/History
        [Authorize(Roles = "Admin, Doctor")]
        public ActionResult History(string id)
        {
            int n;
            bool isInt = int.TryParse(id, out n);
            if (!isInt)
            {
                var user = db.ApplicationUsers.First(u => u.UserName == id);
                var model = new EditUserViewModel(user);
                DoctorModel doctor = db.Doctors.FirstOrDefault(u => u.Name == user.Name);
                if (doctor == null)
                {
                    return View("Error");
                }
                doctor.Appointments.Sort();
                return View(doctor);
            }
            else
            {
                if (!User.IsInRole("Admin"))
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                DoctorModel doctor = db.Doctors.Find(n);
                if (doctor == null)
                {
                    return View("Error");
                }
                doctor.Appointments.Sort();
                return View(doctor);
            }
        }



    }
}
