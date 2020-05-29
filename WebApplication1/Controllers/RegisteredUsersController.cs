using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Infrastructure;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class RegisteredUsersController : Controller
    {
        private HospitalDbContext db = new HospitalDbContext();


        // GET: /RegisteredUser/Delete/5
        [Authorize(Roles = "Admin, Patient")]
        public ActionResult Delete(string id = null)
        {
            var Db = new HospitalDbContext();
            var user = Db.ApplicationUsers.First(u => u.UserName == id);
            var model = new EditUserViewModel(user);
            if (user == null)
            {
                return View("Error");
            }
            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Patient")]
        public ActionResult DeleteConfirmed(string id)
        {
            var Db = new HospitalDbContext();
            var user = Db.ApplicationUsers.First(u => u.UserName == id);
            Db.Users.Remove(user);
            Db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}