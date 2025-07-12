using Microsoft.EntityFrameworkCore;
using StudentService.Data;
using StudentService.Exceptions;
using StudentService.Models;

namespace StudentService.Repository
{
    public class StudentRepository : IStudRepository
    {
        private readonly StudentServiceContext _context;
        public StudentRepository(StudentServiceContext context)
        {
            _context = context;
        }
        public int AddStud(Stud stud)
        {
            _context.Stud.Add(stud);
            return  _context.SaveChanges();
        }
     
        public int DeleteStud(int id)
        {
            Stud c = _context.Stud.Where(x => x.studId == id).FirstOrDefault();
            _context.Stud.Remove(c);
            return _context.SaveChanges();
        }

        public List<Stud> GetAllStudents()
        {
          return  _context.Stud.ToList();
        }

        public Stud GetStudentById(int id)
        {
            return _context.Stud.FirstOrDefault(s => s.studId == id);
        }

        public int UpdateStud(int id, Stud stud)
        {
           // _context.Entry(stud).State = EntityState.Modified;           
           //  return _context.SaveChanges();
            var existingStud = _context.Stud.Find(id); // Retrieve the tracked entity
            if (existingStud == null)
            {
                throw new StudentNotFoundException($"Student with student id {id} does not exist");
            }

            // Update properties
            existingStud.studName = stud.studName;
            existingStud.studDOB = stud.studDOB;
            existingStud.studGender = stud.studGender;
            existingStud.studTotalMarks = stud.studTotalMarks;

            return _context.SaveChanges();


        }
    }
}
