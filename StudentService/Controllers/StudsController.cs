using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentService.Data;
using StudentService.Models;
using StudentService.Services;

namespace StudentService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("MyCorsPolicy")]
    public class StudsController : ControllerBase
    {
        private readonly IStudService _service;
      
        public StudsController(IStudService service)
        {
            _service = service;
        }

        // GET: api/Studs
        [HttpGet]
        public IActionResult GetStud()
        {
            return  Ok(_service.GetAllStudents());
        }
       
        // GET: api/Studs/5
        [HttpGet("{id}")]
          
        public IActionResult GetStud(int id)
        {
            return Ok(_service.GetStudentById(id));
        }

        // PUT: api/Studs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult PutStud(int id, Stud stud)
        {
            return Ok(_service.UpdateStud(id, stud));
        }

        // POST: api/Studs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult PostStud(Stud stud)
        {
            return StatusCode(201, _service.AddStud(stud));
        }

        // DELETE: api/Studs/5
        [HttpDelete("{id}")]
        public IActionResult DeleteStud(int id)
        {
         return Ok(_service.DeleteStud(id));   
        }

      
    }
}












/* [HttpGet("GetMarks/{Marks}")]
        public async Task<ActionResult<IEnumerable<Stud>>> GetStudMarks(int Marks)
        {
            return await _context.Stud.Where(s=>s.studTotalMarks>Marks ).ToListAsync();
        }
        [HttpGet("Getname")]
        public async Task<ActionResult<IEnumerable<Stud>>> GetStudByName()
        {
            return await _context.Stud.Where(s => s.studName.StartsWith("A")).ToListAsync();
        }*/