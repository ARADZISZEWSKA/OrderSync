using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjektMaui.Api.Data;
using ProjektMaui.Api.Models;

namespace ProjektMaui.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ReportController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> SubmitReport([FromBody] Report report)
        {
            _context.Reports.Add(report);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<Report>>> GetReports()
        {
            return await _context.Reports.OrderByDescending(r => r.CreatedAt).ToListAsync();
        }
    }

}
