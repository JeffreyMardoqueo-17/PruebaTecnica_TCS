using Microsoft.AspNetCore.Mvc;
using PruebaTecnicaJeffreyDesarrollo.helpers;
using PruebaTecnicaJeffreyDesarrollo.Models;
using System.Collections.Generic;
using System.Data;

namespace PruebaTecnicaJeffreyDesarrollo.Controllers
{
    public class ProductoController : Controller
    {
        private readonly DatabaseUtils _dbUtils;

        public ProductoController(DatabaseUtils dbUtils)
        {
            _dbUtils = dbUtils;
        }

        // GET: ProductoController
        public ActionResult Index(string filtro = "todos")
        {
            DataTable productosData = null;

            if (filtro == "disponibles")
            {
                productosData = _dbUtils.ExecuteStoredProcedure("SPGetProductosDisponibles", null);
            }
            else if (filtro == "agotados")
            {
                productosData = _dbUtils.ExecuteStoredProcedure("SPGetProductosAgotados", null);
            }
            else
            {
                productosData = _dbUtils.ExecuteStoredProcedure("SPGetProductos", null);
            }

            List<Producto> productos = new List<Producto>();

            foreach (DataRow row in productosData.Rows)
            {
                productos.Add(new Producto
                {
                    Id = Convert.ToInt32(row["Id"]),
                    Nombre = row["Nombre"].ToString(),
                    Descripcion = row["Descripcion"].ToString(),
                    Precio = Convert.ToDecimal(row["Precio"]),
                    Cantidad = Convert.ToInt32(row["Cantidad"]),
                    IdEstadoProducto = Convert.ToInt32(row["IdEstadoProducto"]) 
                });
            }

            ViewBag.FiltroActual = filtro; 
            return View(productos);
        }

        // GET: ProductoController/Details/5
        public ActionResult Details(int id)
        {
            var parameters = new Dictionary<string, object> { { "@Id", id } };
            DataTable productoData = _dbUtils.ExecuteStoredProcedure("SPGetProductos", parameters);

            if (productoData.Rows.Count == 0)
            {
                return NotFound();
            }

            DataRow row = productoData.Rows[0];
            Producto producto = new Producto
            {
                Id = Convert.ToInt32(row["Id"]),
                Nombre = row["Nombre"].ToString(),
                Descripcion = row["Descripcion"].ToString(),
                Precio = Convert.ToDecimal(row["Precio"]),
                Cantidad = Convert.ToInt32(row["Cantidad"]),
                IdEstadoProducto = Convert.ToInt32(row["IdEstadoProducto"]) 
            };

            return View(producto);
        }

        // GET: ProductoController/Create
        public ActionResult Create()
        {
            ViewBag.EstadosProducto = ObtenerEstadosProducto();
            return View();
        }

        // POST: ProductoController/Create
        [HttpPost]
        public ActionResult Create(Producto producto)
        {
            try
            {
                // Si la cantidad es 0 o nula, asigna el estado "Agotado"
                producto.IdEstadoProducto = producto.Cantidad <= 0 ? 2 : 1;

                var parameters = new Dictionary<string, object>
                {
                    { "@Nombre", producto.Nombre },
                    { "@Descripcion", producto.Descripcion },
                    { "@Precio", producto.Precio },
                    { "@Cantidad", producto.Cantidad },
                    { "@IdEstadoProducto", producto.IdEstadoProducto }
                };

                _dbUtils.ExecuteNonQuery("SPCreateProducto", parameters);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewBag.EstadosProducto = ObtenerEstadosProducto();
                return View(producto);
            }
        }

        // GET: ProductoController/Edit/5
        public ActionResult Edit(int id)
        {
            var parameters = new Dictionary<string, object> { { "@Id", id } };
            DataTable productoData = _dbUtils.ExecuteStoredProcedure("SPGetProductos", parameters);

            if (productoData.Rows.Count == 0)
            {
                return NotFound();
            }

            DataRow row = productoData.Rows[0];
            Producto producto = new Producto
            {
                Id = Convert.ToInt32(row["Id"]),
                Nombre = row["Nombre"].ToString(),
                Descripcion = row["Descripcion"].ToString(),
                Precio = Convert.ToDecimal(row["Precio"]),
                Cantidad = Convert.ToInt32(row["Cantidad"]),
                IdEstadoProducto = Convert.ToInt32(row["IdEstadoProducto"]) 
            };

            ViewBag.EstadosProducto = ObtenerEstadosProducto();
            return View(producto);
        }

        // POST: ProductoController/Edit/5
        [HttpPost]
        public ActionResult Edit(Producto producto)
        {
            try
            {
                // Si la cantidad es 0 o nula, asigna el estado "Agotado"
                producto.IdEstadoProducto = producto.Cantidad <= 0 ? 2 : 1;

                var parameters = new Dictionary<string, object>
                {
                    { "@Id", producto.Id },
                    { "@Nombre", producto.Nombre },
                    { "@Descripcion", producto.Descripcion },
                    { "@Precio", producto.Precio },
                    { "@Cantidad", producto.Cantidad },
                    { "@IdEstadoProducto", producto.IdEstadoProducto }
                };

                _dbUtils.ExecuteNonQuery("SPUpdateProducto", parameters);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewBag.EstadosProducto = ObtenerEstadosProducto();
                return View(producto);
            }
        }

        // GET: ProductoController/Delete/5
        public ActionResult Delete(int id)
        {
            var parameters = new Dictionary<string, object> { { "@Id", id } };
            DataTable productoData = _dbUtils.ExecuteStoredProcedure("SPGetProductos", parameters);

            if (productoData.Rows.Count == 0)
            {
                return NotFound();
            }

            DataRow row = productoData.Rows[0];
            Producto producto = new Producto
            {
                Id = Convert.ToInt32(row["Id"]),
                Nombre = row["Nombre"].ToString(),
                Descripcion = row["Descripcion"].ToString(),
                Precio = Convert.ToDecimal(row["Precio"]),
                Cantidad = Convert.ToInt32(row["Cantidad"]),
                IdEstadoProducto = Convert.ToInt32(row["IdEstadoProducto"]) 
            };

            return View(producto);
        }

        // POST: ProductoController/Delete/5
        [HttpPost]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                var parameters = new Dictionary<string, object> { { "@Id", id } };
                _dbUtils.ExecuteNonQuery("SPDeleteProducto", parameters);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        private List<EstadoProducto> ObtenerEstadosProducto()
        {
            DataTable estadosData = _dbUtils.ExecuteStoredProcedure("SPGetEstadoProducto", null);
            List<EstadoProducto> estados = new List<EstadoProducto>();

            foreach (DataRow row in estadosData.Rows)
            {
                estados.Add(new EstadoProducto
                {
                    Id = Convert.ToInt32(row["Id"]),
                    Nombre = row["Nombre"].ToString()
                });
            }

            return estados;
        }
    }
}
