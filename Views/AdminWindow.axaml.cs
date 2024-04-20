using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Diplom.ModelsDTo;
using Diplom.ViewModels;

namespace Diplom;

public partial class AdminWindow : Window
{
    public AdminWindow()
    {
        InitializeComponent();
        DataContext = new AdminWindowViewModel();
    }
    private void NewProduct(object sender, RoutedEventArgs e)
    {
        var window = new NewProductWindow((AdminWindowViewModel)DataContext);
        window.ShowDialog(this);
    }

    private void EditProduct(object sender, RoutedEventArgs e)
    {
        var button = (Button)sender;
        var dataContext = (ProductDTO)button.DataContext;
        var productWindow = new EditProductWindow(dataContext);
        productWindow.ShowDialog(this);
    }
}