using University.Persistence.Context;
using University.Persistence.Entities.Students;
using University.Persistence.Repositories;

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

        Enrollment firstEnrollment = GetEnrollment(student, 0);
        Append(dto.Course1, dto.Course1Grade, dto.Course1DisenrollmentComment, firstEnrollment, student);


        Enrollment secondEnrollment = GetEnrollment(student, 1);
        Append(dto.Course2, dto.Course2Grade, dto.Course2DisenrollmentComment, secondEnrollment, student);
    }

    public void Append(string newCourseName, string newGrade, string courseDisenrollmentComment, Enrollment enrollment, Student student)
    {

        if (HasEnrollmentChanged(newCourseName, newGrade, enrollment))
        {
            if (string.IsNullOrWhiteSpace(newCourseName)) // Student disenrolls
            {
                if (string.IsNullOrWhiteSpace(courseDisenrollmentComment))
                    throw new Exception("Disenrollment comment is required");

                Enrollment enrollmentToRemove = enrollment;
                student.Enrollments.Remove(enrollmentToRemove);
                AddDisenrollmentWithComment(student, enrollment, courseDisenrollmentComment);
            }
            else
            {
                if (string.IsNullOrWhiteSpace(newGrade))
                    throw new Exception("Grade is required");

                Course course = _courseRepository.GetByName(newCourseName);

                if (enrollment == null)
                {
                    // Student enrolls
                    Enroll(student, course, Enum.Parse<Grade>(newGrade));
                }
                else
                {
                    // Student transfers
                    UpdateEnrollment(enrollment, course, Enum.Parse<Grade>(newGrade));
                }
            }
        }
    }
    public Enrollment GetEnrollment(Student student, int index)
    {
        if (student.Enrollments?.Count > index)
            return student.Enrollments[index];

        return null;
    }
    private bool HasEnrollmentChanged(string newCourseName, string newGrade, Enrollment enrollment)
    {
        return enrollment == null || newCourseName != enrollment.Course.Name || newGrade != enrollment.Grade.ToString();
    }

    public void AddDisenrollmentWithComment(Student student, Enrollment enrollment, string comment)
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

    public void Enroll(Student student, Course course, Grade grade)
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