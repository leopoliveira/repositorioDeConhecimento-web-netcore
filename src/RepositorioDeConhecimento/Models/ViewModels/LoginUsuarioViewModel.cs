using System.ComponentModel.DataAnnotations;

namespace RepositorioDeConhecimento.Models.ViewModels
{
    public class LoginUsuarioViewModel
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }

        public bool LembrarMe { get; set; }

        public string redirectURl { get; set; }
    }
}
