using ApiRest.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiRest.Repositorio
{
    public interface IProductosEnMemoria
    {
        Task <Producto> DameProductoAsincrono(string SKU);
        Task <IEnumerable<Producto>> DameProductosAsincrono();
        Task CrearProductoAsincrono(Producto p);
        Task ModificarProductoAsincrono(Producto p);
        Task BorrarProductoAsincrono(string SKU);

    }
}
