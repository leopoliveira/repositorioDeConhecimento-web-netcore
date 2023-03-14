using System.ComponentModel.DataAnnotations;

namespace RepositorioDeConhecimento.Models.ViewModels
{
    public class RegistrarUsuarioViewModel
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public string Usuario { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmação de Senha")]
        [Compare("Senha", ErrorMessage = "As senhas não conferem.")]
        public string ConfirmacaoSenha { get; set; }
    }
}
