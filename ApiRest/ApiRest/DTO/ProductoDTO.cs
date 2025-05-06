using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiRest.DTO
{
    public class ProductoDTO
    {
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Descripcion { get; set; }
        [Required]
        [Range(5, 100,
        ErrorMessage = "Valor {0} debe estar entre {1} y {2}.")]
        public double Precio { get; set; }
        [Required]
        public string SKU { get; init; }
    }
}
