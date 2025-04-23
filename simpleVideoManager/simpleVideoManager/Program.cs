using simpleVideoManager.Client.Pages;
using simpleVideoManager.Components;
using simpleVideoManager.Scripts;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddScoped<VideoState>();

builder.Services.AddLogging(config =>
{
    config.AddConsole();  // Логирование в консоль
    config.SetMinimumLevel(LogLevel.Debug);  // Уровень логирования Debug, чтобы логировать и Debug-сообщения
});


FilesManager.Init(builder.Environment);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(simpleVideoManager.Client._Imports).Assembly);

app.Run();
