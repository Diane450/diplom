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
    public class DishDetailsWindowViewModel:ViewModelBase
    {
        private Dish _dish;

        public Dish Dish
        {
            get { return _dish; }
            set { _dish = this.RaiseAndSetIfChanged(ref _dish, value); }
        }

        private ObservableCollection<Recipe> _recipe;

        public ObservableCollection<Recipe> Recipe
        {
            get { return _recipe; }
            set { _recipe = this.RaiseAndSetIfChanged(ref _recipe, value); }
        }

        public DishDetailsWindowViewModel(MenuDish menuDish)
        {
            Dish = menuDish.Dish;
            GetContent();
        }

        private async Task GetContent()
        {
            Recipe = await DBCall.GetRecipe(Dish);

        }
    }
}
