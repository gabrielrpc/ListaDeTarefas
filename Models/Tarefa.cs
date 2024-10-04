using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace ListaDeTarefas.Models
{
    public class Tarefa
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Necessário informar uma descrição!")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "Informe a data de vencimento da tarefa!")]
        public DateTime? DataVencimento { get; set; }

        [Required(ErrorMessage = "Selecione uma categoria!")]
        public string CategoriaId { get; set; }

        [ValidateNever]
        public Categoria Categoria { get; set; }

        [Required(ErrorMessage = "Selecione um status!")]
        public string StatusId { get; set; }

        [ValidateNever]
        public Status Status { get; set; }

        public bool Atrasado => StatusId == "pendente" && DataVencimento < DateTime.Today;

    }
}
