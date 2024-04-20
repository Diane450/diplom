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
        private int _index;

        public int Index
        {
            get { return _index; }
            set { _index = this.RaiseAndSetIfChanged(ref _index, value); }
        }

        private ObservableCollection<ProductDTO> _products;

        public ObservableCollection<ProductDTO> Products
        {
            get { return _products; }
            set { _products = this.RaiseAndSetIfChanged(ref _products, value); }
        }

        public AdminWindowViewModel()
        {
            GetContent();
        }
        public async Task GetContent()
        {
            Products = await DBCall.GetProducts();
        }
    }
}
