using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Diplom.ViewModels;

namespace Diplom;

public partial class NewProductWindow : Window
{
    public NewProductWindow(AdminWindowViewModel model)
    {
        InitializeComponent();
        DataContext = new NewProductWindowViewModel(model);
    }
}