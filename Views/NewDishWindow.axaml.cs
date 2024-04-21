using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Diplom.Models;
using Diplom.ModelsDTo;
using Diplom.ViewModels;

namespace Diplom;

public partial class NewDishWindow : Window
{
    public NewDishWindow(AdminWindowViewModel adminWindowViewModel)
    {
        InitializeComponent();
        DataContext = new NewDishWindowViewModel(adminWindowViewModel);
    }
    private void RemoveIngridient(object sender, RoutedEventArgs e)
    {
        var button = (Button)sender;
        var dataContext = (Recipe)button.DataContext;
        var context = (NewDishWindowViewModel)DataContext;
        context.Recipe.Remove(dataContext);
    }
}