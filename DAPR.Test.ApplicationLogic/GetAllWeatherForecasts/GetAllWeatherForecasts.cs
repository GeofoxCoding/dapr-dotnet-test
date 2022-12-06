using System;
using MediatR;
using Dapr.Client;

namespace DAPR.Test.ApplicationLogic.GetAllWeatherForecasts;


public class GetAllWeatherForecasts
{
    public class Model
    {
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string? Summary { get; set; }
    }

    public class Query : IRequest<IEnumerable<Model>>
    {
    }

    public class Handler :
        IRequestHandler<GetAllWeatherForecasts.Query,
        IEnumerable<GetAllWeatherForecasts.Model>>
    {
        private readonly DaprClient _daprClient;

        public Handler(DaprClient daprClient)
        {
            _daprClient = daprClient;
        }

        public async Task<IEnumerable<Model>> Handle(
            Query request,
            CancellationToken cancellationToken)
        {

            return await _daprClient.InvokeMethodAsync<IEnumerable<Model>>(
                HttpMethod.Get,
                "API",
                "weatherforecast");
        }
    }

}

