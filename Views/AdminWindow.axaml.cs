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

    private void EditMenu(object sender, RoutedEventArgs e)
    {
        var button = (Button)sender;
        var dataContext = (MenuDTO)button.DataContext;
        var menuWindow = new EditMenuWindow(dataContext);
        menuWindow.ShowDialog(this);
    }

    private void NewMenu(object sender, RoutedEventArgs e)
    {
        var window = new NewMenuWindow((AdminWindowViewModel)DataContext);
        window.ShowDialog(this);
    }
    private void DeleteMenu(object sender, RoutedEventArgs e)
    {
        var button = (Button)sender;
        var dataContext = (MenuDTO)button.DataContext;
        var menuWindow = new DeleteMenuWindow(dataContext, (AdminWindowViewModel)DataContext);
        menuWindow.ShowDialog(this);
    }
    
    private void NewDish(object sender, RoutedEventArgs e)
    {
        var window = new NewDishWindow((AdminWindowViewModel)DataContext);
        window.ShowDialog(this);
    }

    private void EditDish(object sender, RoutedEventArgs e)
    {
        var button = (Button)sender;
        var dataContext = (DishDTO)button.DataContext;
        var window = new EditDishWindow((AdminWindowViewModel)DataContext, dataContext);
        window.ShowDialog(this);
    }
}