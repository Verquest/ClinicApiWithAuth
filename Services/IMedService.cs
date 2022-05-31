using cw_8_22c.Models;
using System.Threading.Tasks;

namespace cw_8_22c.Services
{
    public interface IMedService
    {
        public Task<Doctor> GetDoctorByID(int id);
        public Task SaveDatabase();
        public Task AddDoctor(Models.DTOs.Doctor doctor);
        public Task DeleteDoctor(Doctor doctor);
    }
}
