using DoctorPatient.Infrastructure;
using DoctorPatient.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;


namespace WebApplication1.Controllers
{
	public class DoctorController : Controller
	{
        private HospitalDbContext db = new HospitalDbContext()

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


    }
}
