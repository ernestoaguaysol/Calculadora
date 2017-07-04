using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calculadora.Models;
using System.Collections.ObjectModel;

namespace Calculadora.ViewModels
{
    public class ResultadoViewModel
    {
        private CalculadorPrestamo calculadora;

        public ObservableCollection<Fecha> Fechas { get; set; }

        public ResultadoViewModel(CalculadorPrestamo calculadora)
        {
            this.calculadora = calculadora;

            Fechas = new ObservableCollection<Fecha>();
            LoadFechas(calculadora.GetFechas());
        }

        private void LoadFechas(List<Fecha> fechas)
        {
            Fechas.Clear();
            foreach (var fecha in fechas)
            {
                Fechas.Add(new Fecha
                {
                    DateTime = fecha.DateTime,
                });
            }
        }

    }
}
