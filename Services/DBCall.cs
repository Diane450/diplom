using Diplom.Models;
using Diplom.ModelsDTo;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.Services
{
    public static class DBCall
    {
        private static Ispr2336SadykovViDiplomContext _dbContext = new Ispr2336SadykovViDiplomContext();

        public static async Task<User> Authorize(UserDTO user)
        {
            return await _dbContext.Users.FirstAsync(u => u.Login == user.Login && u.Password == user.Password);
        }

        public static async Task<Teacher> GetTeacherData(User user)
        {
            return await _dbContext.Teachers
                .Include(t => t.User)
                .Where(t => t.UserId == user.Id).FirstAsync();
        }

        public static async Task<Dictionary<Class, List<Student>>> GetClassesData(Teacher teacher)
        {
            List<Class> classes = await _dbContext.Classes.Where(c => c.TeacherId == teacher.Id).ToListAsync();

            Dictionary<Class, List<Student>> result = new Dictionary<Class, List<Student>>();
            for (int i = 0; i < classes.Count; i++)
            {
                List<Student> students = await _dbContext.Students.Where(s => s.ClassId == classes[i].Id)
                    .Include(s => s.Status)
                    .ToListAsync();
                result.Add(classes[i], students);
            }
            return result;
        }

        public static async Task<ObservableCollection<Class>> GetClasses(Teacher teacher)
        {
            return new ObservableCollection<Class>(await _dbContext.Classes.Where(c => c.TeacherId == teacher.Id).ToListAsync());
        }

        public static async Task<ObservableCollection<Status>> GetStatuses()
        {
            return new ObservableCollection<Status>(await _dbContext.Statuses.ToListAsync());
        }

        public static async Task SaveStudentChanges(Student student)
        {
            Student Student = await _dbContext.Students.FirstAsync(s => s.Id == student.Id);
            Student.Status = student.Status;
            //Student.StatusId = student.StatusId;
            await _dbContext.SaveChangesAsync();
        }

        public static async Task<Student> GetStudent(Student student)
        {
            return await _dbContext.Students.FirstAsync(s => s.Id == student.Id);
        }
    }
}
