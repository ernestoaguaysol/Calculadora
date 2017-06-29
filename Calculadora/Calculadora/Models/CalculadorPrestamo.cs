using System;
using System.Collections.Generic;

namespace Calculadora.Models
{
    public class CalculadorPrestamo
    {
        #region Attributes
        private int monto;
        private int interes;
        private int cuotas;
        private int dias;
        #endregion

        #region Constructor
        public CalculadorPrestamo(int monto, int interes, int cuotas)
        {
            this.monto = monto;
            this.interes = interes;
            this.cuotas = cuotas;
        }
        #endregion

        #region Properties
        public int Dias
        {
            get { return dias; }
            set { dias = value; }
        }

        public int Monto
        {
            get { return monto; }
            set { monto = value; }
        }

        public int Interes
        {
            get { return interes; }
            set { interes = value; }
        }

        public int Cuotas
        {
            get { return cuotas; }
            set { cuotas = value; }
        }

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
            for (int i = 0; i < cuotas; i++)
            {       
                fecha = fecha.AddDays(dias);
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
