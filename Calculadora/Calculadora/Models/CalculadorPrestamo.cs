using System;
using System.Collections.Generic;

namespace Calculadora.Models
{
    public class CalculadorPrestamo
    {
        #region Properties
        public Forma Forma { get; set; }

        public int Monto { get; set; }

        public int Interes { get; set; }

        public int Cuotas { get; set; }

        public int MontoTotal
        {
            get
            {
                if (Interes == 0)
                {
                    return Monto / 100 + Monto;
                }
                else
                {
                    return (Monto * Interes) / 100 + Monto;
                }
            }
        }

        public int CuotaPrimera
        {
            get
            {
                return MontoTotal - (CuotaRestante * (Cuotas - 1));
            }
        }

        public int CuotaRestante
        {
            get
            {
                double diferencia = ValorCuotaDouble() - ValorCuotaInt();
                if (diferencia < 0.5)
                {
                    return ValorCuotaInt();
                }
                else
                {
                    return ValorCuotaInt() + 1;
                }
            }
        }

        public List<Fecha> GetFechas()
        {
            var fechas = new List<Fecha>();
            var fecha = DateTime.Now;
            for (int i = 0; i < Cuotas; i++)
            {       
                fecha = fecha.AddDays(Forma.Dias);
                fechas.Add(new Fecha
                {
                    DateTime = fecha
                });
            }
            return fechas;
        }
        #endregion

        #region Methods
        private int ValorCuotaInt()
        {
            return MontoTotal / Cuotas;
        }

        private double ValorCuotaDouble()
        {
            double montoTotal = MontoTotal;
            double cuotas = Cuotas;
            return montoTotal / cuotas;
        }
        #endregion
    }
}
