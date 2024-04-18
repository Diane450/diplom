using Avalonia.Controls;
using Avalonia.Interactivity;
using Diplom.Models;
using Diplom.ModelsDTo;
using Diplom.Services;
using System.Threading.Tasks;

namespace Diplom.Views
{
    public partial class AuthorizeWindow : Window
    {
        public AuthorizeWindow()
        {
            InitializeComponent();
        }
        private async void Authorize(object sender, RoutedEventArgs e)
        {
            try
            {
                UserDTO dto = new UserDTO();
                dto.Password = this.Find<TextBox>("Password")!.Text!;
                dto.Login = this.Find<TextBox>("Login")!.Text!;
                User user = await DBCall.Authorize(dto);
                switch (user.UserTypeId)
                {
                    case 0:
                        Label ErrorLabel = this.Find<Label>("ErrorLabel");
                        ErrorLabel.IsVisible = true;
                        ErrorLabel.Content = "Неправильный код";
                        break;
                    case 1:
                        
                        break;
                    case 2:
                        Teacher teacher = await DBCall.GetTeacherData(user);
                        var window = new TeacherWindow(teacher);
                        window.Show();
                        Close();
                        break;
                    case 3:
                        break;
                }
            }
            catch
            {
                Label ErrorLabel = this.Find<Label>("ErrorLabel");
                ErrorLabel.IsVisible = true;
                ErrorLabel.Content = "Ошибка соединения";
            }
        }
    }
}