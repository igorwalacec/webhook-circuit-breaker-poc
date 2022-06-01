using Webhook.Dispatcher.Consumer;

namespace Webhook.Dispatcher;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly ConsumerTopicTest _consumer;

    public Worker(ILogger<Worker> logger, ConsumerTopicTest consumer)
    {
        _logger = logger;
        _consumer = consumer;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {        
        _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
        
        _ = _consumer.Consume(stoppingToken);        

        _logger.LogInformation("Worker Finalize", DateTimeOffset.Now);

        await base.StopAsync(stoppingToken);
    }
}
