using Radzen;
using Unisuper.GestorCorrespondencia.Frontend.Components;
using Unisuper.GestorCorrespondencia.Frontend.Model;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddRadzenComponents();
builder.Services.AddSingleton<LayoutService>();
builder.Services.AddScoped<ApiService>();
builder.Services.AddScoped<SesionService>();

// Cargar configuración desde appsettings.json
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

var apiSettingsSection = builder.Configuration.GetSection("ApiSettings");
builder.Services.Configure<ApiSettings>(apiSettingsSection);
builder.Services.AddSingleton(apiSettingsSection.Get<ApiSettings>());

builder.Services.AddScoped(sp =>
{
    var client = new HttpClient();
    client.DefaultRequestHeaders.UserAgent.ParseAdd("GestorCorrespondencia/1.0");
    return client;
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
