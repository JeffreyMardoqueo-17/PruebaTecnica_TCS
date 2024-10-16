using Microsoft.AspNetCore.Mvc;
using PruebaTecnicaJeffreyDesarrollo.helpers;
using PruebaTecnicaJeffreyDesarrollo.Models;
using System.Collections.Generic;
using System.Data;

namespace PruebaTecnicaJeffreyDesarrollo.Controllers
{
    public class EstadoClienteController : Controller
    {
        private readonly DatabaseUtils _dbUtils;

        //aqui voy a inyectar el DatabaseUtils en el contrusctor auqe es para usar los metodod que ejecutan los sp
        public EstadoClienteController(DatabaseUtils dbUtils)
        {
            _dbUtils = dbUtils;
        }

        // GET
        public ActionResult Index()
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

            return View(estados);
        }

        // GET
        public ActionResult Details(int id)
        {
            var parameters = new Dictionary<string, object>
            {
                { "@Id", id }
            };

            DataTable estadoData = _dbUtils.ExecuteStoredProcedure("SPGetEstadoCliente", parameters);

            if (estadoData.Rows.Count == 0)
            {
                return NotFound();
            }

            DataRow row = estadoData.Rows[0];
            EstadoCliente estado = new EstadoCliente
            {
                Id = Convert.ToInt32(row["Id"]),
                Nombre = row["Nombre"].ToString()
            };

            return View(estado);
        }

   
        public ActionResult Create()
        {
            // Devuelvo un modelo vacio a la vista
            return View(new EstadoCliente());
        }

       
        [HttpPost]
        public ActionResult Create(EstadoCliente estado)
        {
            try
            {
                var parameters = new Dictionary<string, object>
                {
                    { "@Nombre", estado.Nombre }
                };

                _dbUtils.ExecuteNonQuery("SPCreateEstadoCliente", parameters);

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

            DataTable estadoData = _dbUtils.ExecuteStoredProcedure("SPGetEstadoCliente", parameters);

            if (estadoData.Rows.Count == 0)
            {
                return NotFound();
            }

            DataRow row = estadoData.Rows[0];
            EstadoCliente estado = new EstadoCliente
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

                _dbUtils.ExecuteNonQuery("SPDeleteEstadoCliente", parameters);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
