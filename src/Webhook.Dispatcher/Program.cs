using Webhook.Dispatcher;
using Webhook.Dispatcher.Consumer;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();
        services.AddSingleton<ConsumerTopicTest>();
    })
    .Build();

await host.RunAsync();
