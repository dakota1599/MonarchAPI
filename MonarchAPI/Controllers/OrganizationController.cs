using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Cors;
using MonarchAPI.Models;
using MonarchAPI.Data;
using System.Security.Cryptography;
using System.Text;

namespace MonarchAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationController : Controller
    {
        protected MonarchContext _context;

        public OrganizationController(MonarchContext context) {
            this._context = context;
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> CreateOrganization([FromBody] Organization org) {

            var organ = await _context.Organizations.Where(o => o.Name == org.Name).SingleOrDefaultAsync();

            if (organ == null) {
                _context.Organizations.Add(org);
                await _context.SaveChangesAsync();
                return Json(true);
            }

            return Json(false);
        }
    }
}
