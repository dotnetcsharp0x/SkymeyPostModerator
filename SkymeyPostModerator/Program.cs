using SkymeyLibs;
using SkymeyPostModerator.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Configuration.SetBasePath(builder.Configuration.GetSection("ConfigPath").Value)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
builder.Services.Configure<MainSettings>(builder.Configuration.GetSection("MainSettings"));
builder.WebHost.UseUrls("http://localhost:5040;https://localhost:5050;");
builder.Services.AddHttpClient();

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
