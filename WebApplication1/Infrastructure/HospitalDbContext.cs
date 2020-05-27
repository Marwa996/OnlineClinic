using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApplication1.Models;
using WebApplication1.Models;

namespace WebApplication1.Infrastructure
{
    public class HospitalDbContext : IdentityDbContext
    {
        public HospitalDbContext()
            : base("DefaultConnection")
        {
            //Empty constructor.
        }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
         public DbSet<DoctorModel> Doctors { get; set; }
        public DbSet<AppointmentModel> Appointments { get; set; }
    }
}