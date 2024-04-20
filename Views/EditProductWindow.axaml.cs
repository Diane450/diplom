using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Diplom.ModelsDTo;
using Diplom.ViewModels;

namespace Diplom;

public partial class EditProductWindow : Window
{
    public EditProductWindow(ProductDTO productDTO)
    {
        InitializeComponent();
        DataContext = new EditProductViewModel(productDTO);
    }
}