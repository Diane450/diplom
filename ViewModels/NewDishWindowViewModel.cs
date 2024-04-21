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
    public class NewDishWindowViewModel : ViewModelBase
    {
        public AdminWindowViewModel AdminWindowViewModel { get; set; }

        public DishDTO Dish { get; set; }

        private ObservableCollection<DishTypeDTO> _type;

        public ObservableCollection<DishTypeDTO> Type
        {
            get { return _type; }
            set { _type = this.RaiseAndSetIfChanged(ref _type, value); }
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

        private DishTypeDTO _selectedType;

        public DishTypeDTO SelectedType
        {
            get { return _selectedType; }
            set { _selectedType = this.RaiseAndSetIfChanged(ref _selectedType, value); }
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

        public NewDishWindowViewModel(AdminWindowViewModel adminWindowViewModel)
        {
            AdminWindowViewModel = adminWindowViewModel;
            Dish = new DishDTO();
            Recipe = new ObservableCollection<Recipe>();
            GetContent();
            this.WhenAnyValue(x => x.SelectedProduct, x => x.ProductQuantity, x => x.Dish.Name, x=>x.Recipe.Count).Subscribe(_ => IsEnable());
        }

        private void IsEnable()
        {
            IsAddIngredintEnable = !string.IsNullOrEmpty(ProductQuantity) && SelectedProduct != null;
            IsAddEnable = !string.IsNullOrEmpty(Dish.Name) && Recipe.Count != 0;
        }
        private async Task GetContent()
        {
            Type = await DBCall.GetDishTypes();
            Products = await DBCall.GetAllProducts();
            SelectedType = Type[0];
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

        public async Task AddNewDish()
        {
            try
            {
                Dish.Type = SelectedType;
                await DBCall.AddNewDish(Dish);
                await DBCall.AddNewRecipe(Dish, Recipe);
                AdminWindowViewModel.Dishes.Add(Dish);
                Message = "Блюдо добавлено";
            }
            catch
            {
                Message = "Не удалось добавить блюдо";
            }
        }
    }
}
