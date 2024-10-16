using Microsoft.AspNetCore.Mvc;
using PruebaTecnicaJeffreyDesarrollo.helpers;
using PruebaTecnicaJeffreyDesarrollo.Models;
using System.Collections.Generic;
using System.Data;

namespace PruebaTecnicaJeffreyDesarrollo.Controllers
{
    public class ClienteController : Controller
    {
        private readonly DatabaseUtils _dbUtils;

        public ClienteController(DatabaseUtils dbUtils)
        {
            _dbUtils = dbUtils;
        }

        public ActionResult Index()
        {
            DataTable clientesData = _dbUtils.ExecuteStoredProcedure("SPGetCliente", null);
            List<Cliente> clientes = new List<Cliente>();

            foreach (DataRow row in clientesData.Rows)
            {
                clientes.Add(new Cliente
                {
                    Id = Convert.ToInt32(row["Id"]),
                    Nombre = row["Nombre"].ToString(),
                    Apellido = row["Apellido"].ToString(),
                    Telefono = row["Telefono"].ToString(),
                    Correo = row["Correo"].ToString(),
                    Pass = row["Pass"].ToString(),
                    IdEstadoCliente = row.Table.Columns.Contains("IdEstadoCliente") ? Convert.ToInt32(row["IdEstadoCliente"]) : 0
                });
            }

            return View(clientes);
        }



        public ActionResult Details(int id)
        {
            var parameters = new Dictionary<string, object>
            {
                { "@Id", id }
            };

            DataTable clienteData = _dbUtils.ExecuteStoredProcedure("SPGetCliente", parameters);

            if (clienteData.Rows.Count == 0)
            {
                return NotFound();
            }

            DataRow row = clienteData.Rows[0];
            Cliente cliente = new Cliente
            {
                Id = Convert.ToInt32(row["Id"]),
                Nombre = row["Nombre"].ToString(),
                Apellido = row["Apellido"].ToString(),
                Telefono = row["Telefono"].ToString(),
                Correo = row["Correo"].ToString(),
                Pass = row["Pass"].ToString(),
                IdEstadoCliente = Convert.ToInt32(row["IdEstadoCliente"])
            };

            return View(cliente);
        }

        private List<EstadoCliente> ObtenerEstadosCliente()
        {
            DataTable estadosData = _dbUtils.ExecuteStoredProcedure("SPGetEstadoCliente", null);
            List<EstadoCliente> estados = new List<EstadoCliente>();

            foreach (DataRow row in estadosData.Rows)
            {
                estados.Add(new EstadoCliente
                {
                    Id = Convert.ToInt32(row["Id"]),
                    Nombre = row["Nombre"].ToString()
                });
            }

            return estados;
        }

        public ActionResult Create()
        {
            ViewBag.EstadosCliente = ObtenerEstadosCliente();
            return View(new Cliente());
        }

        [HttpPost]
        public ActionResult Create(Cliente cliente)
        {
            try
            {
                var parameters = new Dictionary<string, object>
        {
            { "@Nombre", cliente.Nombre },
            { "@Apellido", cliente.Apellido },
            { "@Telefono", cliente.Telefono },
            { "@Correo", cliente.Correo },
            { "@Pass", cliente.Pass },
            { "@IdEstadoCliente", 1 }  //asigno el uno por fefecto que es el de Activo
        };

                _dbUtils.ExecuteNonQuery("SPCreateCliente", parameters);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewBag.EstadosCliente = ObtenerEstadosCliente(); 
                return View();
            }
        }


        public ActionResult Edit(int id)
        {
            var parameters = new Dictionary<string, object>
            {
                { "@Id", id }
            };

            DataTable clienteData = _dbUtils.ExecuteStoredProcedure("SPGetCliente", parameters);

            if (clienteData.Rows.Count == 0)
            {
                return NotFound();
            }

            DataRow row = clienteData.Rows[0];
            Cliente cliente = new Cliente
            {
                Id = Convert.ToInt32(row["Id"]),
                Nombre = row["Nombre"].ToString(),
                Apellido = row["Apellido"].ToString(),
                Telefono = row["Telefono"].ToString(),
                Correo = row["Correo"].ToString(),
                Pass = row["Pass"].ToString(),
                IdEstadoCliente = Convert.ToInt32(row["IdEstadoCliente"])
            };

            ViewBag.EstadosCliente = ObtenerEstadosCliente();
            return View(cliente);
        }

        [HttpPost]
        public ActionResult Edit(Cliente cliente)
        {
            try
            {
                var parameters = new Dictionary<string, object>
                {
                    { "@Id", cliente.Id },
                    { "@Nombre", cliente.Nombre },
                    { "@Apellido", cliente.Apellido },
                    { "@Telefono", cliente.Telefono },
                    { "@Correo", cliente.Correo },
                    { "@Pass", cliente.Pass },
                    { "@IdEstadoCliente", cliente.IdEstadoCliente }
                };

                _dbUtils.ExecuteNonQuery("SPUpdateCliente", parameters);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewBag.EstadosCliente = ObtenerEstadosCliente();  // Volver a cargar estados en caso de error
                return View();
            }
        }


        //PARA ELIMINAR
        public ActionResult Delete(int id)
        {
            var parameters = new Dictionary<string, object>
            {
                { "@Id", id }
            };

            DataTable clienteData = _dbUtils.ExecuteStoredProcedure("SPGetCliente", parameters);

            if (clienteData.Rows.Count == 0)
            {
                return NotFound();
            }

            DataRow row = clienteData.Rows[0];
            Cliente cliente = new Cliente
            {
                Id = Convert.ToInt32(row["Id"]),
                Nombre = row["Nombre"].ToString(),
                Apellido = row["Apellido"].ToString(),
                Telefono = row["Telefono"].ToString(),
                Correo = row["Correo"].ToString(),
                Pass = row["Pass"].ToString(),
                IdEstadoCliente = Convert.ToInt32(row["IdEstadoCliente"])
            };

            return View(cliente);
        }
        
        [HttpPost]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                var parameters = new Dictionary<string, object>
                {
                    { "@Id", id }
                };

                _dbUtils.ExecuteNonQuery("SPDeleteCliente", parameters);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
