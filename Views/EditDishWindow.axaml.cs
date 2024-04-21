using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Diplom.Models;
using Diplom.ModelsDTo;
using Diplom.ViewModels;

namespace Diplom;

public partial class EditDishWindow : Window
{
    public EditDishWindow(AdminWindowViewModel adminWindowViewModel, DishDTO dishDTO)
    {
        InitializeComponent();
        DataContext = new EditDishWindowViewModel(adminWindowViewModel, dishDTO);
    }

    private void RemoveIngridient(object sender, RoutedEventArgs e)
    {
        var button = (Button)sender;
        var dataContext = (Recipe)button.DataContext;
        var context = (EditDishWindowViewModel)DataContext;
        context.Recipe.Remove(dataContext);
    }

}