using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ApiStudent.Controller
{
   [ApiController]
   [Route("[controller]")]
   public class StudentController : ControllerBase
   {
       private readonly ILogger<StudentController> _logger;

        public StudentController(ILogger<StudentController> logger)
        {
            _logger = logger;
        }

       [HttpGet]
       public IEnumerable<Student> Get()
       {
           return StudentRepo.Student;
       }

       [HttpGet("a/{id}")]
        public Student Get(int id)
       {
           foreach (Student st in StudentRepo.Student)
           {
               if(st.id==id)
               {
                   return st;
               }
           }
           return null;
       }
       
     
        [ HttpDelete( "{id}" ) ]
       public List<Student> Delete( int id )
        {
            foreach (Student st in StudentRepo.Student)
           {
               if(st.id==id)
                StudentRepo.Student.Remove(st);
           }
           return StudentRepo.Student;
        }

        [HttpPost]
        public List<Student> Post([FromBody] Student student)
        {
            StudentRepo.Student.Add(student);
            return StudentRepo.Student;
        }

        [HttpPut("{id}")]

        public List<Student> Put(int id, [FromBody] Student student)
        {
            foreach(Student st in StudentRepo.Student)
            {
                if(st.id==id)
                {
                    st.id=student.id;
                    st.nume=student.nume;
                    st.prenume=student.prenume;
                    st.facultate=student.facultate;
                    st.an=student.an;
                }
            }
            return StudentRepo.Student;

        }
     
   }
}