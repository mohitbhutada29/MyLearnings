using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PatientApi.Data;
using PatientApi.Models;
using PatientShared.DTOs;

namespace PatientApi.Controllers
{
    [Route("api/[controller]")]
    public class PatientsController
    {
        private readonly ApplicationDbContext _context;

        public PatientsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<PatientDTO>>> GetPatients()
        {
            var patients = await _context.Patients
                .Select(p => new PatientDTO
                {
                    PatientId = p.PatientId,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    DateOfBirth = p.DateOfBirth
                })
                .ToListAsync();
            return new OkObjectResult(patients);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PatientDTO>> GetPatient(int id)
        {
            var patient = await _context.Patients.Where(x => x.PatientId == id)
                .Select(p => new PatientDTO
                {
                    PatientId = p.PatientId,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    DateOfBirth = p.DateOfBirth
                })
                .FirstOrDefaultAsync();
            if (patient == null)
            {
                return new NotFoundObjectResult($"Patient with ID {id} not found.");
            }

            return new OkObjectResult(patient);
        }

        [HttpPost]
        public async Task<ActionResult<PatientDTO>> CreatePatient(PatientDTO patientDto)
        {
            var patient = new Patient
            {
                FirstName = patientDto.FirstName,
                LastName = patientDto.LastName,
                DateOfBirth = patientDto.DateOfBirth.ToUniversalTime()
            };
            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();
            patientDto.PatientId = patient.PatientId;
            return new OkObjectResult(patientDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<PatientDTO>> DeletePatient(int patientId)
        {
            var patient = await _context.Patients.Where(
                x => x.PatientId == patientId).FirstOrDefaultAsync();

            if (patient != null)
            {
                _ = _context.Patients.Remove(patient);
                await _context.SaveChangesAsync();
                return new OkObjectResult($"Patient with ID {patientId} deleted successfully.");
            }
            else
            {
                return new NotFoundObjectResult($"Patient with ID {patientId} not found.");
            }
        }
    }
}