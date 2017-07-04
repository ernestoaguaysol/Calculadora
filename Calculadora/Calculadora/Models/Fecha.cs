using System;

namespace Calculadora.Models
{
    public class Fecha
    {
        #region Properties
        public DateTime DateTime { get; set; }

        public string Dia
        {
            get
            {
                var dia = DiaSemana(DateTime.DayOfWeek.ToString());
                return dia;
            }
        }
        #endregion

        #region Methods
        private string DiaSemana(string diaSemana)
        {
            string dia = string.Empty;
            switch (diaSemana)
            {
                case "Sunday":
                    dia = "Domingo";
                    break;
                case "Monday":
                    dia = "Lunes";
                    break;
                case "Tuesday":
                    dia = "Martes";
                    break;
                case "Wednesday":
                    dia = "Miércoles";
                    break;
                case "Thursday":
                    dia = "Jueves";
                    break;
                case "Friday":
                    dia = "Viernes";
                    break;
                case "Saturday":
                    dia = "Sábado";
                    break;
                default:
                    break;
            }
            return dia;
        }
        #endregion
    }
    }
