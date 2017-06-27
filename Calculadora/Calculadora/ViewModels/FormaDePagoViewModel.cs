using Calculadora.Models;
using System.Collections.Generic;

namespace Calculadora.ViewModels
{
    public class FormaDePagoViewModel
    {
        public List<Forma> Formas
        {
            get
            {
                var lista = new List<Forma>
                {
                    new Forma
                    {
                        Dias = 7,
                        FormaId = 1,
                        Nombre = "Semanal",
                    },

                    new Forma
                    {
                        Dias = 15,
                        FormaId = 2,
                        Nombre = "Quincenal",
                    },

                    new Forma
                    {
                        Dias = 30,
                        FormaId = 3,
                        Nombre = "Mensual",
                    },
                };
                return lista;
            }
        }
    }
}