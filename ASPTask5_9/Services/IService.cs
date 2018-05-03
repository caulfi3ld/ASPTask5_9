using ASPTask5_9.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPTask5_9.Services
{
    public interface IService
    {
        List<StudentModel> GetStudentsList();
        void AddStudent(StudentModel student);
        StudentModel GetStudent(int id);
        void SetStudent(int id, StudentModel student);
        void DeleteStudent(int id);
    }

    public class Service : IService
    {
        public readonly StudentContext _context;

        public Service(StudentContext context)
        {
            _context = context;
        }
        public void AddStudent(StudentModel student)
        {
            _context.Students.Add(student);
            _context.SaveChanges();
        }

        public void DeleteStudent(int id)
        {
            _context.Students.Remove(_context.Students.Find(id));
            _context.SaveChanges();
        }

        public StudentModel GetStudent(int id)
        {
            return _context.Students.Find(id);

        }

        public List<StudentModel> GetStudentsList()
        {
            return _context.Students.ToList();
        }

        public void SetStudent(int id, StudentModel student)
        {
            StudentModel studentToUpdate = _context.Students.Find(id);
            studentToUpdate.Name = student.Name;
            studentToUpdate.Age = student.Age;
            studentToUpdate.Group = student.Group;
            _context.SaveChanges();
        }
    }
}
