namespace Calculadora.Models
{
    public class Calculadora
    {
        #region Attributes
        private int monto;
        private int interes;
        private int cuotas;
        #endregion

        #region Constructor
        public Calculadora(int monto, int interes, int cuotas)
        {
            this.monto = monto;
            this.interes = interes;
            this.cuotas = cuotas;
        }
        #endregion

        #region Properties
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
                return (Monto * Interes) / 100 + Monto;
            }
        }

        public int CuotaPrimera
        {
            get
            {
                return MontoTotal - (CuotaRestante * Cuotas - 1);
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
