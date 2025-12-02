using System;
using System.Windows;
using Domain;
using Desktop.Services;

namespace Desktop
{
    public partial class UsuarioWindow : Window
    {
        private readonly UsuarioService _service = new UsuarioService();
        private Usuario _usuario;

        // Construtor para novo usuário
        public UsuarioWindow()
        {
            InitializeComponent();
            _usuario = new Usuario();
        }

        // Construtor para edição
        public UsuarioWindow(Usuario usuario) : this()
        {
            _usuario = usuario;
            NomeBox.Text = usuario.Nome;
            SenhaBox.Password = usuario.Senha;
            AtivoBox.IsChecked = usuario.Status;
        }

        private async void Salvar_Click(object sender, RoutedEventArgs e)
        {
            _usuario.Nome = NomeBox.Text;
            _usuario.Senha = SenhaBox.Password;
            _usuario.Status = AtivoBox.IsChecked ?? false;

            try
            {
                if (_usuario.Id == 0)
                {
                    var sucesso = await _service.AddUsuarioAsync(_usuario);
                    if (sucesso)
                    {
                        MessageBox.Show("Usuário cadastrado com sucesso!");
                        DialogResult = true;
                        Close();
                    }
                    else
                    {
                        MessageBox.Show("Erro ao cadastrar usuário.");
                    }
                }
                else
                {
                    await _service.UpdateUsuarioAsync(_usuario);
                    MessageBox.Show("Usuário atualizado com sucesso!");
                    DialogResult = true;
                    Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro: {ex.Message}");
            }
        }

        // Handler do botão Cancelar
        private void Cancelar_Click(object sender, RoutedEventArgs e)
        {
            // Se estiver usando ShowDialog(), pode sinalizar cancelamento:
            DialogResult = false;

            // Fecha a janela
            Close();
        }
    }
}
