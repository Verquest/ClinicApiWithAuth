using cw_8_22c.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace cw_8_22c.Services
{
    public class MedService : IMedService
    {

        private readonly MedDbContext _context;

        public MedService(MedDbContext context)
        {
            _context = context;
        }

        public async Task<Doctor> GetDoctorByID(int id)
        {
            return await _context.Doctors.FirstOrDefaultAsync(e => e.IdDoctor == id);
        }

        public async Task SaveDatabase()
        {
            await _context.SaveChangesAsync();
        }

        public async Task AddDoctor(Models.DTOs.Doctor doctor)
        {
            Doctor doc = new Doctor
            {
                FirstName = doctor.FirstName,
                LastName = doctor.LastName,
                Email = doctor.Email
            };
            await _context.Doctors.AddAsync(doc);
        }
        public async Task DeleteDoctor(Doctor doctor)
        {
            _context.Doctors.Remove(doctor);
        }
   
    }
}
