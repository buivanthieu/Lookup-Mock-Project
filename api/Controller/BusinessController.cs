using api.Data;
using api.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class BusinessController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BusinessController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search(string? businessName, string? sicCode, int page = 1)
        {
            int pageSize = 15;

            var query = _context.Businesses.AsQueryable().OrderBy(b => b.BusinessName);

            if (!string.IsNullOrWhiteSpace(businessName))
            {
                query = query.Where(b => b.BusinessName.Contains(businessName)).OrderBy(b => b.BusinessName);
            }

            if (!string.IsNullOrWhiteSpace(sicCode))
            {
                query = query.Where(b => b.SicCode.Contains(sicCode)).OrderBy(b => b.BusinessName);
            }

            var totalItems = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            var businesses = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return Ok(new
            {
                items = businesses,
                totalPagesResult = totalPages
            });
        }
    }

    
}