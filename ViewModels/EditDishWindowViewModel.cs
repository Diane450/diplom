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
    public class EditDishWindowViewModel:ViewModelBase
    {
        public AdminWindowViewModel AdminWindowViewModel { get; set; }

        public DishDTO CurrentDish { get; set; }

        private DishDTO _dish;

        public DishDTO Dish
        {
            get { return _dish; }
            set { _dish = this.RaiseAndSetIfChanged(ref _dish, value); }
        }

        private ObservableCollection<Product> _products;

        public ObservableCollection<Product> Products
        {
            get { return _products; }
            set { _products = this.RaiseAndSetIfChanged(ref _products, value); }
        }

        private ObservableCollection<Recipe> _recipe;

        public ObservableCollection<Recipe> Recipe
        {
            get { return _recipe; }
            set { _recipe = this.RaiseAndSetIfChanged(ref _recipe, value); }
        }

        private Product _selectedProduct;

        public Product SelectedProduct
        {
            get { return _selectedProduct; }
            set { _selectedProduct = this.RaiseAndSetIfChanged(ref _selectedProduct, value); }
        }

        private string _productQuantity;

        public string ProductQuantity
        {
            get { return _productQuantity; }
            set { _productQuantity = this.RaiseAndSetIfChanged(ref _productQuantity, value); }
        }

        private string _message;

        public string Message
        {
            get { return _message; }
            set { _message = this.RaiseAndSetIfChanged(ref _message, value); }
        }

        private bool _isAddIngredintEnable;

        public bool IsAddIngredintEnable
        {
            get { return _isAddIngredintEnable; }
            set { _isAddIngredintEnable = this.RaiseAndSetIfChanged(ref _isAddIngredintEnable, value); }
        }

        private bool _isAddEnable;

        public bool IsAddEnable
        {
            get { return _isAddEnable; }
            set { _isAddEnable = this.RaiseAndSetIfChanged(ref _isAddEnable, value); }
        }
        public EditDishWindowViewModel(AdminWindowViewModel adminWindowViewModel, DishDTO dishDTO)
        {
            AdminWindowViewModel = adminWindowViewModel;
            CurrentDish = dishDTO;
            Dish = new DishDTO()
            {
                Id = dishDTO.Id,
                Name = dishDTO.Name,
                Type = dishDTO.Type,
            };
            GetContent();
            this.WhenAnyValue(x => x.SelectedProduct, x => x.ProductQuantity, x => x.Dish.Name, x => x.Recipe.Count).Subscribe(_ => IsEnable());

        }

        private void IsEnable()
        {
            IsAddIngredintEnable = !string.IsNullOrEmpty(ProductQuantity) && SelectedProduct != null;
            IsAddEnable = !string.IsNullOrEmpty(Dish.Name) && Recipe.Count != 0;
        }

        private async Task GetContent()
        {
            Products = await DBCall.GetAllProducts();
            Recipe = await DBCall.GetRecipe(Dish);

        }
        public void AddIngridient()
        {
            if (Recipe.Count > 10)
            {
                Message = "Максимальное количество ингридиентов 10";
            }
            else
            {
                Recipe.Add(new Recipe
                {
                    Product = SelectedProduct,
                    ProductQuantity = ProductQuantity
                });
            }
        }
        public async Task SaveChanges()
        {
            try
            {
                await DBCall.UpdateDish(Dish);
                await DBCall.UpdateRecipe(Recipe, Dish);
                CurrentDish.Name = Dish.Name;
                Message = "Блюдо обновлено";
            }
            catch
            {
                Message = "Не удалось обновить блюдо";
            }
        }
    }
}
