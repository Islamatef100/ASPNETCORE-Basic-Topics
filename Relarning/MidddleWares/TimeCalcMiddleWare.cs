using System.Diagnostics;

namespace Relarning.MidddleWares
{
    public class TimeCalcMiddleWare
    {
        RequestDelegate _next;
        ILogger<TimeCalcMiddleWare> _logger;
        public TimeCalcMiddleWare(RequestDelegate next ,ILogger<TimeCalcMiddleWare> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context) 
        {
            var stopWatch = new Stopwatch();
            _logger.LogInformation("Request Start Running");
            stopWatch.Start();
            await _next(context);
            stopWatch.Stop();
            _logger.LogInformation($"Request finished in {stopWatch.Elapsed.Microseconds} microseconds");
        }
    }
}
