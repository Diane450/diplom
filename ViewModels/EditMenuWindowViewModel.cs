using Diplom.Models;
using Diplom.ModelsDTo;
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
    public class EditMenuWindowViewModel : ViewModelBase
    {
        public MenuDTO CurrentMenu { get; set; }

        private MenuDTO _menu;

        public MenuDTO Menu
        {
            get { return _menu; }
            set { _menu = this.RaiseAndSetIfChanged(ref _menu, value); }
        }

        private ObservableCollection<Dish> _breakfasts;

        public ObservableCollection<Dish> Breakfasts
        {
            get { return _breakfasts; }
            set { _breakfasts = this.RaiseAndSetIfChanged(ref _breakfasts, value); }
        }

        private ObservableCollection<Dish> _lunches;

        public ObservableCollection<Dish> Lunches
        {
            get { return _lunches; }
            set { _lunches = this.RaiseAndSetIfChanged(ref _lunches, value); }
        }

        private ObservableCollection<Dish> _dinners;

        public ObservableCollection<Dish> Dinners
        {
            get { return _dinners; }
            set { _dinners = this.RaiseAndSetIfChanged(ref _dinners, value); }
        }


        private ObservableCollection<MenuDish> _menuDishes;

        public ObservableCollection<MenuDish> MenuDishes
        {
            get { return _menuDishes; }
            set { _menuDishes = this.RaiseAndSetIfChanged(ref _menuDishes, value); }
        }

        private Dish _selectedBreakfast;

        public Dish SelectedBreakfast
        {
            get { return _selectedBreakfast; }
            set { _selectedBreakfast = this.RaiseAndSetIfChanged(ref _selectedBreakfast, value); }
        }

        private Dish _selectedLunch;

        public Dish SelectedLunch
        {
            get { return _selectedLunch; }
            set { _selectedLunch = this.RaiseAndSetIfChanged(ref _selectedLunch, value); }
        }

        private Dish _selectedDinner;

        public Dish SelectedDinner
        {
            get { return _selectedDinner; }
            set { _selectedDinner = this.RaiseAndSetIfChanged(ref _selectedDinner, value); }
        }

        private string _message;

        public string Message
        {
            get { return _message; }
            set { _message = this.RaiseAndSetIfChanged(ref _message, value); }
        }

        private bool _isEnable;

        public bool IsEnable
        {
            get { return _isEnable; }
            set { _isEnable = this.RaiseAndSetIfChanged(ref _isEnable, value); }
        }

        public EditMenuWindowViewModel(MenuDTO menuDTO)
        {
            CurrentMenu = menuDTO;
            Menu = new MenuDTO()
            {
                Id = menuDTO.Id,
                Name = menuDTO.Name,
            };
            GetContent();
            this.WhenAnyValue(x => x.Menu.Name).Subscribe(_ => Enable());
        }

        private void Enable()
        {
            IsEnable = !string.IsNullOrEmpty(Menu.Name);
        }
        private async void GetContent()
        {
            Breakfasts = await DBCall.GetAllBreakfasts();
            Lunches = await DBCall.GetAllLunches();
            Dinners = await DBCall.GetAllDinners();

            MenuDishes = await DBCall.GetMenuDishes(Menu);
            
            var breakfast = MenuDishes.Where(x => x.Dish.DishesTypeId == 1).First();
            SelectedBreakfast = Breakfasts.Where(x => x.Id == breakfast.DishId).First();
            
            var lunch = MenuDishes.Where(x => x.Dish.DishesTypeId == 2).First();
            SelectedLunch = Lunches.Where(x => x.Id == lunch.DishId).First();

            var dinner = MenuDishes.Where(x => x.Dish.DishesTypeId == 3).First();
            SelectedDinner = Dinners.Where(x => x.Id == dinner.DishId).First();

        }

        public async Task EditMenu()
        {
            try
            {
                MenuDishes[0].Dish = SelectedBreakfast;
                MenuDishes[1].Dish = SelectedLunch;
                MenuDishes[2].Dish = SelectedDinner;

                await DBCall.UpdateMenuDishes(MenuDishes);
                await DBCall.UpdateMenu(Menu);

                CurrentMenu.Name = Menu.Name;

                Message = "Меню обновлено";

            }
            catch 
            {
                Message = "Не удалось обновить данные";
            }
        }
    }
}
