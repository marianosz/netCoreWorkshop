﻿using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Threading.Tasks;
using Weather;
using static Weather.Weather;

namespace WeatherGrpcService
{

    public class WeatherService : WeatherBase
    {
        private IMemoryCache _cache;

        public WeatherService(IMemoryCache cache)
        {
            _cache = cache;
        }

        public override Task<WeatherResponse> GetWeather(Empty request, ServerCallContext context)
        {
            return Task.FromResult(GetCurrentWeatherResponse(_cache.Get<Forecast>("WeatherCache")));
        }

        public override async Task GetWeatherStream(Empty request, IServerStreamWriter<WeatherResponse> responseStream, ServerCallContext context)
        {
            while (!context.CancellationToken.IsCancellationRequested)
            {
                var cachedForecast = _cache.Get<Forecast>("WeatherCache");
                await responseStream.WriteAsync(GetCurrentWeatherResponse(cachedForecast));
                await Task.Delay(TimeSpan.FromSeconds(10));
            }
        }

        public static WeatherResponse GetCurrentWeatherResponse(Forecast forecast)
        {
            return new WeatherResponse()
            {
                WeatherText = forecast.WeatherText,
                IsDayTime = forecast.IsDayTime,
                Pressure = forecast.Pressure.Imperial.Value,
                RelativeHumidity = forecast.RelativeHumidity,
                RetrievedTime = Timestamp.FromDateTime(DateTime.UtcNow),
                Temperature = forecast.Temperature.Metric.Value,
                UvIndex = forecast.UvIndex,
                WeatherIcon = forecast.WeatherIcon,
                WeartherUri = $"https://developer.accuweather.com/sites/default/files/{forecast.WeatherIcon.ToString("00")}-s.png",
                WindSpeed = forecast.Wind.Speed.Imperial.Value,
                WindDirection = forecast.Wind.Direction.English,
                Past6HourMax = forecast.TemperatureSummary.Past6HourRange.Maximum.Imperial.Value,
                Past6HourMin = forecast.TemperatureSummary.Past6HourRange.Minimum.Imperial.Value
            };
        }
    }
}
