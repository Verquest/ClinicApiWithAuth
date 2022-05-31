using cw_8_22c.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cw_8_22c.Services
{
    public class PrescriptionService : IPrescriptionService
    {

        private readonly MedDbContext _context;

        public PrescriptionService(MedDbContext context)
        {
            _context = context;
        }

        public async Task<Doctor> GetDoctorFromPrescription(Prescription prescription)
        {
            var doc = await _context.Doctors.FirstOrDefaultAsync(d => d.IdDoctor == prescription.IdDoctor);
            return doc;
        }

        public async Task<Patient> GetPatientFromPrescription(Prescription prescription)
        {
            var patient = await _context.Patients.FirstOrDefaultAsync(p => p.IdPatient == prescription.IdPatient);
            return patient;
        }

        public async Task<Prescription> GetPrescriptionId(int prescriptionId)
        {
            var prescription = await _context.Prescriptions.FirstOrDefaultAsync(p => p.IdPrescription == prescriptionId);
            return prescription;
        }

        public async Task<IEnumerable<Medicament>> GetMedicamentsFromPrescriptionId(int prescriptionId)
        {
            var medicament_Ids = _context.Prescription_Medicaments.Where(pm => pm.IdPrescription == prescriptionId).Select(pm => pm.IdMedicament);
            List<Medicament> meds = _context.Medicaments.Where(m => medicament_Ids.Contains(m.IdMedicament)).ToList();
            return meds;
        }
        public async Task SaveDatabase()
        {
            await _context.SaveChangesAsync();
        }
    }
}
