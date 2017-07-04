using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calculadora.Models;

namespace Calculadora.ViewModels
{
    public class ResultadoViewModel
    {
        private CalculadorPrestamo calculadora;

        public ResultadoViewModel(CalculadorPrestamo calculadora)
        {
            this.calculadora = calculadora;
        }

    }
}
