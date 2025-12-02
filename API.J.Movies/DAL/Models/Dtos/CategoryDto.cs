using System.ComponentModel.DataAnnotations;

namespace API.J.Movies.DAL.Models.Dtos
{
    public class CategoryDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre de la categoría es obligatorio")]
        [MaxLength(100, ErrorMessage = "El numero maximo de caracteres 100")]
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
