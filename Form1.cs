using Newtonsoft.Json;
using System;
using System.Net;
using System.Reflection.Emit;
using System.Windows.Forms;

namespace Weather_Application
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string APIKey = "";

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string cityName = TBCity.Text;
            GetWeather(cityName);
        }

        void GetWeather(string cityName)
        {
            using (WebClient web = new WebClient())
            {
                string url = string.Format("https://api.openweathermap.org/data/2.5/weather?q={0}&appid={1}", cityName, APIKey);
                var json = web.DownloadString(url);
                WeatherInfo.Root info = JsonConvert.DeserializeObject<WeatherInfo.Root>(json);

                string translatedMain = TranslateToSerbian(info.weather[0].main);
                string translatedDescription = TranslateToSerbian(info.weather[0].description);

                label2.Text = translatedMain;
                label3.Text = translatedDescription;
                //picIcon.ImageLocation = "https://openweathermap.org/img/w/" + info.weather[0].icon + ".png";
                label6.Text = ConvertDateTime(info.sys.sunrise).ToShortTimeString();
                label7.Text = ConvertDateTime(info.sys.sunset).ToShortTimeString();
                label10.Text = info.wind.speed.ToString();
                label11.Text = info.main.pressure.ToString();
            }
        }

        DateTime ConvertDateTime(long millisec)
        {
            DateTime day = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).ToLocalTime();
            day = day.AddSeconds(millisec).ToLocalTime();

            return day;
        }
        string TranslateToSerbian(string englishText)
        {
            switch (englishText.ToLower())
            {
                case "clouds":
                    return "Oblaci";
                case "overcast clouds":
                    return "Oblacno";
                case "broken clouds":
                    return "Promjenjivo oblacno";
                case "rain":
                    return "Kisno";
                case "light rain":
                    return "Slaba kisa";
                case "scattered clouds":
                    return "Dijelimicno oblacno";
                default:
                    return englishText; 
            }
        }
    }
}

