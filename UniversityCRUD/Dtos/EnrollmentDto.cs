namespace University.Dtos;
public class EnrollmentDto
{
    public long Id { get; set; }
    public string CourseName { get; set; }
    public string CourseGrade { get; set; }
    public string DisenrollmentComment { get; set; }
    public int? CourseCredit { get; set; }
}
