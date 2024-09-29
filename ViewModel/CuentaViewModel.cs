using System;
using System.Collections.Generic;
using App_Bancaria.Models;

namespace App_Bancaria.ViewModel
{
    public class CuentaViewModel
    {
        public Cuenta? FormCuenta { get; set; } 
        public IEnumerable<Cuenta>? ListCuenta { get; set; } 
    }
}
