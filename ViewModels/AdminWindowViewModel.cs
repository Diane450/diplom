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
    public class AdminWindowViewModel:ViewModelBase
    {
        private int _index = 0;

        public int Index
        {
            get { return _index; }
            set { _index = this.RaiseAndSetIfChanged(ref _index, value); }
        }

        private ObservableCollection<ProductDTO> _products = null!;

        public ObservableCollection<ProductDTO> Products
        {
            get { return _products; }
            set { _products = this.RaiseAndSetIfChanged(ref _products, value); }
        }

        private ObservableCollection<MenuDTO> _menus = null!;

        public ObservableCollection<MenuDTO> Menus
        {
            get { return _menus; }
            set { _menus = this.RaiseAndSetIfChanged(ref _menus, value); }
        }

        public List<DishDTO> DishesList { get; set; } = null!;

        private ObservableCollection<DishDTO> _dishes = null!;

        public ObservableCollection<DishDTO> Dishes
        {
            get { return _dishes; }
            set { _dishes = this.RaiseAndSetIfChanged(ref _dishes, value); }
        }


        public AdminWindowViewModel()
        {
            GetContent();
        }
        public async Task GetContent()
        {
            var tasks = new List<Task>();
            tasks.Add(DBCall.GetProducts().ContinueWith(t => Products = new ObservableCollection<ProductDTO>(t.Result)));
            tasks.Add(DBCall.GetMenus().ContinueWith(t => Menus = new ObservableCollection<MenuDTO>(t.Result)));
            tasks.Add(DBCall.GetAllDishes().ContinueWith(t => DishesList = t.Result));

            await Task.WhenAll(tasks);

            Dishes = new ObservableCollection<DishDTO>(DishesList);

            //Products = await DBCall.GetProducts();

            //Menus = await DBCall.GetMenus();

            //DishesList = await DBCall.GetAllDishes();

            //Dishes = new ObservableCollection<DishDTO>(DishesList);
        }
    }
}
