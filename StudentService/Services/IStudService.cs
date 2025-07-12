using StudentService.Models;

namespace StudentService.Services
{
    public interface IStudService
    {
        public List<Stud> GetAllStudents();
        public Stud GetStudentById(int id);
       
        public int AddStud(Stud stud);

        public int UpdateStud(int id, Stud stud);


        public int DeleteStud(int id);
    }
}
