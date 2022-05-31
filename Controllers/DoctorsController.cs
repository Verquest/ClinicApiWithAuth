using cw_8_22c.Models.DTOs;
using cw_8_22c.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace cw_8_22c.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DoctorsController : ControllerBase
    {
        private readonly IMedService _service;
        public DoctorsController(IMedService service)
        {
            _service = service;
        }

        [HttpPut]
        public async Task<IActionResult> UpdateDoctor(int id, Doctor data)
        {
            var doctor = await _service.GetDoctorByID(id);
            if (doctor == null) return NotFound();

            doctor.FirstName = data.FirstName;
            doctor.LastName = data.LastName;
            doctor.Email = data.Email;
            await _service.SaveDatabase();

            return Ok();
        }

        [HttpGet]

        public async Task<IActionResult> GetDoctor(int id)
        {
            var doctor = await _service.GetDoctorByID(id);
            if (doctor == null)
                return NotFound("No doctor with given ID.");
            return Ok(doctor);
        }

        [HttpPost]

        public async Task<IActionResult> AddDoctor(Doctor data)
        {
            await _service.AddDoctor(data);
            await _service.SaveDatabase();
            return Ok();
        }
        [HttpDelete]

        public async Task<IActionResult> DelDoctor(int id)
        {
            var doctor = await _service.GetDoctorByID(id);
            if (doctor == null)
                return NotFound();
            await _service.DeleteDoctor(doctor);
            await _service.SaveDatabase();
            return Ok();
        }
    }
}
