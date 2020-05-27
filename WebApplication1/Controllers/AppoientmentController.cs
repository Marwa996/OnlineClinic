using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Infrastructure;
using WebApplication1.Models;
using Microsoft.AspNet.Identity;
namespace WebApplication1.Controllers
{
    public class AppoientmentController : Controller
    {
        private HospitalDbContext db = new HospitalDbContext();


        // GET: /Appointments/Create
        [Authorize(Roles = "Patient")]
        public ActionResult Create()
        {
            ViewBag.DoctorID = new SelectList(db.Doctors.Where(x => x.DisableNewAppointments == false), "ID", "Name");
            ViewBag.TimeBlockHelper = new SelectList(String.Empty);
            var model = new AppointmentModel
            {
                UserID = User.Identity.GetUserId()
            };
            return View(model);
        }

        // POST: /Appointments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Patient")]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AppointmentID,DoctorID,Date,TimeBlockHelper,Time")] AppointmentModel appointment)
        {
            //Add userID
            appointment.UserID = User.Identity.GetUserId();

            //Verify Time
            //if (appointment.TimeBlockHelper == "DONT")
            //    ModelState.AddModelError("", "No Appointments Available for " + appointment.Date.ToShortDateString());
            //else
            //{

            //    //Set Time
            //    //appointment.Time = DateTime.Parse(appointment.TimeBlockHelper);

            //    //CheckWorkingHours
            //    DateTime start = appointment.Date.Add(appointment.Time.TimeOfDay);
            //    DateTime end = (appointment.Date.Add(appointment.Time.TimeOfDay)).AddMinutes(double.Parse(db.Administrations.Find(1).Value));
            //    if (!(BuisnessLogic.IsInWorkingHours(start, end)))
            //        ModelState.AddModelError("", "Doctor Working Hours are from " + int.Parse(db.Administrations.Find(2).Value) + " to " + int.Parse(db.Administrations.Find(3).Value));

            //    //Check Appointment Clash
            //    string check = BuisnessLogic.ValidateNoAppoinmentClash(appointment);
            //    if (check != "")
            //        ModelState.AddModelError("", check);
            //}

            //Continue Normally
            if (ModelState.IsValid)
            {
                db.Appointments.Add(appointment);
                db.SaveChanges();
                return RedirectToAction("Details", new { Controller = "RegisteredUsers", Action = "Details" });
            }

            //Fill Neccessary ViewBags
            ViewBag.DoctorID = new SelectList(db.Doctors.Where(x => x.DisableNewAppointments == false), "ID", "Name", appointment.DoctorID);
            ViewBag.TimeBlockHelper = new SelectList(String.Empty);
            return View(appointment);
        }
    }
}