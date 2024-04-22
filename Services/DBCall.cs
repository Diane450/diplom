using Diplom.Models;
using Diplom.ModelsDTo;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices;
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
                var sortedStudents = students.OrderBy(s => s.FullName).ToList();
                result.Add(classes[i], sortedStudents);
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
                todayMenu.Menu = menus[lastRecord.MenuId];

            await _dbContext.MenuSchedules.AddAsync(todayMenu);
            await _dbContext.SaveChangesAsync();
            return todayMenu;
        }

        public static async Task<ObservableCollection<MenuDish>> GetDishes(MenuSchedule menuSchedule)
        {
            var Dishes = new ObservableCollection<MenuDish>(await _dbContext.MenuDishes
                .Include(t => t.Dish)
                .Where(t => t.MenuId == menuSchedule.MenuId).ToListAsync());
            return Dishes;
        }

        public static async Task<ObservableCollection<Recipe>> GetRecipe(Dish dish)
        {
            return new ObservableCollection<Recipe>(await _dbContext.Recipes
                .Include(r => r.Product)
                .Where(r => r.DishId == dish.Id).ToListAsync());
        }

        public static async Task<ObservableCollection<Recipe>> GetRecipe(DishDTO dish)
        {
            return new ObservableCollection<Recipe>(await _dbContext.Recipes
                .Include(r => r.Product)
                .Where(r => r.DishId == dish.Id).ToListAsync());
        }

        public static async Task<int> GetStudentsCount()
        {
            return await _dbContext.Students.Where(s => s.StatusId == 1).CountAsync();
        }

        public static async Task<ObservableCollection<ProductDTO>> GetProducts()
        {
            return new ObservableCollection<ProductDTO>(await _dbContext.Products.Select(p => new ProductDTO
            {
                Id = p.Id,
                Name = p.Name,
            }).ToListAsync());
        }

        public static async Task AddNewProduct(ProductDTO productDTO)
        {
            var product = new Product
            {
                Name = productDTO.Name
            };
            await _dbContext.Products.AddAsync(product);
            await _dbContext.SaveChangesAsync();
            productDTO.Id = product.Id;
        }

        public static async Task EditProduct(ProductDTO productDTO)
        {
            Product product = await _dbContext.Products.FirstAsync(x => x.Id == productDTO.Id);
            product.Name = productDTO.Name;
            await _dbContext.SaveChangesAsync();
        }

        public static async Task<ObservableCollection<MenuDTO>> GetMenus()
        {
            return new ObservableCollection<MenuDTO>(await _dbContext.Menus.Select(p => new MenuDTO
            {
                Id = p.Id,
                Name = p.Name,
            }).ToListAsync());
        }

        public static async Task<ObservableCollection<Dish>> GetAllBreakfasts()
        {
            return new ObservableCollection<Dish>(await _dbContext.Dishes.Where(d => d.DishesTypeId == 1).ToListAsync());
        }

        public static async Task<ObservableCollection<Dish>> GetAllLunches()
        {
            return new ObservableCollection<Dish>(await _dbContext.Dishes.Where(d => d.DishesTypeId == 2).ToListAsync());
        }

        public static async Task<ObservableCollection<Dish>> GetAllDinners()
        {
            return new ObservableCollection<Dish>(await _dbContext.Dishes.Where(d => d.DishesTypeId == 3).ToListAsync());
        }

        public static async Task<ObservableCollection<MenuDish>> GetMenuDishes(MenuDTO menu)
        {
            return new ObservableCollection<MenuDish>(await _dbContext.MenuDishes.Where(d => d.MenuId == menu.Id)
                .Include(x => x.Dish)
                .ToListAsync());
        }

        public static async Task UpdateMenuDishes(ObservableCollection<MenuDish> menuDishes)
        {
            for (var i = 0; i < menuDishes.Count; i++)
            {
                var dish = await _dbContext.MenuDishes.FirstAsync(x => x.Id == menuDishes[i].Id);
                dish.Dish = menuDishes[i].Dish;
                await _dbContext.SaveChangesAsync();
            }
        }

        public static async Task UpdateMenu(MenuDTO menuDTO)
        {
            var menu = await _dbContext.Menus.FirstAsync(x => x.Id == menuDTO.Id);
            menu.Name = menuDTO.Name;
            await _dbContext.SaveChangesAsync();
        }

        public static async Task AddNewMenu(MenuDTO menuDTO)
        {
            var menu = new Menu()
            {
                Name = menuDTO.Name
            };
            await _dbContext.Menus.AddAsync(menu);
            await _dbContext.SaveChangesAsync();
            menuDTO.Id = menu.Id;
        }

        public static async Task AddNewMenuDishes(MenuDTO menuDTO, ObservableCollection<Dish> menuDishes)
        {
            for (int i = 0; i < menuDishes.Count; i++)
            {
                var menuDish = new MenuDish()
                {
                    MenuId = menuDTO.Id,
                    Dish = menuDishes[i]
                };
                await _dbContext.MenuDishes.AddAsync(menuDish);
                await _dbContext.SaveChangesAsync();
            }
        }

        public static async Task DeleteMenu(MenuDTO menuDTO)
        {
            var menu = await _dbContext.Menus.FirstAsync(x => x.Id == menuDTO.Id);
            _dbContext.Menus.Remove(menu);
            await _dbContext.SaveChangesAsync();
        }

        public static async Task<List<DishDTO>> GetAllDishes()
        {
            return await _dbContext.Dishes
                .Include(d => d.DishesType)
                .Select(d => new DishDTO()
                {
                    Id = d.Id,
                    Name = d.Name,
                    Type = new DishTypeDTO
                    {
                        Id = d.DishesTypeId,
                        Name = d.DishesType.Name,
                    }
                }).ToListAsync();
        }

        public static async Task<ObservableCollection<DishTypeDTO>> GetDishTypes()
        {
            return new ObservableCollection<DishTypeDTO>(await _dbContext.DishesTypes.Select(x => new DishTypeDTO()
            {
                Id = x.Id,
                Name = x.Name,
            }).ToListAsync());
        }

        public static async Task<ObservableCollection<Product>> GetAllProducts()
        {
            return new ObservableCollection<Product>(await _dbContext.Products.ToListAsync());
        }

        public static async Task AddNewDish(DishDTO dishDTO)
        {
            var dish = new Dish()
            {
                Name = dishDTO.Name,
                DishesTypeId = dishDTO.Type.Id,
            };
            await _dbContext.Dishes.AddAsync(dish);
            await _dbContext.SaveChangesAsync();
            dishDTO.Id = dish.Id;
        }

        public static async Task AddNewRecipe(DishDTO dishDTO, ObservableCollection<Recipe> recipe)
        {
            for (int i = 0; i < recipe.Count; i++)
            {
                recipe[i].DishId = dishDTO.Id;
                await _dbContext.Recipes.AddAsync(recipe[i]);
                await _dbContext.SaveChangesAsync();
            }
        }

        public static async Task UpdateDish(DishDTO dishDTO)
        {
            var dish = await _dbContext.Dishes.FirstAsync(x => x.Id == dishDTO.Id);
            dish.Name = dishDTO.Name;
            await _dbContext.SaveChangesAsync();
        }

        public static async Task UpdateRecipe(ObservableCollection<Recipe> newRecipe, DishDTO dishDTO)
        {
            var oldRecipes = await _dbContext.Recipes.Where(x => x.Dish.Id == dishDTO.Id).ToListAsync();
            for (int i = 0; i < oldRecipes.Count; i++)
            {
                _dbContext.Recipes.Remove(oldRecipes[i]);
                await _dbContext.SaveChangesAsync();
            }
            for (int i = 0; i < newRecipe.Count; i++)
            {
                newRecipe[i].DishId = dishDTO.Id;
                await _dbContext.Recipes.AddAsync(newRecipe[i]);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
