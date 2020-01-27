using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Weather;
using static Weather.Weather;

namespace BlazorWeather.Services
{
    public interface IWeatherForecastService
    {
        IAsyncEnumerable<WeatherResponse> GetStreamingWeather(CancellationToken token);
        Task<WeatherResponse> GetWeather();
    }

    public class WeatherForecastService : IWeatherForecastService
    {
        private readonly WeatherClient _weatherClient;

        public WeatherForecastService(WeatherClient weatherClient)
        {
            _weatherClient = weatherClient;
        }

        public async Task<WeatherResponse> GetWeather()
        {
            return await _weatherClient.GetWeatherAsync(new Empty());
        }

        public IAsyncEnumerable<WeatherResponse> GetStreamingWeather(CancellationToken token)
        {
            return _weatherClient.GetWeatherStream(new Empty(), cancellationToken: token)
                .ResponseStream.ReadAllAsync();
        }
    }
}
