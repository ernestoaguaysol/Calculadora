using System.Collections.Generic;
using Calculadora.Models;
using System.Collections.ObjectModel;

namespace Calculadora.ViewModels
{
    public class ResultadoViewModel
    {
        #region Attributes
        private CalculadorPrestamo calculadora;
        #endregion

        #region Properties
        public ObservableCollection<Fecha> Fechas { get; set; }
        #endregion

        #region Constructor
        public ResultadoViewModel(CalculadorPrestamo calculadora)
        {
            this.calculadora = calculadora;

            Fechas = new ObservableCollection<Fecha>();
            LoadFechas(calculadora.GetFechas());
        }
        #endregion

        #region Methods
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
        #endregion

    }
}
