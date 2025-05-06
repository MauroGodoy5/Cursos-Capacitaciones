using ApiRest.DTO;
using ApiRest.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiRest
{
    public static class Utilidades
    {
        public static ProductoDTO convertirDTO(this Producto p)
        {
            if (p != null)
            {
                return new ProductoDTO
                {
                    Nombre = p.Nombre,
                    Descripcion = p.Descripcion,
                    Precio = p.Precio,
                    SKU = p.SKU,

                };
            }

            return null;
        }

        public static UsuarioDTO convertirDTO(this UsuarioAPI u) {

            if (u != null)
            {

                return new UsuarioDTO
                {

                    Token = u.Token,
                    Usuario = u.Usuario,

                };

            }
            return null;
        
        }
     
    }
}
