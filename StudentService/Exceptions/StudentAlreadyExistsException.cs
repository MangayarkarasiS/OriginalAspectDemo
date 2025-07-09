namespace StudentService.Exceptions
{
    public class StudentAlreadyExistsException:ApplicationException
    {
        public StudentAlreadyExistsException() { }
        public StudentAlreadyExistsException(string message) : base(message) { }
    }
}
