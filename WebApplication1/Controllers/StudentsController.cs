using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private List<Student> _students = new List<Student>();

        public StudentsController()
        {
            _students.Add(new Student
            {
                IdStudent = 1,
                FirstName = "Dima",
                LastName = "Dmitrov",
                Address = "Abbey Road 420",
                Email = "dd@666.com",
                IndexNumber = "s69"
            });
            _students.Add(new Student
            {
                IdStudent = 2,
                FirstName = "Dima",
                LastName = "Dmitrov",
                Address = "Abbey Road 420",
                Email = "dd2@666.com",
                IndexNumber = "s420"
            });
        }

        [HttpGet]
        public IActionResult GetStudents()
        {
            return Ok(_students);
        }
        
        [HttpGet("{studentId}")]
        public IActionResult GetStudent(int studentId)
        {
            Student st = null;
            foreach (Student s in _students)
            {
                if (s.IdStudent == studentId)
                {
                    return Ok(s);
                }
            }

            return NotFound($"Student with the id {studentId}  was not found");

        }
        
    }
}