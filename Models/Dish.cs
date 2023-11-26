#pragma warning disable CS8618

using System.ComponentModel.DataAnnotations;

namespace CrudDelicious.Models
{
    public class Dish
    {
        [Key]
        public int DishId { get; set; }

        [Required(ErrorMessage = "El nombre es requerido")]
        [MaxLength(45, ErrorMessage = "El largo máximo es de 45 caracteres")]
        [Display(Name="Name of Dish")]
        public string Name { get; set; }

        [Required(ErrorMessage = "El Chef es requerido")]
        [MaxLength(45, ErrorMessage = "El largo máximo es de 45 caracteres")]
        [Display(Name="Chef's Name")]
        public string Chef { get; set; }

        [Required(ErrorMessage = "El sabor es requerido")]
        // [Range(1, 5, ErrorMessage = "El sabor puede estar entre 1 y 5")]
        [RangeTastiness]
        public int Tastiness { get; set; }

        [Required(ErrorMessage = "Las calorias son requeridas")]
        [MoreThanCero]
        [Display(Name="# of Calories")]
        public int Calories { get; set; }

        [Required(ErrorMessage = "La descripción es requerida")]
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }

    public class RangeTastinessAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is int tastiness)
            {
                if (tastiness < 1 || tastiness > 5)
                {
                    return new ValidationResult("El valor debe ser entre 1 y 5");
                }
            }
            return ValidationResult.Success;
        }
    }

    public class MoreThanCeroAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is int calories)
            {
                if (calories < 1)
                {
                    return new ValidationResult("El valor debe ser superior a 0");
                }
            }
            return ValidationResult.Success;
        }
    }
}