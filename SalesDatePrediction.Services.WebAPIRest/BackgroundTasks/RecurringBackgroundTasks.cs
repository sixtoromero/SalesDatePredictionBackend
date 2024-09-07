
namespace SalesDatePrediction.Services.WebAPIRest.BackgroundTasks
{
    public class RecurringBackgroundTasks : BackgroundService
    {
        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
			try
			{
                while (!stoppingToken.IsCancellationRequested)
                {
                    Console.WriteLine("Ejecutando tarea en segundo plano..");
                    await Task.Delay(TimeSpan.FromSeconds(2), stoppingToken);
                }
            }
			catch (OperationCanceledException)
			{
                Console.WriteLine("Deteniendo la ejecución de la tarea en segundo plano.");
			}
        }
    }

}
