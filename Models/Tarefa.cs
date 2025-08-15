using System.ComponentModel.DataAnnotations;
using TrilhaApiDesafio.Enums;

namespace TrilhaApiDesafio.Models
{
    public class Tarefa
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Titulo { get; set; } = string.Empty;

        public string? Descricao { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Data { get; set; }

        [Required]
        public StatusTarefa Status { get; set; }
    }
}
