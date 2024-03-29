﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RepositorioDeConhecimento.Models.Domain.Entities
{
    public class Categoria : BaseEntity
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório"), StringLength(100)]
        public string Nome { get; set; }

        [StringLength(200)]
        public string? Descricao { get; set; }

        public int? IconeId { get; set; }

        [NotMapped]
        public Imagem Icone { get; set; }
    }
}
