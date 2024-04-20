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
    public class EditProductViewModel : ViewModelBase
    {
        private ProductDTO _product = null!;

        public ProductDTO Product
        {
            get { return _product; }
            set { _product = this.RaiseAndSetIfChanged(ref _product, value); }
        }

        public ProductDTO CurrentProduct { get; set; }

        private string _message;

        public string Message
        {
            get { return _message; }
            set { _message = this.RaiseAndSetIfChanged(ref _message, value); }
        }

        public EditProductViewModel(ProductDTO productDTO)
        {
            Product = new ProductDTO()
            {
                Id = productDTO.Id,
                Name = productDTO.Name
            };
            CurrentProduct = productDTO;
        }

        public async Task EditProduct()
        {
            try
            {
                await DBCall.EditProduct(Product);
                CurrentProduct.Name = Product.Name;
                Message = "Успешно изменено";
            }
            catch
            {
                Message = "Не удалось внести изменения";
            }
        }
    }
}
