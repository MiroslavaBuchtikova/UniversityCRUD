using University.Persistance.Context;
using University.Persistance.Entities.Students;
using University.Persistance.Repositories;

public class StudentService
{
    private readonly StudentRepository _studentRepository;
    private readonly CourseRepository _courseRepository;
    public StudentService(UniversityDbContext dbContext)
    {
        _studentRepository = new StudentRepository(dbContext);
        _courseRepository = new CourseRepository(dbContext);

    }

    public void AppendEnrollments(Student student, StudentDto dto)
    {
        foreach (var enrollmentDto in dto.Enrollments.ToList())
        {
            var enrollment = student.Enrollments.SingleOrDefault(w => w.Id == enrollmentDto.Id);
            if (HasEnrollmentChanged(enrollmentDto.CourseName, enrollmentDto.CourseGrade?.ToString(), enrollment))
            {
                if (string.IsNullOrWhiteSpace(enrollmentDto.CourseName)) // Student disenrolls
                {
                    if (string.IsNullOrWhiteSpace(enrollmentDto.DisenrollmentComment))
                        throw new Exception("Disenrollment comment is required");

                    RemoveEnrollment(student, enrollment);
                    AddDisenrollmentComment(student, enrollment, enrollmentDto.DisenrollmentComment);
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(enrollmentDto.CourseGrade))
                        throw new Exception("Grade is required");

                    Course course = _courseRepository.GetByName(enrollmentDto.CourseName);

                    if (enrollment == null)
                    {
                        // Student enrolls
                        Enroll(student, course, Enum.Parse<Grade>(enrollmentDto.CourseGrade));
                    }
                    else
                    {
                        // Student transfers
                        UpdateEnrollment(enrollment, course, Enum.Parse<Grade>(enrollmentDto.CourseGrade.ToString()));
                    }
                }
            }
        }
    }

    private bool HasEnrollmentChanged(string newCourseName, string newGrade, Enrollment enrollment)
    {
        return enrollment == null || newCourseName != enrollment.Course.Name || newGrade != enrollment.Grade.ToString();
    }

    public virtual void RemoveEnrollment(Student student, Enrollment enrollment)
    {
        student.Enrollments.Remove(enrollment);
    }

    public virtual void AddDisenrollmentComment(Student student, Enrollment enrollment, string comment)
    {
        var disenrollment = new Disenrollment
        {
            Student = student,
            Course = enrollment.Course,
            Comment = comment,
            DateTime = DateTime.Now
        };
        if (student.Disenrollments == null)
        {
            student.Disenrollments = new List<Disenrollment>();
        }
        student.Disenrollments.Add(disenrollment);
    }

    public virtual void Enroll(Student student, Course course, Grade grade)
    {
        if (student.Enrollments?.Count >= 2)
            throw new Exception("Cannot have more than 2 enrollments");


        var enrollment = new Enrollment
        {
            Student = student,
            Course = course,
            Grade = grade
        };

        if (student.Enrollments == null)
        {
            student.Enrollments = new List<Enrollment>();
        }
        student.Enrollments.Add(enrollment);
    }

    public void UpdateEnrollment(Enrollment enrollment, Course course, Grade grade)
    {
        enrollment.Grade = grade;
        enrollment.Course = course;
    }
}