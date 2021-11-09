using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Task_WeatherControl
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
        }

        

        private void ButtonWeather_Click(object sender, RoutedEventArgs e)
        {
            Random random = new Random();
            double speedWind = random.Next(10) + random.NextDouble();
            var directionWind = random.Next(1, 8);
            int t = random.Next(-50, 50);
            int w = random.Next(1, 4);
            WeatherControl newWeather = new WeatherControl(speedWind, directionWind, t, w);

            tb_WindDirection.Text = newWeather.WindDirection;
            tb_Wind.Text = Math.Round(newWeather.Wind,2).ToString();
            tb_Temprature.Text = newWeather.Temprature.ToString();
            tb_Weather.Text = newWeather.Weather;

            if (newWeather.Temprature >0)
            {
                BitmapImage tempratureImage = new BitmapImage(new Uri("Icon/warm.png", UriKind.Relative));
                im_Temprature.Source = tempratureImage;
            }
            else
            {
                BitmapImage tempratureImage = new BitmapImage(new Uri("Icon/cold.png", UriKind.Relative));
                im_Temprature.Source = tempratureImage;
            }

            switch (w)
            {
                case 1:
                    BitmapImage sunWeatherImage = new BitmapImage(new Uri("Icon/sun.png", UriKind.Relative));
                    im_Weather.Source = sunWeatherImage;
                    break;
                case 2:
                    BitmapImage cloudWeatherImage = new BitmapImage(new Uri("Icon/cloud.png", UriKind.Relative));
                    im_Weather.Source = cloudWeatherImage;
                    break;
                case 3:
                    BitmapImage rainyWeatherImage = new BitmapImage(new Uri("Icon/rainy.png", UriKind.Relative));
                    im_Weather.Source = rainyWeatherImage;
                    break;
                case 4:
                    BitmapImage snowWeatherImage = new BitmapImage(new Uri("Icon/snowy.png", UriKind.Relative));
                    im_Weather.Source = snowWeatherImage;
                    break;
                default:
                    break;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ButtonWeather_Click(sender, e);
        }
    }
    class WeatherControl : DependencyObject
    {
        private double wind;
        public double Wind
        {
            get
            {
                return wind;
            }
            set
            {
                if (value >= 0)
                {
                    wind = value;
                }
                else
                {
                    wind = 0;
                }
            }
        }
        public string WindDirection { get; set; }
        public string Weather { get; set; }
        public enum DirectionEnum
        {
            North = 1,
            South,
            East,
            West,
            NordEast,
            NordWest,
            SouthEast,
            SouthWest
        }
        public int Temprature
        {
            get => (int)GetValue(TempratureProperty);
            set => SetValue(TempratureProperty, value);
        }

        public static readonly DependencyProperty TempratureProperty =
            DependencyProperty.Register(
                nameof(Temprature),
                typeof(int),
                typeof(WeatherControl),
                new FrameworkPropertyMetadata(
                    0,
                    null,
                    new CoerceValueCallback(CoerceTemprature)));

        private static object CoerceTemprature(DependencyObject d, object baseValue)
        {
            int t = (int)baseValue;
            if (t >= -50 && t <= 50)
            {
                return t;
            }
            else
            {
                return 0;
            }
        }
                
        public enum WeatherEnum
        {
            Sun = 1,
            Cloud,
            Rainy,
            Snowy
        }
        
        public WeatherControl(double speedWind, int directionWind, int temprature, int weather)
        {
            this.Wind = speedWind;
            var directionWindCounter = (DirectionEnum) directionWind;
            switch (directionWindCounter)
            {
                case DirectionEnum.North:
                    WindDirection = "N";
                    break;
                case DirectionEnum.South:
                    WindDirection = "S";
                    break;
                case DirectionEnum.East:
                    WindDirection = "E";
                    break;
                case DirectionEnum.West:
                    WindDirection = "W";
                    break;
                case DirectionEnum.NordEast:
                    WindDirection = "NE";
                    break;
                case DirectionEnum.NordWest:
                    WindDirection = "NW";
                    break;
                case DirectionEnum.SouthEast:
                    WindDirection = "SE";
                    break;
                case DirectionEnum.SouthWest:
                    WindDirection = "SW";
                    break;
                default:
                    break;
            }
            var weatherCount = (WeatherEnum)weather;
            switch (weatherCount)
            {
                case WeatherEnum.Sun:
                    Weather = "Солнечно";
                    break;
                case WeatherEnum.Cloud:
                    Weather = "Облачно";
                    break;
                case WeatherEnum.Rainy:
                    Weather = "Дождливо";
                    break;
                case WeatherEnum.Snowy:
                    Weather = "Снежно";
                    break;
                default:
                    break;
            }
            Temprature = temprature;            
        }
    }
}
