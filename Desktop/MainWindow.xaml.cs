using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Domain;
using Desktop.Services;
using System.Windows;

namespace Desktop
{
    public partial class MainWindow : Window
    {
        private readonly UsuarioService _service = new UsuarioService();

        public MainWindow()
        {
            InitializeComponent();
            LoadUsuarios();
        }

        private async void LoadUsuarios()
        {
            var usuarios = await _service.GetUsuariosAsync();
            UsuariosGrid.ItemsSource = usuarios;
        }

        private void Adicionar_Click(object sender, RoutedEventArgs e)
        {
            var win = new UsuarioWindow();
            if (win.ShowDialog() == true)
            {
                LoadUsuarios(); // recarrega a lista após salvar
            }
        }

        private void Editar_Click(object sender, RoutedEventArgs e)
        {
            if (UsuariosGrid.SelectedItem is Usuario usuario)
            {
                var win = new UsuarioWindow(usuario);
                if (win.ShowDialog() == true)
                {
                    LoadUsuarios(); // recarrega a lista após salvar
                }
            }
        }


        private async void Excluir_Click(object sender, RoutedEventArgs e)
        {
            if (UsuariosGrid.SelectedItem is Usuario usuario)
            {
                await _service.DeleteUsuarioAsync(usuario.Id);
                LoadUsuarios();
            }
        }
    }
}
