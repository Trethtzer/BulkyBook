using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BulkyBook.Models
{
    public class ShoppingCart
    {        
        // Si no se especifica nada, busca una linea q sea Id y la convierte en primary key. Si no hay, no crea primary keey
        public int Id { get; set; }

        public string ApplicationUserId { get; set; }

        [ForeignKey("ApplicationUserId")]
        public ApplicationUser ApplicationUser { get; set; }

        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        [Range(1,1000,ErrorMessage = "Please enter a value between 1 and 1000")]
        public int Count { get; set; }

        [NotMapped] // This will depend on the number of products (products * count)
        public double Price { get; set; }


        public ShoppingCart()
        {
            Count = 1;
        }
    }
}
