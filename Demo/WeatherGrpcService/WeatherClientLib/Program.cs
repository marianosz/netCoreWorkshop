using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Grpc.Net.Client;
using System;
using System.Threading.Tasks;
using static Weather.Weather;

namespace WeatherConsole
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var channel = GrpcChannel.ForAddress("https://localhost:5001/");

            var client = new WeatherClient(channel);

            var response = client.GetWeatherStream(new Empty());
            var forecasts = response.ResponseStream.ReadAllAsync();

            await foreach (var forecast in forecasts)
            {
                Console.WriteLine($"{forecast.Temperature}");
            }

            Console.WriteLine("Done");
            Console.ReadLine();
        }
    }
}
