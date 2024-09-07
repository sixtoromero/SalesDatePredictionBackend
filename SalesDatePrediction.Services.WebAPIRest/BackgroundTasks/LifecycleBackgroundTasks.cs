
namespace SalesDatePrediction.Services.WebAPIRest.BackgroundTasks
{
    public class LifecycleBackgroundTasks : IHostedLifecycleService
    {
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("2- Iniciando la tarea...");
            await Task.Delay(10000);
            //return Task.CompletedTask;
        }

        public Task StartedAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("3- tarea iniciada..."); 
            return Task.CompletedTask;
        }

        public Task StartingAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("1- Antes de iniciar la tarea...");
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("5- deteniendo la tarea...");
            return Task.CompletedTask;
        }

        public Task StoppedAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("6- tarea detenida...");
            return Task.CompletedTask;
        }

        public Task StoppingAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("4- antes de detener la tarea...");
            return Task.CompletedTask;
        }
    }
}
