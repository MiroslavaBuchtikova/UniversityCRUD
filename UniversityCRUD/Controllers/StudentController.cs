using Microsoft.AspNetCore.Mvc;
using University.Dtos;
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

        public StudentController(UniversityDbContext dbContext)
        {
            _studentRepository = new StudentRepository(dbContext);
            _courseRepository = new CourseRepository(dbContext);
        }

        [HttpGet]
        public IActionResult GetList(string enrolled)
        {
            var students = _studentRepository.GetList(enrolled);
            var dtos = students.Select(x => ConvertToDto(x)).ToList();

            return Ok(dtos);
        }

        private StudentDto ConvertToDto(Student student)
        {

            var studentDto = new StudentDto
            {
                Id = student.Id,
                Name = student.Name,
                Email = student.Email
            };

            if (student.Enrollments.Count > 0)
            {
                studentDto.Course1 = student.Enrollments?[0]?.Course?.Name;
                studentDto.Course1Grade = student.Enrollments?[0]?.Grade.ToString();
                studentDto.Course1Credits = student.Enrollments?[0]?.Course?.Credits;
            }
            if (student.Enrollments.Count > 1)
            {

                studentDto.Course2 = student.Enrollments?[1]?.Course?.Name;
                studentDto.Course2Grade = student.Enrollments?[1]?.Grade.ToString();
                studentDto.Course2Credits = student.Enrollments?[1]?.Course?.Credits;
            }
            return studentDto;
        }

        [HttpPost]
        public IActionResult Create([FromBody] StudentDto dto)
        {
            var student = new Student(dto.Name, dto.Email);

            if (dto.Course1 != null && dto.Course1Grade != null)
            {
                Course course = _courseRepository.GetByName(dto.Course1);
                student.Enroll(course, Enum.Parse<Grade>(dto.Course1Grade));
            }

            if (dto.Course2 != null && dto.Course2Grade != null)
            {
                Course course = _courseRepository.GetByName(dto.Course2);
                student.Enroll(course, Enum.Parse<Grade>(dto.Course2Grade));
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

            Enrollment firstEnrollment = student.GetEnrollment(0);
            Enrollment secondEnrollment = student.GetEnrollment(1);

            if (HasEnrollmentChanged(dto.Course1, dto.Course1Grade, firstEnrollment))
            {
                if (string.IsNullOrWhiteSpace(dto.Course1)) // Student disenrolls
                {
                    if (string.IsNullOrWhiteSpace(dto.Course1DisenrollmentComment))
                        return BadRequest("Disenrollment comment is required");

                    Enrollment enrollment = firstEnrollment;
                    student.RemoveEnrollment(enrollment);
                    student.AddDisenrollmentComment(enrollment, dto.Course1DisenrollmentComment);
                }
                else
                {

                    if (string.IsNullOrWhiteSpace(dto.Course1Grade))
                        return BadRequest("Grade is required");

                    Course course = _courseRepository.GetByName(dto.Course1);

                    if (firstEnrollment == null)
                    {
                        // Student enrolls
                        student.Enroll(course, Enum.Parse<Grade>(dto.Course1Grade));
                    }
                    else
                    {
                        // Student transfers
                        firstEnrollment.Update(course, Enum.Parse<Grade>(dto.Course1Grade));
                    }
                }
            }

            if (HasEnrollmentChanged(dto.Course2, dto.Course2Grade, secondEnrollment))
            {
                if (string.IsNullOrWhiteSpace(dto.Course2)) // Student disenrolls
                {
                    if (string.IsNullOrWhiteSpace(dto.Course2DisenrollmentComment))
                        return BadRequest("Disenrollment comment is required");

                    Enrollment enrollment = secondEnrollment;
                    student.RemoveEnrollment(enrollment);
                    student.AddDisenrollmentComment(enrollment, dto.Course2DisenrollmentComment);
                }
                else
                {

                    if (string.IsNullOrWhiteSpace(dto.Course2Grade))
                        return BadRequest("Grade is required");

                    Course course = _courseRepository.GetByName(dto.Course2);

                    if (secondEnrollment == null)
                    {
                        // Student enrolls
                        student.Enroll(course, Enum.Parse<Grade>(dto.Course2Grade));
                    }
                    else
                    {
                        // Student transfers
                        secondEnrollment.Update(course, Enum.Parse<Grade>(dto.Course2Grade));
                    }
                }
            }
           
            _studentRepository.Save(student);
            return Ok();
        }

        private bool HasEnrollmentChanged(string newCourseName, string newGrade, Enrollment enrollment)
        {
            if (string.IsNullOrWhiteSpace(newCourseName) && enrollment == null)
                return false;

            if (string.IsNullOrWhiteSpace(newCourseName) || enrollment == null)
                return true;

            return newCourseName != enrollment.Course.Name || newGrade != enrollment.Grade.ToString();
        }
    }
}
