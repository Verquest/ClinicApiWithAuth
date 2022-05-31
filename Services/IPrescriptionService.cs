using cw_8_22c.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace cw_8_22c.Services
{
    public interface IPrescriptionService
    {
        public Task<Doctor> GetDoctorFromPrescription(Prescription prescription);
        public Task<Patient> GetPatientFromPrescription(Prescription prescription);
        public Task<Prescription> GetPrescriptionId(int prescriptionId);
        public Task<IEnumerable<Medicament>> GetMedicamentsFromPrescriptionId(int prescriptionId);
        public Task SaveDatabase();
    }
}
