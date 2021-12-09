using Microsoft.AspNetCore.Mvc;
using University.Persistance.Context;
using University.Persistance.Entities.Students;
using University.Persistance.Repositories;

namespace University
{
    [Route("api/students")]
    public sealed class StudentController : Controller
    {
        private readonly StudentRepository _studentRepository;
        private readonly CourseRepository _courseRepository;
        private readonly StudentService _studentService;

        public StudentController(UniversityDbContext dbContext, StudentService studentService)
        {
            _studentRepository = new StudentRepository(dbContext);
            _courseRepository = new CourseRepository(dbContext);
            _studentService = studentService;
        }

        [HttpGet]
        public IActionResult GetList(string courseName)
        {
            var students = _studentRepository.GetList(courseName);
            var dtos = students.Select(x => x.Map()).ToList();

            return Ok(dtos);
        }

        [HttpPost]
        public IActionResult Create([FromBody] StudentDto dto)
        {
            var student = new Student
            {
                Name = dto.Name,
                Email = dto.Email
            };

            foreach (var enrollment in dto.Enrollments)
            {
                Course course = _courseRepository.GetByName(enrollment.CourseName);
                _studentService.Enroll(student, course, Enum.Parse<Grade>(enrollment.CourseGrade));
            }

            _studentRepository.Save(student);

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            Student student = _studentRepository.GetById(id);
            if (student == null)
                return BadRequest($"No student found for Id {id}");

            _studentRepository.Delete(student);

            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] StudentDto dto)
        {
            Student student = _studentRepository.GetById(id);
            if (student == null)
                return BadRequest($"No student found for Id {id}");

            student.Name = dto.Name;
            student.Email = dto.Email;

            _studentService.AppendEnrollments(student, dto);
            _studentRepository.Save(student);

            return Ok();
        }


    }
}
