using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.InkML;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrimaryConnect.Data;
using PrimaryConnect.Dto;
using PrimaryConnect.Models;
using System.Globalization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PrimaryConnect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AbsenceController : ControllerBase
    {

        public AbsenceController(AppDbContext appDbContext)
        {
            _PrimaryConnect_Db = appDbContext;
        }
        private AppDbContext _PrimaryConnect_Db;


        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllById(int id)
        {
            return Ok(await _PrimaryConnect_Db.absences.Where(m => m.StudentId == id).ToListAsync());
        }




        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddAbsence(Absence_Dto Absence)
        {
            Absence _Absence = Absence.ToAbsence();
            _PrimaryConnect_Db.
                absences.Add(_Absence);
            try
            {
                await _PrimaryConnect_Db.SaveChangesAsync();
            }
            catch
            { return BadRequest(); }

            return Ok(Absence);
        }


        // DELETE: api/admins/{id}
        [HttpDelete("{id}", Name = "DeleteAbsence")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAbsence(int id)
        {
            Absence? _Absence = await _PrimaryConnect_Db.absences.FindAsync(id);
            if (_Absence == null)
            {
                return NotFound("Admin not found.");
            }

            _PrimaryConnect_Db.absences.Remove(_Absence);
            await _PrimaryConnect_Db.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("{id}", Name = "UpdateAbsence")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateAbsence(int id,string status)
        {



            var existingAbsence = await _PrimaryConnect_Db.absences.FindAsync(id);
            if (existingAbsence == null)
            {
                return NotFound("Absence not found.");
            }



            existingAbsence.title = status;





            try
            {
                await _PrimaryConnect_Db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to update: {ex.Message}");
            }

            return NoContent();
        }

        // GET: api/Absences/student-absences
        [HttpGet("student-absences")]
        public async Task<ActionResult<List<Absence_Dto>>> GetStudentAbsences(
            [FromQuery] int studentId,
            [FromQuery] string startDate,
            [FromQuery] string endDate)
        {
            try
            {
                // Parse dates (assuming format yyyy-MM-dd)
                DateTime start = DateTime.ParseExact(startDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                DateTime end = DateTime.ParseExact(endDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);

                var absences = await _PrimaryConnect_Db.absences
                    .Where(a => a.StudentId == studentId &&
                               a.Date >= start &&
                               a.Date <= end)
                    .OrderBy(a => a.Date)
                    .Select(a => new Absence_Dto
                    {
                        Id = a.Id,
                        Date = a.Date,
                        Subject = a.Subject,
                        title = a.title,
                        // Map other properties as needed
                    })
                    .ToListAsync();

                return Ok(absences);
            }
            catch (FormatException)
            {
                return BadRequest("Invalid date format. Please use yyyy-MM-dd format.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


    }
}
