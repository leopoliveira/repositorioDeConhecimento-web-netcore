﻿using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RepositorioDeConhecimento.Models.Domain.Entities
{
    public class Imagem : BaseEntity
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório"), StringLength(100)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório"), Column(TypeName = "varbinary(max)")]
        public byte[] Conteudo { get; set; }

        [StringLength(10)]
        public string TipoArquivo { get; set; }

        public int TamanhoArquivo { get; set; }

        public int? ConhecimentoId { get; set; }

        [NotMapped]
        public Conhecimento Conhecimento { get; set; }
    }
}
