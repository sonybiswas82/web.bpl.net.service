using Hangfire;
using Microsoft.AspNetCore.Builder;
using Swashbuckle.AspNetCore.SwaggerUI;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();

builder.Services.AddHangfire(config =>
{
    config.UseSimpleAssemblyNameTypeSerializer().UseRecommendedSerializerSettings()
    .UseSqlServerStorage(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddHangfireServer();
builder.Services.AddScoped<BplService.Services.IJobService, BplService.Services.JobService>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();
app.UseRouting();

app.UseHangfireDashboard();

app.MapRazorPages();
app.MapControllers();
app.Run();
