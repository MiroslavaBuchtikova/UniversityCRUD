using University.Dtos;

public class StudentDto
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }

    public List<EnrollmentDto> Enrollments { get; set; }
}
