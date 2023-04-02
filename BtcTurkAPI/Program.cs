using System.Net.Http.Headers;
using System.Reflection;
using BtcTurkAPI;
using BtcTurkAPI.Authentication;
using BtcTurkAPI.Handlers.AuthHandler;
using BtcTurkAPI.Proxies.BtcTurkApi;
using BtcTurkAPI.Proxies.BtcTurkGraphApi;
using BtcTurkAPI.Websocket;
using MediatR;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((hostingContext, config) =>
    {
        config.SetBasePath(Directory.GetCurrentDirectory());
        config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
        config.AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", optional: true);
        config.AddEnvironmentVariables();
    })
    .ConfigureServices((context, services) =>
    {
        services.AddHttpClient<IBtcTurkApiProxy, BtcTurkApiProxy>(client =>
        {
            string? baseAddress = context.Configuration["BtcTurkApiUrl"];
            client.BaseAddress = new Uri(baseAddress);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }).AddHttpMessageHandler<AuthenticatedHttpClientHandler>();
        
        services.AddHttpClient<IBtcTurkGraphApiProxy, BtcTurkGraphApiProxy>(client =>
        {
            client.BaseAddress = new Uri(context.Configuration["BtcTurkGraphApiUrl"]);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        });
        
        services.AddMediatR(Assembly.GetExecutingAssembly());

        services.AddSingleton<BtcTurkWebsocket>();
        services.AddSingleton<BtcTurkPrivateWebsocket>();
        
        services.AddScoped<IAuthService, AuthService>();
        
        services.AddTransient<AuthenticatedHttpClientHandler>();
        
        services.AddHostedService<Samples>();
    })
    .Build();


Console.WriteLine("Connecting to BtcTurk websocket");
BtcTurkWebsocket websocket = host.Services.GetRequiredService<BtcTurkWebsocket>();
await websocket.Initialize();

Console.WriteLine("Connecting to BtcTurk Private websocket");
BtcTurkPrivateWebsocket btcTurkPrivateWebsocket = host.Services.GetRequiredService<BtcTurkPrivateWebsocket>();
await btcTurkPrivateWebsocket.Initialize();

host.Run();