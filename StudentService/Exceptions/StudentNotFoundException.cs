namespace StudentService.Exceptions
{
    public class StudentNotFoundException:ApplicationException
    {
        public StudentNotFoundException() { }
        public StudentNotFoundException(string message) : base(message) { }
    }
}
