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

        public static async Task<Dictionary<Class, List<StudentsDTO>>> GetClassesData(Teacher teacher)
        {
            List<Class> classes = await _dbContext.Classes.Where(c => c.TeacherId == teacher.Id).ToListAsync();

            Dictionary<Class, List<StudentsDTO>> result = new Dictionary<Class, List<StudentsDTO>>();
            for (int i = 0; i < classes.Count; i++)
            {
                List<StudentsDTO> students = await (from stud in _dbContext.Students
                                                    join statuses in _dbContext.Statuses on stud.StatusId equals statuses.Id
                                                    where stud.ClassId == classes[i].Id
                                                    select new StudentsDTO()
                                                    {
                                                        Id = stud.Id,
                                                        FullName = stud.FullName,
                                                        ClassId = stud.ClassId,
                                                        Statuss = new StatusDTO
                                                        {
                                                            Id = statuses.Id,
                                                            Name = statuses.Name,
                                                        }
                                                    }).ToListAsync();
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

        public static async Task SaveStudentChanges(StudentsDTO student)
        {
            Student Student = await _dbContext.Students.FirstAsync(s => s.Id == student.Id);
            Student.Status = await _dbContext.Statuses.FirstAsync(s => s.Id == student.Statuss.Id);
            await _dbContext.SaveChangesAsync();
        }

        public static async Task<Student> GetStudent(Student student)
        {
            return await _dbContext.Students.FirstAsync(s => s.Id == student.Id);
        }

        public static async Task<MenuSchedule> GetMenuSchedule()
        {
            var lastRecord = await _dbContext.MenuSchedules.OrderByDescending(m => m.Id)
                .Include(m => m.Menu)
                .FirstOrDefaultAsync();
            if (lastRecord?.Date == DateOnly.FromDateTime(DateTime.Now))
                return lastRecord;

            var menus = await _dbContext.Menus.ToListAsync();

            var todayMenu = new MenuSchedule
            {
                Date = DateOnly.FromDateTime(DateTime.Now),
            };

            if (lastRecord == null || lastRecord.MenuId == menus[menus.Count - 1].Id)
                todayMenu.Menu = menus[0];
            else
                todayMenu.Menu = menus[lastRecord.MenuId + 1];

            await _dbContext.MenuSchedules.AddAsync(todayMenu);
            await _dbContext.SaveChangesAsync();
            return todayMenu;
        }

        public static async Task<ObservableCollection<MenuDish>> GetDishes(MenuSchedule menuSchedule)
        {
            var Dishes = new ObservableCollection<MenuDish>(await _dbContext.MenuDishes
                .Include(t => t.Dish)
                .Where(t => t.MenuId == menuSchedule.Id).ToListAsync());
            return Dishes;
        }

        public static async Task<ObservableCollection<Recipe>> GetRecipe(Dish dish)
        {
            return new ObservableCollection<Recipe>(await _dbContext.Recipes
                .Include(r => r.Product)
                .Where(r => r.DishId == dish.Id).ToListAsync());
        }

        public static async Task<int> GetStudentsCount()
        {
            return await _dbContext.Students.Where(s=>s.StatusId==1).CountAsync();
        }
    }
}
