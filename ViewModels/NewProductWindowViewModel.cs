using Diplom.ModelsDTo;
using Diplom.Services;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.ViewModels
{
    public class NewProductWindowViewModel : ViewModelBase
    {
        private ProductDTO _product;

        public ProductDTO Product
        {
            get { return _product; }
            set { _product = this.RaiseAndSetIfChanged(ref _product, value); }
        }

        private string _message;

        public string Message
        {
            get { return _message; }
            set { _message = this.RaiseAndSetIfChanged(ref _message, value); }
        }

        private AdminWindowViewModel _model = null!;

        public AdminWindowViewModel Model
        {
            get { return _model; }
            set { _model = this.RaiseAndSetIfChanged(ref _model, value); }
        }

        private bool _isButtonEnable;

        public bool IsButtonEnable
        {
            get { return _isButtonEnable; }
            set { _isButtonEnable = this.RaiseAndSetIfChanged(ref _isButtonEnable, value); }
        }


        public NewProductWindowViewModel(AdminWindowViewModel model)
        {
            Model = model;
            Product = new ProductDTO();
            this.WhenAnyValue(x => x.Product.Name).Subscribe(_ => ButtonEnable());
        }
        public async Task AddNewProduct()
        {
            try
            {
                //await DBCall.AddNewProduct(Product);
                Model.Products.Add(Product);
                Message = "Новый ингредиент добавлен";
            }
            catch
            {
                Message = "Не удалось добавить ингредиент";
            }
        }
    
        public void ButtonEnable()
        {
            IsButtonEnable = !string.IsNullOrEmpty(Product.Name);
        }
    }
}
