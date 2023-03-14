using System.ComponentModel.DataAnnotations;

namespace RepositorioDeConhecimento.Models.Domain.Entities
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }

        public int IdUsuario { get; set; }
    }
}
