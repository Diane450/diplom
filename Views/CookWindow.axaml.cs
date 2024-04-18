using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Diplom.Models;
using Diplom.ModelsDTo;
using Diplom.ViewModels;

namespace Diplom;

public partial class CookWindow : Window
{
    public CookWindow()
    {
        InitializeComponent();
        DataContext = new CookWindowViewModel();
    }
    private void GetDetails(object sender, RoutedEventArgs e)
    {
        var button = (Button)sender;
        var dataContext = (MenuDish)button.DataContext;
        var requestWindow = new DishDetailsWindow(dataContext);
        requestWindow.ShowDialog(this);
    }
}