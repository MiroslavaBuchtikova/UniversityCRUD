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
            var existingStudent = _studentRepository.GetBySSN(dto.SSN);
            if(dto.SSN == null)
            {
                return BadRequest($"SSN cant't be null");
            }
            if(existingStudent != null)
            {
                return BadRequest($"Student with SSN {dto.SSN} already exists");
            }
            var student = new Student
            {
                SSN = dto.SSN,
                Name = dto.Name,
                Email = dto.Email
            };

            if (dto.Course1 != null && dto.Course1Grade != null)
            {
                Course course = _courseRepository.GetByName(dto.Course1);
                _studentService.Enroll(student, course, Enum.Parse<Grade>(dto.Course1Grade));
            }

            if (dto.Course2 != null && dto.Course2Grade != null)
            {
                Course course = _courseRepository.GetByName(dto.Course2);
                _studentService.Enroll(student, course, Enum.Parse<Grade>(dto.Course2Grade));
            }

            _studentRepository.Save(student);

            return Ok(student.Map());
        }

        [HttpDelete("{ssn}")]
        public IActionResult Delete(string ssn)
        {
            Student student = _studentRepository.GetBySSN(ssn);
            if (student == null)
                return BadRequest($"No student found for SSN {ssn}");

            _studentRepository.Delete(student);

            return Ok();
        }

        [HttpPut("{ssn}")]
        public IActionResult Update(string ssn, [FromBody] StudentDto dto)
        {
            Student student = _studentRepository.GetBySSN(ssn);
            if (student == null)
                return BadRequest($"No student found for SSN {ssn}");

            student.Name = dto.Name;
            student.Email = dto.Email;

            _studentService.AppendEnrollments(student, dto);
            _studentRepository.Save(student);

            return Ok(student.Map());
        }


    }
}
