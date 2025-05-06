using ApiRest.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiRest.Repositorio
{
    public class ProductosEnMemoria: IProductosEnMemoria
    {
        private readonly List<Producto> productos = new()
        {
            new Producto { Id = 1, Nombre = "Martillo", Descripcion = "Martillo super preciso", Precio = 12.99, FechaAlta = DateTime.Now, SKU = "MART01" },
            new Producto { Id = 2, Nombre = "Caja de clavos", Descripcion = "100 unidades de clavos", Precio = 10, FechaAlta = DateTime.Now, SKU = "CLAV01" },
            new Producto { Id = 3, Nombre = "Destornillador", Descripcion = "Excelente destornillador ", Precio = 9.99, FechaAlta = DateTime.Now, SKU = "DEST01" },
            new Producto { Id = 4, Nombre = "Bombilla", Descripcion = "Bombilla muy lumiinosa", Precio = 3, FechaAlta = DateTime.Now, SKU = "BOMB01" },
        };


        public async Task <IEnumerable<Producto>> DameProductosAsincrono()
        {
            return await Task.FromResult(productos);
        }

        public async Task <Producto> DameProductoAsincrono(string SKU)
        {
            return await Task.FromResult(productos.Where(p => p.SKU == SKU).SingleOrDefault());
        }

        public async Task CrearProductoAsincrono(Producto p)
        {
            productos.Add(p);
            await Task.CompletedTask;
        }

        public async Task ModificarProductoAsincrono(Producto p)
        {
            int indice = productos.FindIndex(existeProducto => existeProducto.Id == p.Id);
            productos[indice] = p;
            await Task.CompletedTask;
        }

        public async Task BorrarProductoAsincrono(string SKU)
        {
            int indice = productos.FindIndex(existeProducto => existeProducto.SKU == SKU);
            productos.RemoveAt(indice);

            await Task.CompletedTask;
        }
    }
}
