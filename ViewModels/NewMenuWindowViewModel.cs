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
    public class NewMenuWindowViewModel : ViewModelBase
    {
        public AdminWindowViewModel AdminWindowViewModel { get; set; }

        public MenuDTO Menu { get; set; }

        private ObservableCollection<Dish> _breakfasts = null!;

        public ObservableCollection<Dish> Breakfasts
        {
            get { return _breakfasts; }
            set { _breakfasts = this.RaiseAndSetIfChanged(ref _breakfasts, value); }
        }

        private ObservableCollection<Dish> _lunches = null!;

        public ObservableCollection<Dish> Lunches
        {
            get { return _lunches; }
            set { _lunches = this.RaiseAndSetIfChanged(ref _lunches, value); }
        }

        private ObservableCollection<Dish> _dinners = null!;

        public ObservableCollection<Dish> Dinners
        {
            get { return _dinners; }
            set { _dinners = this.RaiseAndSetIfChanged(ref _dinners, value); }
        }

        private Dish _selectedBreakfast = null!;

        public Dish SelectedBreakfast
        {
            get { return _selectedBreakfast; }
            set { _selectedBreakfast = this.RaiseAndSetIfChanged(ref _selectedBreakfast, value); }
        }

        private Dish _selectedLunch = null!;

        public Dish SelectedLunch
        {
            get { return _selectedLunch; }
            set { _selectedLunch = this.RaiseAndSetIfChanged(ref _selectedLunch, value); }
        }

        private Dish _selectedDinner = null!;

        public Dish SelectedDinner
        {
            get { return _selectedDinner; }
            set { _selectedDinner = this.RaiseAndSetIfChanged(ref _selectedDinner, value); }
        }

        private string _message = null!;

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

        public NewMenuWindowViewModel(AdminWindowViewModel adminWindowViewModel)
        {
            AdminWindowViewModel = adminWindowViewModel;
            Menu = new MenuDTO();
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

            SelectedBreakfast = Breakfasts[0];

            SelectedLunch = Lunches[0];

            SelectedDinner = Dinners[0];
        }

        public async Task AddNewMenu()
        {
            try
            {
                await DBCall.AddNewMenu(Menu);
                await DBCall.AddNewMenuDishes(Menu, new ObservableCollection<Dish>
                {
                    SelectedBreakfast,
                    SelectedLunch,
                    SelectedDinner
                });
                AdminWindowViewModel.Menus.Add(Menu);
                Message = "Добавлено успешно";
            }
            catch 
            {
                Message = "Не удалось добавить меню";
            }
        }
    }
}
