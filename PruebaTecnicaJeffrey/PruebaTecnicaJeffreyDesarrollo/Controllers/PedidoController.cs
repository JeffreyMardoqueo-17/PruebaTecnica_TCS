using Microsoft.AspNetCore.Mvc;
using PruebaTecnicaJeffreyDesarrollo.helpers;
using PruebaTecnicaJeffreyDesarrollo.Models;
using System;
using System.Collections.Generic;
using System.Data;

namespace PruebaTecnicaJeffreyDesarrollo.Controllers
{
    public class PedidoController : Controller
    {
        private readonly DatabaseUtils _dbUtils;

        public PedidoController(DatabaseUtils dbUtils)
        {
            _dbUtils = dbUtils;
        }

        // GET: PedidoController
        public ActionResult Index()
        {
            DataTable pedidosData = _dbUtils.ExecuteStoredProcedure("SPGetPedidos", null);
            List<Pedido> pedidos = new List<Pedido>();

            foreach (DataRow row in pedidosData.Rows)
            {
                pedidos.Add(new Pedido
                {
                    Id = Convert.ToInt32(row["PedidoId"]),
                    ClienteNombre = row["Cliente"].ToString(),
                    ProductoNombre = row["Producto"].ToString(),
                    Precio = Convert.ToDecimal(row["Precio"]),
                    Cantidad = Convert.ToInt32(row["Cantidad"]),
                    Total = Convert.ToDecimal(row["Total"]),
                    FechaPedido = Convert.ToDateTime(row["FechaPedido"])
                });
            }

            return View(pedidos);
        }

        // GET: PedidoController/Create
        public ActionResult Create()
        {
            ViewBag.Clientes = ObtenerClientes();
            ViewBag.Productos = ObtenerProductosDisponibles();
            return View();
        }

        // POST: PedidoController/Create
        [HttpPost]
        public ActionResult Create(Pedido pedido)
        {
            try
            {
                var parameters = new Dictionary<string, object>
                {
                    { "@IdCliente", pedido.IdCliente },
                    { "@IdProducto", pedido.IdProducto },
                    { "@Cantidad", pedido.Cantidad }
                };

                DataTable result = _dbUtils.ExecuteStoredProcedure("SPCrearPedido", parameters);

                string message = result.Rows[0][0].ToString();

                if (message == "Pedido creado exitosamente")
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.ErrorMessage = message;  // Mostrar mensaje de error si la cantidad es insuficiente
                    ViewBag.Clientes = ObtenerClientes();
                    ViewBag.Productos = ObtenerProductosDisponibles();
                    return View(pedido);
                }
            }
            catch
            {
                ViewBag.Clientes = ObtenerClientes();
                ViewBag.Productos = ObtenerProductosDisponibles();
                return View(pedido);
            }
        }

        // Método auxiliar para obtener clientes
        private List<Cliente> ObtenerClientes()
        {
            DataTable clientesData = _dbUtils.ExecuteStoredProcedure("SPGetCliente", null);
            List<Cliente> clientes = new List<Cliente>();

            foreach (DataRow row in clientesData.Rows)
            {
                clientes.Add(new Cliente
                {
                    Id = Convert.ToInt32(row["Id"]),
                    Nombre = row["Nombre"].ToString() + " " + row["Apellido"].ToString()
                });
            }

            return clientes;
        }

        // Método auxiliar para obtener productos disponibles
        private List<Producto> ObtenerProductosDisponibles()
        {
            DataTable productosData = _dbUtils.ExecuteStoredProcedure("SPGetProductosDisponibles", null);
            List<Producto> productos = new List<Producto>();

            foreach (DataRow row in productosData.Rows)
            {
                productos.Add(new Producto
                {
                    Id = Convert.ToInt32(row["Id"]),
                    Nombre = row["Nombre"].ToString(),
                    Precio = Convert.ToDecimal(row["Precio"]),
                    Cantidad = Convert.ToInt32(row["Cantidad"])
                });
            }

            return productos;
        }
    }
}
