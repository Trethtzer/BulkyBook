using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BulkyBook.Models
{
    public class CoverType
    {
        // Si no se especifica nada, busca una linea q sea Id y la convierte en primary key. Si no hay, no crea primary keey
        public int Id { get; set; }

        [Display(Name="Cover Name")]
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

    }
}
