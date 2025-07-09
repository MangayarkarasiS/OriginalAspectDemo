using StudentService.Exceptions;
using StudentService.Models;
using StudentService.Repository;

namespace StudentService.Services
{
    public class StudService : IStudService
    {
        public readonly IStudRepository repo;
        public StudService(IStudRepository studentRepository)
        {
                repo = studentRepository;
        }
        public int AddStud(Stud stud)
        {
            if (repo.GetStudentById(stud.studId) != null)
            {
                throw new StudentAlreadyExistsException($"Student with student id {stud.studId} already exists");
            }
            return repo.AddStud(stud);
        }

        public int DeleteStud(int id)
        {
            if (repo.GetStudentById(id) == null)
            {

                throw new StudentNotFoundException($"Student with student id {id} does not exists");
            }
            return repo.DeleteStud(id);
        }

        public List<Stud> GetAllStudents()
        {
            return repo.GetAllStudents();
        }

        public Stud GetStudentById(int id)
        {
            Stud c = repo.GetStudentById(id);
            if (c == null)
            {
                throw new StudentNotFoundException($"Student with student id {id} does not exists");
            }
            return c;
        }

        public int UpdateStud(int id, Stud stud)
        {
            if (repo.GetStudentById(id) == null)
            {
                throw new StudentNotFoundException($"Student with student id {id} does not exists");
            }
            return repo.UpdateStud(id,stud);
        }
    }
}
