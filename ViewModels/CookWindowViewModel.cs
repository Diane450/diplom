using Diplom.Models;
using Diplom.Services;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.ViewModels
{
    public class CookWindowViewModel:ViewModelBase
    {

        private MenuSchedule _menuSchedule;

        public MenuSchedule MenuSchedule
        {
            get { return _menuSchedule; }
            set { _menuSchedule = this.RaiseAndSetIfChanged(ref _menuSchedule, value); }
        }

        private ObservableCollection<MenuDish> _dishes;

        public ObservableCollection<MenuDish> Dishes
        {
            get { return _dishes; }
            set { _dishes = this.RaiseAndSetIfChanged(ref _dishes, value); }
        }

        private int _studentsCount;

        public int StudentsCount
        {
            get { return _studentsCount; }
            set { _studentsCount = this.RaiseAndSetIfChanged(ref _studentsCount, value); }
        }

        public CookWindowViewModel()
        {
            GetContent();
        }
        private async Task GetContent()
        {
            MenuSchedule = await DBCall.GetMenuSchedule();
            Dishes = await DBCall.GetDishes(MenuSchedule);
            StudentsCount = await DBCall.GetStudentsCount();
        }
    }
}
