using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using App_Bancaria.Data;
using App_Bancaria.Models;
using App_Bancaria.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace App_Bancaria.Controllers
{
    public class CuentaController : Controller
    {
        private readonly ILogger<CuentaController> _logger;
        private readonly ApplicationDbContext _context;

        public CuentaController(ILogger<CuentaController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        
        public IActionResult Index()
        {
            var misCuentas = _context.Cuentas.ToList();
            _logger.LogDebug("Cuentas: {misCuentas}", misCuentas);

            var viewModel = new CuentaViewModel
            {
                FormCuenta = new Cuenta(),
                ListCuenta = misCuentas
            };

            _logger.LogDebug("ViewModel: {viewModel}", viewModel);
            return View(viewModel);
        }

        
        [HttpPost]
        public IActionResult Registrar(CuentaViewModel viewModel)
        {
            var cuenta = new Cuenta
            {
                NombreTitular = viewModel.FormCuenta.NombreTitular,
                TipoCuenta = viewModel.FormCuenta.TipoCuenta,
                SaldoInicial = viewModel.FormCuenta.SaldoInicial,
                Email = viewModel.FormCuenta.Email,
            };

            _context.Cuentas.Add(cuenta);
            _context.SaveChanges();

            ViewData["Message"] = "Cuenta Bancaria Registrada Exitosamente";

            var misCuentas = _context.Cuentas.ToList();
            viewModel.ListCuenta = misCuentas;

            return View("Index", viewModel);
        }

    
        public IActionResult Listar()
        {
            var misCuentas = _context.Cuentas.ToList(); 
            var viewModel = new CuentaViewModel
            {
                ListCuenta = misCuentas
            };

            return View(viewModel);  
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}
