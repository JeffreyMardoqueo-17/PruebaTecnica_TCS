using Microsoft.AspNetCore.Mvc;
using PruebaTecnicaJeffreyDesarrollo.helpers;
using PruebaTecnicaJeffreyDesarrollo.Models;
using System.Collections.Generic;
using System.Data;

namespace PruebaTecnicaJeffreyDesarrollo.Controllers
{
    public class EstadoProductoController : Controller
    {
        private readonly DatabaseUtils _dbUtils;

        // Inyecto de neuvi el DatabaseUtils al contructor
        public EstadoProductoController(DatabaseUtils dbUtils)
        {
            _dbUtils = dbUtils;
        }

        public ActionResult Index()
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

            return View(estados);
        }

        public ActionResult Details(int id)
        {
            var parameters = new Dictionary<string, object>
            {
                { "@Id", id }
            };

            DataTable estadoData = _dbUtils.ExecuteStoredProcedure("SPGetEstadoProducto", parameters);

            if (estadoData.Rows.Count == 0)
            {
                return NotFound();
            }

            DataRow row = estadoData.Rows[0];
            EstadoProducto estado = new EstadoProducto
            {
                Id = Convert.ToInt32(row["Id"]),
                Nombre = row["Nombre"].ToString()
            };

            return View(estado);
        }

        // GET: EstadoProductoController/Create (Mostrar formulario para crear)
        public ActionResult Create()
        {
            return View();
        }

        // POST: EstadoProductoController/Create (Crear nuevo estado de producto)
        [HttpPost]
        public ActionResult Create(EstadoProducto estado)
        {
            try
            {
                var parameters = new Dictionary<string, object>
                {
                    { "@Nombre", estado.Nombre }
                };

                _dbUtils.ExecuteNonQuery("SPCreateEstadoProducto", parameters);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

  
        public ActionResult Delete(int id)
        {
            var parameters = new Dictionary<string, object>
            {
                { "@Id", id }
            };

            DataTable estadoData = _dbUtils.ExecuteStoredProcedure("SPGetEstadoProducto", parameters);

            if (estadoData.Rows.Count == 0)
            {
                return NotFound();
            }

            DataRow row = estadoData.Rows[0];
            EstadoProducto estado = new EstadoProducto
            {
                Id = Convert.ToInt32(row["Id"]),
                Nombre = row["Nombre"].ToString()
            };

            return View(estado);
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

                _dbUtils.ExecuteNonQuery("SPDeleteEstadoProducto", parameters);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
