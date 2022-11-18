using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models2;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Students2Controller : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var dbContext = new LocalDbContext();

            var students = from s in dbContext.Student
                where s.FirstName.StartsWith("A")
                select s;
            var students2 = dbContext.Student
                .Where(s => s.FirstName.StartsWith("A"))
                .OrderBy(s => s.IdStudent);

            return Ok(students2);
        }

        [HttpPost("{id}")]
        public IActionResult Post()
        {
            var dbContext = new LocalDbContext();
            var student = new Student2
            {
                IdStudent = 420,
                FirstName = "Jan",
                LastName = "Kowalski",
            };
            dbContext.Student.Add(student);
            dbContext.SaveChanges();
            return Ok(student);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id)
        {
            var dbContext = new LocalDbContext();
            var student = dbContext.Student.FirstOrDefault(s => s.IdStudent == id);
            if (student == null)
            {
                return NotFound();
            }

            student.FirstName = "Jan";
            student.LastName = "Kowalski";
            dbContext.SaveChanges();
            return Ok(student);
        }
    }
}