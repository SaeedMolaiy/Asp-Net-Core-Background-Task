namespace BackgroundTask.Tasks
{
    public class SomeBackgroundTask : BackgroundService
    {
        private readonly TimeSpan _period = TimeSpan.FromSeconds(5);
        private readonly ILogger<SomeBackgroundTask> _logger;

        public SomeBackgroundTask(ILogger<SomeBackgroundTask> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var periodTimer = new PeriodicTimer(_period);

            while (!stoppingToken.IsCancellationRequested &&
                   await periodTimer.WaitForNextTickAsync(stoppingToken))
            {
                _logger.LogInformation("Background Task is executing...");
            }
        }
    }
}