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
        private int formaElegida;
        private string montoTotal;
        private string descripcionCuotas;
        private string fechaFin;
        private bool isVisible;
        private CalculadorPrestamo calculadora;
        #endregion

        #region Properties
        public ResultadoViewModel Resultado { get; set; }

        public ObservableCollection<Forma> Formas { get; set; }

        public ObservableCollection<Fecha> Fechas { get; set; }

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

        public string MontoTotal
        {
            set
            {
                if (montoTotal != value)
                {
                    montoTotal = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("MontoTotal"));
                }
            }
            get
            {
                return montoTotal;
            }
        }

        public string DescripcionCuotas
        {
            set
            {
                if (descripcionCuotas != value)
                {
                    descripcionCuotas = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("DescripcionCuotas"));
                }
            }
            get
            {
                return descripcionCuotas;
            }
        }

        public string FechaFin
        {
            set
            {
                if (fechaFin != value)
                {
                    fechaFin = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("FechaFin"));
                }
            }
            get
            {
                return fechaFin;
            }
        }

        public bool IsVisible
        {
            set
            {
                if (isVisible != value)
                {
                    isVisible = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsVisible"));
                }
            }
            get
            {
                return isVisible;
            }
        }

        public bool IsEnable { get; set; }
        #endregion

        #region Constructor
        public MainViewModel()
        {
            instance = this;
            dialogService = new DialogService();

            Formas = new ObservableCollection<Forma>();
            Fechas = new ObservableCollection<Fecha>();

            LoadFormas();
            IsVisible = false;
            IsEnable = false;
        }
        #endregion

        #region Commands
        public ICommand BorrarCommand { get { return new RelayCommand(Borrar); } }

        private void Borrar()
        {
            Monto = null;
            Interes = null;
            Cuotas = null;
            MontoTotal = null;
            DescripcionCuotas = null;
            FechaFin = null;
            Formas.Clear();
            LoadFormas();

            IsVisible = false;
            IsEnable = false;
        }

        public ICommand CalcularResultadoCommand { get {return new RelayCommand(CalcularResultado); }  }

        private async void CalcularResultado()
        {
            try
            {
                if (string.IsNullOrEmpty(this.monto) ||
                    string.IsNullOrEmpty(this.interes) ||
                    string.IsNullOrEmpty(this.cuotas))
                {
                    IsEnable = false;
                    await dialogService.ShowMessage("Error", "No puede haber campos vacios");
                    return;
                }

                var monto = int.Parse(this.monto);
                var interes = int.Parse(this.interes);
                var cuotas = int.Parse(this.cuotas);

                if (monto <= 0)
                {
                    IsEnable = false;
                    await dialogService.ShowMessage("Error", "El valor del monto debe ser mayor a cero");
                    return;
                }
                if (interes < 0)
                {
                    IsEnable = false;
                    await dialogService.ShowMessage("Error", "El valor del interés no puede ser negativo");
                    return;
                }
                if (cuotas <= 0)
                {
                    IsEnable = false;
                    await dialogService.ShowMessage("Error", "El valor de cuotas debe ser mayor a cero");
                    return;
                }

                // BindablePicker
                var forma = Formas.ToList().Where(f => f.FormaId == formaElegida).FirstOrDefault();

                calculadora = new CalculadorPrestamo
                {
                    Cuotas = cuotas,
                    Forma = forma,
                    Interes = interes,
                    Monto = monto,
                };
                LoadFechas(calculadora.GetFechas());

                MontoTotal = string.Format("Total $ {0}", calculadora.MontoTotal.ToString());
                DescripcionCuotas = string.Format("Cuotas {0}", LoadDescripcionCuotas());
                FechaFin = string.Format("Termina el {0:dd/MM/yyyy}", Fechas.LastOrDefault().DateTime);

                Resultado = new ResultadoViewModel(calculadora);

                IsVisible = true;
                IsEnable = true;
            }
            catch (FormatException)
            {
                IsEnable = false;
                await dialogService.ShowMessage("Error", "Tipo de formato no válido");
                return;
            }
            catch (Exception ex)
            {
                IsEnable = false;
                await dialogService.ShowMessage("Error", ex.Message);
                return;
            }
        }
        #endregion

        #region Methods
        private string LoadDescripcionCuotas()
        {
            if (calculadora.CuotaPrimera == calculadora.CuotaRestante)
            {
                return string.Format("$ {0}", calculadora.CuotaPrimera);
            }
            else
            {
                return string.Format("$ {0} / $ {1}", calculadora.CuotaPrimera, calculadora.CuotaRestante);
            }
        }

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

        #region Singleton
        private static MainViewModel instance;

        public static MainViewModel GetInstance()
        {
            if (instance == null)
            {
                instance = new MainViewModel();
            }
            return instance;
        }
        #endregion
    }
}
