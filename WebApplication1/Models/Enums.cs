using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Enums
    {
        public enum Sex
        {
            Male, Female
        }

        public enum HospitalDepartment
        {
            Surgery,
            Neurology,
            Anesthesia,
            Gynecology,
            Pediatrics,
            Dentistry,
            Emergency,
            Nursing,
            Pharmacy
        }

        public enum DoctorDegree
        {
            MBBS, FCPS, Diploma, FRCPS
        }
    }
}