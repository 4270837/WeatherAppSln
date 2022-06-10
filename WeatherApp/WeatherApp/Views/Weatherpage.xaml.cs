using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Essentials;
using Xamarin.Forms.Xaml;

namespace WeatherApp
{
    //[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Weatherpage : ContentPage
    {
        public Weatherpage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await GetWeathersData();
        }
        private async Task GetWeathersData()
        {
            var data = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();
            if (data != PermissionStatus.Granted)
            {
                var newdata = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
            }
            var location = await Geolocation.GetLocationAsync();
            var latitude = location.Latitude; 
            var longitude = location.Longitude;
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            string url = $"https://api.openweathermap.org/data/2.5/weather?lat={latitude}&lon={longitude}&units=metric&appid=55d3380a729f5854eb8d5adc906d1122";
            var response = await client.GetStringAsync(url);
            var weathersData = JsonConvert.DeserializeObject<WeatherData>(response);
            BindingContext = weathersData;
        }
    }
}