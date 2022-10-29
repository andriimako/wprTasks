using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
        List<Student> _students = new List<Student>();

        public StudentsController()
        {
        }

        [HttpGet]
        public IActionResult GetStudents(string orderByColumn)
        {
            SqlConnection con =
                new SqlConnection(
                    "Data Source=localhost,1433;Initial Catalog=master;User ID=SA;Password=<YourStrong@Passw0rd>");
            SqlCommand com = new SqlCommand();

            com.Connection = con;
            if (String.Equals(orderByColumn, "name", StringComparison.OrdinalIgnoreCase))
            {
                com.CommandText = "select * from Student ORDER BY FirstName";
            }
            else if (String.Equals(orderByColumn, "surname", StringComparison.OrdinalIgnoreCase))
            {
                com.CommandText = "select * from Student ORDER BY LastName";
            }
            else if (String.Equals(orderByColumn, "id", StringComparison.OrdinalIgnoreCase))

            {
                com.CommandText = "select * from Student";
            }
            else return NotFound("not expected value");


            con.Open();
            SqlDataReader dr = com.ExecuteReader();

            List<Student> names = new List<Student>();
            while (dr.Read())
            {
                var st = new Student();
                st.IdStudent = (int)dr["IdStudent"];
                st.FirstName = dr["FirstName"].ToString();
                st.LastName = dr["LastName"].ToString();
                names.Add(st);
            }

            return Ok(names);
        }


        [HttpGet("{studentId}")]
        public IActionResult GetStudent(int studentId)
        {
            var student = FindStudent(studentId);
            if (student == null)
            {
                return NotFound($"Student with the id {studentId} cannot be found");
            }

            return Ok(student);

            // Student st = null;
            // foreach (Student s in _students)
            // {
            //     if (s.IdStudent == studentId)
            //     {
            //         return Ok(s);
            //     }
            // }
            //
            // return NotFound($"Student with the id {studentId}  was not found");
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
            var studentS = FindStudent(idStudent);
            if (studentS == null)
            {
                return BadRequest(
                    $"Id in the URL ({idStudent}) does not match the id in the request body {updatedData.IdStudent}");
            }
            else
            {
                studentS.FirstName = updatedData.FirstName;
                studentS.LastName = updatedData.LastName;
                // studentS.Email = updatedData.Email;
                // studentS.IndexNumber = updatedData.IndexNumber;

                return Ok(studentS);
            }


            // foreach (var student in _students)
            // {
            //     if (student.IdStudent == updatedData.IdStudent)
            //     {
            //         student.FirstName = updatedData.FirstName;
            //         student.LastName = updatedData.LastName;
            //         student.Email = updatedData.Email;
            //         student.IndexNumber = updatedData.IndexNumber;
            //        
            //         return Ok(student);
            //         
            //     }
            // }

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