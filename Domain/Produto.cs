using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; }   
        public float Preco { get; set; }   
        public bool Status { get; set; }   
        public int IdUsuarioCadastro { get; set; }
        public int? IdUsuarioUpdate { get; set; }

        // Propriedades de navegação
        public Usuario? UsuarioCadastro { get; set; }
        public Usuario? UsuarioUpdate { get; set; }

    }

}
