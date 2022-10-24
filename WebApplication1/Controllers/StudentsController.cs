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
        
        [HttpPost]
        public IActionResult AddStudent(Student newStudent)
        {
            newStudent.IdStudent = new Random().Next(10000);
            _students.Add(newStudent);
            return Ok(newStudent);
        }

        [HttpPut("{idStudent}")]
        public IActionResult UpdateStudent(int idStudent, Student updatedData)
        {
            if (idStudent != updatedData.IdStudent)
            {
                return BadRequest($"Id in the URL ({idStudent}) does not match the id in the request body {updatedData.IdStudent}");
            }
            
            foreach (var student in _students)
            {
                if (student.IdStudent == updatedData.IdStudent)
                {
                    student.FirstName = updatedData.FirstName;
                    student.LastName = updatedData.LastName;
                    student.Email = updatedData.Email;
                    student.IndexNumber = updatedData.IndexNumber;
                   
                    return Ok(student);
                    
                }
            }
            
            return NotFound($"Student with the id {updatedData.IdStudent} cannot be found");
        }

        [HttpDelete("{idStudent}")]

        public IActionResult DeleteStudent(int idStudent)
        {
            var student = FindStudent(idStudent);
            if (student == null)
            {
                return NotFound($"Student with the id {idStudent} cannot be found");
            }
            _students.Remove(student);
            
            return Ok(student);
        }

        private Student FindStudent(int idStudentSearch)
        {
            foreach (var student in _students)
            {
                if (student.IdStudent == idStudentSearch)
                {
                    return student;
                }
            }

            return null;
        }
        
                      
        
    }
}