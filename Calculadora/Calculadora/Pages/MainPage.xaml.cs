
using Calculadora.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Calculadora.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        private MainViewModel _mainViewModel;

        public MainPage()
        {
            InitializeComponent();
            _mainViewModel = MainViewModel.GetInstance();
        }

        //private async void NavigationButton_Clicked(object sender, System.EventArgs e)
        //{
        //    if (_mainViewModel.IsEnable)
        //    {
        //        await Navigation.PushAsync(new ResultadoPage());
        //    }
        //}
    }
}