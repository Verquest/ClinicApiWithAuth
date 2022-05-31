using System;
using System.Collections.Generic;

namespace cw_8_22c.Models
{
    public class Patient
    {
        public int IdPatient { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthdate { get; set; }
        public virtual IEnumerable<Prescription> Prescriptions { get; set; }
    }
}
