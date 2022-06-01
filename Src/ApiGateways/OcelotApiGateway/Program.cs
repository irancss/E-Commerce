using Ocelot.Cache.CacheManager;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

//var builder = WebApplication.CreateBuilder(args);

//IConfiguration configuration = new ConfigurationBuilder()
//    .AddJsonFile($"ocelot.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
//    .Build();

////builder.Configuration.AddJsonFile("ocelot.json");
////builder.Configuration.SetBasePath(builder.Environment.ContentRootPath)
////    .AddJsonFile($"ocelot.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
////    .AddEnvironmentVariables();


////builder.Services.AddOcelot(builder.Configuration);
//builder.Services.AddOcelot(configuration);

//var app = builder.Build();

//app.MapGet("/", () => "Hello World!");

//app.UseOcelot();

//app.Run();

new WebHostBuilder()
    .UseKestrel()
    .UseContentRoot(Directory.GetCurrentDirectory())
    .ConfigureAppConfiguration((hostingContext, config) =>
    {
        config
            .SetBasePath(hostingContext.HostingEnvironment.ContentRootPath)
            .AddJsonFile("appsettings.json", true, true)
            .AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", true, true)
            .AddJsonFile($"ocelot.{hostingContext.HostingEnvironment.EnvironmentName}.json", true, true)
            .AddEnvironmentVariables();
    })
    .ConfigureServices(s => {
        s.AddOcelot().AddCacheManager(x =>
        {
            x.WithDictionaryHandle();
        });
    })
    .ConfigureLogging((hostingContext, logging) =>
    {
        logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
        logging.AddConsole();
        logging.AddDebug();
    })
    .UseIISIntegration()
    .Configure(app =>
    {
        app.UseOcelot().Wait();
    })
    .Build()
    .Run();

