using System.Collections.Generic;

namespace cw_8_22c.Models
{
    public class Doctor
    {
        public int IdDoctor { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public virtual IEnumerable<Prescription> Prescriptions { get; set; }
    }
}
