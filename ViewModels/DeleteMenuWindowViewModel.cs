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
    public class DeleteMenuWindowViewModel:ViewModelBase
    {
        public AdminWindowViewModel AdminWindowViewModel { get; set; }

        public MenuDTO MenuDTO { get; set; }

        private string _message;

        public string Message
        {
            get { return _message; }
            set { _message = this.RaiseAndSetIfChanged(ref _message, value); }
        }

        public DeleteMenuWindowViewModel(MenuDTO menuDTO, AdminWindowViewModel adminWindowViewModel)
        {
            AdminWindowViewModel = adminWindowViewModel;
            MenuDTO = menuDTO;
        }

        public async Task Delete()
        {
            try
            {
                await DBCall.DeleteMenu(MenuDTO);
            }
            catch
            {
                Message = "Не удалось обновить меню";
            }
        }
    }
}
