using System.ComponentModel.DataAnnotations;

namespace API.J.Movies.DAL.Models.Dtos
{
    public class CategoryCreateDto
    {
        [Required(ErrorMessage = "El nombre de la categoría es obligatorio")]
        [MaxLength(100, ErrorMessage = "El numero maximo de caracteres es 100")]
        public string Name { get; set; }
    }
}
