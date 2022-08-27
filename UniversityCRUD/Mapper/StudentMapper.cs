using University.Persistence.Entities.Students;

public static class StudentMapper{

    public static StudentDto Map (this Student student)
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
}
