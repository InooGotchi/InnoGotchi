using InnoGotchi.Application.Common.Interfaces;
using InnoGotchi.Application.Common.Services;
using InnoGotchi.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add InnoGotchi services to the container.
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddWebServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseMigrationsEndPoint();

    // Initialise and seed database
    using (var scope = app.Services.CreateScope())
    {
        var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();
        await initialiser.InitialiseAsync();
    }
}
else
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHealthChecks("/health");
app.UseHttpsRedirection();
app.UseStaticFiles();

// Register the Swagger generator and the Swagger UI middlewares
app.UseOpenApi();
app.UseSwaggerUi3();

app.UseHttpLogging();
app.UseRouting();

app.UseAuthentication();
app.UseIdentityServer();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

ILoggerFactory loggerFactory = app.Services.GetRequiredService<ILoggerFactory>();

app.Use(async (context, next) =>
    {
        var logger = loggerFactory.CreateLogger("Request Information");
        logger.LogInformation($"{DateTime.Now}: Incoming request to {context.Request.Path}");
        await next();
    });
app.Run();