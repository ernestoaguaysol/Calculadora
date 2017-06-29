using Calculadora.Services;
using GalaSoft.MvvmLight.Command;
using System.ComponentModel;
using System.Windows.Input;
using Calculadora.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Collections.Generic;

namespace Calculadora.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        #region Events
        public event PropertyChangedEventHandler PropertyChanged; 
        #endregion

        #region Attributes
        private DialogService dialogService;
        private string monto;
        private string interes;
        private string cuotas;
        private string primeraCuota;
        private string restoDeCuotas;
        private int formaElegida;
        private string forma;
        private bool habilitarControl;
        #endregion

        #region Properties
        public ObservableCollection<Forma> Formas { get; set; }

        public ObservableCollection<Fecha> Fechas { get; set; }

        public string Forma
        {
            set
            {
                if (forma != value)
                {
                    forma = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Forma"));
                }
            }
            get
            {
                return forma;
            }
        }

        public bool HabilitarControl
        {
            set
            {
                if (habilitarControl != value)
                {
                    habilitarControl = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("HabilitarControl"));
                }
            }
            get
            {
                return habilitarControl;
            }
        }

        public int FormaElegida
        {
            set
            {
                if (formaElegida != value)
                {
                    formaElegida = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("FormaElegida"));
                }
            }
            get
            {
                return formaElegida;
            }
        }

        public string Monto
        {
            set
            {
                if (monto != value)
                {
                    monto = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Monto"));
                }
            }
            get
            {
                return monto;
            }
        }

        public string Interes
        {
            set
            {
                if (interes != value)
                {
                    interes = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Interes"));
                }
            }
            get
            {
                return interes;
            }
        }

        public string Cuotas
        {
            set
            {
                if (cuotas != value)
                {
                    cuotas = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Cuotas"));
                }
            }
            get
            {
                return cuotas;
            }
        }

        public string PrimeraCuota
        {
            set
            {
                if (primeraCuota != value)
                {
                    primeraCuota = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("PrimeraCuota"));
                }
            }
            get
            {
                return primeraCuota;
            }
        }

        public string RestoDeCuotas
        {
            set
            {
                if (restoDeCuotas != value)
                {
                    restoDeCuotas = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("RestoDeCuotas"));
                }
            }
            get
            {
                return restoDeCuotas;
            }
        }
        #endregion

        #region Constructor
        public MainViewModel()
        {
            dialogService = new DialogService();

            Formas = new ObservableCollection<Forma>();
            Fechas = new ObservableCollection<Fecha>();

            LoadFormas();
        }
        #endregion

        #region Commands
        public ICommand CalcularResultadoCommand { get {return new RelayCommand(CalcularResultado); }  }

        private async void CalcularResultado()
        {
            try
            {
                if (string.IsNullOrEmpty(this.monto) ||
                    string.IsNullOrEmpty(this.interes) ||
                    string.IsNullOrEmpty(this.cuotas))
                {
                    await dialogService.ShowMessage("Error", "No puede haber campos vacios");
                    return;
                }

                var monto = int.Parse(this.monto);
                var interes = int.Parse(this.interes);
                var cuotas = int.Parse(this.cuotas);

                if (monto <= 0)
                {
                    await dialogService.ShowMessage("Error", "El valor del monto debe ser mayor a cero");
                    return;
                }
                if (interes < 0)
                {
                    await dialogService.ShowMessage("Error", "El valor del interés no puede ser negativo");
                    return;
                }
                if (cuotas <= 0)
                {
                    await dialogService.ShowMessage("Error", "El valor de cuotas debe ser mayor a cero");
                    return;
                }

                var forma = Formas.ToList().Where(f => f.FormaId == formaElegida).FirstOrDefault();
                var calculadora = new CalculadorPrestamo(monto, interes, cuotas)
                {
                    Dias = forma.Dias
                };

                PrimeraCuota = string.Format("Primera Cuota $ {0}", calculadora.CuotaPrimera.ToString());
                RestoDeCuotas = string.Format("Resto de Cuotas $ {0}", calculadora.CuotaRestante.ToString());
                Forma = forma.Nombre;

                LoadFechas(calculadora.GetFechas());

                Monto = null;
                Interes = null;
                Cuotas = null;
            }
            catch (FormatException)
            {
                await dialogService.ShowMessage("Error", "Tipo de formato no válido");
                return;
            }
            catch (Exception ex)
            {
                await dialogService.ShowMessage("Error", ex.Message);
                return;
            }
        }
        #endregion

        #region Methods
        private void LoadFormas()
        {
            var formas = new FormaDePagoViewModel();
            Formas.Clear();

            foreach (var forma in formas.Formas)
            {
                Formas.Add(new Forma
                {
                    Dias = forma.Dias,
                    FormaId = forma.FormaId,
                    Nombre = forma.Nombre,
                });
            }
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
        #endregion
    }
}
