using cw_8_22c.Models.DTOs;
using cw_8_22c.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace cw_8_22c.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PrescriptionsController : ControllerBase
    {
        private readonly IPrescriptionService _prescriptionService;

        public PrescriptionsController(IPrescriptionService prescriptionService)
        {
            _prescriptionService = prescriptionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPrescription(int id)
        {
            var prescription = await _prescriptionService.GetPrescriptionId(id);

            if (prescription == null) return NotFound("prescription does not exist.");

            var doctor = await _prescriptionService.GetDoctorFromPrescription(prescription);
            var patient = await _prescriptionService.GetPatientFromPrescription(prescription);
            var meds = await _prescriptionService.GetMedicamentsFromPrescriptionId(id);

            var returnDTO = new PrescriptionReturnDTO();
            returnDTO.patient = patient;
            returnDTO.doctor = doctor;
            returnDTO.meds = meds;
            await _prescriptionService.SaveDatabase();

            return Ok(returnDTO);
        }
    }
}
