using StudentService.Models;
using System.Net.NetworkInformation;

namespace StudentService.Repository
{
    public interface IStudRepository
    {
        public List<Stud> GetAllStudents();
        public Stud GetStudentById(int id);
      
        public int AddStud(Stud stud);

        public int UpdateStud(int id, Stud stud);

        public int DeleteStud(int id);
    }
}
