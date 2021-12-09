using University.Dtos;
using University.Persistance.Entities.Students;

public static class StudentMapper{

    public static StudentDto Map (this Student student)
    {
        var studentDto = new StudentDto
        {
            Id = student.Id,
            Name = student.Name,
            Email = student.Email,
            Enrollments = student.Enrollments.Map(),
        };

        return studentDto;
    }

    public static List<EnrollmentDto> Map (this IList<Enrollment> enrollments)
    {
        var mappedEnrolments = enrollments.Select(x => new EnrollmentDto
        {
            Id = x.Id,
            CourseCredit = x.Course.Credits,
            CourseGrade = x.Grade.ToString(),
            CourseName = x.Course.Name
        }).ToList();
        return mappedEnrolments;
    }
}
