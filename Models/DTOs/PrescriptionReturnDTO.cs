using System.Collections.Generic;

namespace cw_8_22c.Models.DTOs
{
    public class PrescriptionReturnDTO
    {
        public Patient patient { get; set; }
        public Models.Doctor doctor { get; set; }
        public IEnumerable<Medicament> meds { get; set; }
    }
}
